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
        /// Obiekt reprezentujący rozważany wpis urlopowy.
        /// </summary>
        private Leave consideredLeave;

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
        public FormLeaveConsideration(LeaveManagerForm leaveManagerParentForm, SqlConnection connection, Leave consideredLeave )
        {
            InitializeComponent();
            this.connection = connection;
            this.consideredLeave = consideredLeave;
            this.leaveManagerParentForm = leaveManagerParentForm;
            labelFirstDayValue.Text = consideredLeave.FirstDay.ToString("d");
            labelLastDayValue.Text = consideredLeave.LastDay.ToString("d");
            try
            {
                //Zczytanie imienia, nazwiska oraz pozycji pracownika.
                Employee employee = this.GetEmployee(consideredLeave.EmployeeId);
                labelNameValue.Text = employee.Name;
                labelPositionValue.Text = employee.Position;
                DateTime tmp = consideredLeave.FirstDay;
                while (tmp <= consideredLeave.LastDay)
                {
                    dataGridView.Rows.Add(tmp, this.GetSimiliarWorkerCount(this.GetPositionID(employee.Position), employee.EmployeeId, tmp));
                    tmp = tmp.AddDays(1);
                }
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
                this.AcceptLeave(consideredLeave.Id);
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
                this.RejectLeave(consideredLeave.Id);
                this.Close();
            }
            catch 
            {
                throw new NotImplementedException();
            }//todo obsługa wszystkich wyjątków.
        }

    }
}
