using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace leave_manager
{

    public class DatabaseOperator
    {
        private SqlConnection connection;
        private SqlTransaction transaction;
        private bool transactionOn;       
      
        //todo throw somthng?
        public void BeginTransaction()
        {
            transactionOn = true;
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            transactionOn = false;
            transaction.Commit();
        }

        public DatabaseOperator(SqlConnection connection)
        {
            this.connection = connection;
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public static bool login(this FormLogin form, String login, String password, ref Employee employee)
        {
          //  try
          //  {
                SqlCommand command = new SqlCommand("SELECT E.Employee_ID, Perm.Description AS Permission, E.Name, E.Surname, E.Birth_date," +
                                                   "E.Address, E.PESEL, E.EMail, Pos.Description AS Position, E.Year_leave_days, " +
                                                   "E.Leave_days, E.Old_leave_days " +
                                                   "FROM Employee E, Permission Perm, Position Pos WHERE Login = @Login AND " +
                                                   "Password = @Password AND E.Permission_ID = Perm.Permission_ID AND " +
                                                   "E.Position_ID = Pos.Position_ID", connection);
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(login);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    employee = new Employee((int)reader["Employee_ID"], reader["Permission"].ToString(),
                       reader["Name"].ToString(), reader["Surname"].ToString(), (DateTime)reader["Birth_date"],
                       reader["Address"].ToString(), reader["PESEL"].ToString(), reader["EMail"].ToString(),
                       reader["Position"].ToString(), (int)reader["Year_leave_days"], (int)reader["Leave_days"],
                       (int)reader["Old_leave_days"]);
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
          /*  }
            catch
            {
                return false;
            }*/

        }

        public List<LeaveType> GetLeaveTypes()
        {
            List<LeaveType> result = new List<LeaveType>();
            SqlCommand command = new SqlCommand("SELECT LT_ID, Name, Consumes_days FROM Leave_type ORDER BY LT_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new LeaveType((int)reader["LT_ID"], reader["Name"].ToString(), (bool)reader["Consumes_days"]));
            }
            reader.Close();
            return result;
        }

        public List<String> GetPermissions()
        {
            List<String> result = new List<String>();
            SqlCommand command = new SqlCommand("SELECT Description FROM Permission ORDER BY Permission_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Description"].ToString());
            }
            reader.Close();
            return result;
        }

        public List<String> GetPositions()
        {
            List<String> result = new List<string>();
            SqlCommand command = new SqlCommand("SELECT Description FROM Position ORDER BY Position_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Description"].ToString());
            }
            reader.Close();
            return result;
        }

        public List<String> GetStatusTypes()
        {
            List<String> result = new List<string>();
            SqlCommand command = new SqlCommand("SELECT Name FROM Status_type ORDER BY ST_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Name"].ToString());
            }
            reader.Close();
            return result;
        }

        public bool AddEmployee(Employee employee)
        {
            SqlTransaction transaction = connection.BeginTransaction();
          //  try
          //  {
                Random random = new Random();
                int login;
                SqlCommand commandCheckLogin = new SqlCommand("SELECT Login FROM Employee WHERE Login = @Login", connection, transaction);
                SqlDataReader reader;
                while (true)
                {
                    login = random.Next(1000000, 10000000);
                    commandCheckLogin.Parameters.Clear();
                    commandCheckLogin.Parameters.Add("@Login", SqlDbType.VarChar).Value = login.ToString();
                    reader = commandCheckLogin.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        reader.Dispose();
                        break;
                    }
                    reader.Close();
                }
                int password = random.Next(1000000, 10000000);
                SqlCommand commandInsertEmployee = new SqlCommand("INSERT INTO Employee (Employee_ID, Permission_ID, Position_ID, " +
                  "Login, Password, Name, Surname, Birth_date, Address, PESEL, EMail, " +
                  "Year_leave_days, Leave_days, Old_leave_days) VALUES ((SELECT MAX(Employee_ID) + 1 FROM Employee)," +
                  "(SELECT Permission_ID FROM Permission WHERE Description = @Permission_Description), (SELECT Position_ID FROM Position WHERE Description = @Position_Description), @Login, @Password, @Name, @Surname, @Birth_date, @Address," +
                  "@PESEL, @EMail, @Year_leave_days, @Leave_days, 0)", connection, transaction);
                commandInsertEmployee.Parameters.Add("@Permission_Description", SqlDbType.VarChar).Value = employee.Permission;
                // commandInsertEmployee.Parameters.Add("@Position_ID", SqlDbType.Int).Value = comboBoxPossition.SelectedIndex;
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
                SqlCommand commandInsertUninformed = new SqlCommand("INSERT INTO Uninformed (Employee_ID, Password) " +
                    "VALUES ((SELECT MAX(Employee_ID) FROM Employee), @Password)",
                    connection, transaction);
                commandInsertUninformed.Parameters.Add("@Password", SqlDbType.VarChar).Value = password.ToString();
                commandInsertUninformed.ExecuteNonQuery();
                transaction.Commit();
                return true;
          /*  }
            catch (SqlException)
            {
                transaction.Rollback();
                return false;
            }*/
        }

        public bool getNeedsAction(object parent, ref DataTable destinationTable)
        {
           // try
           // {
                SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', P.Description AS 'Position', " +
                    "E.Name, E.Surname, E.EMail AS 'e-mail', LT.Name AS 'Type', " +
                    "L.First_day AS 'First day', L.Last_day AS 'Last day' FROM Employee E, " +
                    "Leave L, Leave_type LT, Position P, Status_type LS WHERE L.Employee_ID = E.Employee_ID " +
                    "AND L.LT_ID = LT.LT_ID AND P.Position_ID = E.Position_ID AND LS.ST_ID = L.LS_ID AND LS.Name = @Name ORDER BY L.First_day", connection);
                if (parent.GetType() == new FormAssistant().GetType())
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending validation";
                }
                else//to znaczy, że manager //todo ładny koment :P
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
                }

                SqlDataReader reader = command.ExecuteReader();
                destinationTable.Load(reader);
                reader.Close();
                destinationTable.Columns.Add("No. used work days");
                for (int i = 0; i < destinationTable.Rows.Count; i++)
                {
                    destinationTable.Rows[i]["No. used work days"] = TimeTools.GetNumberOfWorkDays(
                        (DateTime)destinationTable.Rows[i]["First day"], (DateTime)destinationTable.Rows[i]["Last day"]);
                }
                return true;
          /*  }
            catch
            {
                return false;
            }*/
        }

        public bool getEmployees(ref DataTable destinationTable)
        {
          //  try
          //  {
                SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', E.Name, E.Surname, E.Birth_date AS 'Birth date'," +
                   "E.Address, E.PESEL, E.EMail AS 'e-mail', Pos.Description AS Position, " +
                   "Perm.Description AS Permission, E.Leave_days AS 'Remaining leave days', " +
                   "E.Old_leave_days AS 'Old left leave days' " +
                   "FROM Employee E, Position Pos, Permission Perm " +
                   "WHERE E.Permission_ID = Perm.Permission_ID " +
                   "AND E.Position_ID = Pos.Position_ID", connection);

                SqlDataReader reader = command.ExecuteReader();
                destinationTable.Load(reader);
                reader.Close();
                return true;
            /*}
            catch
            {
                return false;
            }*/
        }

        public bool getEmployee(int employeeId, ref Employee destinationEmployeeObject)
        {
           // try
         //   {
                SqlCommand command = new SqlCommand("SELECT E.Employee_ID, Perm.Description AS Permission, E.Name, E.Surname, E.Birth_date," +
                                                       "E.Address, E.PESEL, E.EMail, Pos.Description AS Position, E.Year_leave_days, " +
                                                       "E.Leave_days, E.Old_leave_days " +
                                                       "FROM Employee E, Permission Perm, Position Pos WHERE Employee_ID = @Employee_ID " +
                                                       "AND E.Permission_ID = Perm.Permission_ID AND " +
                                                       "E.Position_ID = Pos.Position_ID", connection);
                command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    destinationEmployeeObject = new Employee(employeeId, reader["Permission"].ToString(),
                       reader["Name"].ToString(), reader["Surname"].ToString(), (DateTime)reader["Birth_date"],
                       reader["Address"].ToString(), reader["PESEL"].ToString(), reader["EMail"].ToString(),
                       reader["Position"].ToString(), (int)reader["Year_leave_days"], (int)reader["Leave_days"],
                       (int)reader["Old_leave_days"]);
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
        /*    }
            catch
            {
                return false;
            }*/
        }

        public bool acceptLeave(object parent, int employeeId, DateTime firstDay)
        {
          //  try
          //  {
                SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
              //  try
             //   {
                    SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                        "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                        "AND First_day = @First_day ", connection, transaction);
                    commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                    commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
                    if (parent.GetType() == new FormAssistant().GetType())
                    {
                        commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
                    }
                    else
                    {
                        commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Approved";
                    }
                    commandUpdateLeave.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
            /*    }
                catch
                {
                    transaction.Rollback();
                    return false;
                }*/
          /*  }
            catch
            {
                return false;
            }*/
        }

        public bool rejectLeave(int employeeId, DateTime firstDay)
        {
          //  try
          //  {
                SqlCommand command = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                    "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                    "AND First_day = @First_day ", connection);
                command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                command.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Rejected";
                command.ExecuteNonQuery();//todo try catch;
                return true;
          /*  }
            catch
            {
                return false;
            }*/
        }
        //todo transactions
        public bool getDays(int employeeId, ref int leaveDays, ref int oldLeaveDays)
        {
       //     try
        //    {
                SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee " +
                    "WHERE Employee_ID = @Employee_ID", connection);
                // "WHERE Employee_ID = @Employee_ID", connection, transaction);
                commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                SqlDataReader readerDays = commandSelectDays.ExecuteReader();
                readerDays.Read();//todo zabezpiecz przed błędami.*/
                leaveDays = (int)readerDays["Leave_days"];
                oldLeaveDays = (int)readerDays["Old_leave_days"];
                readerDays.Close();
                return true;
       /*     }
            catch
            {
                return false;
            }*/
        }

        public bool getDays(int employeeId, ref int leaveDays, ref int oldLeaveDays, ref int yearDays)
        {
         //   try
         //   {
                SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days, Year_leave_days FROM Employee " +
                    "WHERE Employee_ID = @Employee_ID", connection);
                // "WHERE Employee_ID = @Employee_ID", connection, transaction);
                commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                SqlDataReader readerDays = commandSelectDays.ExecuteReader();
                readerDays.Read();//todo zabezpiecz przed błędami.*/
                leaveDays = (int)readerDays["Leave_days"];
                oldLeaveDays = (int)readerDays["Old_leave_days"];
                yearDays = (int)readerDays["Year_leave_days"];
                readerDays.Close();
                return true;
         /*   }
            catch
            {
                return false;
            }*/
        }

        public bool getLeaves(ref DataTable destinationTable, int employeeId)
        {
           // try
          //  {
                SqlCommand commandSelectLeaves = new SqlCommand("SELECT LS.Name AS 'Status', L.First_day AS 'First day', " +
                            "L.Last_day AS 'Last day', LT.Name AS 'Type', LT.Consumes_days, L.Remarks " +
                            "FROM Employee E, Leave L, Leave_type LT, Status_type LS " +
                            "WHERE L.LT_ID = LT.LT_ID AND L.LS_ID = LS.ST_ID AND E.Employee_ID = L.Employee_ID " +
                            "AND E.Employee_ID = @Employee_ID ORDER BY L.First_day", connection);
                //   "AND E.Employee_ID = @Employee_ID ORDER BY L.First_day", connection, transaction);
                commandSelectLeaves.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                SqlDataReader readerLeaves = commandSelectLeaves.ExecuteReader();

                destinationTable.Load(readerLeaves);
                readerLeaves.Close();
                // transaction.Commit();
                destinationTable.Columns.Add("No. used days");
                // int sumOfUsedDays = 0;
                int usedDays;
                for (int i = 0; i < destinationTable.Rows.Count; i++)
                {
                    if ((bool)destinationTable.Rows[i]["Consumes_days"])
                    {
                        destinationTable.Rows[i]["No. used days"] = usedDays = TimeTools.GetNumberOfWorkDays((DateTime)destinationTable.Rows[i]["First day"],
                                (DateTime)destinationTable.Rows[i]["Last day"]);
                    }
                    else
                        destinationTable.Rows[i]["No. used days"] = 0;
                }
                destinationTable.Columns.Remove("Consumes_days");
                return true;
           /* }
            catch
            {
                return false;
            }*/
        }
        //todo jakaś obsługa błędu.. co gdy błąd?
        public bool IsDateFromPeriodUsed(DateTime date1, DateTime date2, int employeeID)
        {
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID", connection);
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

        //todo jakaś obsługa błędu.. co gdy błąd?
        public bool IsDateFromPeriodUsed(DateTime date1, DateTime date2,
            int employeeID, DateTime skippedEntryFirstDay)
        {
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID AND First_day != @Skipped_entry_first_day", connection);
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

        public bool AddLeaveDays(int employeeId, int number)
        {
            //try
           // {
                int leaveDays = 0;
                int oldLeaveDays = 0;
                int yearDays = 0;
                if (getDays(employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays))
                {
                    if (AddLeaveDays(employeeId, number, leaveDays, oldLeaveDays, yearDays))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }                
           /* }
            catch
            {
                return false;
            }*/
        }

        public bool AddLeaveDays(int employeeId, int number, int leaveDays, int oldLeaveDays, int yearDays)
        {
           // try
           // {
                SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                        "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                        "WHERE Employee_ID = @Employee_ID", connection);
                //  "WHERE Employee_ID = @Employee_ID", connection, transaction);
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
                return true;
         /*   }
            catch
            {
                return false;
            }*/
        }
        //todo jakoś obsługe błędów.. jak?
        bool ConsumesDays(String leaveTypeName)
        {
            SqlCommand commandSelectConsumesDays = new SqlCommand("SELECT Consumes_days FROM  " +
                "Leave_type WHERE Name = @Name", connection);
           // "Leave_type WHERE Name = @Name", connection, transaction);
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
        public int GetNumberOfWorkDays(DateTime date1, DateTime date2)
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

        public bool EditLeave(Leave leave, DateTime oldFirstDay)
        {
          //  try
          //  {
                SqlCommand commandUpdate;
                commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = (SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), LS_ID = " +
                    "(SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                    "First_day = @First_day, Last_day = @Last_day, Remarks = @Remarks " +
                    "WHERE Employee_ID = @Employee_ID AND First_day = @Old_first_day", connection);
                //  "WHERE Employee_ID = @Employee_ID AND First_day = @Old_first_day", connection, transaction);
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
                if (ConsumesDays(leave.LeaveType))
                {
                    if (AddLeaveDays(leave.EmployeeId, -GetNumberOfWorkDays(leave.FirstDay, leave.LastDay)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
          /*  }
            catch
            {
                return false;
            }*/
        }

        public bool addLeave(Leave leave)
        {
         //   try
          //  {
                SqlCommand commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Employee_ID, " +
                     "(SELECT LT_ID FROM Leave_type WHERE Name = @Leave_type_name), (SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                     "@First_day, @Last_day, @Remarks)", connection);
                // "@First_day, @Last_day, @Remarks)", connection, transaction);

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

                if (ConsumesDays(leave.LeaveType))
                {
                    if (AddLeaveDays(leave.EmployeeId, -GetNumberOfWorkDays(leave.FirstDay, leave.LastDay)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
          /*  }
            catch
            {
                return false;
            }*/
        }

        public bool addSickLeave(Leave leave, ref int leaveDays, ref int oldLeaveDays, int yearDays)
        {
            DataTable dataLeaves = new DataTable();
            if (getLeaves(ref dataLeaves, leave.EmployeeId))
            {
                int returnedLeaveDays = 0;
                foreach (DataRow row in dataLeaves.Rows)
                {
                    //pierwszy dzień sprawdzanego urlopu jest później lub ten sam, co pierwszy dzień chorobowego
                    if ((((DateTime)row.ItemArray.GetValue(1)).CompareTo(leave.FirstDay) >= 0)
                        //i jest wcześniej lub taki sam jak ostatni dzień chorobowego.
                    && (((DateTime)row.ItemArray.GetValue(1)).CompareTo(leave.LastDay) <= 0))
                    {
                        if(row.ItemArray.GetValue(3).ToString().Equals("Sick"))
                            return false; //todo jakiś eksport ładnego błędu.
                        SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                           "LS_ID = (SELECT ST_ID FROM Status_type WHERE Name = @Status_name) " +
                           "WHERE Employee_ID = @Employee_ID AND First_day = @First_day", connection);
                          // "WHERE Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
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
                            "Last_day = @Last_day WHERE Employee_ID = @Employee_ID AND First_day = @First_day", connection);
                           // "Last_day = @Last_day WHERE Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
                        commandUpdateLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = leave.FirstDay.AddDays(-1);
                        commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = leave.EmployeeId;
                        commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(1);
                        commandUpdateLeave.ExecuteNonQuery();
                        returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(0)).GetNumberOfWorkDays(leave.FirstDay.AddDays(-1));
                        continue;
                    }
                }
                AddLeaveDays(leave.EmployeeId, returnedLeaveDays, leaveDays, oldLeaveDays, yearDays);                
                if (addLeave(leave))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //todo error message
                return false;
            }
        }

        public bool ChangeLoginOrPassword(int employeeId, String oldPassword, String newPassword, String newLogin)
        {
            SqlCommand command = new SqlCommand("SELECT Employee_ID FROM Employee WHERE " +
                "Employee_ID = @Employee_ID AND Password = @Password", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(oldPassword);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                if (newLogin.Length != 0 && newPassword.Length != 0)
                {
                    //if (newPassword.Text.Equals(textBoxRepeatNewPassword.Text))
                   // {
                        command.CommandText = "UPDATE Employee SET Login = @Login, " +
                            "Password = @Password WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Login", SqlDbType.VarChar).Value = newLogin;
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(newPassword);
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery();//todo try catch
                       // textBoxNewLogin.Text = "";
                     //   textBoxNewPassword.Text = "";
                      //  textBoxRepeatNewPassword.Text = "";
                  //  }
                  //  else
                  //  {
                       // MessageBox.Show("Repeated password is not equal to new password. No changes will be made.");
                   // }
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
                      //  textBoxNewLogin.Text = "";
                    }

                    if (newPassword.Length != 0)
                    {                        
                            command.CommandText = "UPDATE Employee SET Password = @Password WHERE Employee_ID = @Employee_ID";
                            command.Parameters.Clear();
                            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(newPassword);
                            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                            command.ExecuteNonQuery(); //todo try catch
                           // textBoxNewPassword.Text = "";
                           // textBoxRepeatNewPassword.Text = "";
                       
                    }
                }
            }
            else
            {
                reader.Close();
                return false;                
              //  MessageBox.Show("Old password is incorrect.");
            }
            return true;
        }

        public bool DeleteLeave(int employeeId, DateTime firstDay, DateTime lastDay)
        {
            SqlCommand commandDeleteLeave = new SqlCommand("DELETE FROM Leave WHERE " +
                 "Employee_ID = @Employee_ID AND First_day = @First_day", connection);
                          //  "Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
            commandDeleteLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            commandDeleteLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            commandDeleteLeave.ExecuteNonQuery();
            SqlCommand commandReadDays = new SqlCommand("SELECT Year_leave_days, Leave_days, Old_leave_days " +
                "FROM Employee WHERE Employee_ID = @Employee_ID", connection);
                    //"FROM Employee WHERE Employee_ID = @Employee_ID", connection, transaction);
            commandReadDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader readerDays = commandReadDays.ExecuteReader();
            readerDays.Read();//todo try catch?
            SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                "WHERE Employee_ID = @Employee_ID", connection);
               // "WHERE Employee_ID = @Employee_ID", connection, transaction);
          //  int returnedLeaveDays = ((DateTime)row.Cells["First day"].Value).GetNumberOfWorkDays((DateTime)row.Cells["Last day"].Value);
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
            // SqlCommand commandUpdateDays = new SqlCommand("UPDATE Employee SET @
            return true;
        }
    }
}
