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
    public class LeaveManagerForm : Form
    {
        protected SqlConnection connection;
        public bool TransactionOn { get { return transactionOn; } }
        private SqlTransaction transaction;
        public SqlTransaction Transaction { get { return transaction; } }
        private bool transactionOn;
        public SqlConnection Connection { get { return connection; } }

        protected void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
            transactionOn = true;
        }

        protected void CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
            transactionOn = false;
        }

        protected void BeginTransaction(IsolationLevel isolationLeavel)
        {
            transaction = connection.BeginTransaction(isolationLeavel);
            transactionOn = true;
        }

        protected void RollbackTransaction()
        {
            transaction.Rollback();
            transaction = null;
            transactionOn = false;
        }
    }
}
