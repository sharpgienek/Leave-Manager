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
    /// Klasa formularza dodawania nowego rodzaju pozycji pracowników.
    /// </summary>
    public partial class FormAddPosition : LeaveManagerForm
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie do bazy danych. Powinno być otwarte.</param>
        public FormAddPosition(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia guzika anulowania. Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia guzika dodawania nowego 
        /// rodzaju pozycji pracownika.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Sprawdzenie, czy nazwa dodawanego rodzaju pozycji nie jest pusta.
            if (textBoxName.Text.Length != 0)
            {
                try
                {
                    //Próba dodania nowej pozycji.
                    this.AddPositionType(textBoxName.Text);
                    this.Close();
                }//todo obsłużyć wszystkie wyjątki
                catch 
                {
                    throw new NotImplementedException();
                }                
            }
            else
            {
                MessageBox.Show("Name field empty.");
            }
        }
    }
}
