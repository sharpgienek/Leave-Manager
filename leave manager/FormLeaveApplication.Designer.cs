namespace leave_manager
{
    partial class FormLeaveApplication
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
            this.labelNormal = new System.Windows.Forms.Label();
            this.labelNormalValue = new System.Windows.Forms.Label();
            this.labelOldValue = new System.Windows.Forms.Label();
            this.labelOld = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(38, 398);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(258, 398);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(34, 37);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(205, 17);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "Specify leave aplication details.";
            // 
            // labelFirstDay
            // 
            this.labelFirstDay.AutoSize = true;
            this.labelFirstDay.Location = new System.Drawing.Point(38, 95);
            this.labelFirstDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFirstDay.Name = "labelFirstDay";
            this.labelFirstDay.Size = new System.Drawing.Size(70, 17);
            this.labelFirstDay.TabIndex = 3;
            this.labelFirstDay.Text = "First day: ";
            // 
            // labelLastDay
            // 
            this.labelLastDay.AutoSize = true;
            this.labelLastDay.Location = new System.Drawing.Point(38, 122);
            this.labelLastDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLastDay.Name = "labelLastDay";
            this.labelLastDay.Size = new System.Drawing.Size(66, 17);
            this.labelLastDay.TabIndex = 4;
            this.labelLastDay.Text = "Last day:";
            // 
            // labelAvailableDays
            // 
            this.labelAvailableDays.AutoSize = true;
            this.labelAvailableDays.Location = new System.Drawing.Point(38, 360);
            this.labelAvailableDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAvailableDays.Name = "labelAvailableDays";
            this.labelAvailableDays.Size = new System.Drawing.Size(107, 17);
            this.labelAvailableDays.TabIndex = 5;
            this.labelAvailableDays.Text = "Available days: ";
            // 
            // dateTimePickerFirstDay
            // 
            this.dateTimePickerFirstDay.Location = new System.Drawing.Point(116, 88);
            this.dateTimePickerFirstDay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePickerFirstDay.Name = "dateTimePickerFirstDay";
            this.dateTimePickerFirstDay.Size = new System.Drawing.Size(240, 22);
            this.dateTimePickerFirstDay.TabIndex = 6;
            this.dateTimePickerFirstDay.ValueChanged += new System.EventHandler(this.dateTimePickerFirstDay_ValueChanged);
            // 
            // dateTimePickerLastDay
            // 
            this.dateTimePickerLastDay.Location = new System.Drawing.Point(116, 115);
            this.dateTimePickerLastDay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePickerLastDay.Name = "dateTimePickerLastDay";
            this.dateTimePickerLastDay.Size = new System.Drawing.Size(240, 22);
            this.dateTimePickerLastDay.TabIndex = 7;
            this.dateTimePickerLastDay.ValueChanged += new System.EventHandler(this.dateTimePickerLastDay_ValueChanged);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "sick leave"});
            this.comboBoxType.Location = new System.Drawing.Point(116, 61);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(240, 24);
            this.comboBoxType.TabIndex = 8;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(38, 64);
            this.labelType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(44, 17);
            this.labelType.TabIndex = 9;
            this.labelType.Text = "Type:";
            // 
            // labelUsedDays
            // 
            this.labelUsedDays.AutoSize = true;
            this.labelUsedDays.Location = new System.Drawing.Point(38, 344);
            this.labelUsedDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsedDays.Name = "labelUsedDays";
            this.labelUsedDays.Size = new System.Drawing.Size(79, 17);
            this.labelUsedDays.TabIndex = 10;
            this.labelUsedDays.Text = "Used days:";
            // 
            // labelUsedDaysValue
            // 
            this.labelUsedDaysValue.AutoSize = true;
            this.labelUsedDaysValue.Location = new System.Drawing.Point(155, 344);
            this.labelUsedDaysValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsedDaysValue.Name = "labelUsedDaysValue";
            this.labelUsedDaysValue.Size = new System.Drawing.Size(0, 17);
            this.labelUsedDaysValue.TabIndex = 11;
            // 
            // labelAvailableDaysValue
            // 
            this.labelAvailableDaysValue.AutoSize = true;
            this.labelAvailableDaysValue.Location = new System.Drawing.Point(155, 360);
            this.labelAvailableDaysValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAvailableDaysValue.Name = "labelAvailableDaysValue";
            this.labelAvailableDaysValue.Size = new System.Drawing.Size(0, 17);
            this.labelAvailableDaysValue.TabIndex = 12;
            // 
            // labelRemarks
            // 
            this.labelRemarks.AutoSize = true;
            this.labelRemarks.Location = new System.Drawing.Point(42, 180);
            this.labelRemarks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRemarks.Name = "labelRemarks";
            this.labelRemarks.Size = new System.Drawing.Size(64, 17);
            this.labelRemarks.TabIndex = 13;
            this.labelRemarks.Text = "Remarks";
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Location = new System.Drawing.Point(42, 200);
            this.textBoxRemarks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.Size = new System.Drawing.Size(315, 139);
            this.textBoxRemarks.TabIndex = 14;
            // 
            // labelNormal
            // 
            this.labelNormal.AutoSize = true;
            this.labelNormal.Location = new System.Drawing.Point(190, 360);
            this.labelNormal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNormal.Name = "labelNormal";
            this.labelNormal.Size = new System.Drawing.Size(57, 17);
            this.labelNormal.TabIndex = 15;
            this.labelNormal.Text = "Normal:";
            // 
            // labelNormalValue
            // 
            this.labelNormalValue.AutoSize = true;
            this.labelNormalValue.Location = new System.Drawing.Point(254, 360);
            this.labelNormalValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNormalValue.Name = "labelNormalValue";
            this.labelNormalValue.Size = new System.Drawing.Size(0, 17);
            this.labelNormalValue.TabIndex = 16;
            // 
            // labelOldValue
            // 
            this.labelOldValue.AutoSize = true;
            this.labelOldValue.Location = new System.Drawing.Point(254, 376);
            this.labelOldValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOldValue.Name = "labelOldValue";
            this.labelOldValue.Size = new System.Drawing.Size(0, 17);
            this.labelOldValue.TabIndex = 18;
            // 
            // labelOld
            // 
            this.labelOld.AutoSize = true;
            this.labelOld.Location = new System.Drawing.Point(190, 376);
            this.labelOld.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOld.Name = "labelOld";
            this.labelOld.Size = new System.Drawing.Size(34, 17);
            this.labelOld.TabIndex = 17;
            this.labelOld.Text = "Old:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(38, 144);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(52, 17);
            this.labelStatus.TabIndex = 19;
            this.labelStatus.Text = "Status:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(116, 141);
            this.comboBoxStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(240, 24);
            this.comboBoxStatus.TabIndex = 20;
            // 
            // FormLeaveApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 445);
            this.Controls.Add(this.labelFirstDay);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelOldValue);
            this.Controls.Add(this.labelOld);
            this.Controls.Add(this.labelNormalValue);
            this.Controls.Add(this.labelNormal);
            this.Controls.Add(this.textBoxRemarks);
            this.Controls.Add(this.labelRemarks);
            this.Controls.Add(this.labelAvailableDaysValue);
            this.Controls.Add(this.labelUsedDaysValue);
            this.Controls.Add(this.labelUsedDays);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.dateTimePickerLastDay);
            this.Controls.Add(this.dateTimePickerFirstDay);
            this.Controls.Add(this.labelAvailableDays);
            this.Controls.Add(this.labelLastDay);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormLeaveApplication";
            this.Text = "Leave Manager";
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.labelLastDay, 0);
            this.Controls.SetChildIndex(this.labelAvailableDays, 0);
            this.Controls.SetChildIndex(this.dateTimePickerFirstDay, 0);
            this.Controls.SetChildIndex(this.dateTimePickerLastDay, 0);
            this.Controls.SetChildIndex(this.comboBoxType, 0);
            this.Controls.SetChildIndex(this.buttonOk, 0);
            this.Controls.SetChildIndex(this.labelUsedDays, 0);
            this.Controls.SetChildIndex(this.labelUsedDaysValue, 0);
            this.Controls.SetChildIndex(this.labelAvailableDaysValue, 0);
            this.Controls.SetChildIndex(this.labelRemarks, 0);
            this.Controls.SetChildIndex(this.textBoxRemarks, 0);
            this.Controls.SetChildIndex(this.labelNormal, 0);
            this.Controls.SetChildIndex(this.labelNormalValue, 0);
            this.Controls.SetChildIndex(this.labelOld, 0);
            this.Controls.SetChildIndex(this.labelOldValue, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.comboBoxStatus, 0);
            this.Controls.SetChildIndex(this.labelType, 0);
            this.Controls.SetChildIndex(this.labelFirstDay, 0);
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
        private System.Windows.Forms.Label labelNormal;
        private System.Windows.Forms.Label labelNormalValue;
        private System.Windows.Forms.Label labelOldValue;
        private System.Windows.Forms.Label labelOld;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
    }
}