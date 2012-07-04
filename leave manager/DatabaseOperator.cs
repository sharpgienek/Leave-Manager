﻿using System;
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
                throw new EmployeeIdException();
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
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie.</param>
        /// <param name="firstDay">Pierwszy dzień zgłoszenia urlopowego.</param>
        public static void rejectLeave(this FormLeaveConsideration form, int employeeId, DateTime firstDay)
        {
            rejectLeave((LeaveManagerForm)form, employeeId, firstDay);
        }

        /// <summary>
        /// Metoda służąca do odrzucania zgłoszeń urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący.</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie.</param>
        /// <param name="firstDay">Pierwszy dzień zgłoszenia urlopowego.</param>
        private static void rejectLeave(LeaveManagerForm form, int employeeId, DateTime firstDay)
        {
            //Polecenie sql służące do aktualizacji stanu zgłoszenia.
            SqlCommand command = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                "AND First_day = @First_day ", form.Connection);
            //Jeżeli formularz posiada uruchomioną transakcję, podłącz do niej polecenie.
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Rejected";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        public static void getDays(this FormEmployee form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz danych pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        public static void getDays(this FormEmployeeData form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        public static void getDays(this FormLeaveApplication form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        private static void getDays(LeaveManagerForm form, int employeeId, ref int leaveDays, ref int oldLeaveDays)
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
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <param name="yearDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba dni pracownika na rok.</param>
        public static void getDays(this FormLeaveApplication form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// Rozszerza formularz danych pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <param name="yearDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba dni pracownika na rok.</param>
        public static void getDays(this FormEmployeeData form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
            getDays((LeaveManagerForm)form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
        }

        /// <summary>
        /// Metoda pobierania dni urlopowych i zaległych dni urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="leaveDays">Referencja do zmiennej do której ma zostać
        /// zczytana liczba dni urlopowych pracownika.</param>
        /// <param name="oldLeaveDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba zaległych dni urlopowych pracownika.</param>
        /// <param name="yearDays">Referencja do zmiennej do której ma zostać 
        /// zczytana liczba dni pracownika na rok.</param>
        private static void getDays(LeaveManagerForm form, int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
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
        /// Kolumny tabeli: "Status", "First day", "Last day", "Type",
        /// "Remarks", "No. used days"</returns>
        public static DataTable getLeaves(this FormEmployee form, int employeeId)
        {
            return getLeaves((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda pobietania tabeli z danymi urlopowymi danego pracownika.
        /// Rozszerza formularz danych pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <returns>Zwraca tabelę z danymi urlopowymi pracownika.
        /// Kolumny tabeli: "Status", "First day", "Last day", "Type",
        /// "Remarks", "No. used days"</returns>
        public static DataTable getLeaves(this FormEmployeeData form, int employeeId)
        {
            return getLeaves((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda pobietania tabeli z danymi urlopowymi danego pracownika.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <returns>Zwraca tabelę z danymi urlopowymi pracownika.
        /// Kolumny tabeli: "Status", "First day", "Last day", "Type",
        /// "Remarks", "No. used days"</returns>
        private static DataTable getLeaves(LeaveManagerForm form, int employeeId)
        {
            //Zapytanie zczytujące dane.
            SqlCommand commandSelectLeaves = new SqlCommand("SELECT LS.Name AS 'Status', L.First_day AS 'First day', " +
                        "L.Last_day AS 'Last day', LT.Name AS 'Type', LT.Consumes_days, L.Remarks " +
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
            readerLeaves.Close();
            //Dodanie kolumny z liczbą używanych dni przez urlop, oraz obliczenie ich wartości.
            leaves.Columns.Add("No. used days");
            int usedDays;
            for (int i = 0; i < leaves.Rows.Count; i++)
            {
                //Jeżeli urlop konsumuje dni.
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
        /// Rozszerza formularz zgłoszenia urlopowego. 
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="employeeId">Numer id pracownika, któremu mają zostać dodane dni.</param>
        /// <param name="number">Liczba dodawanych dni. Może być ujemna.</param>
        public static void AddLeaveDays(this FormLeaveApplication form, int employeeId, int number)
        {
            AddLeaveDays((LeaveManagerForm)form, employeeId, number);
        }

        /// <summary>
        /// Metoda służąca do "dawania" pracownikowi dodatkowych dni urlopowych. Dodaje je najpierw do 
        /// normalnych dni urlopowych, a gdy ich liczba == liczba dni/rok, dodaje resztę dni do zaległych dni urlopowych.
        /// </summary>
        /// <param name="form">Formularz wywołujący</param>
        /// <param name="employeeId">Numer id pracownika, któremu mają zostać dodane dni.</param>
        /// <param name="number">Liczba dodawanych dni. Może być ujemna.</param>
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
                getDays(form, employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
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

        //todo tabela z dniami wolnymi od pracy, uwzględnić godziny/dni pracy pracownika, gdy już będa. 

        /// <summary>
        /// Metoda zwracająca liczbę dni w okresie od date1(włącznie) do date2(włącznie), które konsumują dni urlopowe.
        /// </summary>
        /// <param name="form">Formularz wymagający metody.</param>
        /// <param name="date1">Pierwszy dzień sprawdzanego okresu.</param>
        /// <param name="date2">Ostatni dzień sprawdzanego okresu.</param>
        /// <returns>Liczba dni w okresie między date1 i date2, które konsumują dni rulopowe.</returns>
        private static int GetNumberOfWorkDays(LeaveManagerForm form, DateTime date1, DateTime date2)
        {
            //Obliczenie różnicy czasu pomiędzy datami.
            TimeSpan timeSpan = date2 - date1;
            //Obliczenie maksymalnej liczby dni, które mogą konsumować dni.
            int numberOfDays = (int)Math.Round(timeSpan.TotalDays) + 1;
            //Dopóki nie sprawdzono wszystkich dni.
            while (date1.CompareTo(date2) <= 0)
            {
                //Jeżeli sprawdzany dzień, to sobota lub niedziela, to od liczby dni konsumujących odejmujemy 1.
                if (date1.DayOfWeek == DayOfWeek.Saturday || date1.DayOfWeek == DayOfWeek.Sunday)
                    numberOfDays--;
                //Przesunięcie sprawdzanego dnia na następny.
                date1 = date1.AddDays(1);
            }
            return numberOfDays;
        }

        /// <summary>
        /// Metoda edytująca wpis urlopowy.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="leave">Obiekt urlopu wpisany w miejsce starego wpisu.</param>
        /// <param name="oldFirstDay">Pierwszy dzień urlopu wpisu zamienianego.</param>
        public static void EditLeave(this FormLeaveApplication form, Leave leave, DateTime oldFirstDay)
        {
            EditLeave((LeaveManagerForm)form, leave, oldFirstDay);
        }

        /// <summary>
        /// Metoda pobierająca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="firstDay">Dzień rozpoczęcia urlopu.</param>
        /// <returns>Obiekt reprezentujący dany urlop.</returns>
        public static Leave GetLeave(this FormEmployee form, DateTime firstDay)
        {
            return GetLeave(form, form.EmployeeId, firstDay);
        }

        /// <summary>
        /// Metoda pobierająca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="employeeId">Numer id pracownika, który jest właścicielem urlopu.</param>
        /// <param name="firstDay">Dzień rozpoczęcia urlopu.</param>
        private static Leave GetLeave(LeaveManagerForm form, int employeeId, DateTime firstDay)
        {
            SqlCommand command = new SqlCommand("SELECT L.Employee_ID, LT.Name AS 'Type', " +
                "LS.Name AS 'Status', L.First_day, L.Last_day, " +
                "L.Remarks FROM Leave L, Leave_type LT, Status_type LS WHERE Employee_ID = @Employee_ID " +
                "AND First_day = @First_day", form.Connection);
            if (form.TransactionOn)
                command.Transaction = form.Transaction;
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            SqlDataReader reader = command.ExecuteReader();
            return new Leave((int)reader["Employee_ID"], reader["Type"].ToString(), reader["Status"].ToString(),
                (DateTime)reader["First_day"], (DateTime)reader["Last_day"], reader["Remarks"].ToString());
        }

        /// <summary>
        /// Metoda edytująca wpis urlopowy.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt urlopu wpisany w miejsce starego wpisu.</param>
        /// <param name="oldFirstDay">Pierwszy dzień urlopu wpisu zamienianego.</param>
        private static void EditLeave(LeaveManagerForm form, Leave leave, DateTime oldFirstDay)
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
                Leave oldLeave = GetLeave(form, leave.EmployeeId, oldFirstDay);
                //Jeżeli wpisywany urlop konsumuje dni urlopowe.
                if (ConsumesDays(form, leave.LeaveType))
                {
                    //Obliczenie różnicy o którą trzeba zmienić liczbę dni urlopowych pracownika.
                    int difference = GetNumberOfWorkDays(form, oldLeave.FirstDay, oldLeave.LastDay) - GetNumberOfWorkDays(form, leave.FirstDay, leave.LastDay);
                    AddLeaveDays(form, leave.EmployeeId, difference);
                }
                else//Nowy urlop nie konsumuje dni.
                {
                    //Jeżeli stary urlop konsumuje dni.
                    if (ConsumesDays(form, oldLeave.LeaveType))
                        //Dodajemy pracownikowi tyle dni, ile stary urlop konsumował.
                        AddLeaveDays(form, leave.EmployeeId, GetNumberOfWorkDays(form, oldLeave.FirstDay, oldLeave.LastDay));
                }
                //Polecenie sql aktualizujące wszystkie dane w starym wpisie.
                SqlCommand commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = (SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), LS_ID = " +
                   "(SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                   "First_day = @First_day, Last_day = @Last_day, Remarks = @Remarks " +
                   "WHERE Employee_ID = @Employee_ID AND First_day = @Old_first_day", form.Connection, form.Transaction);
                commandUpdate.Parameters.Add("@Old_first_day", SqlDbType.Date).Value = oldFirstDay;
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
        public static void addLeave(this FormLeaveApplication form, Leave leave)
        {
            addLeave((LeaveManagerForm)form, leave);
        }

        /// <summary>
        /// Metoda dodawania zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt reprezentujący nowy wpis urlopowy.</param>
        private static void addLeave(LeaveManagerForm form, Leave leave)
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
                //Polecenie sql dodające nowy wpis urlopowy.
                SqlCommand commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Employee_ID, " +
                     "(SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), (SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                     "@First_day, @Last_day, @Remarks)", form.Connection, form.Transaction);
                //Jeżeli wpis to chorobowe, to automatycznie otrzymuje stan zatwierdzony.
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
                //Jeżeli urlop konsumuje dni.
                if (ConsumesDays(form, leave.LeaveType))
                {
                    //Odejmujemy pracownikowi dni za dany urlop.
                    AddLeaveDays(form, leave.EmployeeId, -GetNumberOfWorkDays(form, leave.FirstDay, leave.LastDay));
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
        /// Dodanie chorobowego.
        /// Rozszerza formularz zgłoszenia urlopowego.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="leave">Obiekt reprezentujący dodawany urlop.</param>
        public static void addSickLeave(this FormLeaveApplication form, Leave leave)
        {
            addSickLeave((LeaveManagerForm)form, leave);
        }

        /// <summary>
        /// Dodanie chorobowego.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt reprezentujący dodawany urlop.</param>
        private static void addSickLeave(LeaveManagerForm form, Leave leave)
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
                DataTable dataLeaves = getLeaves(form, leave.EmployeeId);
                /* Zmienna w której będzie przechowywana liczba dni do zwrócenia pracownikowi.
                 * Powiększa się w przypadku, gdy chorobowe zachodzi na jakiś urlop.
                 */
                int returnedLeaveDays = 0;
                //Dla każdego istniejącego w bazie urlopu.
                foreach (DataRow row in dataLeaves.Rows)
                {
                    //Pierwszy dzień sprawdzanego urlopu jest później lub ten sam, co pierwszy dzień chorobowego
                    if ((((DateTime)row.ItemArray.GetValue(1)).CompareTo(leave.FirstDay) >= 0)
                        //i jest wcześniej lub taki sam jak ostatni dzień chorobowego.
                    && (((DateTime)row.ItemArray.GetValue(1)).CompareTo(leave.LastDay) <= 0))
                    {//Czyli w praktyce: Zaczyna się w trakcie chorobowego -> jest anulowany.
                        //Jeżeli zachodzący wpis to chorobowe.
                        if (row.ItemArray.GetValue(3).ToString().Equals("Sick"))
                            throw new EntryExistsException();
                        //Polecenie sql zmieniające stan zachodzącego urlopu na anulowany.
                        SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                           "LS_ID = (SELECT ST_ID FROM Status_type WHERE Name = @Status_name) " +
                           "WHERE Employee_ID = @Employee_ID AND First_day = @First_day", form.Connection, form.Transaction);
                        commandUpdateLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Canceled";
                        commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                        commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(1);
                        commandUpdateLeave.ExecuteNonQuery();
                        //Dodanie do liczby dni do zwrócenia pracownikowi dni anulowanego urlopu.
                        returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(1)).GetNumberOfWorkDays((DateTime)row.ItemArray.GetValue(2));
                        continue;
                    }

                    if ((leave.FirstDay.CompareTo((DateTime)row.ItemArray.GetValue(1)) >= 0)//Sick first day later than leave first day 
                    && (leave.FirstDay.CompareTo((DateTime)row.ItemArray.GetValue(2)) <= 0))//and earlier than leave last day.
                    {//Czyli w praktyce: Kończy się w trakcie chorobowego -> jest 'przycinany' do ostatniego dnia przed chorobowym.
                        SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                            "Last_day = @Last_day WHERE Employee_ID = @Employee_ID AND First_day = @First_day", form.Connection, form.Transaction);
                        commandUpdateLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.FirstDay.AddDays(-1);
                        commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                        commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(1);
                        commandUpdateLeave.ExecuteNonQuery();
                        //Dodanie do liczby dni do zwrócenia pracownikowi liczby dni za okres od początku chorobowego do końca urlopu.
                        returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(0)).GetNumberOfWorkDays(leave.FirstDay.AddDays(-1));
                        continue;
                    }
                }
                //Zwrócenie pracownikowi dni.
                AddLeaveDays(form, leave.EmployeeId, returnedLeaveDays);
                //Dodanie urlopu.
                addLeave(form, leave);
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
        public static void ChangePassword(this FormChangeLoginOrPassword form, int employeeId, String oldPassword, String newPassword)
        {
            try
            {
                ChangeLoginOrPassword((LeaveManagerForm)form, employeeId, oldPassword, newPassword, "");
            }
            catch (LoginOrPasswordException)
            {
                throw new PasswordException();
            }
        }

        /// <summary>
        /// Metoda zmiany loginu.
        /// Rozszerza formularz zmiany loginu i/lub hasła.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika, któremu zmienione zostają dane.</param>
        /// <param name="oldPassword">Stare hasło.</param>
        /// <param name="newLogin">Nowy login.</param>
        public static void ChangeLogin(this FormChangeLoginOrPassword form, int employeeId, String oldPassword, String newLogin)
        {
            try
            {
                ChangeLoginOrPassword((LeaveManagerForm)form, employeeId, oldPassword, "", newLogin);
            }
            catch (LoginOrPasswordException)
            {
                throw new LoginException();
            }
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
        /// <param name="leave">Obiekt reprezentujący usuwany urlop.</param>
        public static void DeleteLeave(this FormEmployee form, Leave leave)
        {
            DeleteLeave((LeaveManagerForm)form, leave);
        }

        /// <summary>
        /// Metoda usuwania wpisu urlopowego.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="leave">Obiekt reprezentujący usuwany urlop.</param>
        private static void DeleteLeave(LeaveManagerForm form, Leave leave)
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
                //Polecenie sql usuwające wpis urlopowy.
                SqlCommand commandDeleteLeave = new SqlCommand("DELETE FROM Leave WHERE " +
                     "Employee_ID = @Employee_ID AND First_day = @First_day", form.Connection, form.Transaction);
                commandDeleteLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                commandDeleteLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = leave.FirstDay;
                commandDeleteLeave.ExecuteNonQuery();
                if (!leave.LeaveStatus.Equals("Canceled") && !leave.LeaveStatus.Equals("Rejected"))
                    AddLeaveDays(form, leave.EmployeeId, GetNumberOfWorkDays(form, leave.FirstDay, leave.LastDay));
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
        public static void addLeaveType(this FormAddLeaveType form, string name, bool consumesDays)
        {
            addLeaveType((LeaveManagerForm)form, name, consumesDays);
        }

        /// <summary>
        /// Metoda dodawania typu urlopu.
        /// </summary>
        /// <param name="form">Formularz potrzebujący metody.</param>
        /// <param name="name">Nazwa nowego typu.</param>
        /// <param name="consumesDays">Czy nowy typ konsumuje dni.</param>
        private static void addLeaveType(LeaveManagerForm form, string name, bool consumesDays)
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
        public static void AddPositionType(this FormAddPosition form, string name)
        {
            AddPositionType((LeaveManagerForm)form, name);
        }

        /// <summary>
        /// Metoda dodająca nowy rodzaj pozycji (pracowników) do bazy.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="name">Nazwa nowej pozycji.</param>
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
        public static DataTable GetUninformedEmployees(this FormAdmin form)
        {
            return GetUninformedEmployees((LeaveManagerForm)form);
        }

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
        public static void EmployeeInformed(this FormAdmin form, int employeeId)
        {
            EmployeeInformed((LeaveManagerForm)form, employeeId);
        }

        /// <summary>
        /// Metoda oznaczająca nowego pracownika jako poinformowanego o swoim loginie/haśle.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="employeeId">Numer id pracownika poinformowanego.</param>
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
        /// Metoda służąca do usunięcia z bazy danych typu pozycji pracowników.
        /// </summary>
        /// <param name="form">Formularz wywołujący metodę.</param>
        /// <param name="replacementPosition">Pozycja, która zostanie przypisana wszystkim pracownikom, którzy zajmują pozycję usuwaną.</param>
        /// <param name="deletedPosition">Nazwa pozycji usuwanej.</param>
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
    }
}
