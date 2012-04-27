namespace leave_manager
{
    partial class FormReplacement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelPositionValue = new System.Windows.Forms.Label();
            this.labelDayValue = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPesel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnSurname,
            this.ColumnBirthDate,
            this.ColumnAdress,
            this.ColumnPesel,
            this.ColumnPosition});
            this.dataGridView.Location = new System.Drawing.Point(17, 141);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(643, 222);
            this.dataGridView.TabIndex = 16;
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Location = new System.Drawing.Point(413, 85);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(75, 36);
            this.buttonShowAll.TabIndex = 15;
            this.buttonShowAll.Text = "Show all employees";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(305, 92);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 14;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(84, 94);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(175, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(14, 97);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(64, 13);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Enter name:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(14, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(575, 13);
            this.labelInfo.TabIndex = 9;
            this.labelInfo.Text = "You are selecting replacement. Make sure, that employee you select doesn\'t alread" +
    "y work at shift, that need replacement";
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(17, 37);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(32, 13);
            this.labelDay.TabIndex = 18;
            this.labelDay.Text = "Day: ";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(17, 54);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(47, 13);
            this.labelPosition.TabIndex = 19;
            this.labelPosition.Text = "Position:";
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(71, 54);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(39, 13);
            this.labelPositionValue.TabIndex = 20;
            this.labelPositionValue.Text = "Doctor";
            // 
            // labelDayValue
            // 
            this.labelDayValue.AutoSize = true;
            this.labelDayValue.Location = new System.Drawing.Point(71, 37);
            this.labelDayValue.Name = "labelDayValue";
            this.labelDayValue.Size = new System.Drawing.Size(61, 13);
            this.labelDayValue.TabIndex = 21;
            this.labelDayValue.Text = "28-08-2012";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(553, 397);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(44, 397);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 23;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            // 
            // ColumnSurname
            // 
            this.ColumnSurname.HeaderText = "Surname";
            this.ColumnSurname.Name = "ColumnSurname";
            // 
            // ColumnBirthDate
            // 
            this.ColumnBirthDate.HeaderText = "Birth date";
            this.ColumnBirthDate.Name = "ColumnBirthDate";
            // 
            // ColumnAdress
            // 
            this.ColumnAdress.HeaderText = "Adress";
            this.ColumnAdress.Name = "ColumnAdress";
            // 
            // ColumnPesel
            // 
            this.ColumnPesel.HeaderText = "PESEL";
            this.ColumnPesel.Name = "ColumnPesel";
            // 
            // ColumnPosition
            // 
            this.ColumnPosition.HeaderText = "Position";
            this.ColumnPosition.Name = "ColumnPosition";
            // 
            // FormReplacement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 445);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelDayValue);
            this.Controls.Add(this.labelPositionValue);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelDay);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelInfo);
            this.Name = "FormReplacement";
            this.Text = "Leave Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonShowAll;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelPositionValue;
        private System.Windows.Forms.Label labelDayValue;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPesel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPosition;
    }
}