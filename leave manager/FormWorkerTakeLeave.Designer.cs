namespace leave_manager
{
    partial class FormWorkerTakeLeave
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelFirstDay = new System.Windows.Forms.Label();
            this.labelLastDay = new System.Windows.Forms.Label();
            this.labelAvailableDays = new System.Windows.Forms.Label();
            this.dateTimePickerFirstDay = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerLastDay = new System.Windows.Forms.DateTimePicker();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.labelUsedDays = new System.Windows.Forms.Label();
            this.labelUsedDaysValue = new System.Windows.Forms.Label();
            this.labelAvailableDaysValue = new System.Windows.Forms.Label();
            this.labelRemarks = new System.Windows.Forms.Label();
            this.textBoxRemarks = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(32, 285);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(197, 285);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(29, 18);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(160, 13);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "Your\'re about to aply for a leave ";
            // 
            // labelFirstDay
            // 
            this.labelFirstDay.AutoSize = true;
            this.labelFirstDay.Location = new System.Drawing.Point(32, 65);
            this.labelFirstDay.Name = "labelFirstDay";
            this.labelFirstDay.Size = new System.Drawing.Size(52, 13);
            this.labelFirstDay.TabIndex = 3;
            this.labelFirstDay.Text = "First day: ";
            this.labelFirstDay.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelLastDay
            // 
            this.labelLastDay.AutoSize = true;
            this.labelLastDay.Location = new System.Drawing.Point(32, 87);
            this.labelLastDay.Name = "labelLastDay";
            this.labelLastDay.Size = new System.Drawing.Size(50, 13);
            this.labelLastDay.TabIndex = 4;
            this.labelLastDay.Text = "Last day:";
            // 
            // labelAvailableDays
            // 
            this.labelAvailableDays.AutoSize = true;
            this.labelAvailableDays.Location = new System.Drawing.Point(32, 260);
            this.labelAvailableDays.Name = "labelAvailableDays";
            this.labelAvailableDays.Size = new System.Drawing.Size(81, 13);
            this.labelAvailableDays.TabIndex = 5;
            this.labelAvailableDays.Text = "Available days: ";
            // 
            // dateTimePickerFirstDay
            // 
            this.dateTimePickerFirstDay.Location = new System.Drawing.Point(91, 59);
            this.dateTimePickerFirstDay.Name = "dateTimePickerFirstDay";
            this.dateTimePickerFirstDay.Size = new System.Drawing.Size(181, 20);
            this.dateTimePickerFirstDay.TabIndex = 6;
            // 
            // dateTimePickerLastDay
            // 
            this.dateTimePickerLastDay.Location = new System.Drawing.Point(91, 81);
            this.dateTimePickerLastDay.Name = "dateTimePickerLastDay";
            this.dateTimePickerLastDay.Size = new System.Drawing.Size(181, 20);
            this.dateTimePickerLastDay.TabIndex = 7;
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "sick leave"});
            this.comboBoxType.Location = new System.Drawing.Point(91, 37);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(181, 21);
            this.comboBoxType.TabIndex = 8;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(32, 40);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(34, 13);
            this.labelType.TabIndex = 9;
            this.labelType.Text = "Type:";
            // 
            // labelUsedDays
            // 
            this.labelUsedDays.AutoSize = true;
            this.labelUsedDays.Location = new System.Drawing.Point(32, 247);
            this.labelUsedDays.Name = "labelUsedDays";
            this.labelUsedDays.Size = new System.Drawing.Size(60, 13);
            this.labelUsedDays.TabIndex = 10;
            this.labelUsedDays.Text = "Used days:";
            // 
            // labelUsedDaysValue
            // 
            this.labelUsedDaysValue.AutoSize = true;
            this.labelUsedDaysValue.Location = new System.Drawing.Point(120, 247);
            this.labelUsedDaysValue.Name = "labelUsedDaysValue";
            this.labelUsedDaysValue.Size = new System.Drawing.Size(13, 13);
            this.labelUsedDaysValue.TabIndex = 11;
            this.labelUsedDaysValue.Text = "0";
            // 
            // labelAvailableDaysValue
            // 
            this.labelAvailableDaysValue.AutoSize = true;
            this.labelAvailableDaysValue.Location = new System.Drawing.Point(120, 260);
            this.labelAvailableDaysValue.Name = "labelAvailableDaysValue";
            this.labelAvailableDaysValue.Size = new System.Drawing.Size(19, 13);
            this.labelAvailableDaysValue.TabIndex = 12;
            this.labelAvailableDaysValue.Text = "14";
            // 
            // labelRemarks
            // 
            this.labelRemarks.AutoSize = true;
            this.labelRemarks.Location = new System.Drawing.Point(35, 114);
            this.labelRemarks.Name = "labelRemarks";
            this.labelRemarks.Size = new System.Drawing.Size(49, 13);
            this.labelRemarks.TabIndex = 13;
            this.labelRemarks.Text = "Remarks";
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Location = new System.Drawing.Point(35, 130);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.Size = new System.Drawing.Size(237, 114);
            this.textBoxRemarks.TabIndex = 14;
            // 
            // FormWorkerTakeLeave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 346);
            this.Controls.Add(this.textBoxRemarks);
            this.Controls.Add(this.labelRemarks);
            this.Controls.Add(this.labelAvailableDaysValue);
            this.Controls.Add(this.labelUsedDaysValue);
            this.Controls.Add(this.labelUsedDays);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.dateTimePickerLastDay);
            this.Controls.Add(this.dateTimePickerFirstDay);
            this.Controls.Add(this.labelAvailableDays);
            this.Controls.Add(this.labelLastDay);
            this.Controls.Add(this.labelFirstDay);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Name = "FormWorkerTakeLeave";
            this.Text = "Leave Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelFirstDay;
        private System.Windows.Forms.Label labelLastDay;
        private System.Windows.Forms.Label labelAvailableDays;
        private System.Windows.Forms.DateTimePicker dateTimePickerFirstDay;
        private System.Windows.Forms.DateTimePicker dateTimePickerLastDay;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelUsedDays;
        private System.Windows.Forms.Label labelUsedDaysValue;
        private System.Windows.Forms.Label labelAvailableDaysValue;
        private System.Windows.Forms.Label labelRemarks;
        private System.Windows.Forms.TextBox textBoxRemarks;
    }
}