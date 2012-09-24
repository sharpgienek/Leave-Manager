using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
namespace leave_manager
{
    //public abstract class LeaveManagerForm : Form
    //todo abstract jw. Gdy się tak zrobi to przestaje działać designer.

    /// <summary>
    /// Klasa rodzić dla wszystkich formularzy tego projektu.
    /// </summary>
    public class LeaveManagerForm : Form
    {
        public LeaveManagerForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Połączenie z bazą sql. 
        /// </summary>
        /// 
        protected SqlConnection connection;

        /// <summary>
        /// Właściwość zwracająca obiekt połączenia z bazą sql.
        /// </summary>
        public SqlConnection Connection { get { return connection; } }

        /// <summary>
        /// Czy transakcja z bazą sql jest włączona.
        /// </summary>
        private bool transactionOn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;

        /// <summary>
        /// Właściwość zwracająca informację o tym, czy transakcja z bazą sql jest włączona.
        /// </summary>
        public bool TransactionOn { get { return transactionOn; } }

        /// <summary>
        /// Transakcja z bazą sql. Jeżeli transakcja nie jest włączona == null.
        /// </summary>
        private SqlTransaction transaction;

        /// <summary>
        /// Właściwość zwracająca obiekt transakcji sql, gdy ta jest włączona. 
        /// Gdy nie jest włączona zwraca null.
        /// </summary>
        public SqlTransaction Transaction { get { return transaction; } }   

        /// <summary>
        /// Metoda rozpoczęcia transakcji przez formularz.
        /// </summary>
        public void BeginTransaction()
        {
            //Rozpoczęcie transakcji.
            transaction = connection.BeginTransaction();
            //Zapisanie informacji o tym, że transakcja jest rozpoczęta.
            transactionOn = true;
        }

        /// <summary>
        /// Zatwierdzenie transakcji sql.
        /// </summary>
        public void CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
            transactionOn = false;
        }

        /// <summary>
        /// Rozpoczęcie transakcji sql o podany poziomie izolacji.
        /// </summary>
        /// <param name="isolationLeavel">Poziom izolacji transakcji.</param>
        public void BeginTransaction(IsolationLevel isolationLeavel)
        {
            transaction = connection.BeginTransaction(isolationLeavel);
            transactionOn = true;
        }

        /// <summary>
        /// Cofnięcie zmian transakcji.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                transaction.Rollback();
            }
            catch { }
            transaction = null;
            transactionOn = false;
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(282, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // LeaveManagerForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LeaveManagerForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("index.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot find help file");
            }
        }
    }
}
