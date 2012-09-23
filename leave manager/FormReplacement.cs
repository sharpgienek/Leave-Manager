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
    public partial class FormReplacement : LeaveManagerForm
    {
        int position;
        DateTime date;
        /// <summary>
        /// Konstruktor, ustawia wszystkie niezbędne pola
        /// </summary>
        /// <param name="positionDesc">Opis pozycji pracownika dla, którego szukamy zastępstwa</param>
        /// <param name="tmpDay">Data</param>
        /// <param name="connection">Połączenie z bazą danych</param>
        public FormReplacement(string positionDesc, DateTime tmpDay, SqlConnection connection)
        {
            this.connection = connection;
            position = this.GetPositionID(positionDesc);
            date = tmpDay; 
            InitializeComponent();
            labelDayValue.Text = date.ToString("yyyy-MM-dd");
            labelPositionValue.Text = positionDesc;
            RefreshDataGrid();
        }

        /// <summary>
        /// Anuluje akcje.
        /// </summary>
        /// <param name="sender">Obiekt zgłaszający zdarzenie</param>
        /// <param name="e">Argumenty</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Odświerza tabelę danych wyszukując pracowników mogących zastąpić pracownika w danym dniu.
        /// </summary>
        private void RefreshDataGrid()
        {
            try
            {
                dataGridView.DataSource = this.GetFreeWorkers(position, date, "", "");
                dataGridView.Columns["Employee id"].Visible = false;
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL error. Please try connection to database or try again later");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }
        }

        /// <summary>
        /// Wyszukuje pracowników na zastępstwo o konkretnym imieniu i nazwisku.
        /// </summary>
        /// <param name="sender">Obiekt zgłaszający zdarzenie</param>
        /// <param name="e">Argumenty</param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string name, surname;
            name = textBoxName.Text;
            surname = textBoxSurname.Text;
            try
            {
                dataGridView.DataSource = this.GetFreeWorkers(position, date, name , surname);
                dataGridView.Columns["Employee id"].Visible = false;
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL error. Please try connection to database or try again later");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }

        }

        /// <summary>
        /// Dodaje zaznaczonych pracowników jako zastępstwo
        /// </summary>
        /// <param name="sender">Obiekt zgłaszający zdarzenie</param>
        /// <param name="e">Argumenty</param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej wiersz.
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridView.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza.
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                //Dodajemy zastępstwo.
                try
                {
                    this.AddReplacment((int)row.Cells["Employee id"].Value, date);
                }
                catch (SqlException)
                {
                    MessageBox.Show("SQL error. Please try connection to database or try again later");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown exception has occured" + ex.Message);
                }
            }            
            Close();
        }
    }
}
