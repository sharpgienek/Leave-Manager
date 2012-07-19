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
    /// Klasa formatki służącej do tworzenia planu pracy dla pracownika
    /// </summary>
    public partial class FormWorkSchedule : LeaveManagerForm
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="con">Połączenie z bazą danych</param>
        public FormWorkSchedule(SqlConnection con)
        {
            InitializeComponent();
            dataGridViewWorkPlan.Columns.Add("Monday", "Monday");
            dataGridViewWorkPlan.Columns.Add("Tuesday", "Tuesday");
            dataGridViewWorkPlan.Columns.Add("Wednesday", "Wednesday");
            dataGridViewWorkPlan.Columns.Add("Thursday", "Thursday");
            dataGridViewWorkPlan.Columns.Add("Friday", "Friday");
            dataGridViewWorkPlan.Columns.Add("Saturday", "Saturday");
            dataGridViewWorkPlan.Columns.Add("Sunday", "Sunday");
            dataGridViewWorkPlan.Rows.Add(22);
            int hour = 7;
            for (int i = 0; i < 23; ++i)
            {
                if (i % 2 == 0)
                    dataGridViewWorkPlan.Rows[i].HeaderCell.Value = hour.ToString() + ".00";
                else
                {
                    dataGridViewWorkPlan.Rows[i].HeaderCell.Value = hour.ToString() + ".30";
                    hour++;
                }
            }
            DateTime start, end;
            start = DateTime.Now.StartOfWeek();
            end = start.AddDays(6);
            for (int i = 0; i < 5; ++i)
            {
                comboBoxWeek.Items.Add(start.ToString("dd.MM") + " - " + end.ToString("dd.MM"));
                start = start.AddDays(7);
                end = end.AddDays(7);
            }
            comboBoxWeek.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Metoda rozszerza klase DateTime o możliwość wyznaczenia daty pierwszego dnia tygodnia
        /// </summary>
        /// <param name="dt">obiekt wywołujący</param>
        /// <returns>Data pierwszego dnia obecnego tygodnia</returns>
        public static DateTime StartOfWeek(this DateTime dt)
        {
            int diff = dt.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}
