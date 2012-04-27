namespace leave_manager
{
    partial class FormWorker
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
            this.buttonTakeLeave = new System.Windows.Forms.Button();
            this.labelDaysToUseValue = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelPositionValue = new System.Windows.Forms.Label();
            this.labelNameValue = new System.Windows.Forms.Label();
            this.labelDaysToUse = new System.Windows.Forms.Label();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUsedDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonTakeLeave
            // 
            this.buttonTakeLeave.Location = new System.Drawing.Point(236, 238);
            this.buttonTakeLeave.Name = "buttonTakeLeave";
            this.buttonTakeLeave.Size = new System.Drawing.Size(95, 40);
            this.buttonTakeLeave.TabIndex = 4;
            this.buttonTakeLeave.Text = "Take a leave";
            this.buttonTakeLeave.UseVisualStyleBackColor = true;
            // 
            // labelDaysToUseValue
            // 
            this.labelDaysToUseValue.AutoSize = true;
            this.labelDaysToUseValue.Location = new System.Drawing.Point(394, 12);
            this.labelDaysToUseValue.Name = "labelDaysToUseValue";
            this.labelDaysToUseValue.Size = new System.Drawing.Size(19, 13);
            this.labelDaysToUseValue.TabIndex = 11;
            this.labelDaysToUseValue.Text = "10";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(13, 35);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(47, 13);
            this.labelPosition.TabIndex = 1;
            this.labelPosition.Text = "Position:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(13, 13);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStatus,
            this.ColumnFirstDay,
            this.ColumnLastDay,
            this.ColumnType,
            this.ColumnUsedDays});
            this.dataGridView.Location = new System.Drawing.Point(16, 63);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(543, 162);
            this.dataGridView.TabIndex = 6;
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(73, 35);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(39, 13);
            this.labelPositionValue.TabIndex = 8;
            this.labelPositionValue.Text = "Doctor";
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Location = new System.Drawing.Point(73, 13);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(69, 13);
            this.labelNameValue.TabIndex = 9;
            this.labelNameValue.Text = "Jan Kowalski";
            // 
            // labelDaysToUse
            // 
            this.labelDaysToUse.AutoSize = true;
            this.labelDaysToUse.Location = new System.Drawing.Point(277, 12);
            this.labelDaysToUse.Name = "labelDaysToUse";
            this.labelDaysToUse.Size = new System.Drawing.Size(110, 13);
            this.labelDaysToUse.TabIndex = 10;
            this.labelDaysToUse.Text = "Available days to use:";
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            // 
            // ColumnFirstDay
            // 
            this.ColumnFirstDay.HeaderText = "First Day";
            this.ColumnFirstDay.Name = "ColumnFirstDay";
            // 
            // ColumnLastDay
            // 
            this.ColumnLastDay.HeaderText = "Last Day";
            this.ColumnLastDay.Name = "ColumnLastDay";
            // 
            // ColumnType
            // 
            this.ColumnType.HeaderText = "Type";
            this.ColumnType.Name = "ColumnType";
            // 
            // ColumnUsedDays
            // 
            this.ColumnUsedDays.HeaderText = "No. used days";
            this.ColumnUsedDays.Name = "ColumnUsedDays";
            // 
            // FormWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 290);
            this.Controls.Add(this.labelDaysToUseValue);
            this.Controls.Add(this.labelDaysToUse);
            this.Controls.Add(this.labelNameValue);
            this.Controls.Add(this.labelPositionValue);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonTakeLeave);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelName);
            this.Name = "FormWorker";
            this.Text = "Leave Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTakeLeave;
        private System.Windows.Forms.Label labelDaysToUseValue;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelPositionValue;
        private System.Windows.Forms.Label labelNameValue;
        private System.Windows.Forms.Label labelDaysToUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUsedDays;
    }
}