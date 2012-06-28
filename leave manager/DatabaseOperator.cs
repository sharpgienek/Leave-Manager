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
        /// Metoda logowania rozszerzająca formularz logowania.
        /// </summary>
        /// <param name="form">Formularz logowania.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Hasło.</param>
        /// <returns>Zwraca obiekt reprezentujący zalogowanego pracownika.</returns>
        public static Employee login(this FormLogin form, String login, String password)
        {
            return DatabaseOperator.login((LeaveManagerForm)form, login, password);
        }

        /// <summary>
        /// Metoda logowania.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Hasło.</param>
        /// <returns>Zwraca obiekt reprezentujący zalogowanego pracownika.</returns>
        private static Employee login(LeaveManagerForm form, String login, String password)
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
                throw new WrongLoginOrPasswordException();
            }
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typy urlopów rozszerzająca formularz administratora.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca tabelę zawierającą informacje o typach urlopów posortowane po numerze id.
        /// Zwracana tabela zawiera następujące kolumny: 
        /// "Leave type id", "Name", "Consumes days". </returns>
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
        public static List<LeaveType> GetLeaveTypesList(this FormLeaveApplication form)
        {
            return GetLeaveTypesList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typów urlopów do listy. Rozszerza formularz usuwania typu urlopu.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę typów urlopów.</returns>
        public static List<LeaveType> GetLeaveTypesList(this FormDeleteLeaveType form)
        {
            return GetLeaveTypesList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierania z bazy danych typów urlopów do listy.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę typów urlopów.</returns>
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
        public static List<String> GetPermissions(this FormAddEmployee form)
        {
            return GetPermissions((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobrania listy uprawnień.
        /// </summary>
        /// <param name="form">Formularz z którego została wywołana metoda.</param>
        /// <returns>Zwraca listę ciągów znaków reprezentujących poziomy uprawnień.</returns>
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
        public static List<String> GetPositionsList(this FormDeletePosition form)
        {
            return GetPositionsList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników. Rozszerza formularz dodawania pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
        public static List<String> GetPositionsList(this FormAddEmployee form)
        {
            return GetPositionsList((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych pozycji pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca listę możliwych pozycji pracowników..</returns>
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
        public static List<String> GetStatusTypes(this FormLeaveApplication form)
        {
            return GetStatusTypes((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca listę możliwych stanów zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <returns>Lista możliwych stanów zgłoszenia (same nazwy).</returns>
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
        public static void AddEmployee(this FormAddEmployee form, Employee employee)
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
        private static void AddEmployee(LeaveManagerForm form, Employee employee)
        {
            /* Dla poprawnego działania tej metody konieczne jest aby posiadała ona transakcję
             * o odpowiednim poziomie izolacji.
             */
            
            //Jeżeli formularz posiada uruchomioną transakcję.
            SqlTransaction transaction;
            if (form.TransactionOn)
            {
                //Sprawdzenie, czy poziom izolacji istniejącej transakcji jest wystarczający.
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new WrongIsolationLevelException();
                }
                //Przypisanie istniejącej transakcji do transakcji tej metody.
                transaction = form.Transaction;
            }
            else//Jeżeli formularz nie posiada uruchomionej transakcji.
                //Stworzenie nowej transakcji na potrzeby tej metody.
                transaction = form.Connection.BeginTransaction(IsolationLevel.RepeatableRead);
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
                    form.Connection, transaction);
                //Stworzenie obiektu służącego do czytania wyników zapytania.
                SqlDataReader reader;
                //Pętla szukająca wolnego loginu składającego się z 7 cyfr.
               for(int i = 1000000; i <= 10000000; ++i)
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
                  "(SELECT Permission_ID FROM Permission WHERE Description = @Permission_Description), (SELECT Position_ID FROM Position WHERE Description = @Position_Description), @Login, @Password, @Name, @Surname, @Birth_date, @Address," +
                  "@PESEL, @EMail, @Year_leave_days, @Leave_days, 0)", form.Connection, transaction);
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
                    "VALUES ((SELECT MAX(Employee_ID) FROM Employee), @Password)", form.Connection, transaction);
                commandInsertUninformed.Parameters.Add("@Password", SqlDbType.VarChar).Value = password.ToString();
                commandInsertUninformed.ExecuteNonQuery();
                //Jeżeli transakcja nie pochodzi z formularza to następuje jej zatwierdzenie.
                if (!form.TransactionOn)
                    transaction.Commit();
            }
            catch (Exception e)
            {
                //Jeżeli wystąpił błąd i transakcja nie pochodzi z formularza, to następuje jej cofnięcie.
                if (!form.TransactionOn)
                    transaction.Rollback();
                //Rzucenie wyjątku do obsługi poza metodą.
                throw e;
            }
        }

        /// <summary>
        /// Metoda pobierająca tabelę ze zgłoszeniami urlopowymi wymagającymi zatwierdzenia/odrzucenia.
        /// Rozszerza formularz kierownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę zawierającą zgłoszenia urlopowe wymagające zatwierdzenia/odrzucenia.
        /// Tabela zawiera kolumny: "Employee id", "Position", "Name", "Surname", "e-mail", "Type",
        /// "First day", "Last day", "No. used work days".</returns>
        public static DataTable getNeedsAction(this FormManager form)
        {
            return getNeedsAction((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca tabelę ze zgłoszeniami urlopowymi wymagającymi zatwierdzenia/odrzucenia.
        /// Rozszerza formularz rejestratorki.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę zawierającą zgłoszenia urlopowe wymagające zatwierdzenia/odrzucenia.
        /// Tabela zawiera kolumny: "Employee id", "Position", "Name", "Surname", "e-mail", "Type",
        /// "First day", "Last day", "No. used work days".</returns>
        public static DataTable getNeedsAction(this FormAssistant form)
        {
            return getNeedsAction((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobierająca tabelę ze zgłoszeniami urlopowymi (i ich właścicielami) wymagającymi zatwierdzenia/odrzucenia
        /// różnego typu w zależności od tego, przej jaki typ formularza została wywołana.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Zwraca tabelę zawierającą zgłoszenia urlopowe wymagające zatwierdzenia/odrzucenia.
        /// Tabela zawiera kolumny: "Employee id", "Position", "Name", "Surname", "e-mail", "Type",
        /// "First day", "Last day", "No. used work days".</returns>
        private static DataTable getNeedsAction(LeaveManagerForm form)
        {
            //Zapytanie sql zczytujące zgłoszenia wymagające zatwierdzenia/odrzucenia.
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', P.Description AS 'Position', " +
                "E.Name, E.Surname, E.EMail AS 'e-mail', LT.Name AS 'Type', " +
                "L.First_day AS 'First day', L.Last_day AS 'Last day' FROM Employee E, " +
                "Leave L, Leave_type LT, Position P, Status_type LS WHERE L.Employee_ID = E.Employee_ID " +
                "AND L.LT_ID = LT.LT_ID AND P.Position_ID = E.Position_ID AND LS.ST_ID = L.LS_ID AND LS.Name = @Name ORDER BY L.First_day", form.Connection);
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
                    //Unreachable.
                }
            }
            //Stworzenie obiektu czytającego wyniki zapytania.
            SqlDataReader reader = command.ExecuteReader();
            DataTable Result = new DataTable();
            //Zczytanie wyników zapytania do tabeli.
            Result.Load(reader);
            reader.Close();
            //Dodanie kolumny zawierającej informację o liczbie używanych dni przez urlop.
            Result.Columns.Add("No. used work days");
            //Obliczenie i wpisanie liczby używanych dni przez każdy z urlopów.
            for (int i = 0; i < Result.Rows.Count; i++)
            {
                Result.Rows[i]["No. used work days"] = TimeTools.GetNumberOfWorkDays(
                    (DateTime)Result.Rows[i]["First day"], (DateTime)Result.Rows[i]["Last day"]);
            }
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
        public static DataTable getEmployees(this FormManager form)
        {
            return getEmployees((LeaveManagerForm)form);
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
        public static DataTable getEmployees(this FormAssistant form)
        {
            return getEmployees((LeaveManagerForm)form);
        }

        /// <summary>
        /// Metoda pobrania tabeli z danymi pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <returns>Tabelę z danymi pracowników. Zawiera następujące kolumny:
        /// "Employee id", "Name", "Surname", "Birth date", "Address",
        /// "PESEL", "e-mail", "Position", "Permission", "Remaining leave days",
        /// "Old leave days".</returns>
        private static DataTable getEmployees(LeaveManagerForm form)
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
        public static Employee getEmployee(this FormLogin form, int employeeId)
        {
            return getEmployee((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda zwracająca obiekt reprezentujący dane pracownika.
        /// Rozszerza formularz rozważenia zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pobieranego pracownika.</param>
        /// <returns>Obiekt pracownika, którego numer id zgadza się z argumentem.</returns>
        public static Employee getEmployee(this FormLeaveConsideration form, int employeeId)
        {
            return getEmployee((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda zwracająca obiekt reprezentujący dane pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pobieranego pracownika.</param>
        /// <returns>Obiekt pracownika, którego numer id zgadza się z argumentem.</returns>
        private static Employee getEmployee(LeaveManagerForm form, int employeeId)
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
                throw new WrongEmployeeIdException();
            }
        }

        /// <summary>
        /// Metoda akceptacji zgłoszenia urlopowego.
        /// Rozszerza formularz rozważania zgłoszenia.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie.</param>
        /// <param name="firstDay">Dzień rozpoczęcia urlopu.</param>
        public static void acceptLeave(this FormLeaveConsideration form, int employeeId, DateTime firstDay)
        {            
            //Polecenie sql które zmienia stan akceptowanego zgłoszenia.
            SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                "AND First_day = @First_day ", form.Connection);
            //Jeżeli formularz ma uruchomioną transakcję, dodaj do niej polecenie.
            if (form.TransactionOn)
                commandUpdateLeave.Transaction = form.Transaction;
            commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            //Ustalenie nowego stanu zgłoszenia w zależności od typu formularza rodzica(poziomu uprawnień)
            if (form.LeaveManagerParentForm.GetType() == new FormAssistant().GetType())
            {
                commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
            }
            else
            {
                if (form.LeaveManagerParentForm.GetType() ==new FormManager().GetType())
                    commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Approved";
                else
                    throw new ArgumentException();
            }
            commandUpdateLeave.ExecuteNonQuery();
        }
        

        public static void rejectLeave(this FormLeaveConsideration form, int employeeId, DateTime firstDay)
        {
            rejectLeave((LeaveManagerForm)form, employeeId, firstDay);
        }

        private static void rejectLeave(LeaveManagerForm form, int employeeId, DateTime firstDay)
        {
            SqlCommand command = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                "AND First_day = @First_day ", form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Rejected";
            command.ExecuteNonQuery();
        }

        public static void getDays(this FormEmployee form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        public static void getDays(this FormEmployeeData form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        public static void getDays(this FormLeaveApplication form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        private static void getDays(LeaveManagerForm form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee " +
                "WHERE Employee_ID = @Employee_ID", form.Connection);
            if (form.TransactionOn)
                commandSelectDays.Transaction = form.Transaction;
            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader readerDays = commandSelectDays.ExecuteReader();
            readerDays.Read();//todo zabezpiecz przed błędami.*/
            leaveDays = (int)readerDays["Leave_days"];
            oldLeaveDays = (int)readerDays["Old_leave_days"];
            readerDays.Close();
        }

        public static void getDays(this FormLeaveApplication form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
        }

        public static void getDays(this FormEmployeeData form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
        }

        private static void getDays(LeaveManagerForm form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days, Year_leave_days FROM Employee " +
                "WHERE Employee_ID = @Employee_ID", form.Connection);
            if (form.TransactionOn)
                commandSelectDays.Transaction = form.Transaction;
            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader readerDays = commandSelectDays.ExecuteReader();
            readerDays.Read();//todo zabezpiecz przed błędami.*/
            leaveDays = (int)readerDays["Leave_days"];
            oldLeaveDays = (int)readerDays["Old_leave_days"];
            yearDays = (int)readerDays["Year_leave_days"];
            readerDays.Close();
        }

        public static DataTable getLeaves(this FormEmployee form, int employeeId)
        {
            return getLeaves((LeaveManagerForm)form, employeeId);
        }

        public static DataTable getLeaves(this FormEmployeeData form, int employeeId)
        {
            return getLeaves((LeaveManagerForm)form, employeeId);
        }

        private static DataTable getLeaves(LeaveManagerForm form, int employeeId)
        {

            SqlCommand commandSelectLeaves = new SqlCommand("SELECT LS.Name AS 'Status', L.First_day AS 'First day', " +
                        "L.Last_day AS 'Last day', LT.Name AS 'Type', LT.Consumes_days, L.Remarks " +
                        "FROM Employee E, Leave L, Leave_type LT, Status_type LS " +
                        "WHERE L.LT_ID = LT.LT_ID AND L.LS_ID = LS.ST_ID AND E.Employee_ID = L.Employee_ID " +
                        "AND E.Employee_ID = @Employee_ID ORDER BY L.First_day", form.Connection);
            if (form.TransactionOn)
                commandSelectLeaves.Transaction = form.Transaction;
            commandSelectLeaves.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader readerLeaves = commandSelectLeaves.ExecuteReader();
            DataTable leaves = new DataTable();
            leaves.Load(readerLeaves);
            readerLeaves.Close();
            leaves.Columns.Add("No. used days");
            int usedDays;
            for (int i = 0; i < leaves.Rows.Count; i++)
            {
                if ((bool)leaves.Rows[i]["Consumes_days"])
                {
                    leaves.Rows[i]["No. used days"] = usedDays = TimeTools.GetNumberOfWorkDays((DateTime)leaves.Rows[i]["First day"],
                            (DateTime)leaves.Rows[i]["Last day"]);
                }
                else
                    leaves.Rows[i]["No. used days"] = 0;
            }
            leaves.Columns.Remove("Consumes_days");
            return leaves;
        }

        public static bool IsDateFromPeriodUsed(this FormLeaveApplication form, DateTime date1, DateTime date2, int employeeID)
        {
            return IsDateFromPeriodUsed((LeaveManagerForm)form, date1, date2, employeeID);
        }

        private static bool IsDateFromPeriodUsed(LeaveManagerForm form, DateTime date1, DateTime date2, int employeeID)
        {
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID", form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
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
            reader.Close();
            return false;
        }

        public static bool IsDateFromPeriodUsed(this FormLeaveApplication form, DateTime date1, DateTime date2,
           int employeeID, DateTime skippedEntryFirstDay)
        {
            return IsDateFromPeriodUsed((LeaveManagerForm)form, date1, date2, employeeID, skippedEntryFirstDay);
        }

        private static bool IsDateFromPeriodUsed(LeaveManagerForm form, DateTime date1, DateTime date2,
            int employeeID, DateTime skippedEntryFirstDay)
        {
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID AND First_day != @Skipped_entry_first_day", form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeID;
            command.Parameters.Add("@Skipped_entry_first_day", SqlDbType.Date).Value = skippedEntryFirstDay;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
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
            reader.Close();
            return false;
        }

        public static void AddLeaveDays(this FormLeaveApplication form, int employeeId, int number)
        {
            AddLeaveDays((LeaveManagerForm)form, employeeId, number);
        }

        private static void AddLeaveDays(LeaveManagerForm form, int employeeId, int number)
        {
            int leaveDays = 0;
            int oldLeaveDays = 0;
            int yearDays = 0;
            getDays(form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
            AddLeaveDays(form, employeeId, number, leaveDays, oldLeaveDays, yearDays);
        }

        public static void AddLeaveDays(this FormLeaveApplication form, int employeeId, int number, int leaveDays, int oldLeaveDays, int yearDays)
        {
            AddLeaveDays((LeaveManagerForm)form, employeeId, number, leaveDays, oldLeaveDays, yearDays);
        }

        private static void AddLeaveDays(LeaveManagerForm form, int employeeId, int number, int leaveDays, int oldLeaveDays, int yearDays)
        {
            SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                    "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                    "WHERE Employee_ID = @Employee_ID", form.Connection);
            if (form.TransactionOn)
                commandUpdateEmployee.Transaction = form.Transaction;
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
            commandUpdateEmployee.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            commandUpdateEmployee.ExecuteNonQuery();
        }

        private static bool ConsumesDays(LeaveManagerForm form, String leaveTypeName)
        {
            SqlCommand commandSelectConsumesDays = new SqlCommand("SELECT Consumes_days FROM  " +
                "Leave_type WHERE Name = @Name", form.Connection);
            if (form.TransactionOn)
                commandSelectConsumesDays.Transaction = form.Transaction;
            commandSelectConsumesDays.Parameters.Add("@Name", SqlDbType.VarChar).Value = leaveTypeName;
            SqlDataReader readerConsumesDays = commandSelectConsumesDays.ExecuteReader();
            readerConsumesDays.Read();//todo try catch
            if ((bool)readerConsumesDays["Consumes_days"])
            {
                readerConsumesDays.Close();
                return true;
            }
            else
            {
                readerConsumesDays.Close();
                return false;
            }
        }
        //todo tabela z dniami wolnymi od pracy
        private static int GetNumberOfWorkDays(LeaveManagerForm form, DateTime date1, DateTime date2)
        {
            TimeSpan timeSpan = date1 - date2;
            if (timeSpan.TotalDays < 0)
                timeSpan = timeSpan.Negate();
            int numberOfDays = (int)Math.Round(timeSpan.TotalDays) + 1;
            while (date1.CompareTo(date2) <= 0)
            {
                if (date1.DayOfWeek == DayOfWeek.Saturday || date1.DayOfWeek == DayOfWeek.Sunday)
                    numberOfDays--;
                date1 = date1.AddDays(1);
            }
            return numberOfDays;
        }

        public static void EditLeave(this FormManager form, Leave leave, DateTime oldFirstDay)
        {
            EditLeave((LeaveManagerForm)form, leave, oldFirstDay);
        }

        public static void EditLeave(this FormAssistant form, Leave leave, DateTime oldFirstDay)
        {
            EditLeave((LeaveManagerForm)form, leave, oldFirstDay);
        }

        private static void EditLeave(LeaveManagerForm form, Leave leave, DateTime oldFirstDay)
        {
            SqlCommand commandUpdate;
            commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = (SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), LS_ID = " +
                "(SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                "First_day = @First_day, Last_day = @Last_day, Remarks = @Remarks " +
                "WHERE Employee_ID = @Employee_ID AND First_day = @Old_first_day", form.Connection);
            if (form.TransactionOn)
                commandUpdate.Transaction = form.Transaction;
            commandUpdate.Parameters.Add("@Old_first_day", SqlDbType.Date).Value = oldFirstDay;

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
            if (ConsumesDays(form, leave.LeaveType))
            {
                AddLeaveDays(form, leave.EmployeeId, -GetNumberOfWorkDays(form, leave.FirstDay, leave.LastDay));
            }
        }

        public static void addLeave(this FormLeaveApplication form, Leave leave)
        {
            addLeave((LeaveManagerForm)form, leave);
        }

        private static void addLeave(LeaveManagerForm form, Leave leave)
        {
            SqlCommand commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Employee_ID, " +
                 "(SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), (SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                 "@First_day, @Last_day, @Remarks)", form.Connection);
            if (form.TransactionOn)
                commandInsertLeave.Transaction = form.Transaction;
            if (leave.LeaveType.Equals("Sick"))
            {
                commandInsertLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Approved";
            }
            else
            {
                commandInsertLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = leave.LeaveStatus;
            }
            commandInsertLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
            commandInsertLeave.Parameters.Add("@Leave_type_name", SqlDbType.VarChar).Value = leave.LeaveType;
            commandInsertLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = leave.FirstDay;
            commandInsertLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.LastDay;
            commandInsertLeave.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = leave.Remarks;
            commandInsertLeave.ExecuteNonQuery();

            if (ConsumesDays(form, leave.LeaveType))
            {
                GetNumberOfWorkDays(form, leave.FirstDay, leave.LastDay);
                AddLeaveDays(form, leave.EmployeeId, 6);
            }
        }

        public static void addSickLeave(this FormLeaveApplication form, Leave leave, ref int leaveDays, ref int oldLeaveDays, int yearDays)
        {
            addSickLeave((LeaveManagerForm)form, leave, ref leaveDays, ref oldLeaveDays, yearDays);
        }

        private static void addSickLeave(LeaveManagerForm form, Leave leave, ref int leaveDays, ref int oldLeaveDays, int yearDays)
        {
            DataTable dataLeaves = getLeaves(form, leave.EmployeeId);

            int returnedLeaveDays = 0;
            foreach (DataRow row in dataLeaves.Rows)
            {
                //pierwszy dzień sprawdzanego urlopu jest później lub ten sam, co pierwszy dzień chorobowego
                if ((((DateTime)row.ItemArray.GetValue(1)).CompareTo(leave.FirstDay) >= 0)
                    //i jest wcześniej lub taki sam jak ostatni dzień chorobowego.
                && (((DateTime)row.ItemArray.GetValue(1)).CompareTo(leave.LastDay) <= 0))
                {
                    if (row.ItemArray.GetValue(3).ToString().Equals("Sick"))
                        throw new EntryExistsException();
                    SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                       "LS_ID = (SELECT ST_ID FROM Status_type WHERE Name = @Status_name) " +
                       "WHERE Employee_ID = @Employee_ID AND First_day = @First_day", form.Connection);
                    if (form.TransactionOn)
                        commandUpdateLeave.Transaction = form.Transaction;
                    commandUpdateLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Canceled";
                    commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                    commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(1);
                    commandUpdateLeave.ExecuteNonQuery();
                    returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(1)).GetNumberOfWorkDays((DateTime)row.ItemArray.GetValue(2));
                    continue;
                }

                if ((leave.FirstDay.CompareTo((DateTime)row.ItemArray.GetValue(1)) >= 0)//Sick first day later than leave first day 
                && (leave.FirstDay.CompareTo((DateTime)row.ItemArray.GetValue(2)) <= 0))//and earlier than leave last day.
                {
                    SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                        "Last_day = @Last_day WHERE Employee_ID = @Employee_ID AND First_day = @First_day", form.Connection);
                    if (form.TransactionOn)
                        commandUpdateLeave.Transaction = form.Transaction;
                    commandUpdateLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.FirstDay.AddDays(-1);
                    commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                    commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(1);
                    commandUpdateLeave.ExecuteNonQuery();
                    returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(0)).GetNumberOfWorkDays(leave.FirstDay.AddDays(-1));
                    continue;
                }
            }
            //todo transakcja.
            AddLeaveDays(form, leave.EmployeeId, returnedLeaveDays, leaveDays, oldLeaveDays, yearDays);
            addLeave(form, leave);
        }

      
        public static void ChangeLoginOrPassword(this FormChangeLoginOrPassword form, int employeeId, String oldPassword, String newPassword, String newLogin)
        {
            ChangeLoginOrPassword((LeaveManagerForm)form, employeeId, oldPassword, newPassword, newLogin);
        }

        private static void ChangeLoginOrPassword(LeaveManagerForm form, int employeeId, String oldPassword, String newPassword, String newLogin)
        {
            SqlCommand command = new SqlCommand("SELECT Employee_ID FROM Employee WHERE " +
                "Employee_ID = @Employee_ID AND Password = @Password", form.Connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(oldPassword);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                if (newLogin.Length != 0 && newPassword.Length != 0)
                {
                    command.CommandText = "UPDATE Employee SET Login = @Login, " +
                        "Password = @Password WHERE Employee_ID = @Employee_ID";
                    command.Parameters.Clear();
                    command.Parameters.Add("@Login", SqlDbType.VarChar).Value = newLogin;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(newPassword);
                    command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                    command.ExecuteNonQuery();//todo try catch
                }
                else
                {
                    if (newLogin.Length != 0)
                    {
                        command.CommandText = "UPDATE Employee SET Login = @Login WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Login", SqlDbType.VarChar).Value = newLogin;
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery(); //todo try catch
                    }

                    if (newPassword.Length != 0)
                    {
                        command.CommandText = "UPDATE Employee SET Password = @Password WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(newPassword);
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery(); //todo try catch
                    }
                }
            }
            else
            {
                reader.Close();
                throw new WrongPasswordException();
            }
        }

        public static void DeleteLeave(this FormManager form, int employeeId, DateTime firstDay, DateTime lastDay)
        {
            DeleteLeave((LeaveManagerForm)form, employeeId, firstDay, lastDay);
        }

        public static void DeleteLeave(this FormAssistant form, int employeeId, DateTime firstDay, DateTime lastDay)
        {
            DeleteLeave((LeaveManagerForm)form, employeeId, firstDay, lastDay);
        }

        public static void DeleteLeave(this FormEmployee form, int employeeId, DateTime firstDay, DateTime lastDay)
        {
            DeleteLeave((LeaveManagerForm)form, employeeId, firstDay, lastDay);
        }

        private static void DeleteLeave(LeaveManagerForm form, int employeeId, DateTime firstDay, DateTime lastDay)
        {
            SqlTransaction transaction;
            if (form.TransactionOn)
            {
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new WrongIsolationLevelException();
                }
                transaction = form.Transaction;
            }
            else
                transaction = form.Connection.BeginTransaction();
            try
            {
                SqlCommand commandDeleteLeave = new SqlCommand("DELETE FROM Leave WHERE " +
                     "Employee_ID = @Employee_ID AND First_day = @First_day", form.Connection, transaction);
                commandDeleteLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                commandDeleteLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
                commandDeleteLeave.ExecuteNonQuery();

                SqlCommand commandReadDays = new SqlCommand("SELECT Year_leave_days, Leave_days, Old_leave_days " +
                    "FROM Employee WHERE Employee_ID = @Employee_ID", form.Connection, transaction);
                commandReadDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                SqlDataReader readerDays = commandReadDays.ExecuteReader();
                readerDays.Read();
                SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                    "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                    "WHERE Employee_ID = @Employee_ID", form.Connection, transaction);
                int returnedLeaveDays = firstDay.GetNumberOfWorkDays(lastDay);
                if ((int)readerDays["Leave_days"] + returnedLeaveDays > (int)readerDays["Year_leave_days"])
                {
                    commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = (int)readerDays["Year_leave_days"];
                    commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value =
                        (int)readerDays["Old_leave_days"] + returnedLeaveDays - ((int)readerDays["Year_leave_days"] - (int)readerDays["Leave_days"]);
                }
                else
                {
                    commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = (int)readerDays["Leave_days"] + returnedLeaveDays;
                    commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value = 0;
                }
                readerDays.Close();
                commandUpdateEmployee.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                commandUpdateEmployee.ExecuteNonQuery();
                if (!form.TransactionOn)
                    transaction.Commit();
            }
            catch (Exception e)
            {
                if (!form.TransactionOn)
                    transaction.Rollback();
                throw e;
            }
        }

        public static void addLeaveType(this FormAddLeaveType form, string name, bool consumesDays)
        {
            addLeaveType((LeaveManagerForm)form, name, consumesDays);
        }

        private static void addLeaveType(LeaveManagerForm form, string name, bool consumesDays)
        {
            SqlTransaction transaction;
            if (form.TransactionOn)
            {
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new WrongIsolationLevelException();
                }
                transaction = form.Transaction;
            }
            else
                transaction = form.Connection.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                SqlCommand commandCheckIfExists = new SqlCommand("SELECT LT_ID " +
                    "FROM Leave_type WHERE Name = @Name", form.Connection, transaction);
                commandCheckIfExists.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                SqlDataReader reader = commandCheckIfExists.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    transaction.Rollback();
                    throw new EntryExistsException();
                }
                reader.Close();
                SqlCommand command = new SqlCommand("INSERT INTO Leave_type " +
                    "VALUES((SELECT MAX(LT_ID) + 1 FROM Leave_type), @Name, @Consumes_days)", form.Connection, transaction);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@Consumes_days", SqlDbType.Bit).Value = consumesDays;
                command.ExecuteNonQuery();
                if (!form.TransactionOn)
                    transaction.Commit();
            }
            catch (Exception e)
            {
                if (!form.TransactionOn)
                    transaction.Rollback();
                throw e;
            }
        }

        public static void AddPositionType(this FormAddPosition form, string name)
        {
            AddPositionType((LeaveManagerForm)form, name);
        }

        private static void AddPositionType(LeaveManagerForm form, string name)
        {
            SqlTransaction transaction;
            if (form.TransactionOn)
            {
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new WrongIsolationLevelException();
                }
                transaction = form.Transaction;
            }
            else
                transaction = form.Connection.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                SqlCommand commandCheckIfExists = new SqlCommand("SELECT Position_ID " +
                    "FROM Position WHERE Description = @Description", form.Connection, transaction);
                commandCheckIfExists.Parameters.Add("@Description", SqlDbType.VarChar).Value = name;
                SqlDataReader reader = commandCheckIfExists.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    throw new EntryExistsException();
                }
                reader.Close();
                SqlCommand command = new SqlCommand("INSERT INTO Position " +
                    "VALUES((SELECT MAX(Position_ID) + 1 FROM Position), @Name)", form.Connection, transaction);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                command.ExecuteNonQuery();
                if (!form.TransactionOn)
                    transaction.Commit();
            }
            catch (Exception e)
            {
                if (!form.TransactionOn)
                    transaction.Rollback();
                throw e;
            }
        }

        public static DataTable GetUninformedEmployees(this FormAdmin form)
        {
            return GetUninformedEmployees((LeaveManagerForm)form);
        }

        private static DataTable GetUninformedEmployees(LeaveManagerForm form)
        {
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'ID', E.EMail AS 'e-mail', E.Login, U.Password, E.Name, " +
                "E.Surname FROM Employee E, Uninformed U WHERE E.Employee_ID = U.Employee_ID", form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            SqlDataReader reader = command.ExecuteReader();
            DataTable uninformedEmployees = new DataTable();
            uninformedEmployees.Load(reader);
            reader.Close();
            return uninformedEmployees;
        }    

        public static bool TestConnection(this SqlConnection connection)
        {
            connection.Close();
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Uninformed", connection))
                {                    
                    command.ExecuteNonQuery();
                }
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                connection.Close();
                return false;
            }
        }

        public static void EmployeeInformed(this FormAdmin form, int employeeId)
        {
            EmployeeInformed((LeaveManagerForm)form, employeeId);
        }

        private static void EmployeeInformed(LeaveManagerForm form, int employeeId)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Uninformed WHERE Employee_ID = @Employee_ID", form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;

            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.ExecuteNonQuery();
        }

        public static void DeleteLeaveType(this FormDeleteLeaveType form, string replacementType, string deletedType)
        {
            DeleteLeaveType((LeaveManagerForm)form, replacementType, deletedType);
        }

        private static void DeleteLeaveType(LeaveManagerForm form, string replacementType, string deletedType)
        {
            SqlTransaction transaction;
            if (form.TransactionOn)
            {
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new WrongIsolationLevelException();
                }
                transaction = form.Transaction;
            }
            else
                transaction = form.Connection.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                SqlCommand commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = " +
                    "(SELECT LT_ID FROM Leave_type WHERE Name = @Name_new) " +
                    "WHERE LT_ID = " +
                    "(SELECT LT_ID FROM Leave_type WHERE Name = @Name_replaced)", form.Connection, transaction);
                commandUpdate.Parameters.Add("@Name_new", SqlDbType.VarChar).Value = replacementType;
                commandUpdate.Parameters.Add("@Name_replaced", SqlDbType.VarChar).Value = deletedType;
                commandUpdate.ExecuteNonQuery();
                SqlCommand commandDelete = new SqlCommand("DELETE FROM Leave_type WHERE " +
                    "Name = @Name", form.Connection, transaction);
                commandDelete.Parameters.Add("@Name", SqlDbType.VarChar).Value = deletedType;
                commandDelete.ExecuteNonQuery();
                if (!form.TransactionOn)
                    transaction.Commit();
            }
            catch(Exception e)
            {
                if (!form.TransactionOn)
                    transaction.Rollback();
                throw e;
            }
        }

        public static void DeletePosition(this FormDeletePosition form, string replacementPosition, string deletedPosition)
        {
            DeletePosition((LeaveManagerForm)form, replacementPosition, deletedPosition);
        }

        private static void DeletePosition(LeaveManagerForm form, string replacementPosition, string deletedPosition)
        { 
            SqlTransaction transaction;
            if (form.TransactionOn)
            {
                if (form.Transaction.IsolationLevel != IsolationLevel.RepeatableRead &&
                    form.Transaction.IsolationLevel != IsolationLevel.Serializable)
                {
                    throw new WrongIsolationLevelException();
                }
                transaction = form.Transaction;
            }
            else
                transaction = form.Connection.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                SqlCommand commandUpdate = new SqlCommand("UPDATE Employee SET Position_ID = " +
                    "(SELECT Position_ID FROM Position WHERE Description = @Description_new) " +
                    "WHERE Position_ID = " +
                    "(SELECT Position_ID FROM Position WHERE Description = @Description_replaced)", form.Connection, transaction);
                commandUpdate.Parameters.Add("@Description_new", SqlDbType.VarChar).Value = replacementPosition;
                commandUpdate.Parameters.Add("@Description_replaced", SqlDbType.VarChar).Value = deletedPosition;
                commandUpdate.ExecuteNonQuery();
                SqlCommand commandDelete = new SqlCommand("DELETE FROM Position WHERE " +
                    "Description = @Description", form.Connection, transaction);
                commandDelete.Parameters.Add("@Description", SqlDbType.VarChar).Value = deletedPosition;
                commandDelete.ExecuteNonQuery();
                if(!form.TransactionOn)
                    transaction.Commit();
            }
            catch (Exception e)
            {
                if (!form.TransactionOn)
                    transaction.Rollback();
                throw e;
            }
        }
    }
}
