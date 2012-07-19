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
              hours.Add(i.ToString() + ":00");
              hours.Add(i.ToString() + ":30");
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
