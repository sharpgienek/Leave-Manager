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
    public partial class FormLeaveConsideration : LeaveManagerForm
    {
        /// <summary>
        /// Numer id pracownika, którego dotyczy zgłoszenie urlopowe.
        /// </summary>
        private int employeeId;

        /// <summary>
        /// Data rozpoczęcia urlopu.
        /// </summary>
        private DateTime firstDay;

        /// <summary>
        /// Data zakończenia urlopu.
        /// </summary>
        private DateTime lastDay;

        /// <summary>
        /// Obiekt rodzica używany do określenia uprawnień.
        /// </summary>
        private LeaveManagerForm leaveManagerParentForm;

        /// <summary>
        /// Właściwość zwracająca obiekt formularza rodzica.
        /// </summary>
        public LeaveManagerForm LeaveManagerParentForm { get { return leaveManagerParentForm; } }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="leaveManagerParentForm">Obiekt rodzica.</param>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie urlopowe.</param>
        /// <param name="firstDay">Data rozpoczęcia urlopu.</param>
        /// <param name="lastDay">Data zakończenia urlopu.</param>
        public FormLeaveConsideration(LeaveManagerForm leaveManagerParentForm, SqlConnection connection, int employeeId, DateTime firstDay, DateTime lastDay )
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.leaveManagerParentForm = leaveManagerParentForm;
            labelFirstDayValue.Text = firstDay.ToString("d");
            labelLastDayValue.Text = lastDay.ToString("d");
            try
            {
                //Zczytanie imienia, nazwiska oraz pozycji pracownika.
                Employee employee = this.GetEmployee(employeeId);
                labelNameValue.Text = employee.Name;
                labelPositionValue.Text = employee.Position;
            }
            catch//todo obsługa wszystkich wyjątków.
            { }            
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika pozostawienia zgłoszenia bez zmian.
        /// Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonLeaveUnchanged_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie guzika zaakceptowania zgłoszenia.
        /// Po wykonaniu tej operacji urlop zmieni swój stan w zależności od
        /// typu obiektu rodzica. Zatwierdzenie przez rejestratorkę skutkuje
        /// stanem: "Pending manager approval", natomiast przez kierownika
        /// stanem: "Approved".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            try
            {
                this.AcceptLeave(employeeId, firstDay);
                this.Close();
            }
            catch 
            {
                throw new NotImplementedException();
            }//todo obsługa wszystkich wyjątków           
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika odrzucenia zgłoszenia urlopowego.
        /// Skutkuje przypisaniem do stanu zgłoszenia "Rejected".
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonReject_Click(object sender, EventArgs e)
        {
            try
            {
                this.RejectLeave(employeeId, firstDay);
                this.Close();
            }
            catch { }//todo obsługa wszystkich wyjątków.
        }

    }
}
