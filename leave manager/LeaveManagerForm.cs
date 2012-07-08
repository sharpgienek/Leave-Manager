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
    //public abstract class LeaveManagerForm : Form
    //todo abstract jw. Gdy się tak zrobi to przestaje działać designer.

    /// <summary>
    /// Klasa rodzić dla wszystkich formularzy tego projektu.
    /// </summary>
    public class LeaveManagerForm : Form
    {
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
    }
}
