using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
namespace leave_manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String connectionString = "";
            if (!File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini"))
            {
                FormDefineDatabase form = new FormDefineDatabase();
                Application.Run(form);
                connectionString = form.ConnectionString;
            }
            else
            {
                connectionString = File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini");
            }
            
            SqlConnection connection = new SqlConnection(connectionString);
            if (!FormDefineDatabase.checkConnection(connection))
                MessageBox.Show("Could not connect to database. Try again later. If this problem will not stop, contact your administrator.");
            else
            {                
                connection.Open();
                FormLogin form = new FormLogin(connection);
                Application.Run(form);
                switch (form.Employee.Permissions)
                {
                    case 'a':
                        Application.Run(new FormAdmin(connection));
                        break;
                }
                connection.Close();
            }
        }
    }
}
