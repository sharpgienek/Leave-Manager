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
    /// Klasa zgłoszenia urlopowego.
    /// </summary>
    public partial class FormLeaveApplication : LeaveManagerForm
    {
        /// <summary>
        /// Lista zawierająca możliwe do wyboru typy urlopów.
        /// </summary>
        private List<LeaveType> leaveTypes;

        /// <summary>
        /// Id pracownika, którego dotyczy wniosek.
        /// </summary>
        private int employeeId;

        /// <summary>
        /// Zmienna określająca czy formularz operuje na istniejącym urlopie i go
        /// modyfikuje (true), czy tworzy nowe zgłoszenie urlopowe (false).
        /// </summary>
        private bool editMode;

        /// <summary>
        /// Zmienna przechowująca datę rozpoczęcia urlopu z przed modyfikacji,
        /// o ile editMode == true;
        /// </summary>
        private DateTime oldFirstDay;

        /// <summary>
        /// Konstruktor przeznaczony do tworzenia instancji edytującej istniejący wpis
        /// urlopowy. Ustawia wartości w elementach zczytujących dane (np. textbox, combobox)
        /// zgodne z wartościami w edytowanym wpisie urlopowym.
        /// </summary>
        /// <param name="connection">Połączenie do bazy danych. Powinno być otwarte.</param>
        /// <param name="leaveDays">Dostępna liczba dni urlopowych (bez dni zaległych).</param>
        /// <param name="oldLeaveDays">Dostępna liczba zaległych dni urlopowych.</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie urlopowe.</param>
        /// <param name="leaveType">Typ edytowanego zgłoszenia urlopowego.</param>
        /// <param name="firstDay">Data rozpoczęcia urlopu w edytowanym zgłoszeniu urlopowym.</param>
        /// <param name="lastDay">Data zakończenia urlopu w edytowanym zgłoszeniu urlopowym.</param>
        /// <param name="remarks">Uwagi edytowanego zgłoszenia urlopowego.</param>
        /// <param name="leaveStatus">Stan edytowanego złoszenia urlopowego.</param>
        public FormLeaveApplication(SqlConnection connection, int leaveDays, int oldLeaveDays, int employeeId,
            String leaveType, DateTime firstDay, DateTime lastDay, String remarks, String leaveStatus)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
            this.editMode = true;
            this.oldFirstDay = firstDay;
            //Zczytanie listy typów urlopów i przypisanie do atrybutu klasy oraz do odpowiedniego
            //comboBox'a.
            comboBoxType.DataSource = leaveTypes = this.GetLeaveTypesList();
            //Zaznaczenie w comboBox'ie zawierającym typy urlopów typu edytowanego zgłoszenia.
            comboBoxType.SelectedIndex = comboBoxType.FindStringExact(leaveType);
            /* Ustawienie ograniczenia odnośnie możliwości wyboru dnia rozpoczęcia i zakończenia urlopu.
             * Jeżeli pierwszy dzień w zgłoszeniu już był (jest wcześniej niż teraz),
             * to najwcześniejszą możliwą datą jest ten właśnie dzień. W innym wypadku najwcześniejszą możliwą
             * datą jest dzisiaj.
             */
            if (firstDay.CompareTo(DateTime.Now) <= 0)
            {
                dateTimePickerFirstDay.MinDate = firstDay.Trim(TimeSpan.TicksPerDay);
                dateTimePickerLastDay.MinDate = firstDay.Trim(TimeSpan.TicksPerDay);
            }
            else
            {
                dateTimePickerLastDay.MinDate = dateTimePickerFirstDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay);
            }
            //Ograniczenie wyboru dnia rozpoczęcia i zakończenia. Najpóźniej na rok od dnia dzisiejszego.
            dateTimePickerFirstDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            dateTimePickerLastDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            /* Ustawienie wartości początkowej dla elementu wyboru dni rozpoczęcia i zakończenia
             * na dni rozpoczęcia i zakończenia w edytowanym zgłoszeniu.
             */
            dateTimePickerFirstDay.Value = firstDay;
            dateTimePickerLastDay.Value = lastDay;
            /* Jeżli zgłoszenie urlopowe jest typu, który konsumuje dni urlopowe, to następuje 
             * obliczenie i ustawienie wartości etykiety z liczbą zużywanych przez urlop dni.
             */
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                labelUsedDaysValue.Text = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value).ToString();
            }
            else
            {
                labelUsedDaysValue.Text = "0";
            }
            //Obliczenie i przypisanie do etykiety liczby dostępnych do zużycia dni.
            labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
            labelNormalValue.Text = leaveDays.ToString();
            labelOldValue.Text = oldLeaveDays.ToString();
            textBoxRemarks.Text = remarks;
            //Zczytanie listy statusów.
            List<String> statusList = this.GetStatusTypes();
            //Jeżeli status bieżącego urlopu nie jest approved, to usuwana jest możliwość wyboru tego statusu.
            if (!leaveStatus.Equals("Approved"))
            {
                statusList.Remove("Approved");
            }
            comboBoxStatus.DataSource = statusList;
            comboBoxStatus.SelectedIndex = comboBoxStatus.FindStringExact(leaveStatus);
        }

        /// <summary>
        /// Konstruktor przeznaczony do dodawania nowego zgłoszenia urlopowego.
        /// </summary>
        /// <param name="parent">Obiekt rodzica. Używany w celu określenia uprawnień.</param>
        /// <param name="connection">Połączenie do bazy danych. Powinno być otwarte.</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie urlopowe.</param>
        public FormLeaveApplication(object parent, SqlConnection connection, int employeeId)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
            this.editMode = false;
            //Zczytanie listy możliwych statusów urlopu.
            List<String> statusList = this.GetStatusTypes();
            //Zczytanie listy możliwych typów urlopu.
            leaveTypes = this.GetLeaveTypesList();
            //Usunięcie z listy statusów statusu odrzuconego. Nie można dodać zgłoszenia od razu odrzuconego.
            statusList.Remove("Rejected");
            //Tylko kierownik może dodać zgłoszenie od razu zatwierdzone.
            if (parent.GetType() != new FormManager().GetType())
            {
                statusList.Remove("Approved");
            }
            //Jeżeli zgłoszenie pochodzi od pracownika.
            if (parent.GetType() == new FormEmployee().GetType())
            {
                //Usunięcie możliwości dodania chorobowego przez zwykłego pracownika.
                leaveTypes.Remove(new LeaveType("Sick"));
                //Usunięcie możliwości wybrania statusu zgłoszenia przez zwykłego pracownika.
                labelStatus.Visible = false;
                comboBoxStatus.Visible = false;
            }
            else
            {
                //Przypisanie wartości do listy rozwijanej statusów.
                comboBoxStatus.DataSource = statusList;
            }
            //Przypisanie wartości do listy rozwijanej typów urlopów.
            comboBoxType.DataSource = leaveTypes;
            //Ustawienie ograniczeń dla daty rozpoczęcia i daty zakończenia urlopu.
            dateTimePickerFirstDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddDays(1);
            dateTimePickerFirstDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            dateTimePickerLastDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddDays(1);
            dateTimePickerLastDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            //Jeżeli zaznaczony typ urlopu konsumuje dni.
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                /* Obliczenie i przypisanie do etykiety liczby używanych dni przez okres zaznaczony w
                 * elementach dateTimePicker.
                 */
                labelUsedDaysValue.Text = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value).ToString();
            }
            else
            {
                //Jeżeli zaznaczony typ urlopu nie konsumuje dni, to ustawiamy 
                labelUsedDaysValue.Text = "0";
            }
            try
            {
                /* Zmienne do których zostaną zczytane wartości dostępnych dni urlopowych  
                 * z bierzącego roku, oraz zaległych dni urlopowych.
                 */
                int leaveDays = 0;
                int oldLeaveDays = 0;
                // Zczytanie wartości do zmiennych.
                this.getDays(employeeId, ref leaveDays, ref oldLeaveDays);
                //Przypisanie wartości do etykiet.
                labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
                labelNormalValue.Text = leaveDays.ToString();
                labelOldValue.Text = oldLeaveDays.ToString();
            }
            catch { }//todo obsługa wszystkich rodzajów wyjątków

        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika anulowania. Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //todo łądnie ogarnąć try i catche.
        /// <summary>
        /// Metoda obsługi zdarzenia wciśnięcia guzika ok.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            int numberOfUsedDays = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value);
            int leaveDays = 0;
            int oldLeaveDays = 0;
            int yearDays = 0;

            //Zczytanie aktualnych wartości dostępnych dni urlopowych.
            this.getDays(employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
            //Uaktualnienie warotości etykiet.
            labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
            labelNormalValue.Text = leaveDays.ToString();
            labelOldValue.Text = oldLeaveDays.ToString();
            //Sprawdzenie, czy nowe zgłoszenie nie wykorzystuje więcej dni, niż pracownik ma ich dostępnych.
            if (numberOfUsedDays <= oldLeaveDays + leaveDays)
            {
                //Sprawdzenie, czy zgłoszenie urlopowe wykorzystuje jakiekolwiek dni.
                if (numberOfUsedDays != 0)
                {
                    /* Sprawdzenie, czy zgłoszenie nie koliduje z już istniejącym.
                     * Jeżeli jest to nowe zgłoszenie (!editMode), to należy po prostu sprawdzić, czy 
                     * któryś z dni zgłoszenia nie jest już wykorzystany w innym zgłoszeniu.
                     * Jeżeli jest to zgłoszenie edytowane, to należy sprawdzić, czy któryś z dni zgłoszenia
                     * nie jest już wykorzystany w innym zgłoszeniu, ale należy pominąć sprawdzanie wpisu 
                     * edytowanego. 
                     * Jeżeli zgłoszenie jest zgłoszeniem chorobowego, to może kolidować z innymi zgłoszeniami.
                     */
                    if ((!editMode && !this.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, employeeId))
                        || (editMode && !this.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, employeeId, oldFirstDay))
                        || comboBoxType.SelectedItem.ToString().Equals("Sick"))
                    {
                        //Stworzenie nowego obiektu urlopu.
                        Leave leave = new Leave(employeeId, comboBoxType.SelectedItem.ToString(),
                            comboBoxStatus.SelectedItem.ToString(), dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, textBoxRemarks.Text);
                        if (editMode)
                        {
                            try
                            {
                                this.EditLeave(leave, oldFirstDay);
                                this.Close();
                            }
                            catch//todo obsłużyć wszystkie wyjątki.
                            {
                                throw new NotImplementedException();
                            }
                        }
                        else
                        {                            
                            //Jeżeli wybrano zgłoszenie chorobowe.
                            if (comboBoxType.SelectedItem.ToString().Equals("Sick"))
                            {
                                try
                                {
                                    //Dodanie urlopu.
                                    this.addSickLeave(leave);
                                    this.Close();
                                }
                                catch//todo obsłużyć wszystkie wyjątki.
                                {
                                    throw new NotImplementedException();
                                }
                            }
                            else
                            {
                                try
                                {
                                    this.addLeave(leave);
                                    this.Close();
                                }
                                catch 
                                {
                                    throw new NotImplementedException();
                                }//todo obsługa wszystkich wyjątków.
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("At least one day from selected period is already used for leave.");
                    }
                }
                else
                {
                    MessageBox.Show("No work days selected.");
                }
            }

        }

        /// <summary>
        /// Metoda obsługi zdarzenia zmiany wartości w elemencie wyboru
        /// daty rozpoczęcia urlopu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void dateTimePickerFirstDay_ValueChanged(object sender, EventArgs e)
        {
            //Ustawienie ograniczenia minimalnej daty zakończenia urlopu na datę rozpoczęcia.
            dateTimePickerLastDay.MinDate = dateTimePickerFirstDay.Value;
            //Aktualizacja wartości etykiety używanych dni.
            updateLabelUsedDaysValue();
        }

        /// <summary>
        /// Metoda służąca do aktualizacji wartości etykiety używanych przez urlop dni urlopowych.
        /// </summary>
        private void updateLabelUsedDaysValue()
        {
            //Jeżeli wybrany typ urlopu konsumuje dni urlopowe.
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                //Aktualizacja etykiety 
                labelUsedDaysValue.Text = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value).ToString();
            }
            else
            {
                labelUsedDaysValue.Text = "0";
            }
        }

        /// <summary>
        /// Metoda obsługi zdarzenia zmiany wartości w elemencie służącym do wprowadzenia daty
        /// zakończenia urlopu. Wywołuje metodę aktualizacji etykiety używanych dni urlopowych.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void dateTimePickerLastDay_ValueChanged(object sender, EventArgs e)
        {
            updateLabelUsedDaysValue();
        }

        /// <summary>
        /// Metoda obsługi zdarzenia zmiany wyboru w liście rozwijanej zawierającej
        /// typy urlopów. Sprawdza, czy wybrany typ to chorobowe i jeżeli tak, to
        /// wyłacza możliwość korzystania z listy rozwijanej zawierającej statusy.
        /// Chorobowe ma zawsze status zatwierdzony ("Approved"). 
        /// Metoda ta powoduje również aktualizację etykiety używanych dni,
        /// ponieważ niektóre typy urlopów nie konsumują dni.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Aktualizacja etykiety używanych dni urlopu.
            updateLabelUsedDaysValue();
            //Jeżeli wybrany typ urlopu to chorobowe.
            if (comboBoxType.SelectedItem.ToString().Equals("Sick"))
            {
                //Zablokuj listę rozwijaną stanów urlopów.
                comboBoxStatus.Enabled = false;
            }
            else
            {
                //Odblokuj listę rozwijaną stanów urlopów.
                comboBoxStatus.Enabled = true;
            }
        }
    }
}
