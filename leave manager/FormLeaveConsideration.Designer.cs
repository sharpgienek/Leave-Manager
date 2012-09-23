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
            this.labelInfo.Location = new System.Drawing.Point(21, 28);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(241, 17);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "You are considering leave aplication:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(8, 20);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(8, 36);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(62, 17);
            this.labelPosition.TabIndex = 2;
            this.labelPosition.Text = "Position:";
            // 
            // labelFirstDay
            // 
            this.labelFirstDay.AutoSize = true;
            this.labelFirstDay.Location = new System.Drawing.Point(8, 52);
            this.labelFirstDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFirstDay.Name = "labelFirstDay";
            this.labelFirstDay.Size = new System.Drawing.Size(66, 17);
            this.labelFirstDay.TabIndex = 3;
            this.labelFirstDay.Text = "First day:";
            // 
            // labelLastDay
            // 
            this.labelLastDay.AutoSize = true;
            this.labelLastDay.Location = new System.Drawing.Point(8, 68);
            this.labelLastDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLastDay.Name = "labelLastDay";
            this.labelLastDay.Size = new System.Drawing.Size(66, 17);
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
            this.groupBoxEmployee.Location = new System.Drawing.Point(21, 47);
            this.groupBoxEmployee.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxEmployee.Name = "groupBoxEmployee";
            this.groupBoxEmployee.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxEmployee.Size = new System.Drawing.Size(228, 101);
            this.groupBoxEmployee.TabIndex = 5;
            this.groupBoxEmployee.TabStop = false;
            this.groupBoxEmployee.Text = "Employee dataLeaves";
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Location = new System.Drawing.Point(84, 20);
            this.labelNameValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(89, 17);
            this.labelNameValue.TabIndex = 8;
            this.labelNameValue.Text = "Jan Kowalski";
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(84, 36);
            this.labelPositionValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(50, 17);
            this.labelPositionValue.TabIndex = 7;
            this.labelPositionValue.Text = "Doctor";
            // 
            // labelFirstDayValue
            // 
            this.labelFirstDayValue.AutoSize = true;
            this.labelFirstDayValue.Location = new System.Drawing.Point(84, 52);
            this.labelFirstDayValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFirstDayValue.Name = "labelFirstDayValue";
            this.labelFirstDayValue.Size = new System.Drawing.Size(82, 17);
            this.labelFirstDayValue.TabIndex = 6;
            this.labelFirstDayValue.Text = "24-07-2012";
            // 
            // labelLastDayValue
            // 
            this.labelLastDayValue.AutoSize = true;
            this.labelLastDayValue.Location = new System.Drawing.Point(84, 68);
            this.labelLastDayValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLastDayValue.Name = "labelLastDayValue";
            this.labelLastDayValue.Size = new System.Drawing.Size(82, 17);
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
            this.dataGridView.Location = new System.Drawing.Point(21, 186);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(459, 290);
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
            this.labelDependencies.Location = new System.Drawing.Point(21, 162);
            this.labelDependencies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDependencies.Name = "labelDependencies";
            this.labelDependencies.Size = new System.Drawing.Size(99, 17);
            this.labelDependencies.TabIndex = 6;
            this.labelDependencies.Text = "Dependencies";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(21, 530);
            this.buttonAccept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(100, 28);
            this.buttonAccept.TabIndex = 7;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonReject
            // 
            this.buttonReject.Location = new System.Drawing.Point(192, 530);
            this.buttonReject.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(100, 28);
            this.buttonReject.TabIndex = 8;
            this.buttonReject.Text = "Reject";
            this.buttonReject.UseVisualStyleBackColor = true;
            this.buttonReject.Click += new System.EventHandler(this.buttonReject_Click);
            // 
            // buttonLeaveUnchanged
            // 
            this.buttonLeaveUnchanged.Location = new System.Drawing.Point(380, 522);
            this.buttonLeaveUnchanged.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLeaveUnchanged.Name = "buttonLeaveUnchanged";
            this.buttonLeaveUnchanged.Size = new System.Drawing.Size(100, 46);
            this.buttonLeaveUnchanged.TabIndex = 9;
            this.buttonLeaveUnchanged.Text = "Leave uchanged";
            this.buttonLeaveUnchanged.UseVisualStyleBackColor = true;
            this.buttonLeaveUnchanged.Click += new System.EventHandler(this.buttonLeaveUnchanged_Click);
            // 
            // FormLeaveConsideration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 603);
            this.Controls.Add(this.buttonLeaveUnchanged);
            this.Controls.Add(this.buttonReject);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.labelDependencies);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBoxEmployee);
            this.Controls.Add(this.labelInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormLeaveConsideration";
            this.Text = "Leave Manager";
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.groupBoxEmployee, 0);
            this.Controls.SetChildIndex(this.dataGridView, 0);
            this.Controls.SetChildIndex(this.labelDependencies, 0);
            this.Controls.SetChildIndex(this.buttonAccept, 0);
            this.Controls.SetChildIndex(this.buttonReject, 0);
            this.Controls.SetChildIndex(this.buttonLeaveUnchanged, 0);
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