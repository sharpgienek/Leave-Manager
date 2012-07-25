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
    /// <summary>
    /// Klasa formatki służącej do tworzenia planu pracy dla pracownika.
    /// </summary>
    public partial class FormWorkSchedule : LeaveManagerForm
    {
        /// <summary>
        /// Numer id pracownika, którego dotyczy harmonogram.
        /// </summary>
        int employeeId;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych</param>
        /// <param name="employeeId">Numer id pracownika, którego dotyczy harmonogram.</param>
        public FormWorkSchedule(SqlConnection connection, int employeeId)
        {
            this.employeeId = employeeId;
            this.connection = connection;
            InitializeComponent();
            List<string> hours = new List<string>();
            for (int i = 0; i < 23; ++i)
            {
                hours.Add(string.Format("{0:00}", i) + ":00");
                hours.Add(string.Format("{0:00}", i) + ":30");
            }            

            comboBoxMondayEndHour.DataSource = hours.ToArray();           
            comboBoxTuesdayEndHour.DataSource = hours.ToArray();
            comboBoxWednesdayEndHour.DataSource = hours.ToArray();
            comboBoxThursdayEndHour.DataSource = hours.ToArray();
            comboBoxFridayEndHour.DataSource = hours.ToArray();
            comboBoxSaturdayEndHour.DataSource = hours.ToArray();
            comboBoxSundayEndHour.DataSource = hours.ToArray();

            comboBoxMondayStartingHour.DataSource = hours.ToArray();
            comboBoxTuesdayStartingHour.DataSource = hours.ToArray();
            comboBoxWednesdayStartingHour.DataSource = hours.ToArray();
            comboBoxThursdayStartingHour.DataSource = hours.ToArray();
            comboBoxFridayStartingHour.DataSource = hours.ToArray();
            comboBoxSaturdayStartingHour.DataSource = hours.ToArray();
            comboBoxSundayStartingHour.DataSource = hours.ToArray();
           
            DataTable schedule = this.GetSchedule(employeeId);
            /*Zaznaczamy w comboboxach godziny, które są aktualnie zapisane w bazie danych.
            Z pobranej godziny interesuje nas 5 pierwszych znaków z tąd Substring.*/
            if (schedule.Rows.Count > 0)
            {
                comboBoxMondayStartingHour.SelectedIndex = comboBoxMondayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(0).ToString().Substring(0, 5));
                comboBoxTuesdayStartingHour.SelectedIndex = comboBoxTuesdayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(2).ToString().Substring(0, 5));
                comboBoxWednesdayStartingHour.SelectedIndex = comboBoxWednesdayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(4).ToString().Substring(0, 5));
                comboBoxThursdayStartingHour.SelectedIndex = comboBoxThursdayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(6).ToString().Substring(0, 5));
                comboBoxFridayStartingHour.SelectedIndex = comboBoxFridayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(8).ToString().Substring(0, 5));
                comboBoxSaturdayStartingHour.SelectedIndex = comboBoxSaturdayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(10).ToString().Substring(0, 5));
                comboBoxSundayStartingHour.SelectedIndex = comboBoxSundayStartingHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(12).ToString().Substring(0, 5));

                comboBoxMondayEndHour.SelectedIndex = comboBoxMondayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(1).ToString().Substring(0, 5));
                comboBoxTuesdayEndHour.SelectedIndex = comboBoxTuesdayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(3).ToString().Substring(0, 5));
                comboBoxWednesdayEndHour.SelectedIndex = comboBoxWednesdayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(5).ToString().Substring(0, 5));
                comboBoxThursdayEndHour.SelectedIndex = comboBoxThursdayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(7).ToString().Substring(0, 5));
                comboBoxFridayEndHour.SelectedIndex = comboBoxFridayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(9).ToString().Substring(0, 5));
                comboBoxSaturdayEndHour.SelectedIndex = comboBoxSaturdayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(11).ToString().Substring(0, 5));
                comboBoxSundayEndHour.SelectedIndex = comboBoxSundayEndHour.FindString(
                    schedule.Rows[0].ItemArray.GetValue(13).ToString().Substring(0, 5));
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxMondayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMondayEndHour.SelectedIndex < comboBoxMondayStartingHour.SelectedIndex)
            {
                comboBoxMondayEndHour.SelectedIndex = comboBoxMondayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxMondayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMondayEndHour.SelectedIndex < comboBoxMondayStartingHour.SelectedIndex)
            {
                comboBoxMondayEndHour.SelectedIndex = comboBoxMondayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxTuesdayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTuesdayEndHour.SelectedIndex < comboBoxTuesdayStartingHour.SelectedIndex)
            {
                comboBoxTuesdayEndHour.SelectedIndex = comboBoxTuesdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxTuesdayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTuesdayEndHour.SelectedIndex < comboBoxTuesdayStartingHour.SelectedIndex)
            {
                comboBoxTuesdayEndHour.SelectedIndex = comboBoxTuesdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxWednesdayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWednesdayEndHour.SelectedIndex < comboBoxWednesdayStartingHour.SelectedIndex)
            {
                comboBoxWednesdayEndHour.SelectedIndex = comboBoxWednesdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxWednesdayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWednesdayEndHour.SelectedIndex < comboBoxWednesdayStartingHour.SelectedIndex)
            {
                comboBoxWednesdayEndHour.SelectedIndex = comboBoxWednesdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxThursdayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxThursdayEndHour.SelectedIndex < comboBoxThursdayStartingHour.SelectedIndex)
            {
                comboBoxThursdayEndHour.SelectedIndex = comboBoxThursdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxThursdayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxThursdayEndHour.SelectedIndex < comboBoxThursdayStartingHour.SelectedIndex)
            {
                comboBoxThursdayEndHour.SelectedIndex = comboBoxThursdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxFridayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFridayEndHour.SelectedIndex < comboBoxFridayStartingHour.SelectedIndex)
            {
                comboBoxFridayEndHour.SelectedIndex = comboBoxFridayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxFridayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFridayEndHour.SelectedIndex < comboBoxFridayStartingHour.SelectedIndex)
            {
                comboBoxFridayEndHour.SelectedIndex = comboBoxFridayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxSaturdayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSaturdayEndHour.SelectedIndex < comboBoxSaturdayStartingHour.SelectedIndex)
            {
                comboBoxSaturdayEndHour.SelectedIndex = comboBoxSaturdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxSaturdayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSaturdayEndHour.SelectedIndex < comboBoxSaturdayStartingHour.SelectedIndex)
            {
                comboBoxSaturdayEndHour.SelectedIndex = comboBoxSaturdayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxSundayStartingHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSundayEndHour.SelectedIndex < comboBoxSundayStartingHour.SelectedIndex)
            {
                comboBoxSundayEndHour.SelectedIndex = comboBoxSundayStartingHour.SelectedIndex;
            }
        }

        private void comboBoxSundayEndHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSundayEndHour.SelectedIndex < comboBoxSundayStartingHour.SelectedIndex)
            {
                comboBoxSundayEndHour.SelectedIndex = comboBoxSundayStartingHour.SelectedIndex;
            }
        }

        private void buttonSetWorkPlan_Click(object sender, EventArgs e)
        {
            string[] hours = 
            {
                comboBoxMondayStartingHour.SelectedItem.ToString(),
                comboBoxMondayEndHour.SelectedItem.ToString(),

                comboBoxTuesdayStartingHour.SelectedItem.ToString(),
                comboBoxTuesdayEndHour.SelectedItem.ToString(),

                comboBoxWednesdayStartingHour.SelectedItem.ToString(),
                comboBoxWednesdayEndHour.SelectedItem.ToString(),

                comboBoxThursdayStartingHour.SelectedItem.ToString(),
                comboBoxThursdayEndHour.SelectedItem.ToString(),

                comboBoxFridayStartingHour.SelectedItem.ToString(),
                comboBoxFridayEndHour.SelectedItem.ToString(),

                comboBoxSaturdayStartingHour.SelectedItem.ToString(),
                comboBoxSaturdayEndHour.SelectedItem.ToString(),

                comboBoxSundayStartingHour.SelectedItem.ToString(),
                comboBoxSundayEndHour.SelectedItem.ToString(),
            };
            try
            {
                this.SetSchedule(hours, this.employeeId);
                this.Close();
            }
            catch//todo obsługa wszystkich wyjątków.
            {
                throw new NotImplementedException();
            }
        }
    }
}
