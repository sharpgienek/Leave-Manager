namespace leave_manager
{
    partial class FormLeaveConsideration
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelFirstDay = new System.Windows.Forms.Label();
            this.labelLastDay = new System.Windows.Forms.Label();
            this.groupBoxEmployee = new System.Windows.Forms.GroupBox();
            this.labelNameValue = new System.Windows.Forms.Label();
            this.labelPositionValue = new System.Windows.Forms.Label();
            this.labelFirstDayValue = new System.Windows.Forms.Label();
            this.labelLastDayValue = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWorkersAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWorkersNeeded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelDependencies = new System.Windows.Forms.Label();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonReject = new System.Windows.Forms.Button();
            this.buttonLeaveUnchanged = new System.Windows.Forms.Button();
            this.groupBoxEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(13, 13);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(181, 13);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "You are considering leave aplication:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 16);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(6, 29);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(47, 13);
            this.labelPosition.TabIndex = 2;
            this.labelPosition.Text = "Position:";
            // 
            // labelFirstDay
            // 
            this.labelFirstDay.AutoSize = true;
            this.labelFirstDay.Location = new System.Drawing.Point(6, 42);
            this.labelFirstDay.Name = "labelFirstDay";
            this.labelFirstDay.Size = new System.Drawing.Size(49, 13);
            this.labelFirstDay.TabIndex = 3;
            this.labelFirstDay.Text = "First day:";
            // 
            // labelLastDay
            // 
            this.labelLastDay.AutoSize = true;
            this.labelLastDay.Location = new System.Drawing.Point(6, 55);
            this.labelLastDay.Name = "labelLastDay";
            this.labelLastDay.Size = new System.Drawing.Size(50, 13);
            this.labelLastDay.TabIndex = 4;
            this.labelLastDay.Text = "Last day:";
            // 
            // groupBoxEmployee
            // 
            this.groupBoxEmployee.Controls.Add(this.labelNameValue);
            this.groupBoxEmployee.Controls.Add(this.labelPositionValue);
            this.groupBoxEmployee.Controls.Add(this.labelFirstDayValue);
            this.groupBoxEmployee.Controls.Add(this.labelLastDayValue);
            this.groupBoxEmployee.Controls.Add(this.labelName);
            this.groupBoxEmployee.Controls.Add(this.labelLastDay);
            this.groupBoxEmployee.Controls.Add(this.labelPosition);
            this.groupBoxEmployee.Controls.Add(this.labelFirstDay);
            this.groupBoxEmployee.Location = new System.Drawing.Point(16, 38);
            this.groupBoxEmployee.Name = "groupBoxEmployee";
            this.groupBoxEmployee.Size = new System.Drawing.Size(171, 82);
            this.groupBoxEmployee.TabIndex = 5;
            this.groupBoxEmployee.TabStop = false;
            this.groupBoxEmployee.Text = "Employee data";
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Location = new System.Drawing.Point(63, 16);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(69, 13);
            this.labelNameValue.TabIndex = 8;
            this.labelNameValue.Text = "Jan Kowalski";
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(63, 29);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(39, 13);
            this.labelPositionValue.TabIndex = 7;
            this.labelPositionValue.Text = "Doctor";
            // 
            // labelFirstDayValue
            // 
            this.labelFirstDayValue.AutoSize = true;
            this.labelFirstDayValue.Location = new System.Drawing.Point(63, 42);
            this.labelFirstDayValue.Name = "labelFirstDayValue";
            this.labelFirstDayValue.Size = new System.Drawing.Size(61, 13);
            this.labelFirstDayValue.TabIndex = 6;
            this.labelFirstDayValue.Text = "24-07-2012";
            // 
            // labelLastDayValue
            // 
            this.labelLastDayValue.AutoSize = true;
            this.labelLastDayValue.Location = new System.Drawing.Point(63, 55);
            this.labelLastDayValue.Name = "labelLastDayValue";
            this.labelLastDayValue.Size = new System.Drawing.Size(61, 13);
            this.labelLastDayValue.TabIndex = 5;
            this.labelLastDayValue.Text = "27-07-2012";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDay,
            this.ColumnWorkersAvailable,
            this.ColumnWorkersNeeded});
            this.dataGridView.Location = new System.Drawing.Point(16, 151);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(344, 236);
            this.dataGridView.TabIndex = 0;
            // 
            // ColumnDay
            // 
            this.ColumnDay.HeaderText = "Day";
            this.ColumnDay.Name = "ColumnDay";
            // 
            // ColumnWorkersAvailable
            // 
            this.ColumnWorkersAvailable.HeaderText = "No. similar workers available";
            this.ColumnWorkersAvailable.Name = "ColumnWorkersAvailable";
            // 
            // ColumnWorkersNeeded
            // 
            this.ColumnWorkersNeeded.HeaderText = "No. similar workers needed";
            this.ColumnWorkersNeeded.Name = "ColumnWorkersNeeded";
            // 
            // labelDependencies
            // 
            this.labelDependencies.AutoSize = true;
            this.labelDependencies.Location = new System.Drawing.Point(16, 132);
            this.labelDependencies.Name = "labelDependencies";
            this.labelDependencies.Size = new System.Drawing.Size(76, 13);
            this.labelDependencies.TabIndex = 6;
            this.labelDependencies.Text = "Dependencies";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(16, 431);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 7;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonReject
            // 
            this.buttonReject.Location = new System.Drawing.Point(144, 431);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(75, 23);
            this.buttonReject.TabIndex = 8;
            this.buttonReject.Text = "Reject";
            this.buttonReject.UseVisualStyleBackColor = true;
            this.buttonReject.Click += new System.EventHandler(this.buttonReject_Click);
            // 
            // buttonLeaveUnchanged
            // 
            this.buttonLeaveUnchanged.Location = new System.Drawing.Point(285, 424);
            this.buttonLeaveUnchanged.Name = "buttonLeaveUnchanged";
            this.buttonLeaveUnchanged.Size = new System.Drawing.Size(75, 37);
            this.buttonLeaveUnchanged.TabIndex = 9;
            this.buttonLeaveUnchanged.Text = "Leave uchanged";
            this.buttonLeaveUnchanged.UseVisualStyleBackColor = true;
            this.buttonLeaveUnchanged.Click += new System.EventHandler(this.buttonLeaveUnchanged_Click);
            // 
            // FormLeaveConsideration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 490);
            this.Controls.Add(this.buttonLeaveUnchanged);
            this.Controls.Add(this.buttonReject);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.labelDependencies);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBoxEmployee);
            this.Controls.Add(this.labelInfo);
            this.Name = "FormLeaveConsideration";
            this.Text = "Leave Manager";
            this.groupBoxEmployee.ResumeLayout(false);
            this.groupBoxEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelFirstDay;
        private System.Windows.Forms.Label labelLastDay;
        private System.Windows.Forms.GroupBox groupBoxEmployee;
        private System.Windows.Forms.Label labelNameValue;
        private System.Windows.Forms.Label labelPositionValue;
        private System.Windows.Forms.Label labelFirstDayValue;
        private System.Windows.Forms.Label labelLastDayValue;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelDependencies;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonReject;
        private System.Windows.Forms.Button buttonLeaveUnchanged;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWorkersAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWorkersNeeded;
    }
}