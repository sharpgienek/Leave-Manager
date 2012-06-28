using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace leave_manager
{
    /// <summary>
    /// Klasa formularza zmiany loginu i/lub hasła.
    /// </summary>
    public partial class FormChangeLoginOrPassword : LeaveManagerForm
    {
        /// <summary>
        /// Id pracownika, który zmienia login i/lub hasło.
        /// </summary>
        private int employeeId;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="employeeId">Id pracownika, który zmienia login i/lub hasło.</param>
        public FormChangeLoginOrPassword(SqlConnection connection, int employeeId)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
        }

        //todo więcej i ładniejsze komunikaty błędów.
        /// <summary>
        /// Metoda obsługująca zdarzenie akceptacji zmian.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            //Jeżeli hasło i powtórzone hasło są takie same.
            if (textBoxNewPassword.Text.Equals(textBoxRepeatNewPassword.Text))
            {
                try
                {
                    //Próba zmiany hasła.
                    this.ChangeLoginOrPassword(employeeId, textBoxOldPassword.Text, textBoxNewPassword.Text, textBoxNewLogin.Text);
                    //Jeżeli nie było wyjątku (operacja się powiodła) to zostaje wyświetlony komunikat o powodzeniu.
                    MessageBox.Show("Operation compleated successfuly.");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Error occured. Plese retype your old password and try again.");
                }
            }
            else
            {
                MessageBox.Show("Repeated password is not equal to new password. No changes will be made.");
            }
        }

        /// <summary>
        /// Metoda obsługująza kliknięcie guzika anulowania. Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
