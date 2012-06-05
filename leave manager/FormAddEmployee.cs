using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;


namespace leave_manager
{
    public partial class FormAddEmployee : Form
    {
       // private Employee addedEmployee;
        //public Employee AddedEmployee { get { return addedEmployee; } set { addedEmployee = value; } }
        private DatabaseOperator databaseOperator;
        public FormAddEmployee(DatabaseOperator databaseOperator)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            //Ustawienie ograniczeń dla wybrania daty urodzenia nowego pracownika.
            dateTimePickerBirthDate.MinDate = (DateTime.Today.AddYears(-100));
            dateTimePickerBirthDate.MaxDate = (DateTime.Today.AddYears(-16));
            dateTimePickerBirthDate.Value = (DateTime.Today.AddYears(-20));

            comboBoxPermissions.DataSource = databaseOperator.GetPermissions();
            comboBoxPossition.DataSource = databaseOperator.GetPositions();
            //todo usunąć dane wspomagające testy poniżej
            textBoxName.Text = "trol";
            textBoxSurname.Text = "trol";
            textBoxAdress.Text = "jaskinia";
            textBoxPesel.Text = "99999999999";
            textBoxEMail.Text = "asdf";
            textBoxNumerOfLeaveDays.Text = "22";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsLetter(e.KeyChar) 
                && !char.IsControl(e.KeyChar) 
                && !char.IsWhiteSpace(e.KeyChar)) 
                e.Handled = true;
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsLetter(e.KeyChar) 
                && !char.IsControl(e.KeyChar) 
                && !char.IsWhiteSpace(e.KeyChar)) 
                e.Handled = true;
        }

        private void textBoxAdress_KeyPress(object sender, KeyPressEventArgs e)
        {             
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!e.KeyChar.Equals('-') 
                && !e.KeyChar.Equals('.') 
                && !char.IsLetterOrDigit(e.KeyChar)  
                && !char.IsWhiteSpace(e.KeyChar)
                && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxPESEL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxEMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!e.KeyChar.Equals('.')
               && !e.KeyChar.Equals('_')
               && !e.KeyChar.Equals('-')
               && !char.IsLetterOrDigit(e.KeyChar)
               && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxNumerOfLeaveDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            String errorString = "";
            if (textBoxName.Text.Length == 0)
                errorString += "- Name empty\n";
            if (textBoxName.Text.Length > 50)
                errorString += "- Name to long\n";

            if (textBoxSurname.Text.Length == 0)
                errorString += "- Surname empty\n";
            if (textBoxSurname.Text.Length > 50)
                errorString += "- Surname to long\n";

            if (textBoxAdress.Text.Length == 0)
                errorString += "- Address empty\n";
            if (textBoxAdress.Text.Length > 100)
                errorString += "- Address to long\n";

            if (textBoxPesel.Text.Length == 0)
                errorString += "- PESEL empty\n";
            if (textBoxPesel.Text.Length > 11)
                errorString += "- PESEL to long\n";

            if (textBoxEMail.Text.Length == 0)
                errorString += "- e-mail empty\n";
            if (textBoxEMail.Text.Length > 50)
                errorString += "- e-mail to long\n";

            int daysPerYear = 0;
            try
            {
                daysPerYear = System.Int32.Parse(textBoxNumerOfLeaveDays.Text);
            }
            catch
            {
                errorString += "- Wrong number of leave days per year";
            }

            if (errorString.Length > 0)
                MessageBox.Show("Error! \n\n" + errorString);
            else
            {               
               /* Random random = new Random();                
                int login; 
                SqlCommand commandCheckLogin = new SqlCommand("SELECT Login FROM Employee WHERE Login = @Login", connection);
                SqlDataReader reader;
                while(true)
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

                SqlTransaction transaction = connection.BeginTransaction();
                int password = random.Next(1000000, 10000000);
                SqlCommand commandInsertEmployee = new SqlCommand("INSERT INTO Employee (Employee_ID, Permission_ID, Position_ID, " +
                  "Login, Password, Name, Surname, Birth_date, Address, PESEL, EMail, " +
                  "Year_leave_days, Leave_days, Old_leave_days) VALUES ((SELECT MAX(Employee_ID) + 1 FROM Employee)," +
                  "@Permission_ID, (SELECT Position_ID FROM Position WHERE Description = @Description), @Login, @Password, @Name, @Surname, @Birth_date, @Address," +
                  "@PESEL, @EMail, @Year_leave_days, @Leave_days, 0)", connection, transaction);
                commandInsertEmployee.Parameters.Add("@Permission_ID", SqlDbType.Int).Value = comboBoxPermissions.SelectedIndex;
               // commandInsertEmployee.Parameters.Add("@Position_ID", SqlDbType.Int).Value = comboBoxPossition.SelectedIndex;
                commandInsertEmployee.Parameters.Add("@Description", SqlDbType.VarChar).Value = comboBoxPossition.SelectedItem.ToString();
                commandInsertEmployee.Parameters.Add("@Login", SqlDbType.VarChar).Value = login.ToString();
                commandInsertEmployee.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(password.ToString());
                commandInsertEmployee.Parameters.Add("@Name", SqlDbType.VarChar).Value = textBoxName.Text;
                commandInsertEmployee.Parameters.Add("@Surname", SqlDbType.VarChar).Value = textBoxSurname.Text;
                commandInsertEmployee.Parameters.Add("@Birth_date", SqlDbType.DateTime).Value = dateTimePickerBirthDate.Value;
                commandInsertEmployee.Parameters.Add("@Address", SqlDbType.VarChar).Value = textBoxAdress.Text;
                commandInsertEmployee.Parameters.Add("@PESEL", SqlDbType.VarChar).Value = textBoxPesel.Text;
                commandInsertEmployee.Parameters.Add("@EMail", SqlDbType.VarChar).Value = textBoxEMail.Text;
                commandInsertEmployee.Parameters.Add("@Year_leave_days", SqlDbType.Int).Value = daysPerYear;
                commandInsertEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = daysPerYear;
*/
                if (databaseOperator.AddEmployee(new Employee(-1, comboBoxPermissions.SelectedItem.ToString(), textBoxName.Text, textBoxSurname.Text, dateTimePickerBirthDate.Value, textBoxAdress.Text,
                    textBoxPesel.Text, textBoxEMail.Text, comboBoxPossition.SelectedItem.ToString(), daysPerYear, daysPerYear, 0)))
                {
                }
                this.Close();
            }

        }       
       
    }
}
