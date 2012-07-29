using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using leave_manager.Exceptions;

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
        /// Zmienna przechowująca numer id modyfikowanego urlopu,
        /// o ile editMode == true;
        /// </summary>
       // private int oldLeaveId;

        private Leave editedLeave;

        /// <summary>
        /// Konstruktor przeznaczony do tworzenia instancji edytującej istniejący wpis
        /// urlopowy. Ustawia wartości w elementach zczytujących dane (np. textbox, combobox)
        /// zgodne z wartościami w edytowanym wpisie urlopowym.
        /// </summary>
        /// <param name="connection">Połączenie do bazy danych. Powinno być otwarte.</param>
        /// <param name="leaveDaysList">Dostępna liczba dni urlopowych (bez dni zaległych).</param>
        /// <param name="oldLeaveDaysList">Dostępna liczba zaległych dni urlopowych.</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy zgłoszenie urlopowe.</param>
        /// <param name="editedLeave">Obiekt reprezentujący edytowany wpis urlopowy.</param>
        public FormLeaveApplication(SqlConnection connection, Leave editedLeave)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = editedLeave.EmployeeId;
            this.editMode = true;
            this.editedLeave = editedLeave;
            //Zczytanie listy typów urlopów i przypisanie do atrybutu klasy oraz do odpowiedniego
            //comboBox'a.
            try
            {
                comboBoxType.DataSource = leaveTypes = this.GetLeaveTypesList();
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error. This form will be close. Please try again later.");
                this.Close();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. This form will be close. Please try again later");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
                this.Close();
            }
            //Zaznaczenie w comboBox'ie zawierającym typy urlopów typu edytowanego zgłoszenia.
            comboBoxType.SelectedIndex = comboBoxType.FindStringExact(editedLeave.LeaveType);
            /* Ustawienie ograniczenia odnośnie możliwości wyboru dnia rozpoczęcia i zakończenia urlopu.
             * Jeżeli pierwszy dzień w zgłoszeniu już był (jest wcześniej niż teraz),
             * to najwcześniejszą możliwą datą jest ten właśnie dzień. W innym wypadku najwcześniejszą możliwą
             * datą jest dzisiaj.
             */
            if (editedLeave.FirstDay.CompareTo(DateTime.Now) <= 0)
            {
                dateTimePickerFirstDay.MinDate = editedLeave.FirstDay.Trim(TimeSpan.TicksPerDay);
                dateTimePickerLastDay.MinDate = editedLeave.FirstDay.Trim(TimeSpan.TicksPerDay);
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
            dateTimePickerFirstDay.Value = editedLeave.FirstDay;
            dateTimePickerLastDay.Value = editedLeave.LastDay;
            /* Jeżli zgłoszenie urlopowe jest typu, który konsumuje dni urlopowe, to następuje 
             * obliczenie i ustawienie wartości etykiety z liczbą zużywanych przez urlop dni.
             */
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                labelUsedDaysValue.Text = this.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value, employeeId).ToString();
            }
            else
            {
                labelUsedDaysValue.Text = "0";
            }
            int oldLeaveDays = 0;
            int leaveDays = 0;
            this.GetDays(this.employeeId, ref leaveDays, ref oldLeaveDays);
            //Obliczenie i przypisanie do etykiety liczby dostępnych do zużycia dni.
            labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
            labelNormalValue.Text = leaveDays.ToString();
            labelOldValue.Text = oldLeaveDays.ToString();
            textBoxRemarks.Text = editedLeave.Remarks;
            //Zczytanie listy statusów.
            List<String> statusList = this.GetStatusTypes();
            //Jeżeli status bieżącego urlopu nie jest approved, to usuwana jest możliwość wyboru tego statusu.
            if (!editedLeave.LeaveStatus.Equals("Approved"))
            {
                statusList.Remove("Approved");
            }
            comboBoxStatus.DataSource = statusList;
            comboBoxStatus.SelectedIndex = comboBoxStatus.FindStringExact(editedLeave.LeaveStatus);
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

            dateTimePickerFirstDay.CustomFormat = "hh";

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
                labelUsedDaysValue.Text = this.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value, employeeId).ToString();
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
                this.GetDays(employeeId, ref leaveDays, ref oldLeaveDays);
                //Przypisanie wartości do etykiet.
                labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
                labelNormalValue.Text = leaveDays.ToString();
                labelOldValue.Text = oldLeaveDays.ToString();
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error. This form will be close. Please try again later.");
                this.Close();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. This form will be close. Please try again later");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
                this.Close();
            }

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
            int numberOfUsedDays = this.GetNumberOfWorkDays(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, employeeId);
            int leaveDays = 0;
            int oldLeaveDays = 0;
            int yearDays = 0;

            //Zczytanie aktualnych wartości dostępnych dni urlopowych.
            try
            {
                this.GetDays(employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays);
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL error. Please try connection to database or try again later");
                return;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
                return;
            }
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
                        || (editMode && !this.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, employeeId, editedLeave.FirstDay))
                        || comboBoxType.SelectedItem.ToString().Equals("Sick"))
                    {
                        string choosenStatus;
                        if (comboBoxStatus.SelectedItem != null)
                            choosenStatus = comboBoxStatus.SelectedItem.ToString();
                        else
                            choosenStatus = "Pending validation";
                        //Stworzenie nowego obiektu urlopu.
                        Leave leave = new Leave(employeeId, comboBoxType.SelectedItem.ToString(),
                            choosenStatus, dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, textBoxRemarks.Text);
                        if (editMode)
                        {
                            try
                            {
                                leave.Id = editedLeave.Id;
                                this.EditLeave(leave);
                                this.Close();
                            }
                            catch (SqlException)
                            {
                                MessageBox.Show("SQL error. Please try connection to database or try again later");
                            }
                            catch (InvalidOperationException)
                            {
                                MessageBox.Show("Invalid operation. Please try again later.");
                            }
                            catch (IsolationLevelException)
                            {
                                MessageBox.Show("Isolation level error. Please try again later or contact administrator");
                            }
                            catch (ArgumentException)
                            {
                                MessageBox.Show("Wrong argument\n");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Unknown exception has occured" + ex.Message);
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
                                    this.AddSickLeave(leave);
                                    this.Close();
                                }
                                catch (SqlException)
                                {
                                    MessageBox.Show("SQL error. Please try connection to database or try again later");
                                }
                                catch (InvalidOperationException)
                                {
                                    MessageBox.Show("Invalid operation. Please try again later.");
                                }
                                catch (IsolationLevelException)
                                {
                                    MessageBox.Show("Isolation level error. Please try again later or contact administrator");
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    MessageBox.Show("Wrong argument\n");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Unknown exception has occured" + ex.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    this.AddLeave(leave);
                                    this.Close();
                                }
                                catch (OverflowException)
                                {
                                    MessageBox.Show("You don't have enough extraordinary days left.");
                                }
                                catch (SqlException)
                                {
                                    MessageBox.Show("SQL error. Please try connection to database or try again later");
                                }
                                catch (InvalidOperationException)
                                {
                                    MessageBox.Show("Invalid operation. Please try again later.");
                                }
                                catch (IsolationLevelException)
                                {
                                    MessageBox.Show("Isolation level error. Please try again later or contact administrator");
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    MessageBox.Show("Wrong argument\n");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Unknown exception has occured" + ex.Message);
                                }
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
            UpdateLabelUsedDaysValue();
        }

        /// <summary>
        /// Metoda służąca do aktualizacji wartości etykiety używanych przez urlop dni urlopowych.
        /// </summary>
        private void UpdateLabelUsedDaysValue()
        {
            //Jeżeli wybrany typ urlopu konsumuje dni urlopowe.
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                //Aktualizacja etykiety 
                labelUsedDaysValue.Text = this.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value, employeeId).ToString();
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
            UpdateLabelUsedDaysValue();
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
            UpdateLabelUsedDaysValue();
            //Jeżeli wybrany typ urlopu to chorobowe.
            if (comboBoxType.SelectedItem.ToString().Equals("Sick") ||
                comboBoxType.SelectedItem.ToString().Equals("Extraordinary"))
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
