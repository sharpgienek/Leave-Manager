using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using leave_manager.Exceptions;


namespace leave_manager
{
    /// <summary>
    /// Klasa zawierająca rozszerzenia formularzy dotyczące bazy danych.
    /// </summary>
    /// <remarks>
    /// Wszystkie właściwe metody operujące na bazie danych powinny być prywatne i statyczne.
    /// Metody rozszerzające formularze powinny odwoływać się do metod operujących na bazie danych.
    /// </remarks>
    public static class DatabaseOperator
    {
        /// <summary>
        /// Metoda pobierająca aktualny czas serwera.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Aktualny czas serwera.</returns>
        private static DateTime GetServerTimeNow(LeaveManagerForm form)
        {
            SqlCommand commandGetServerTime = new SqlCommand("SELECT GETDATE()", form.Connection);
            if (form.TransactionOn)
                commandGetServerTime.Transaction = form.Transaction;
            return (DateTime)commandGetServerTime.ExecuteScalar();
        }

        /// <summary>
        /// Metoda aktualizacji dni urlopowych. Wykonuje aktualizację dni urlopowych poprzez przypisanie
        /// do dni urlopowych wartości przypadającej na rok, a do zaległych dni urlopowych 
        /// sumy aktualnych zaległych dni i zwykłych dni urlopowych. Aktualizacja wykonuje się tylko wtedy,
        /// gdy rok ostatniej aktualizacji jest mniejszy niż aktualny rok. Po aktualizacji zapisywana jest 
        /// na serwerze data ostatniej aktualizacji.
        /// </summary>
        /// <param name="connection"></param>
        public static void UpdateLeaveDays(SqlConnection connection)
        {
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
            SqlCommand commandGetServerTime = new SqlCommand("SELECT GETDATE()", connection, transaction);
            DateTime serverTime = (DateTime)commandGetServerTime.ExecuteScalar();
            SqlCommand commandLastUpdateDate = new SqlCommand("SELECT Last_update_date FROM Dates", connection, transaction);
            DateTime lastUpdateDate = (DateTime)commandLastUpdateDate.ExecuteScalar();
            if (lastUpdateDate == null || serverTime.Year > lastUpdateDate.Year)
            {
                using (SqlCommand commandGetEmployees = new SqlCommand("SELECT Employee_ID, Year_leave_days, " +
                    "Leave_days, Old_leave_days FROM Employee", connection, transaction))
                {
                    List<int> employeeIdList = new List<int>();
                    List<int> yearLeaveDaysList = new List<int>();
                    List<int> leaveDaysList = new List<int>();
                    List<int> oldLeaveDaysList = new List<int>();
                    using (SqlDataReader employeeReader = commandGetEmployees.ExecuteReader())
                    {
                        while (employeeReader.Read())
                        {
                            employeeIdList.Add((int)employeeReader["Employee_ID"]);
                            yearLeaveDaysList.Add((int)employeeReader["Year_leave_days"]);
                            leaveDaysList.Add((int)employeeReader["Leave_days"]);
                            oldLeaveDaysList.Add((int)employeeReader["Old_leave_days"]);
                        }
                        employeeReader.Close();
                    }
                    using (SqlCommand updateEmployee = new SqlCommand("UPDATE Employee SET " +
                           "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days, " +
                           "Demand_days = @Demand_days WHERE Employee_ID = @Employee_ID", connection, transaction))
                    {
                        for (int i = 0; i < employeeIdList.Count; ++i)
                        {
                            updateEmployee.Parameters.Clear();
                            updateEmployee.Parameters.AddWithValue("@Employee_ID", employeeIdList[i]);
                            updateEmployee.Parameters.AddWithValue("@Leave_days", yearLeaveDaysList[i]);
                            updateEmployee.Parameters.AddWithValue("@Old_leave_days", leaveDaysList[i] + oldLeaveDaysList[i]);
                            updateEmployee.Parameters.AddWithValue("@Demand_days", 0);
                            updateEmployee.ExecuteNonQuery();
                        }
                    }
                }
                using (SqlCommand commandUpdateUpdateDate = new SqlCommand("UPDATE Dates SET " +
                "Last_update_date = @Last_update_date", connection, transaction))
                {
                    commandUpdateUpdateDate.Parameters.AddWithValue("@Last_update_date", serverTime);
                    commandUpdateUpdateDate.ExecuteNonQuery();
                }
            }
            transaction.Commit();
        }

        /// <summary>
        /// Metoda logowania rozszerzająca formularz logowania.
        /// </summary>
        /// <param name="form">Formularz logowania.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Hasło.</param>
        /// <returns>Zwraca obiekt reprezentujący zalogowanego pracownika.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="LoginOrPasswordException">Login or password is wrong.</exception>
        public static Employee Login(this FormLogin form, String login, String password)
        {
            return DatabaseOperator.Login((LeaveManagerForm)form, login, password);
        }

        /// <summary>
        /// Metoda logowania.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Hasło.</param>
        /// <returns>Zwraca obiekt reprezentujący zalogowanego pracownika.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="LoginOrPasswordException">Login or password is wrong.</exception>
        private static Employee Login(LeaveManagerForm form, String login, String password)
        {
            //Zapytanie sql zczytujące dane pracownika o podanym loginie i haśle.
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID, Perm.Description AS Permission, E.Name, E.Surname, E.Birth_date," +
                                               "E.Address, E.PESEL, E.EMail, Pos.Description AS Position, E.Year_leave_days, " +
                                               "E.Leave_days, E.Old_leave_days " +
                                               "FROM Employee E, Permission Perm, Position Pos WHERE Login = @Login AND " +
                                               "Password = @Password AND E.Permission_ID = Perm.Permission_ID AND " +
                                               "E.Position_ID = Pos.Position_ID", form.Connection);
            //Dodanie parametru loginu.
            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
            //Dodanie parametru hasła. Hasła są przechowywane w bazie w formie skrótu, stąd parametr to skrót hasła.
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(password);
            //Jeżeli formularz ma włączoną transakcję.
            if (form.TransactionOn)
                //Przypisanie do zapytania transakcji formularza.
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Jeżeli wyniki zapytania są nie puste.
            if (reader.Read())
            {
                //Stworzenie nowego obiektu pracownika z danymi zczytanymi za pomocą zapytania.
                Employee employee = new Employee((int)reader["Employee_ID"], reader["Permission"].ToString(),
                    reader["Name"].ToString(), reader["Surname"].ToString(), (DateTime)reader["Birth_date"],
                    reader["Address"].ToString(), reader["PESEL"].ToString(), reader["EMail"].ToString(),
                    reader["Position"].ToString(), (int)reader["Year_leave_days"], (int)reader["Leave_days"],
                    (int)reader["Old_leave_days"]);
                //Zamknięcie obiektu czytającego.
                reader.Close();
                //Zwrócenie wyniku.
                return employee;
            }
            else//Nie znaleziono żadnego pracownika o podanym loginie i hasle.
            {
                //Zamknięcie obiektu czytającego.
                reader.Close();
                throw new LoginOrPasswordException();
            }
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typy urlopów rozszerzająca formularz administratora.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca tabelę zawierającą informacje o typach urlopów posortowane po numerze id.
        /// Zwracana tabela zawiera następujące kolumny: 
        /// "Leave type id", "Name", "Consumes days". </returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetLeaveTypesTable(this FormAdmin form)
        {
            return GetLeaveTypesTable((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typów urlopów do tabeli.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca tabelę zawierającą informacje o typach urlopów posortowane po numerze id.
        /// Zwracana tabela zawiera następujące kolumny: 
        /// "Leave type id", "Name", "Consumes days". </returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static DataTable GetLeaveTypesTable(LeaveManagerForm form)
        {
            //Zapytanie sql zczytujące dane.
            SqlCommand command = new SqlCommand("SELECT LT_ID AS 'Leave type id', Name, Consumes_days AS 'Consumes days' " +
                "FROM Leave_type ORDER BY LT_ID", form.Connection);
            //Jeżeli transakcja jest włączona, to ujmij nią również to zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Inicjalizacja obiektu wyniku.
            DataTable result = new DataTable();
            //Stworzenie obiektu czytającego wyniki polecenia.
            SqlDataReader reader = command.ExecuteReader();
            //Wypełnienie tabeli wynikiem zapytania sql.
            result.Load(reader);
            //Zamknięcie obiektu czytającego.
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typów urlopów do listy rozszerzająca formularz zgłoszenia urlopu.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę typów urlopów.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<LeaveType> GetLeaveTypesList(this FormLeaveApplication form)
        {
            return GetLeaveTypesList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typów urlopów do listy. Rozszerza formularz usuwania typu urlopu.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę typów urlopów.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<LeaveType> GetLeaveTypesList(this FormDeleteLeaveType form)
        {
            return GetLeaveTypesList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typów urlopów do listy.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę typów urlopów.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static List<LeaveType> GetLeaveTypesList(LeaveManagerForm form)
        {
            //Lista do której zostaną zczytane dane z bazy danych.
            List<LeaveType> result = new List<LeaveType>();
            //Zapytanie sql zczytujące typy urlopów.
            SqlCommand command = new SqlCommand("SELECT LT_ID AS 'Leave type id', Name, Consumes_days AS 'Consumes days' " +
                "FROM Leave_type ORDER BY LT_ID", form.Connection);
            //Jeżeli formularz ma włączoną transakcję, to dodaj zapytanie do tej transakcji.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Wykonuj dopóki udało się odczytać kolejny wiersz.
            while (reader.Read())
            {
                //Dodanie kolejnego wpisu do listy wyniku.
                result.Add(new LeaveType((int)reader["Leave type id"], reader["Name"].ToString(), (bool)reader["Consumes days"]));
            }
            //Zamknięcie obiektu czytającego.
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda pobrania listy uprawnień. Rozszerza formularz dodawania pracownika.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę ciągów znaków reprezentujących poziomy uprawnień.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<String> GetPermissions(this FormAddOrEditEmployee form)
        {
            return GetPermissions((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobrania listy uprawnień.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę ciągów znaków reprezentujących poziomy uprawnień.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static List<String> GetPermissions(LeaveManagerForm form)
        {
            //Lista do której będą zczytywane rodzaje uprawnień.
            List<String> result = new List<String>();
            //Zapytanie sql zczytujące rodzaje uprawnień.
            SqlCommand command = new SqlCommand("SELECT Description FROM Permission ORDER BY Permission_ID", form.Connection);
            //Jeżeli formularz posiada uruchomioną transakcję, to ustaw ją jako transakcję polecenia sql.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Dopóki udało się odczytać kolejny wiersz wyników zapytania.
            while (reader.Read())
            {
                //Dodanie nowego obiektu do listy.
                result.Add(reader["Description"].ToString());
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda pobrania tabeli z danymi pozycji. Rozszerza formularz administratora.
        /// </summary>
        /// <param name="form">Formularz wysyłający.</param>
        /// <returns>Zwraca tabelę z danymi pozycji posortowane po numerach id pozycji.
        /// Kolumny w zwracanej tabeli: "Position id", "Name".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetPositionsTable(this FormAdmin form)
        {
            return GetPositionsTable((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobrania tabeli z danymi pozycji. 
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę z danymi pozycji posortowane po numerach id pozycji.
        /// Kolumny w zwracanej tabeli: "Position id", "Name".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static DataTable GetPositionsTable(LeaveManagerForm form)
        {
            SqlCommand command = new SqlCommand("SELECT Position_ID AS 'Position id', " +
                "Description AS 'Name' FROM Position ORDER BY Position_ID", form.Connection);
            //Jeżeli formularz ma włączoną transakcję, to dołącz do niej zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu wyników.
            DataTable result = new DataTable();
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Zczytanie wyników zapytania do tabeli.
            result.Load(reader);
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników. Rozszerza formularz usuwania pozycji.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<String> GetPositionsList(this FormDeletePosition form)
        {
            return GetPositionsList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników. Rozszerza formularz dodawania pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<String> GetPositionsList(this FormAddOrEditEmployee form)
        {
            return GetPositionsList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników. Rozszerza formularz asystentki.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<String> GetPositionsList(this FormAssistant form)
        {
            return GetPositionsList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników. Rozszerza formularz manadżera.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<String> GetPositionsList(this FormManager form)
        {
            return GetPositionsList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static List<String> GetPositionsList(LeaveManagerForm form)
        {
            //Stworzenie obiektu do którego będą zczytywane wyniki zapytania.
            List<String> result = new List<string>();
            //Zapytanie sql zczytujące nazwy pozycji.
            SqlCommand command = new SqlCommand("SELECT Description FROM Position ORDER BY Position_ID", form.Connection);
            //Jeżeli formularz ma rozpoczętą transakcję, to dodaj do niej zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Dopóki udało się odczytać kolejną linijkę wyników zapytania.
            while (reader.Read())
            {
                //Dodanie nowego obiektu do listy wyników.
                result.Add(reader["Description"].ToString());
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych stanów zgłoszenia urlopowego.
        /// Rozszerza formularz dodawania/edycji zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <returns>Lista możliwych stanów zgłoszenia (same nazwy).</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static List<String> GetStatusTypes(this FormLeaveApplication form)
        {
            return GetStatusTypes((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych stanów zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <returns>Lista możliwych stanów zgłoszenia (same nazwy).</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static List<String> GetStatusTypes(LeaveManagerForm form)
        {
            //Lista do której zostaną zczytane możliwe stany zgłoszeń urlopowych.
            List<String> result = new List<string>();
            //Zapytanie sql zczytujące możliwe stany zgłoszeń urlopowych.
            SqlCommand command = new SqlCommand("SELECT Name FROM Status_type ORDER BY ST_ID", form.Connection);
            //Jeżeli formularz posiada rozpoczętą transakcję, to dodaj do niej zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Dopóki udało się odczytać kolejną linijkę wyników zapytania.
            while (reader.Read())
            {
                result.Add(reader["Name"].ToString());
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda dodawania nowego pracownika.
        /// Rozszerza formularz dodawania pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="employee">Obiekt zawierający dane dodawanego pracownika.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="NoFreeLoginException">Wyjątek występuje, gdy wszystkie z możliwych do wylosowania loginów są zajęte.</exception>
        public static void AddEmployee(this FormAddOrEditEmployee form, Employee employee)
        {
            AddEmployee((LeaveManagerForm)form, employee);
        }

        /// <summary>
        /// Metoda dodawania nowego pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="employee">Obiekt zawierający dane dodawanego pracownika.</param>
        /// <remarks>Jeżeli formularz ma uruchomioną transakcję,to poziom jej izolacji
        /// musi być == ReapeatableRead lub Serializable.
        /// 
        /// Nowy pracownik będzie posiadał losowo wygenerowane login i hasło składające się z cyfr.</remarks>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="NoFreeLoginException">Wyjątek występuje, gdy wszystkie z możliwych do wylosowania loginów są zajęte.</exception>
        private static void AddEmployee(LeaveManagerForm form, Employee employee)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
             * o odpowiednim poziomie izolacji.
             */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Obiekt umożliwiający losowanie.
                Random random = new Random();
                //Zmienna do której zostanie wygenerowany login.
                int login = 0;
                /* Zapytanie sql służące do sprawdzenia, czy wygenerowany login już istnieje. Jeżeli zwróci wynik,
                 * to oznacza to, że podany login istnieje.
                 */
                SqlCommand commandCheckLogin = new SqlCommand("SELECT Login FROM Employee WHERE Login = @Login",
                    form.Connection, form.Transaction);
                //Stworzenie obiektu służącego do czytania wyników zapytania.
                SqlDataReader reader;
                //Pętla szukająca wolnego loginu składającego się z 7 cyfr.
                for (int i = 1000000; i <= 10000000; ++i)
                {
                    //Wylosowanie loginu.
                    login = random.Next(1000000, 10000000);
                    //Wyczyszczenie parametrów zapytania.
                    commandCheckLogin.Parameters.Clear();
                    //Dodanei nowego parametru zapytania: losowo wygenerowanego loginu.
                    commandCheckLogin.Parameters.Add("@Login", SqlDbType.VarChar).Value = login.ToString();
                    //Przypisanie wyników zapytania z nowym parametrem do obiektu czytającego.
                    reader = commandCheckLogin.ExecuteReader();
                    /* Jeżeli obiekt czytający nie ma czego czytać (=> zapytanie nic nie zwróciło =>
                     * wygenerowany login nie istnieje w bazie.
                     */
                    if (!reader.Read())
                    {
                        //Zamykamy obiekt czytający i przerywamy pętle szukającą nie wykorzystanego loginu.
                        reader.Close();
                        reader.Dispose();
                        break;
                    }
                    reader.Close();
                    //Jeżeli szukanie wolnego loginu zakończyło się niepowodzeniem.
                    if (i == 10000000)
                        throw new NoFreeLoginException();
                }
                //Wygenerowanie 7 znakowego hasła składającego się z samych cyfr.
                int password = random.Next(1000000, 10000000);
                //Polecenie sql dodające wpis nowego pracownika. Nadaje mu id o 1 większe od największego numeru id w bazie.
                SqlCommand commandInsertEmployee = new SqlCommand("INSERT INTO Employee (Employee_ID, Permission_ID, Position_ID, " +
                  "Login, Password, Name, Surname, Birth_date, Address, PESEL, EMail, " +
                  "Year_leave_days, Leave_days, Old_leave_days) VALUES ((SELECT MAX(Employee_ID) + 1 FROM Employee)," +
                  "(SELECT Permission_ID FROM Permission WHERE Description = @Permission_Description), " +
                  "(SELECT Position_ID FROM Position WHERE Description = @Position_Description), " +
                  "@Login, @Password, @Name, @Surname, @Birth_date, @Address," +
                  "@PESEL, @EMail, @Year_leave_days, @Leave_days, 0)", form.Connection, form.Transaction);
                //Ustawienie parametrów polecenia.
                commandInsertEmployee.Parameters.Add("@Permission_Description", SqlDbType.VarChar).Value = employee.Permission;
                commandInsertEmployee.Parameters.Add("@Position_Description", SqlDbType.VarChar).Value = employee.Position;
                commandInsertEmployee.Parameters.Add("@Login", SqlDbType.VarChar).Value = login.ToString();
                commandInsertEmployee.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(password.ToString());
                commandInsertEmployee.Parameters.Add("@Name", SqlDbType.VarChar).Value = employee.Name;
                commandInsertEmployee.Parameters.Add("@Surname", SqlDbType.VarChar).Value = employee.Surname;
                commandInsertEmployee.Parameters.Add("@Birth_date", SqlDbType.DateTime).Value = employee.BirthDate;
                commandInsertEmployee.Parameters.Add("@Address", SqlDbType.VarChar).Value = employee.Address;
                commandInsertEmployee.Parameters.Add("@PESEL", SqlDbType.VarChar).Value = employee.Pesel;
                commandInsertEmployee.Parameters.Add("@EMail", SqlDbType.VarChar).Value = employee.EMail;
                commandInsertEmployee.Parameters.Add("@Year_leave_days", SqlDbType.Int).Value = employee.YearLeaveDays;
                commandInsertEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = employee.LeaveDays;
                commandInsertEmployee.ExecuteNonQuery();
                /* Polecenie sql odpowiedzialne za wpisanie nowego pracownika do tablicy pracowników nie poinformowanych
                 * o swoim loginie i hasle.
                 */
                SqlCommand commandInsertUninformed = new SqlCommand("INSERT INTO Uninformed (Employee_ID, Password) " +
                    "VALUES ((SELECT MAX(Employee_ID) FROM Employee), @Password)", form.Connection, form.Transaction);
                commandInsertUninformed.Parameters.Add("@Password", SqlDbType.VarChar).Value = password.ToString();
                commandInsertUninformed.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda pobierająca tabelę ze zgłoszeniami urlopowymi wymagającymi zatwierdzenia/odrzucenia.
        /// Rozszerza formularz kierownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę zawierającą zgłoszenia urlopowe wymagające zatwierdzenia/odrzucenia.
        /// Tabela zawiera kolumny: "Employee id", "Position", "Name", "Surname", "e-mail", "Leave Id",
        /// "Type", "Status"
        /// "First day", "Last day", "Remarks", "Used days".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetNeedsAction(this FormManager form)
        {
            return GetNeedsAction((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca tabelę ze zgłoszeniami urlopowymi wymagającymi zatwierdzenia/odrzucenia.
        /// Rozszerza formularz rejestratorki.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę zawierającą zgłoszenia urlopowe wymagające zatwierdzenia/odrzucenia.
        /// Tabela zawiera kolumny: "Employee id", "Position", "Name", "Surname", "e-mail", "Leave Id",
        /// "Type", "Status"
        /// "First day", "Last day", "Remarks", "Used days".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetNeedsAction(this FormAssistant form)
        {
            return GetNeedsAction((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca tabelę ze zgłoszeniami urlopowymi (i ich właścicielami) wymagającymi zatwierdzenia/odrzucenia
        /// różnego typu w zależności od tego, przej jaki typ formularza została wywołana.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę zawierającą zgłoszenia urlopowe wymagające zatwierdzenia/odrzucenia.
        /// Tabela zawiera kolumny: "Employee id", "Position", "Name", "Surname", "e-mail", "Leave Id",
        /// "Type", "Status"
        /// "First day", "Last day", "Remarks", "Used days".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="ArgumentException">Rzucany, gdy formularz nie jest typu, który pozwala na tą operację.</exception>
        private static DataTable GetNeedsAction(LeaveManagerForm form)
        {
            //Zapytanie sql zczytujące zgłoszenia wymagające zatwierdzenia/odrzucenia.
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', P.Description AS 'Position', " +
                "E.Name, E.Surname, E.EMail AS 'e-mail',L.Leave_ID AS 'Leave Id', LT.Name AS 'Type', " +
                "LS.Name AS 'Status', L.First_day AS 'First day', L.Last_day AS 'Last day', L.Remarks, L.Used_days " +
                "AS 'Used days' FROM Employee E, " +
                "Leave L, Leave_type LT, Position P, Status_type LS WHERE L.Employee_ID = E.Employee_ID " +
                "AND L.LT_ID = LT.LT_ID AND P.Position_ID = E.Position_ID AND LS.ST_ID = L.LS_ID AND " +
                "LS.Name = @Name ORDER BY L.First_day", form.Connection);
            //Jeżeli formularz posiada uruchomioną transakcję, to dodaj do niej zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Ustaw parametr nazwy statusu w zależności od typu formularza.
            if (form.GetType() == new FormAssistant().GetType())
            {
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending validation";
            }
            else//to znaczy, że manager //todo ładny koment :P
            {
                if (form.GetType() == new FormManager().GetType())
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            DataTable Result = new DataTable();
            //Zczytanie wyników zapytania do tabeli.
            Result.Load(reader);
            reader.Close();
            return Result;
        }

        /// <summary>
        /// Metoda pobrania tabeli z danymi pracowników.
        /// Rozszerza formularz kierownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Tabelę z danymi pracowników. Zawiera następujące kolumny:
        /// "Employee id", "Name", "Surname", "Birth date", "Address",
        /// "PESEL", "e-mail", "Position", "Permission", "Remaining leave days",
        /// "Old leave days".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetEmployees(this FormManager form)
        {
            return GetEmployees((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobrania tabeli z danymi pracowników.
        /// Rozszerza formularz rejestratorki.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Tabelę z danymi pracowników. Zawiera następujące kolumny:
        /// "Employee id", "Name", "Surname", "Birth date", "Address",
        /// "PESEL", "e-mail", "Position", "Permission", "Remaining leave days",
        /// "Old leave days".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetEmployees(this FormAssistant form)
        {
            return GetEmployees((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobrania tabeli z danymi pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Tabelę z danymi pracowników. Zawiera następujące kolumny:
        /// "Employee id", "Name", "Surname", "Birth date", "Address",
        /// "PESEL", "e-mail", "Position", "Permission", "Remaining leave days",
        /// "Old leave days".</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static DataTable GetEmployees(LeaveManagerForm form)
        {
            //Zapytanie sql zwracające dane wszystkich pracowników.
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', " +
                "E.Name, E.Surname, E.Birth_date AS 'Birth date'," +
               "E.Address, E.PESEL, E.EMail AS 'e-mail', Pos.Description AS 'Position', " +
               "Perm.Description AS 'Permission', E.Leave_days AS 'Remaining leave days', " +
               "E.Old_leave_days AS 'Old left leave days' " +
               "FROM Employee E, Position Pos, Permission Perm " +
               "WHERE E.Permission_ID = Perm.Permission_ID " +
               "AND E.Position_ID = Pos.Position_ID", form.Connection);
            //Jeżeli formularz ma włączoną transakcję, dołącz do niej zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            DataTable Results = new DataTable();
            //Załadowanie wyników zapytania do tabeli.
            Results.Load(reader);
            reader.Close();
            return Results;
        }

        /// <summary>
        /// Metoda zwracająca obiekt reprezentujący dane pracownika.
        /// Rozszerza formularz logowania.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pobieranego pracownika.</param>
        /// <returns>Obiekt pracownika, którego numer id zgadza się z argumentem.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static Employee GetEmployee(this FormLogin form, int employeeId)
        {
            return GetEmployee((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda zwracająca obiekt reprezentujący dane pracownika.
        /// Rozszerza formularz rozważenia zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pobieranego pracownika.</param>
        /// <returns>Obiekt pracownika, którego numer id zgadza się z argumentem.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static Employee GetEmployee(this FormLeaveConsideration form, int employeeId)
        {
            return GetEmployee((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda zwracająca obiekt reprezentujący dane pracownika.
        /// Rozszerza formularz dodawania lub edycji pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pobieranego pracownika.</param>
        /// <returns>Obiekt pracownika, którego numer id zgadza się z argumentem.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static Employee GetEmployee(this FormAddOrEditEmployee form, int employeeId)
        {
            return GetEmployee((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda zwracająca obiekt reprezentujący dane pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pobieranego pracownika.</param>
        /// <returns>Obiekt pracownika, którego numer id zgadza się z argumentem.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static Employee GetEmployee(LeaveManagerForm form, int employeeId)
        {
            //Zapytanie sql czytające dane pracownika o danym numerze id.
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID, Perm.Description AS Permission, E.Name, E.Surname, E.Birth_date," +
                                                   "E.Address, E.PESEL, E.EMail, Pos.Description AS Position, E.Year_leave_days, " +
                                                   "E.Leave_days, E.Old_leave_days " +
                                                   "FROM Employee E, Permission Perm, Position Pos WHERE Employee_ID = @Employee_ID " +
                                                   "AND E.Permission_ID = Perm.Permission_ID AND " +
                                                   "E.Position_ID = Pos.Position_ID", form.Connection);
            //Jeżeli formularz ma uruchomioną transakcję, dodaj do niej zapytanie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Ustawienie parametru numeru id szukanego pracownika w zapytaniu.
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Jeżeli znaleziono wpis z danym numerem id.
            if (reader.Read())
            {
                //Zczytanie danych z obiektu czytającego do nowego obiektu pracownika i zwrócenie go.
                Employee employee = new Employee(employeeId, reader["Permission"].ToString(),
                    reader["Name"].ToString(), reader["Surname"].ToString(), (DateTime)reader["Birth_date"],
                    reader["Address"].ToString(), reader["PESEL"].ToString(), reader["EMail"].ToString(),
                    reader["Position"].ToString(), (int)reader["Year_leave_days"], (int)reader["Leave_days"],
                    (int)reader["Old_leave_days"]);
                reader.Close();
                return employee;
            }
            else
            {
                reader.Close();
                throw new EmployeeIdException();
            }
        }

        /// <summary>
        /// Metoda akceptacji zgłoszenia urlopowego.
        /// Rozszerza formularz rozważania zgłoszenia.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>    
        /// <param name="id">Numer id akceptiowanego urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="ArgumentException">Rzucany, gdy formularz rodzica nie jest typu, który pozwala na tą operację.</exception>
        public static void AcceptLeave(this FormLeaveConsideration form, int leaveId)
        {
            //Polecenie sql które zmienia stan akceptowanego zgłoszenia.
            SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                "Status_type WHERE Name = @Name) WHERE Leave_ID = @Leave_ID ",
                form.Connection);
            //Jeżeli formularz ma uruchomioną transakcję, dodaj do niej polecenie.
            if (form.TransactionOn)
                commandUpdateLeave.Transaction = form.Transaction;
            commandUpdateLeave.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = leaveId;
            //Ustalenie nowego stanu zgłoszenia w zależności od typu formularza rodzica(poziomu uprawnień)
            if (form.LeaveManagerParentForm.GetType() == new FormAssistant().GetType())
            {
                commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
            }
            else
            {
                if (form.LeaveManagerParentForm.GetType() == new FormManager().GetType())
                    commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Approved";
                else
                    throw new ArgumentException();
            }
            commandUpdateLeave.ExecuteNonQuery();
        }

        /// <summary>
        /// Metoda służąca do odrzucania zgłoszeń urlopowych.
        /// Rozszerza formularz rozważania zgłoszeń.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="rejectedLeaveId">Numer id odrzucanego urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        public static void RejectLeave(this FormAssistant form, int rejectedLeaveId)
        {
            RejectLeave((LeaveManagerForm)form, rejectedLeaveId);
        }

        /// <summary>
        /// Metoda służąca do odrzucania zgłoszeń urlopowych.
        /// Rozszerza formularz rozważania zgłoszeń.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="rejectedLeaveId">Numer id odrzucanego urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        public static void RejectLeave(this FormManager form, int rejectedLeaveId)
        {
            RejectLeave((LeaveManagerForm)form, rejectedLeaveId);
        }

        /// <summary>
        /// Metoda służąca do odrzucania zgłoszeń urlopowych.
        /// Rozszerza formularz rozważania zgłoszeń.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="rejectedLeaveId">Numer id odrzucanego urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        public static void RejectLeave(this FormLeaveConsideration form, int rejectedLeaveId)
        {
            RejectLeave((LeaveManagerForm)form, rejectedLeaveId);
        }

        /// <summary>
        /// Metoda służąca do odrzucania zgłoszeń urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="rejectedLeaveId">Numer id odrzucanego urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        private static void RejectLeave(LeaveManagerForm form, int rejectedLeaveId)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                Leave rejectedLeave = GetLeave(form, rejectedLeaveId);
                //Polecenie sql służące do aktualizacji stanu zgłoszenia.
                SqlCommand command = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                    "Status_type WHERE Name = @Name), Used_days = @Used_days WHERE Leave_ID = @Leave_ID", form.Connection,
                    form.Transaction);
                command.Parameters.Add("@Used_days", SqlDbType.Int).Value = 0;
                command.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = rejectedLeave.Id;
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Rejected";
                command.ExecuteNonQuery();
                AddLeaveDays(form, rejectedLeave.EmployeeId, rejectedLeave.UsedDays);
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static void GetDays(this FormEmployee form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            GetDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz danych pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static void GetDays(this FormEmployeeData form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            GetDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static void GetDays(this FormLeaveApplication form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            GetDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static void GetDays(LeaveManagerForm form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            //Zapytanie zczytujące.
            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee " +
                "WHERE Employee_ID = @Employee_ID", form.Connection);
            //Jeżeli formularz posiada uruchomioną transakcję, to dołącz ją do polecenia.
            if (form.TransactionOn)
                commandSelectDays.Transaction = form.Transaction;
            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader readerDays = commandSelectDays.ExecuteReader();
            //Odczytanie pierwszego wiersza wyników(powinien być tylko jeden).
            readerDays.Read();
            //Przypisanie wartości do zmiennych.
            leaveDays = (int)readerDays["Leave_days"];
            oldLeaveDays = (int)readerDays["Old_leave_days"];
            readerDays.Close();
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <param name="yearDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba dni pracownika na rok.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static void GetDays(this FormLeaveApplication form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            GetDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz danych pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <param name="yearDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba dni pracownika na rok.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static void GetDays(this FormEmployeeData form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            GetDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDaysList">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDaysList">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <param name="yearDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba dni pracownika na rok.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static void GetDays(LeaveManagerForm form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            //Zapytanie sql zczytujące dane.
            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days, Year_leave_days FROM Employee " +
                "WHERE Employee_ID = @Employee_ID", form.Connection);
            //Jeżeli formularz ma uruchomioną transakcję, dodaj ją do polecenia.
            if (form.TransactionOn)
                commandSelectDays.Transaction = form.Transaction;
            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader readerDays = commandSelectDays.ExecuteReader();
            //Zczytanie pierwszej linijki wyników(powinna być tylko jedna).
            readerDays.Read();
            //Wpisanie wyników do zmiennych.
            leaveDays = (int)readerDays["Leave_days"];
            oldLeaveDays = (int)readerDays["Old_leave_days"];
            yearDays = (int)readerDays["Year_leave_days"];
            readerDays.Close();
        }

        /// <summary>
        /// Metoda pobietania tabeli z danymi urlopowymi danego pracownika.
        /// Rozszerza formularz pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <returns>Zwraca tabelę z danymi urlopowymi pracownika.
        /// Kolumny tabeli: "Leave Id", "Status", "First day", "Last day", "Type",
        /// "Remarks", "No. used days"</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetLeaves(this FormEmployee form, int employeeId)
        {
            return GetLeaves((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda pobietania tabeli z danymi urlopowymi danego pracownika.
        /// Rozszerza formularz danych pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <returns>Zwraca tabelę z danymi urlopowymi pracownika.
        /// Kolumny tabeli: "Leave Id", "Status", "First day", "Last day", "Type",
        /// "Remarks", "No. used days"</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetLeaves(this FormEmployeeData form, int employeeId)
        {
            return GetLeaves((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda pobietania tabeli z danymi urlopowymi danego pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <returns>Zwraca tabelę z danymi urlopowymi pracownika.
        /// Kolumny tabeli: "Leave Id", "Status", "First day", "Last day", "Type",
        /// "Remarks", "No. used days"</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static DataTable GetLeaves(LeaveManagerForm form, int employeeId)
        {
            //Zapytanie zczytujące dane.
            SqlCommand commandSelectLeaves = new SqlCommand("SELECT Leave_ID AS 'Leave Id', LS.Name AS 'Status', L.First_day AS 'First day', " +
                        "L.Last_day AS 'Last day', LT.Name AS 'Type', L.Remarks, L.Used_days AS 'No. used days' " +
                        "FROM Employee E, Leave L, Leave_type LT, Status_type LS " +
                        "WHERE L.LT_ID = LT.LT_ID AND L.LS_ID = LS.ST_ID AND E.Employee_ID = L.Employee_ID " +
                        "AND E.Employee_ID = @Employee_ID ORDER BY L.First_day", form.Connection);
            //Jeżeli formularz ma uruchomioną transakcję, dodaj ją do zapytania.
            if (form.TransactionOn)
                commandSelectLeaves.Transaction = form.Transaction;
            commandSelectLeaves.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            //Stwórz obiekt czytający wyniki zapytania.
            SqlDataReader readerLeaves = commandSelectLeaves.ExecuteReader();
            //Stworzenie obiektu do którego załadowane zostaną dane.
            DataTable leaves = new DataTable();
            //Wczytanie danych.
            leaves.Load(readerLeaves);
            return leaves;
        }

        /// <summary>
        /// Metoda sprawdzająca, czy któryś z dni z okresu między argumentami date1 i date2
        /// jest użyty w ktorymś aktywnym(nie odrzuconym/anulowanym) zgłoszeniu urlopowym
        /// danego pracownika.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="date1">Data rozpoczęcia okresu.</param>
        /// <param name="date2">Data zakończenia okresu.</param>
        /// <param name="employeeID">Numer id pracownika, którego urlopy będą brane pod uwagę.</param>
        /// <returns>Wartość logiczną mówiącą czy któryś z dni okresu jest już użyty.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static bool IsDateFromPeriodUsed(this FormLeaveApplication form, DateTime date1, DateTime date2, int employeeID)
        {
            return IsDateFromPeriodUsed((LeaveManagerForm)form, date1, date2, employeeID);
        }

        /// <summary>
        /// Metoda sprawdzająca, czy któryś z dni z okresu między argumentami date1 i date2
        /// jest użyty w ktorymś aktywnym(nie odrzuconym/anulowanym) zgłoszeniu urlopowym
        /// danego pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="date1">Data rozpoczęcia okresu.</param>
        /// <param name="date2">Data zakończenia okresu.</param>
        /// <param name="employeeID">Numer id pracownika, którego urlopy będą brane pod uwagę.</param>
        /// <returns>Wartość logiczną mówiącą czy któryś z dni okresu jest już użyty.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static bool IsDateFromPeriodUsed(LeaveManagerForm form, DateTime date1, DateTime date2, int employeeID)
        {
            //Jeżeli date1 jest później niż date2 zamień je miejscami.
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            //Zapytanie sql zczytujące daty początków i końców wszystkich urlopów.
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID", form.Connection);//todo dodać warunek w zapytaniu wykluczający wpisy, canceled i rejected.
            //Jeżeli formularz ma uruchomioną transakcję, to podepnij ją do zapytania.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeID;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Dopóki udało się odczytać wiersz.
            while (reader.Read())
            {
                //Zczytanie dat początku i końca urlopu.
                DateTime firstDay = (DateTime)reader["First_day"];
                DateTime lastDay = (DateTime)reader["Last_day"];
                if ((date1.CompareTo(firstDay) <= 0) && (date2.CompareTo(firstDay) >= 0)//firstDay between date1 & date2
                    || (date1.CompareTo(lastDay) <= 0) && (date2.CompareTo(lastDay) >= 0)//lastDay between date1 & date2
                    || (date1.CompareTo(firstDay) >= 0 && (date2.CompareTo(lastDay) <= 0)))//date1 & date2 between firstDay & lastDay
                {
                    reader.Close();
                    return true;
                }
            }
            //Jeżeli nie znaleziono żadnej kolizji.
            reader.Close();
            return false;
        }

        /// <summary>
        /// Metoda sprawdzająca, czy któryś z dni z okresu między argumentami date1 i date2
        /// jest użyty w ktorymś aktywnym(nie odrzuconym/anulowanym) zgłoszeniu urlopowym
        /// danego pracownika. Przy sprawdzaniu pomija wpis urlopowy zaczynający się w podanym
        /// dniu.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="date1">Data rozpoczęcia okresu.</param>
        /// <param name="date2">Data zakończenia okresu.</param>
        /// <param name="employeeID">Numer id pracownika, którego urlopy będą brane pod uwagę.</param>
        /// <param name="skippedEntryFirstDay">Data rozpoczęcia ignorowanego wpisu urlopowego.</param>
        /// <returns>Wartość logiczną mówiącą czy któryś z dni okresu jest już użyty.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static bool IsDateFromPeriodUsed(this FormLeaveApplication form, DateTime date1, DateTime date2,
           int employeeID, DateTime skippedEntryFirstDay)
        {
            return IsDateFromPeriodUsed((LeaveManagerForm)form, date1, date2, employeeID, skippedEntryFirstDay);
        }

        /// <summary>
        /// Metoda sprawdzająca, czy któryś z dni z okresu między argumentami date1 i date2
        /// jest użyty w ktorymś aktywnym(nie odrzuconym/anulowanym) zgłoszeniu urlopowym
        /// danego pracownika. Przy sprawdzaniu pomija wpis urlopowy zaczynający się w podanym
        /// dniu.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="date1">Data rozpoczęcia okresu.</param>
        /// <param name="date2">Data zakończenia okresu.</param>
        /// <param name="employeeID">Numer id pracownika, którego urlopy będą brane pod uwagę.</param>
        /// <param name="skippedEntryFirstDay">Data rozpoczęcia ignorowanego wpisu urlopowego.</param>
        /// <returns>Wartość logiczną mówiącą czy któryś z dni okresu jest już użyty.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static bool IsDateFromPeriodUsed(LeaveManagerForm form, DateTime date1, DateTime date2,
            int employeeID, DateTime skippedEntryFirstDay)
        {
            //Jeżeli date2 jest wcześniej niż date1 zamień je miejscami.
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            //Zapytanie sql zczytujące daty urlopów.
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID AND First_day != @Skipped_entry_first_day", form.Connection);//todo dodać warunek w zapytaniu wykluczający wpisy, canceled i rejected.
            //Jeżeli formularz ma uruchomioną transakcję, dołącz ją do zapytania.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeID;
            command.Parameters.Add("@Skipped_entry_first_day", SqlDbType.Date).Value = skippedEntryFirstDay;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Dopóki udało się odczytać wiersz z wyników zapytania.
            while (reader.Read())
            {
                //Zczytanie dni początku i końca urlopu.
                DateTime firstDay = (DateTime)reader["First_day"];
                DateTime lastDay = (DateTime)reader["Last_day"];
                if ((date1.CompareTo(firstDay) <= 0) && (date2.CompareTo(firstDay) >= 0)//firstDay between date1 & date2
                    || (date1.CompareTo(lastDay) <= 0) && (date2.CompareTo(lastDay) >= 0)//lastDay between date1 & date2
                    || (date1.CompareTo(firstDay) >= 0 && (date2.CompareTo(lastDay) <= 0)))//date1 & date2 between firstDay & lastDay
                {
                    reader.Close();
                    return true;
                }
            }
            //Jeżeli nie znaleziono kolizji.
            reader.Close();
            return false;
        }

        /// <summary>
        /// Metoda służąca do "dawania" pracownikowi dodatkowych dni urlopowych. Dodaje je najpierw do 
        /// normalnych dni urlopowych, a gdy ich liczba == liczba dni/rok, dodaje resztę dni do zaległych dni urlopowych.
        /// Możliwe jest odejmowanie dni urlopowych za pomocą tej metody. Aby to uczynić należy podać ujemną wartość do 
        /// argumentu number.
        /// Rozszerza formularz zgłoszenia urlopowego. 
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="employeeId">Numer id pracownika, któremu mają zostać dodane dni.</param>
        /// <param name="number">Liczba dodawanych dni. Może być ujemna.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby odjęcia większej liczby dni, niż pracownik posiada.</exception>
        public static void AddLeaveDays(this FormLeaveApplication form, int employeeId, int number)
        {
            AddLeaveDays((LeaveManagerForm)form, employeeId, number);
        }

        /// <summary>
        /// Metoda służąca do "dawania" pracownikowi dodatkowych dni urlopowych. Dodaje je najpierw do 
        /// normalnych dni urlopowych, a gdy ich liczba == liczba dni/rok, dodaje resztę dni do zaległych dni urlopowych.
        /// Możliwe jest odejmowanie dni urlopowych za pomocą tej metody. Aby to uczynić należy podać ujemną wartość do 
        /// argumentu number.
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="employeeId">Numer id pracownika, któremu mają zostać dodane dni.</param>
        /// <param name="number">Liczba dodawanych dni. Może być ujemna.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby odjęcia większej liczby dni, niż pracownik posiada.</exception>
        private static void AddLeaveDays(LeaveManagerForm form, int employeeId, int number)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
             * o odpowiednim poziomie izolacji.
             */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                /* Zmienne do których zczytane zostaną wartości odpowiednio liczby dni urlopowych, 
                 * zaległych dni urlopowych oraz dni urlopowych przypadających na rok.
                 */
                int leaveDays = 0, oldLeaveDays = 0, yearDays = 0;
                /* Zczytanie aktualnych wartości do zmiennych dni. Dane te muszą być aktualne, aby
                 * nie było ryzyka, obliczamy i wpisujemy do bazy liczbę dni na podstawie danych 
                 * nie aktualnych.
                 */
                GetDays(form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
                //Polecenie sql aktualizujące liczbę dni danego pracownika.
                SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                    "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                    "WHERE Employee_ID = @Employee_ID", form.Connection, form.Transaction);
                //Obliczenie wartości dla poszczególnych pól w bazie.
                //Jeżeli dodajemy dni.
                if (number > 0)
                {
                    //Jeżeli trzeba dodać dni zaległe.
                    if (leaveDays + number > yearDays)
                    {
                        commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = yearDays;
                        commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value =
                            oldLeaveDays + number - (yearDays - leaveDays);
                    }
                    else
                    {
                        commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = leaveDays + number;
                        commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value = 0;
                    }
                }
                else//Jeżeli odejmujemy dni.
                {
                    //Jeżeli odejmujemy więcej niż jest.
                    if (number + leaveDays + oldLeaveDays < 0)
                        throw new ArgumentException();
                    else
                    {
                        //Jeżeli liczba odejmowanych dni jest <= liczbie zaległych dni.
                        if (number + oldLeaveDays >= 0)
                        {
                            commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = leaveDays;
                            commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value = number + oldLeaveDays;
                        }
                        else
                        {
                            commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = number + leaveDays + oldLeaveDays;
                            commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value = 0;
                        }
                    }
                }
                commandUpdateEmployee.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                commandUpdateEmployee.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda sprawdzająca, czy dany typ urlopu konsumuje dni urlopowe.
        /// </summary>
        /// <param name="form">Formularz wymagający metody.</param>
        /// <param name="leaveTypeName">Nazwa typu urlopu.</param>
        /// <returns>Zwraca odpowiedź na pytanie: "Czy dany typ urlopu konsumuje dni urlopowe?'.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static bool ConsumesDays(LeaveManagerForm form, String leaveTypeName)
        {
            SqlCommand commandSelectConsumesDays = new SqlCommand("SELECT Consumes_days FROM  " +
                "Leave_type WHERE Name = @Name", form.Connection);
            //Jeżeli formularz ma uruchomioną transakcję to podepnij ją do zapytania.
            if (form.TransactionOn)
                commandSelectConsumesDays.Transaction = form.Transaction;
            //Dodanie do zapytania informacji o nazwie sprawdzanego typu.
            commandSelectConsumesDays.Parameters.Add("@Name", SqlDbType.VarChar).Value = leaveTypeName;
            //Obiekt czytający wyniki zapytania.
            SqlDataReader readerConsumesDays = commandSelectConsumesDays.ExecuteReader();
            //Odczytanie wyniku zapytania(1 wiersza, powinien być tylko jeden).
            readerConsumesDays.Read();
            //Zczytanie wyniku do zmiennej.
            bool result = (bool)readerConsumesDays["Consumes_days"];
            //Zamknięcie obiektu czytającego.
            readerConsumesDays.Close();
            return result;
        }

        /// <summary>
        /// Metoda zwracająca liczbę dni w okresie od date1(włącznie) do date2(włącznie), które konsumują dni urlopowe.
        /// </summary>
        /// <param name="form">Formularz wymagający metody.</param>
        /// <param name="date1">Pierwszy dzień sprawdzanego okresu.</param>
        /// <param name="date2">Ostatni dzień sprawdzanego okresu.</param>
        /// <returns>Liczba dni w okresie między date1 i date2, które konsumują dni rulopowe.</returns>
        public static int GetNumberOfWorkDays(this FormLeaveApplication form, DateTime date1, DateTime date2, int employeeId)
        {
            return GetNumberOfWorkDays((LeaveManagerForm)form, date1, date2, employeeId);
        }

        //todo tabela z dniami wolnymi od pracy, uwzględnić godziny/dni pracy pracownika, gdy już będa. 
        //Nie zapomnieć dodać odpowiednich wyjątków, gdy będzie operacja na BD.

        /// <summary>
        /// Metoda zwracająca liczbę dni w okresie od date1(włącznie) do date2(włącznie), które konsumują dni urlopowe.
        /// </summary>
        /// <param name="form">Formularz wymagający metody.</param>
        /// <param name="date1">Pierwszy dzień sprawdzanego okresu.</param>
        /// <param name="date2">Ostatni dzień sprawdzanego okresu.</param>
        /// <returns>Liczba dni w okresie między date1 i date2, które konsumują dni rulopowe.</returns>
        private static int GetNumberOfWorkDays(LeaveManagerForm form, DateTime date1, DateTime date2, int employeeId)
        {
            bool[] workingDays = GetWorkingDaysOfWeek(form, employeeId);
            //Obliczenie różnicy czasu pomiędzy datami.
            TimeSpan timeSpan = date2 - date1;
            //Obliczenie maksymalnej liczby dni, które mogą konsumować dni.
            int numberOfDays = (int)Math.Round(timeSpan.TotalDays) + 1;
            //Dopóki nie sprawdzono wszystkich dni.
            while (date1.CompareTo(date2) <= 0)
            {
                switch (date1.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        if (!workingDays[0])
                        {
                            numberOfDays--;
                        }
                        break;
                    case DayOfWeek.Tuesday:
                        if (!workingDays[1])
                        {
                            numberOfDays--;
                        }
                        break;
                    case DayOfWeek.Wednesday:
                        if (!workingDays[2])
                        {
                            numberOfDays--;
                        }
                        break;
                    case DayOfWeek.Thursday:
                        if (!workingDays[3])
                        {
                            numberOfDays--;
                        }
                        break;
                    case DayOfWeek.Friday:
                        if (!workingDays[4])
                        {
                            numberOfDays--;
                        }
                        break;
                    case DayOfWeek.Saturday:
                        if (!workingDays[5])
                        {
                            numberOfDays--;
                        }
                        break;
                    case DayOfWeek.Sunday:
                        if (!workingDays[6])
                        {
                            numberOfDays--;
                        }
                        break;
                }
                //Przesunięcie sprawdzanego dnia na następny.
                date1 = date1.AddDays(1);
            }
            return numberOfDays;
        }

        /// <summary>
        /// Metoda pobierająca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="id">Numer id wpisu urlopowego.</param>
        /// <returns>Obiekt reprezentujący dany urlop.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static Leave GetLeave(this FormEmployeeData form, int leaveId)
        {
            return GetLeave((LeaveManagerForm)form, leaveId);
        }

        /// <summary>
        /// Metoda pobierająca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="id">Numer id wpisu urlopowego.</param>
        /// <returns>Obiekt reprezentujący dany urlop.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static Leave GetLeave(this FormEmployee form, int leaveId)
        {
            return GetLeave((LeaveManagerForm)form, leaveId);
        }

        /// <summary>
        /// Metoda pobierająca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="id">Numer id pobieranego urlopu.</param>
        /// <returns>Obiekt reprezentujący dany urlop.</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static Leave GetLeave(LeaveManagerForm form, int leaveId)
        {
            SqlCommand command = new SqlCommand("SELECT L.Employee_ID, LT.Name AS 'Type', " +
                "LS.Name AS 'Status', L.First_day, L.Last_day, " +
                "L.Remarks, L.Used_days FROM Leave L, Leave_type LT, Status_type LS WHERE Leave_ID = @Leave_ID",
                form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = leaveId;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Leave result = new Leave(leaveId, (int)reader["Employee_ID"], reader["Type"].ToString(), reader["Status"].ToString(),
                (DateTime)reader["First_day"], (DateTime)reader["Last_day"], reader["Remarks"].ToString(), (int)reader["Used_days"]);
            reader.Close();
            return result;
        }

        /// <summary>
        /// Metoda edytująca wpis urlopowy.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="leave">Obiekt urlopu wpisany w miejsce starego wpisu.</param>
        /// <param name="oldLeaveId">Numer id edytowanego wpisu urlopowego.</param>        
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby wzięcia pracownikowi większej liczby dni, niż posiada.</exception> 
        public static void EditLeave(this FormLeaveApplication form, Leave leave, int editedLeaveId)
        {
            EditLeave((LeaveManagerForm)form, leave, editedLeaveId);
        }

        /// <summary>
        /// Metoda edytująca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt urlopu wpisany w miejsce starego wpisu.</param>
        /// <param name="editedLeaveId">Numer id edytowanego wpisu urlopowego.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby wzięcia pracownikowi większej liczby dni, niż posiada.</exception> 
        private static void EditLeave(LeaveManagerForm form, Leave leave, int editedLeaveId)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
            * o odpowiednim poziomie izolacji.
            */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Zczytanie podmienianego urlopu.
                Leave oldLeave = GetLeave(form, editedLeaveId);
                //Jeżeli wpisywany urlop konsumuje dni urlopowe.
                if (ConsumesDays(form, leave.LeaveType))
                {
                    //Obliczenie różnicy o którą trzeba zmienić liczbę dni urlopowych pracownika.
                    int difference = oldLeave.UsedDays - leave.UsedDays;
                    AddLeaveDays(form, leave.EmployeeId, difference);
                }
                else//Nowy urlop nie konsumuje dni.
                {
                    //Jeżeli stary urlop konsumuje dni.
                    if (ConsumesDays(form, oldLeave.LeaveType))
                        //Dodajemy pracownikowi tyle dni, ile stary urlop konsumował.
                        AddLeaveDays(form, leave.EmployeeId, oldLeave.UsedDays);
                }
                //Polecenie sql aktualizujące wszystkie dane w starym wpisie.
                SqlCommand commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = (SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), LS_ID = " +
                   "(SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                   "First_day = @First_day, Last_day = @Last_day, Remarks = @Remarks " +
                   "WHERE Leave_ID = @OldLeave_ID", form.Connection, form.Transaction);
                commandUpdate.Parameters.Add("@OldLeave_ID", SqlDbType.Date).Value = editedLeaveId;
                //Jeżeli wpisywany urlop jest chorobowym, to niezależnie od ustawionego stanu jest ustawiony stan zatwierdzony.
                if (leave.LeaveStatus.Equals("Sick"))
                {
                    commandUpdate.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Approved";
                }
                else
                {
                    commandUpdate.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = leave.LeaveStatus;
                }
                commandUpdate.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                commandUpdate.Parameters.Add("@Leave_type_name", SqlDbType.VarChar).Value = leave.LeaveType;
                commandUpdate.Parameters.Add("@First_day", SqlDbType.Date).Value = leave.FirstDay;
                commandUpdate.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.LastDay;
                commandUpdate.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = leave.Remarks;
                commandUpdate.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda dodawania zgłoszenia urlopowego.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="leave">Obiekt reprezentujący nowy wpis urlopowy.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby odjęcia większej liczby dni, niż pracownik posiada.</exception>
        public static void AddLeave(this FormLeaveApplication form, Leave leave)
        {
            AddLeave((LeaveManagerForm)form, leave);
        }

        /// <summary>
        /// Metoda dodawania zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt reprezentujący nowy wpis urlopowy.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby odjęcia większej liczby dni, niż pracownik posiada.</exception>
        private static void AddLeave(LeaveManagerForm form, Leave leave)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
           * o odpowiednim poziomie izolacji.
           */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Liczba zużytych przez urlop dni.
                int usedDays;
                //Jeżeli urlop konsumuje dni.
                if (ConsumesDays(form, leave.LeaveType))
                {
                    usedDays = GetNumberOfWorkDays(form, leave.FirstDay, leave.LastDay, leave.EmployeeId);
                    //Odejmujemy pracownikowi dni za dany urlop.
                    AddLeaveDays(form, leave.EmployeeId, -usedDays);
                }
                else
                {
                    usedDays = 0;
                }
                //Polecenie sql Zczytujące największy nr id + 1
                SqlCommand commandGetNewId = new SqlCommand("(SELECT MAX(Leave_ID) + 1 FROM Leave)", form.Connection, form.Transaction);
                int newLeaveId;
                try
                {
                    newLeaveId = (int)commandGetNewId.ExecuteScalar();
                }
                catch
                {
                    //Jeżeli nie udało się zczytać id, to możliwe, że żaden wpis w bazie nie istnieje. Wtedy przypisujemy id = 0
                    newLeaveId = 0;
                }

                //Polecenie sql dodające nowy wpis urlopowy.
                SqlCommand commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Leave_ID, @Employee_ID, " +
                     "(SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), (SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                     "@First_day, @Last_day, @Remarks, @Used_days)", form.Connection, form.Transaction);
                //Jeżeli wpis to chorobowe, to automatycznie otrzymuje stan zatwierdzony.
                if (leave.LeaveType.Equals("Sick"))
                {
                    commandInsertLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Approved";
                }
                else
                {
                    commandInsertLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = leave.LeaveStatus;
                }
                commandInsertLeave.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = newLeaveId;
                commandInsertLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                commandInsertLeave.Parameters.Add("@Leave_type_name", SqlDbType.VarChar).Value = leave.LeaveType;
                commandInsertLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = leave.FirstDay;
                commandInsertLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.LastDay;
                commandInsertLeave.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = leave.Remarks;
                commandInsertLeave.Parameters.Add("@Used_days", SqlDbType.Int).Value = usedDays;
                commandInsertLeave.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Dodanie chorobowego.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="leave">Obiekt reprezentujący dodawany urlop.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentException">Występuje w przypadku próby odjęcia większej liczby dni, niż pracownik posiada.</exception>
        public static void AddSickLeave(this FormLeaveApplication form, Leave leave)
        {
            AddSickLeave((LeaveManagerForm)form, leave);
        }

        /// <summary>
        /// Dodanie chorobowego.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt reprezentujący dodawany urlop.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        private static void AddSickLeave(LeaveManagerForm form, Leave leave)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
           * o odpowiednim poziomie izolacji.
           */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Zczytanie urlopów pracownika, któremu dodajemy chorobowe.
                /// Kolumny tabeli: "Leave Id", "Status", "First day", "Last day", "Type",
                /// "Remarks", "No. used days"
                DataTable dataLeaves = GetLeaves(form, leave.EmployeeId);

                /* Zmienna w której będzie przechowywana liczba dni do zwrócenia pracownikowi.
                 * Powiększa się w przypadku, gdy chorobowe zachodzi na jakiś urlop.
                 */
                int returnedLeaveDays = 0;
                //Dla każdego istniejącego w bazie urlopu.
                foreach (DataRow row in dataLeaves.Rows)
                {
                    //Pierwszy dzień sprawdzanego urlopu jest później lub ten sam, co pierwszy dzień chorobowego
                    if ((((DateTime)row.ItemArray.GetValue(2)).CompareTo(leave.FirstDay) >= 0)
                        //i jest wcześniej lub taki sam jak ostatni dzień chorobowego.
                    && (((DateTime)row.ItemArray.GetValue(2)).CompareTo(leave.LastDay) <= 0))
                    {//Czyli w praktyce: Zaczyna się w trakcie chorobowego -> jest anulowany.
                        //Jeżeli zachodzący wpis to chorobowe.
                        if (row.ItemArray.GetValue(4).ToString().Equals("Sick"))
                            throw new EntryExistsException();
                        //Polecenie sql zmieniające stan zachodzącego urlopu na anulowany.
                        SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                           "LS_ID = (SELECT ST_ID FROM Status_type WHERE Name = @Status_name), Used_days = @Used_days " +
                           "WHERE Leave_ID = @Leave_ID", form.Connection, form.Transaction);
                        commandUpdateLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Canceled";
                        commandUpdateLeave.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = (int)row.ItemArray.GetValue(0);
                        commandUpdateLeave.Parameters.Add("@Used_days", SqlDbType.Int).Value = 0;
                        commandUpdateLeave.ExecuteNonQuery();
                        //Dodanie do liczby dni do zwrócenia pracownikowi dni anulowanego urlopu.
                        returnedLeaveDays += (int)row.ItemArray.GetValue(6);
                        continue;
                    }

                    if ((leave.FirstDay.CompareTo((DateTime)row.ItemArray.GetValue(2)) >= 0)//Sick first day later than leave first day 
                    && (leave.FirstDay.CompareTo((DateTime)row.ItemArray.GetValue(3)) <= 0))//and earlier than leave last day.
                    {//Czyli w praktyce: Kończy się w trakcie chorobowego -> jest 'przycinany' do ostatniego dnia przed chorobowym.
                        SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                            "Last_day = @Last_day, Used_days = @Used_days WHERE Leave_ID = @Leave_ID", form.Connection, form.Transaction);
                        commandUpdateLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.FirstDay.AddDays(-1);
                        commandUpdateLeave.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = (int)row.ItemArray.GetValue(0);
                        //Nowa liczba użytych dni to liczba użytych dni pomiędzy pierwszym dniem zmienianego urlopu i pierwszym-1 dniem chorobowego.
                        commandUpdateLeave.Parameters.Add("@Used_days", SqlDbType.Int).Value =
                            GetNumberOfWorkDays(form, (DateTime)row.ItemArray.GetValue(2), leave.FirstDay.AddDays(-1), leave.EmployeeId);
                        commandUpdateLeave.ExecuteNonQuery();
                        //Dodanie do liczby dni do zwrócenia pracownikowi liczby dni za okres od początku chorobowego do końca urlopu.
                        returnedLeaveDays += GetNumberOfWorkDays(form, leave.FirstDay.AddDays(-1), (DateTime)row.ItemArray.GetValue(1), leave.EmployeeId);
                        continue;
                    }
                }
                //Zwrócenie pracownikowi dni.
                AddLeaveDays(form, leave.EmployeeId, returnedLeaveDays);
                //Dodanie urlopu.
                AddLeave(form, leave);
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda zmiany hasła.
        /// Rozszerza formularz zmiany loginu i/lub hasła.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmienione zostają dane.</param>
        /// <param name="oldPassword">Stare hasło.</param>
        /// <param name="newPassword">Nowe hasło.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="PasswordException">Występuje, gdy podane aktualne hasło jest nieprawidłowe.</exception>
        public static void ChangePassword(this FormChangeLoginOrPassword form, int employeeId, String oldPassword, String newPassword)
        {
            ChangeLoginOrPassword((LeaveManagerForm)form, employeeId, oldPassword, newPassword, "");
        }

        /// <summary>
        /// Metoda zmiany loginu.
        /// Rozszerza formularz zmiany loginu i/lub hasła.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmienione zostają dane.</param>
        /// <param name="oldPassword">Stare hasło.</param>
        /// <param name="newLogin">Nowy login.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="PasswordException">Występuje, gdy podane aktualne hasło jest nieprawidłowe.</exception>
        public static void ChangeLogin(this FormChangeLoginOrPassword form, int employeeId, String oldPassword, String newLogin)
        {
            ChangeLoginOrPassword((LeaveManagerForm)form, employeeId, oldPassword, "", newLogin);
        }

        /// <summary>
        /// Metoda zmiany loginu i hasła.
        /// Rozszerza formularz zmiany loginu i/lub hasła.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmienione zostają dane.</param>
        /// <param name="oldPassword">Stare hasło.</param>
        /// <param name="newPassword">Nowe hasło.</param>
        /// <param name="newLogin">Nowy login.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="PasswordException">Występuje, gdy podane aktualne hasło jest nieprawidłowe.</exception>
        public static void ChangeLoginOrPassword(this FormChangeLoginOrPassword form, int employeeId, String oldPassword, String newPassword, String newLogin)
        {
            ChangeLoginOrPassword((LeaveManagerForm)form, employeeId, oldPassword, newPassword, newLogin);
        }

        /// <summary>
        /// Metoda zmiany loginu i hasła.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmienione zostają dane.</param>
        /// <param name="oldPassword">Stare hasło.</param>
        /// <param name="newPassword">Nowe hasło. Jeżeli nie jest zmieniane to podaj pusty string.</param>
        /// <param name="newLogin">Nowy login. Jeżeli nie jest zmieniany to podaj pusty string.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="PasswordException">Występuje, gdy podane aktualne hasło jest nieprawidłowe.</exception>
        private static void ChangeLoginOrPassword(LeaveManagerForm form, int employeeId, String oldPassword, String newPassword, String newLogin)
        {
            //Sprawdzenie, czy zgadza się stare hasło.
            SqlCommand command = new SqlCommand("SELECT Employee_ID FROM Employee WHERE " +
                "Employee_ID = @Employee_ID AND Password = @Password", form.Connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(oldPassword);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())//Jeżeli stare hasło się zgadza. (Znaleziono wpis z id pracownika i danym hasłem)
            {
                reader.Close();
                //Jeżeli zmieniamy i login i hasło.
                if (newLogin.Length != 0 && newPassword.Length != 0)
                {
                    command.CommandText = "UPDATE Employee SET Login = @Login, " +
                        "Password = @Password WHERE Employee_ID = @Employee_ID";
                    command.Parameters.Clear();
                    command.Parameters.Add("@Login", SqlDbType.VarChar).Value = newLogin;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(newPassword);
                    command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                    command.ExecuteNonQuery();
                }
                else
                {
                    //Jeżeli zmieniamy tylko login.
                    if (newLogin.Length != 0)
                    {
                        command.CommandText = "UPDATE Employee SET Login = @Login WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Login", SqlDbType.VarChar).Value = newLogin;
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery();
                    }
                    //Jeżeli zmieniamy tylko hasło.
                    if (newPassword.Length != 0)
                    {
                        command.CommandText = "UPDATE Employee SET Password = @Password WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(newPassword);
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery();
                    }
                }
            }
            else//Podane stare hasło nie jest prawidłowe.
            {
                reader.Close();
                throw new PasswordException();
            }
        }

        /// <summary>
        /// Metoda usuwania wpisu urlopowego.
        /// Rozszerza formularz pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="leaveId">Numer id usuwanego wpisu urlopowego.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        public static void DeleteLeave(this FormEmployeeData form, int leaveId)
        {
            DeleteLeave((LeaveManagerForm)form, leaveId);
        }

        /// <summary>
        /// Metoda usuwania wpisu urlopowego.
        /// Rozszerza formularz pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="leaveId">Numer id usuwanego wpisu urlopowego.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        public static void DeleteLeave(this FormEmployee form, int leaveId)
        {
            DeleteLeave((LeaveManagerForm)form, leaveId);
        }

        /// <summary>
        /// Metoda usuwania wpisu urlopowego.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leaveId">Numer id usuwanego wpisu urlopowego.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        private static void DeleteLeave(LeaveManagerForm form, int leaveId)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
            * o odpowiednim poziomie izolacji.
            */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                Leave leave = GetLeave(form, leaveId);
                //Polecenie sql usuwające wpis urlopowy.
                SqlCommand commandDeleteLeave = new SqlCommand("DELETE FROM Leave WHERE Leave_ID = @Leave_ID",
                    form.Connection, form.Transaction);
                commandDeleteLeave.Parameters.Add("@Leave_ID", SqlDbType.Int).Value = leave.Id;
                commandDeleteLeave.ExecuteNonQuery();
                AddLeaveDays(form, leave.EmployeeId, leave.UsedDays);
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda dodawania typu urlopu.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="name">Nazwa nowego typu.</param>
        /// <param name="consumesDays">Czy nowy typ konsumuje dni.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="EntryExists">Występuje w przypadku próby dodania istniejącego typu.</exception>
        public static void AddLeaveType(this FormAddLeaveType form, string name, bool consumesDays)
        {
            AddLeaveType((LeaveManagerForm)form, name, consumesDays);
        }

        /// <summary>
        /// Metoda dodawania typu urlopu.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="name">Nazwa nowego typu.</param>
        /// <param name="consumesDays">Czy nowy typ konsumuje dni.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="EntryExists">Występuje w przypadku próby dodania istniejącego typu.</exception>
        private static void AddLeaveType(LeaveManagerForm form, string name, bool consumesDays)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Sprawdzenie, czy dany typ jest już w bazie.
                SqlCommand commandCheckIfExists = new SqlCommand("SELECT LT_ID " +
                    "FROM Leave_type WHERE Name = @Name", form.Connection, form.Transaction);
                commandCheckIfExists.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                SqlDataReader reader = commandCheckIfExists.ExecuteReader();
                if (reader.Read())//Jeżeli tak rzuć wyjątek.
                {
                    reader.Close();
                    throw new EntryExistsException();
                }
                reader.Close();
                //Poleceni sql dodające nowy typ.
                SqlCommand command = new SqlCommand("INSERT INTO Leave_type " +
                    "VALUES((SELECT MAX(LT_ID) + 1 FROM Leave_type), @Name, @Consumes_days)", form.Connection, form.Transaction);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@Consumes_days", SqlDbType.Bit).Value = consumesDays;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda dodająca nowy rodzaj pozycji (pracowników) do bazy.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="name">Nazwa nowej pozycji.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="EntryExists">Występuje w przypadku próby dodania istniejącej pozycji.</exception>
        public static void AddPositionType(this FormAddPosition form, string name)
        {
            AddPositionType((LeaveManagerForm)form, name);
        }

        /// <summary>
        /// Metoda dodająca nowy rodzaj pozycji (pracowników) do bazy.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="name">Nazwa nowej pozycji.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="EntryExists">Występuje w przypadku próby dodania istniejącej pozycji.</exception>
        private static void AddPositionType(LeaveManagerForm form, string name)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Sprawdzenie, czy dana pozycja już istnieje.
                SqlCommand commandCheckIfExists = new SqlCommand("SELECT Position_ID " +
                    "FROM Position WHERE Description = @Description", form.Connection, form.Transaction);
                commandCheckIfExists.Parameters.Add("@Description", SqlDbType.VarChar).Value = name;
                SqlDataReader reader = commandCheckIfExists.ExecuteReader();
                if (reader.Read())//Jeżeli dana pozycja już istnieje to rzuć wyjątek.
                {
                    reader.Close();
                    throw new EntryExistsException();
                }
                reader.Close();
                SqlCommand command = new SqlCommand("INSERT INTO Position " +
                    "VALUES((SELECT MAX(Position_ID) + 1 FROM Position), @Name)", form.Connection, form.Transaction);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda pobierania tabeli zawierającej informacje o nowych pracownikach, którzy nie zostali jeszcze 
        /// poinformowanie o swoim loginie/haśle.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <returns>Zwraca tabelę zawierającą informacje o nowych pracownikach, którzy nie zostali jeszcze
        /// poinformowani o swoim loginie/haśle. Kolumny tabeli:
        /// "ID", "e-mail", "Login", "Password", "Name", "Surname"</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static DataTable GetUninformedEmployees(this FormAdmin form)
        {
            return GetUninformedEmployees((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierania tabeli zawierającej informacje o nowych pracownikach, którzy nie zostali jeszcze 
        /// poinformowanie o swoim loginie/haśle.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <returns>Zwraca tabelę zawierającą informacje o nowych pracownikach, którzy nie zostali jeszcze
        /// poinformowani o swoim loginie/haśle. Kolumny tabeli:
        /// "ID", "e-mail", "Login", "Password", "Name", "Surname"</returns>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static DataTable GetUninformedEmployees(LeaveManagerForm form)
        {
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'ID', E.EMail AS 'e-mail', E.Login, U.Password, E.Name, " +
                "E.Surname FROM Employee E, Uninformed U WHERE E.Employee_ID = U.Employee_ID", form.Connection);
            //Jeżeli formularz ma włączoną transakcję, to dołącz ją do zapytania.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Stworzenie tabeli na dane.
            DataTable uninformedEmployees = new DataTable();
            //Załadowanie danych do tabeli.
            uninformedEmployees.Load(reader);
            reader.Close();
            return uninformedEmployees;
        }

        /// <summary>
        /// Metoda testująca połączenie z bazą danych.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych.</param>
        /// <returns></returns>
        /// <remarks>Po zakończeniu działania metody połączenie jest zamknięte.</remarks>
        public static bool TestConnection(this SqlConnection connection)
        {
            connection.Close();
            //Próba wykonania prostego zapytania sql.
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Uninformed", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
                //Jeżeli się udało.
                return true;
            }
            catch (Exception)
            {
                connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Metoda oznaczająca nowego pracownika jako poinformowanego o swoim loginie/haśle.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika poinformowanego.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        public static void EmployeeInformed(this FormAdmin form, int employeeId)
        {
            EmployeeInformed((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda oznaczająca nowego pracownika jako poinformowanego o swoim loginie/haśle.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika poinformowanego.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        private static void EmployeeInformed(LeaveManagerForm form, int employeeId)
        {
            //Polecenie sql usuwające pracownika z listy nie poinformowanych pracowników.
            SqlCommand command = new SqlCommand("DELETE FROM Uninformed WHERE Employee_ID = @Employee_ID", form.Connection);
            //Jeżeli formularz ma rozpoczętą transakcję to podłącz ją do polecenia sql.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Metoda służąca do usuwania z bazy danych danego typu urlopu.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="replacementType">Nazwa typu urlopu, który będzie wpisany jako typ urlopów, które są typu usuwanego.</param>
        /// <param name="deletedType">Nazwa usuwanego typu urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Występuje w przypadku, gdy w bazie nie istnieje replacementType.</exception>
        public static void DeleteLeaveType(this FormDeleteLeaveType form, string replacementType, string deletedType)
        {
            DeleteLeaveType((LeaveManagerForm)form, replacementType, deletedType);
        }

        /// <summary>
        /// Metoda służąca do usuwania z bazy danych danego typu urlopu.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="replacementType">Nazwa typu urlopu, który będzie wpisany jako typ urlopów, które są typu usuwanego.</param>
        /// <param name="deletedType">Nazwa usuwanego typu urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Występuje w przypadku, gdy w bazie nie istnieje replacementType.</exception>
        private static void DeleteLeaveType(LeaveManagerForm form, string replacementType, string deletedType)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Zapytanie sql zczytujące id typu, który ma zastąpić usuwany typ.
                SqlCommand commandCheckReplaceType = new SqlCommand("SELECT LT_ID FROM Leave_type WHERE Name = @Name",
                    form.Connection, form.Transaction);
                commandCheckReplaceType.Parameters.Add("@Name", SqlDbType.VarChar).Value = replacementType;
                SqlDataReader reader = commandCheckReplaceType.ExecuteReader();
                if (reader.Read())//Sprawdzenie, czy typ, na który zamieniamy typ usuwany w ogóle istnieje.
                {
                    //Polecenie sql aktualizujące typy wszystkich urlopów, które są usuwanego typu.
                    SqlCommand commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = " +
                        "@ReplacementLT_ID " +
                        "WHERE LT_ID = " +
                        "(SELECT LT_ID FROM Leave_type WHERE Name = @Name_replaced)", form.Connection, form.Transaction);
                    commandUpdate.Parameters.Add("@ReplacementLT_ID", SqlDbType.Int).Value = (int)reader["LT_ID"];
                    reader.Close();
                    commandUpdate.Parameters.Add("@Name_replaced", SqlDbType.VarChar).Value = deletedType;
                    commandUpdate.ExecuteNonQuery();
                    //Polecenie sql usuwające typ do usunięcia.
                    SqlCommand commandDelete = new SqlCommand("DELETE FROM Leave_type WHERE " +
                        "Name = @Name", form.Connection, form.Transaction);
                    commandDelete.Parameters.Add("@Name", SqlDbType.VarChar).Value = deletedType;
                    commandDelete.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda służąca do usuwania z bazy danych danego typu urlopu.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="deletedType">Nazwa usuwanego typu urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        public static void DeleteLeaveType(this FormDeleteLeaveType form, LeaveType deletedType)
        {
            DeleteLeaveType((LeaveManagerForm)form, deletedType);
        }

        /// <summary>
        /// Metoda służąca do usuwania z bazy danych danego typu urlopu.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="deletedType">Nazwa usuwanego typu urlopu.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        private static void DeleteLeaveType(LeaveManagerForm form, LeaveType deletedType)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                SqlCommand commandGetLeavesToDelete = new SqlCommand("SELECT L.Leave_ID, L.Employee_ID, LS.Name AS 'Status', " +
                    "L.First_day, L.Last_day, L.Remarks, L.Used_days FROM Leave L, Status_type LS WHERE " +
                    "L.LS_ID = LS.ST_ID AND L.LT_ID = @LT_ID",
                    form.Connection, form.Transaction);
                commandGetLeavesToDelete.Parameters.AddWithValue("@LT_ID", deletedType.Id);
                SqlDataReader reader = commandGetLeavesToDelete.ExecuteReader();
                List<Leave> leaveEntriesToDelete = new List<Leave>();
                while (reader.Read())
                {
                    leaveEntriesToDelete.Add(new Leave((int)reader["Leave_ID"], (int)reader["Employee_ID"], deletedType.Name,
                       reader["Status"].ToString(), (DateTime)reader["First_day"],
                       (DateTime)reader["Last_day"], reader["Remarks"].ToString(), (int)reader["Used_days"]));
                }
                reader.Close();
                foreach (Leave leave in leaveEntriesToDelete)
                {
                    DeleteLeave(form, leave.Id);
                }
                SqlCommand commandDeleteType = new SqlCommand("DELETE FROM Leave_type WHERE LT_ID = @LT_ID",
                    form.Connection, form.Transaction);
                commandDeleteType.Parameters.AddWithValue("@LT_ID", deletedType.Id);
                commandDeleteType.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda służąca do usunięcia z bazy danych typu pozycji pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="replacementPosition">Pozycja, która zostanie przypisana wszystkim pracownikom, którzy zajmują pozycję usuwaną.</param>
        /// <param name="deletedPosition">Nazwa pozycji usuwanej.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Występuje w przypadku, gdy w bazie nie istnieje replacementPosition.</exception>
        public static void DeletePosition(this FormDeletePosition form, string replacementPosition, string deletedPosition)
        {
            DeletePosition((LeaveManagerForm)form, replacementPosition, deletedPosition);
        }

        /// <summary>
        /// Metoda służąca do usunięcia z bazy danych typu pozycji pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="replacementPosition">Pozycja, która zostanie przypisana wszystkim pracownikom, którzy zajmują pozycję usuwaną.</param>
        /// <param name="deletedPosition">Nazwa pozycji usuwanej.</param>
        /// <exception cref="SqlException">An exception occurred while executing the command against a locked row.</exception>
        /// <exception cref="InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="IsolationLevelException">Wyjątek występuje, gdy poziom izolacji uruchomionej w 
        /// formularzu transakcji jest zbyt niski do zapewnienia poprawnego wykonania poleceń metody.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Występuje w przypadku, gdy w bazie nie istnieje replacementPosition.</exception>
        private static void DeletePosition(LeaveManagerForm form, string replacementPosition, string deletedPosition)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                //Zapytanie sql zczytujące id pozycji, która ma zastąpić miejsce usuwanej.
                SqlCommand commandCheckReplacedPosition = new SqlCommand("SELECT Position_ID FROM " +
                    "Position WHERE Description = @Description", form.Connection, form.Transaction);
                commandCheckReplacedPosition.Parameters.Add("@Description", SqlDbType.VarChar).Value = replacementPosition;
                SqlDataReader reader = commandCheckReplacedPosition.ExecuteReader();
                //Sprawdzenie, czy zastępująca pozycja istnieje.
                if (reader.Read())
                {
                    //Polecenie aktualizacji pozycji wszystkich pracowników, którzy zajmują pozycję usuwaną.
                    SqlCommand commandUpdate = new SqlCommand("UPDATE Employee SET Position_ID = " +
                        "@ReplacementPosition_ID " +
                        "WHERE Position_ID = " +
                        "(SELECT Position_ID FROM Position WHERE Description = @Description_replaced)", form.Connection, form.Transaction);
                    commandUpdate.Parameters.Add("@ReplacementPosition_ID", SqlDbType.Int).Value = (int)reader["Position_ID"];
                    reader.Close();
                    commandUpdate.Parameters.Add("@Description_replaced", SqlDbType.VarChar).Value = deletedPosition;
                    commandUpdate.ExecuteNonQuery();
                    //Polecenie usunięcia rodzaju pozycji przeznaczonego do usunięcia.
                    SqlCommand commandDelete = new SqlCommand("DELETE FROM Position WHERE " +
                        "Description = @Description", form.Connection, form.Transaction);
                    commandDelete.Parameters.Add("@Description", SqlDbType.VarChar).Value = deletedPosition;
                    commandDelete.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        /// <summary>
        /// Metoda służaca do wyszukiwania pracownika. Rozszerza formularz asystentki.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę</param>
        /// <param name="name">Imię pracownika, który ma zostać wyszukany</param>
        /// <param name="surname">Nazwisko pracownika, który ma zostać wyszukany</param>
        /// <param name="pesel">Pesel pracownika, który ma zostać wyszukany</param>
        /// <param name="position">Pozycja pracownika, który ma zostać wyszukany</param>
        /// <returns>Tabele z danymi pracowników spełniających kryteria</returns>
        public static DataTable SearchEmployee(this FormAssistant form, String name, String surname, String pesel, String position)
        {
            return DatabaseOperator.SearchEmployee((LeaveManagerForm)form, name, surname, pesel, position);
        }

        /// <summary>
        /// Metoda służaca do wyszukiwania pracownika. Rozszerza formularz manadżera.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę</param>
        /// <param name="name">Imię pracownika, który ma zostać wyszukany</param>
        /// <param name="surname">Nazwisko pracownika, który ma zostać wyszukany</param>
        /// <param name="pesel">Pesel pracownika, który ma zostać wyszukany</param>
        /// <param name="position">Pozycja pracownika, który ma zostać wyszukany</param>
        /// <returns>Tabele z danymi pracowników spełniających kryteria</returns>
        public static DataTable SearchEmployee(this FormManager form, String name, String surname, String pesel, String position)
        {
            return DatabaseOperator.SearchEmployee((LeaveManagerForm)form, name, surname, pesel, position);
        }

        /// <summary>
        /// Metoda służaca do wyszukiwania pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę</param>
        /// <param name="name">Imię pracownika, który ma zostać wyszukany</param>
        /// <param name="surname">Nazwisko pracownika, który ma zostać wyszukany</param>
        /// <param name="pesel">Pesel pracownika, który ma zostać wyszukany</param>
        /// <param name="position">Pozycja pracownika, który ma zostać wyszukany</param>
        /// <returns>Tabele z danymi pracowników spełniających kryteria</returns>
        private static DataTable SearchEmployee(LeaveManagerForm form, String name, String surname, String pesel, String position)
        {
            //Zapytanie sql zczytujące dane pracownika o podanym loginie i haśle.
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', " +
                "E.Name, E.Surname, E.Birth_date AS 'Birth date'," +
               "E.Address, E.PESEL, E.EMail AS 'e-mail', Pos.Description AS 'Position', " +
               "Perm.Description AS 'Permission', E.Leave_days AS 'Remaining leave days', " +
               "E.Old_leave_days AS 'Old left leave days' " +
               "FROM Employee E, Position Pos, Permission Perm " +
               "WHERE E.Permission_ID = Perm.Permission_ID " +
               "AND E.Position_ID = Pos.Position_ID AND Name LIKE @Name AND " +
               "Surname LIKE @Surname AND PESEL LIKE @Pesel AND Pos.Description LIKE @Position", form.Connection);

            //Dodanie parametru imienia.
            name = "%" + name + "%";
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
            //Dodanie parametru nazwiska.
            surname = "%" + surname + "%";
            command.Parameters.Add("@Surname", SqlDbType.VarChar).Value = surname;
            //Dodania parametru peselu
            pesel = "%" + pesel + "%";
            command.Parameters.Add("@Pesel", SqlDbType.VarChar).Value = pesel;
            //Dodanie parametru pozycji.
            position = "%" + position + "%";
            command.Parameters.Add("@Position", SqlDbType.VarChar).Value = position;
            //Jeżeli formularz ma włączoną transakcję.
            if (form.TransactionOn)
                //Przypisanie do zapytania transakcji formularza.
                command.Transaction = form.Transaction;
            //Stworzenie obiektu wyniku.
            DataTable result = new DataTable();
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            //Jeżeli wyniki zapytania są nie puste.
            result.Load(reader);
            //Zamknięcie obiektu czytającego.
            reader.Close();
            //Zwrócenie wyniku.
            return result;
        }

        public static void EditEmployee(this FormAddOrEditEmployee form, Employee newEmployee)
        {
            EditEmployee((LeaveManagerForm)form, newEmployee);
        }

        private static void EditEmployee(LeaveManagerForm form, Employee newEmployee)
        {
            //Zapytanie SQL edytujące danego pracownika
            SqlCommand command = new SqlCommand("UPDATE Employee SET Permission_ID = @Permission, Position_ID = @Position"
                + ", Name = @Name, Surname = @Surname, Birth_date = @Birth_date, Address = @Address, Pesel = @Pesel"
                + ", EMail = @Email, Year_leave_days = @Year_leave_days WHERE Employee_ID = @Employee_ID", form.Connection);
            //Ustawienie parametrów zapytania
            command.Parameters.Add("@Permission", SqlDbType.Int).Value = GetPermissionID(form, newEmployee.Permission);
            command.Parameters.Add("@Position", SqlDbType.Int).Value = GetPositionID(form, newEmployee.Position);
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = newEmployee.Name;
            command.Parameters.Add("@Surname", SqlDbType.VarChar).Value = newEmployee.Surname;
            command.Parameters.Add("@Birth_date", SqlDbType.Date).Value = newEmployee.BirthDate;
            command.Parameters.Add("@Address", SqlDbType.VarChar).Value = newEmployee.Address;
            command.Parameters.Add("@Pesel", SqlDbType.VarChar).Value = newEmployee.Pesel;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = newEmployee.EMail;
            command.Parameters.Add("@Year_leave_days", SqlDbType.Int).Value = newEmployee.YearLeaveDays;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = newEmployee.EmployeeId;
            if (form.TransactionOn)
                //Przypisanie do zapytania transakcji formularza.
                command.Transaction = form.Transaction;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Metoda zwraca ID uprawnien o podanym opisie
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="permission">Opis uprawnień</param>
        /// <returns></returns>
        private static int GetPermissionID(LeaveManagerForm form, string permission)
        {
            SqlCommand command = new SqlCommand("SELECT Permission_ID FROM Permission WHERE Description" +
                " = @Description", form.Connection);
            command.Parameters.Add("@Description", SqlDbType.VarChar).Value = permission;
            if (form.TransactionOn)
                //Przypisanie do zapytania transakcji formularza.
                command.Transaction = form.Transaction;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int result = (int)reader["Permission_ID"];
                reader.Close();
                return result;
            }
            reader.Close();
            throw new PermissionException();
        }

        /// <summary>
        /// Metoda zwraca identyfikator pozycji o podanym opisie
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="position">Opis pozycji</param>
        /// <returns></returns>
        private static int GetPositionID(LeaveManagerForm form, string position)
        {
            SqlCommand command = new SqlCommand("SELECT Position_ID FROM Position WHERE Description" +
                " = @Description", form.Connection);
            command.Parameters.Add("@Description", SqlDbType.VarChar).Value = position;
            if (form.TransactionOn)
                //Przypisanie do zapytania transakcji formularza.
                command.Transaction = form.Transaction;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int result = (int)reader["Position_ID"];
                reader.Close();
                return result;
            }
            reader.Close();
            throw new PermissionException();
        }

        /// <summary>
        /// Metoda ustawiania godzin pracy pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="hours">Tablica 14 elementów zawierająca godziny rozpoczęcia i zakończenia pracy
        /// w kolejnych dniach tygodnia. Np. hours[0] == godzina rozpoczęcia pracy w poniedziałek, 
        /// hours[1] == godzina zakończenia pracy w poniedziałek,
        /// hours[2] == godzina rozpoczęcia pracy we wtorek itd.
        /// 
        /// Format godziny to dd:mm:[ss[.nnnnnnn]]</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmieniamy harmonogram.</param>
        public static void SetSchedule(this FormWorkSchedule form, string[] hours, int employeeId)
        {
            SetSchedule((LeaveManagerForm)form, hours, employeeId);
        }

        /// <summary>
        /// Metoda ustawiania godzin pracy pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="hours">Tablica 14 elementów zawierająca godziny rozpoczęcia i zakończenia pracy
        /// w kolejnych dniach tygodnia. Np. hours[0] == godzina rozpoczęcia pracy w poniedziałek, 
        /// hours[1] == godzina zakończenia pracy w poniedziałek,
        /// hours[2] == godzina rozpoczęcia pracy we wtorek itd.
        /// 
        /// Format godziny to dd:mm:[ss[.nnnnnnn]]</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmieniamy harmonogram.</param>
        private static void SetSchedule(LeaveManagerForm form, string[] hours, int employeeId)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
              * o odpowiednim poziomie izolacji.
              */

            //Zmienna przechowująca stan transakcji przed uruchomieniem metody.
            bool isFormTransactionOn = form.TransactionOn;
            //Jeżeli formularz posiada uruchomioną transakcję.
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new IsolationLevelException();
                }
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Uruchomienie nowej transakcji na potrzeby tej metody z odpowiednim poziomem izolacji.
                form.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                if (hours.Length != 14)
                {
                    throw new ArgumentException();
                }
                using (SqlCommand command = new SqlCommand("UPDATE Work_hours " +
                    "SET " +
                    "MondayStart = @MondayStart, " +
                    "MondayEnd = @MondayEnd, " +

                    "TuesdayStart = @TuesdayStart, " +
                    "TuesdayEnd = @TuesdayEnd, " +

                    "WednesdayStart = @WednesdayStart, " +
                    "WednesdayEnd = @WednesdayEnd, " +

                    "ThursdayStart = @ThursdayStart, " +
                    "ThursdayEnd = @ThursdayEnd, " +

                    "FridayStart = @FridayStart, " +
                    "FridayEnd = @FridayEnd, " +

                    "SaturdayStart = @SaturdayStart, " +
                    "SaturdayEnd = @SaturdayEnd, " +

                    "SundayStart = @SundayStart, " +
                    "SundayEnd = @SundayEnd " +
                    "WHERE Employee_ID = @Employee_ID", form.Connection, form.Transaction))
                {
                    command.Parameters.AddWithValue("@MondayStart", hours[0]);
                    command.Parameters.AddWithValue("@MondayEnd", hours[1]);

                    command.Parameters.AddWithValue("@TuesdayStart", hours[2]);
                    command.Parameters.AddWithValue("@TuesdayEnd", hours[3]);

                    command.Parameters.AddWithValue("@WednesdayStart", hours[4]);
                    command.Parameters.AddWithValue("@WednesdayEnd", hours[5]);

                    command.Parameters.AddWithValue("@ThursdayStart", hours[6]);
                    command.Parameters.AddWithValue("@ThursdayEnd", hours[7]);

                    command.Parameters.AddWithValue("@FridayStart", hours[8]);
                    command.Parameters.AddWithValue("@FridayEnd", hours[9]);

                    command.Parameters.AddWithValue("@SaturdayStart", hours[10]);
                    command.Parameters.AddWithValue("@SaturdayEnd", hours[11]);

                    command.Parameters.AddWithValue("@SundayStart", hours[12]);
                    command.Parameters.AddWithValue("@SundayEnd", hours[13]);

                    command.Parameters.AddWithValue("@Employee_ID", employeeId);

                    command.ExecuteNonQuery();
                }
                int daysToAdd = 0;
                DataTable leaves = GetLeaves(form, employeeId);
                for (int i = 0; i < leaves.Rows.Count; ++i)
                {
                    //Jeżeli urlop zaczyna się później niż teraz.
                    if ((GetServerTimeNow(form).CompareTo((DateTime)leaves.Rows[i].ItemArray.GetValue(2)) < 0)
                        //Jeżeli urlop konsumuje dni.
                        && ConsumesDays(form, leaves.Rows[i].ItemArray.GetValue(4).ToString()))
                    {
                        int numberOfUsedDays = GetNumberOfWorkDays(form, (DateTime)leaves.Rows[i].ItemArray.GetValue(2),
                            (DateTime)leaves.Rows[i].ItemArray.GetValue(3), employeeId);
                        if ((int)leaves.Rows[i].ItemArray.GetValue(6) != numberOfUsedDays)
                        {
                            daysToAdd += ((int)leaves.Rows[i].ItemArray.GetValue(6)) - numberOfUsedDays;
                            using (SqlCommand command = new SqlCommand("UPDATE Leave SET Used_days = @Used_days WHERE Leave_ID = @Leave_ID", form.Connection, form.Transaction))
                            {
                                command.Parameters.AddWithValue("@Leave_ID", leaves.Rows[i].ItemArray.GetValue(0));
                                command.Parameters.AddWithValue("@Used_days", numberOfUsedDays);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                AddLeaveDays(form, employeeId, daysToAdd);
            }
            catch (Exception e)
            {
                //Jeżeli transakcja formularza była uruchomiona tylko na potrzeby tej metody, to ją cofamy.
                if (!isFormTransactionOn)
                    form.RollbackTransaction();
                //Wyrzucamy wyjątek do dalszej obsługi.
                throw e;
            }
            //Jeżeli operacja powiodła się, a transakcja była uruchomiona tylko na potrzeby tej metody to ją zatwierdzamy.
            if (!isFormTransactionOn)
                form.CommitTransaction();
        }

        public static DataTable GetSchedule(this FormWorkSchedule form, int employeeId)
        {
            return GetSchedule((LeaveManagerForm)form, employeeId);
        }

        private static DataTable GetSchedule(LeaveManagerForm form, int employeeId)
        {
            using (SqlCommand command = new SqlCommand("SELECT MondayStart, MondayEnd, TuesdayStart, TuesdayEnd" +
                ", WednesdayStart, WednesdayEnd, ThursdayStart, ThursdayEnd, FridayStart, FridayEnd" +
                ", SaturdayStart, SaturdayEnd, SundayStart, SundayEnd FROM Work_hours WHERE Employee_ID = @Employee_ID",
                form.Connection))
            {
                if (form.TransactionOn)
                {
                    command.Transaction = form.Transaction;
                }
                command.Parameters.AddWithValue("@Employee_ID", employeeId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }
        }

        private static bool[] GetWorkingDaysOfWeek(LeaveManagerForm form, int employeeId)
        {
            bool[] days = new bool[7];
            DataTable schedule = GetSchedule(form, employeeId);
            for (int i = 0; i < 7; ++i)
            {
                if (!schedule.Rows[0].ItemArray.GetValue(2 * i).ToString().Equals(schedule.Rows[0].ItemArray.GetValue((2 * i) + 1).ToString()))
                {
                    days[i] = true;
                }
                else
                {
                    days[i] = false;
                }
            }
            return days;
        }
    }
}
