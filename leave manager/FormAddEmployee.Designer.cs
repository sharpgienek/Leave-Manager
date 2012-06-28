namespace leave_manager
{
    partial class FormAddEmployee
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
            this.labelPermissions = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSurname = new System.Windows.Forms.Label();
            this.labelBirthDate = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelPesel = new System.Windows.Forms.Label();
            this.labelEMail = new System.Windows.Forms.Label();
            this.labelPossition = new System.Windows.Forms.Label();
            this.labelNumberOfLeaveDays = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxPermissions = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.dateTimePickerBirthDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxPesel = new System.Windows.Forms.TextBox();
            this.textBoxEMail = new System.Windows.Forms.TextBox();
            this.comboBoxPossition = new System.Windows.Forms.ComboBox();
            this.textBoxNumberOfLeaveDays = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelPermissions
            // 
            this.labelPermissions.AutoSize = true;
            this.labelPermissions.Location = new System.Drawing.Point(16, 41);
            this.labelPermissions.Name = "labelPermissions";
            this.labelPermissions.Size = new System.Drawing.Size(65, 13);
            this.labelPermissions.TabIndex = 0;
            this.labelPermissions.Text = "Permissions:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(16, 69);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            // 
            // labelSurname
            // 
            this.labelSurname.AutoSize = true;
            this.labelSurname.Location = new System.Drawing.Point(16, 95);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new System.Drawing.Size(52, 13);
            this.labelSurname.TabIndex = 2;
            this.labelSurname.Text = "Surname:";
            // 
            // labelBirthDate
            // 
            this.labelBirthDate.AutoSize = true;
            this.labelBirthDate.Location = new System.Drawing.Point(16, 121);
            this.labelBirthDate.Name = "labelBirthDate";
            this.labelBirthDate.Size = new System.Drawing.Size(55, 13);
            this.labelBirthDate.TabIndex = 3;
            this.labelBirthDate.Text = "Birth date:";
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(16, 151);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(48, 13);
            this.labelAddress.TabIndex = 4;
            this.labelAddress.Text = "Address:";
            // 
            // labelPesel
            // 
            this.labelPesel.AutoSize = true;
            this.labelPesel.Location = new System.Drawing.Point(16, 204);
            this.labelPesel.Name = "labelPesel";
            this.labelPesel.Size = new System.Drawing.Size(44, 13);
            this.labelPesel.TabIndex = 5;
            this.labelPesel.Text = "PESEL:";
            // 
            // labelEMail
            // 
            this.labelEMail.AutoSize = true;
            this.labelEMail.Location = new System.Drawing.Point(16, 230);
            this.labelEMail.Name = "labelEMail";
            this.labelEMail.Size = new System.Drawing.Size(37, 13);
            this.labelEMail.TabIndex = 6;
            this.labelEMail.Text = "e-mail:";
            // 
            // labelPossition
            // 
            this.labelPossition.AutoSize = true;
            this.labelPossition.Location = new System.Drawing.Point(16, 256);
            this.labelPossition.Name = "labelPossition";
            this.labelPossition.Size = new System.Drawing.Size(52, 13);
            this.labelPossition.TabIndex = 7;
            this.labelPossition.Text = "Possition:";
            // 
            // labelNumberOfLeaveDays
            // 
            this.labelNumberOfLeaveDays.AutoSize = true;
            this.labelNumberOfLeaveDays.Location = new System.Drawing.Point(16, 291);
            this.labelNumberOfLeaveDays.Name = "labelNumberOfLeaveDays";
            this.labelNumberOfLeaveDays.Size = new System.Drawing.Size(154, 13);
            this.labelNumberOfLeaveDays.TabIndex = 8;
            this.labelNumberOfLeaveDays.Text = "Number of leave days per year:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(16, 13);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(166, 13);
            this.labelInfo.TabIndex = 9;
            this.labelInfo.Text = "Here you can add new employee.";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(87, 346);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(82, 38);
            this.buttonAdd.TabIndex = 10;
            this.buttonAdd.Text = "Add new employee";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(236, 346);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(84, 38);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxPermissions
            // 
            this.comboBoxPermissions.FormattingEnabled = true;
            this.comboBoxPermissions.Location = new System.Drawing.Point(87, 38);
            this.comboBoxPermissions.Name = "comboBoxPermissions";
            this.comboBoxPermissions.Size = new System.Drawing.Size(286, 21);
            this.comboBoxPermissions.TabIndex = 12;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(87, 65);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(286, 20);
            this.textBoxName.TabIndex = 13;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Location = new System.Drawing.Point(87, 92);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(286, 20);
            this.textBoxSurname.TabIndex = 14;
            this.textBoxSurname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSurname_KeyPress);
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(87, 144);
            this.textBoxAddress.Multiline = true;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(286, 51);
            this.textBoxAddress.TabIndex = 15;
            this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAddress_KeyPress);
            // 
            // dateTimePickerBirthDate
            // 
            this.dateTimePickerBirthDate.Location = new System.Drawing.Point(87, 118);
            this.dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
            this.dateTimePickerBirthDate.Size = new System.Drawing.Size(286, 20);
            this.dateTimePickerBirthDate.TabIndex = 16;
            this.dateTimePickerBirthDate.Value = new System.DateTime(2012, 5, 17, 14, 32, 23, 0);
            // 
            // textBoxPesel
            // 
            this.textBoxPesel.Location = new System.Drawing.Point(87, 201);
            this.textBoxPesel.Name = "textBoxPesel";
            this.textBoxPesel.Size = new System.Drawing.Size(286, 20);
            this.textBoxPesel.TabIndex = 17;
            this.textBoxPesel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPesel_KeyPress);
            // 
            // textBoxEMail
            // 
            this.textBoxEMail.Location = new System.Drawing.Point(87, 227);
            this.textBoxEMail.Name = "textBoxEMail";
            this.textBoxEMail.Size = new System.Drawing.Size(286, 20);
            this.textBoxEMail.TabIndex = 18;
            this.textBoxEMail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEMail_KeyPress);
            // 
            // comboBoxPossition
            // 
            this.comboBoxPossition.FormattingEnabled = true;
            this.comboBoxPossition.Location = new System.Drawing.Point(87, 253);
            this.comboBoxPossition.Name = "comboBoxPossition";
            this.comboBoxPossition.Size = new System.Drawing.Size(286, 21);
            this.comboBoxPossition.TabIndex = 19;
            // 
            // textBoxNumberOfLeaveDays
            // 
            this.textBoxNumberOfLeaveDays.Location = new System.Drawing.Point(176, 288);
            this.textBoxNumberOfLeaveDays.Name = "textBoxNumberOfLeaveDays";
            this.textBoxNumberOfLeaveDays.Size = new System.Drawing.Size(55, 20);
            this.textBoxNumberOfLeaveDays.TabIndex = 20;
            this.textBoxNumberOfLeaveDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumberOfLeaveDays_KeyPress);
            // 
            // FormAddEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 420);
            this.Controls.Add(this.textBoxNumberOfLeaveDays);
            this.Controls.Add(this.comboBoxPossition);
            this.Controls.Add(this.textBoxEMail);
            this.Controls.Add(this.textBoxPesel);
            this.Controls.Add(this.dateTimePickerBirthDate);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.textBoxSurname);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.comboBoxPermissions);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelNumberOfLeaveDays);
            this.Controls.Add(this.labelPossition);
            this.Controls.Add(this.labelEMail);
            this.Controls.Add(this.labelPesel);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.labelBirthDate);
            this.Controls.Add(this.labelSurname);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelPermissions);
            this.Name = "FormAddEmployee";
            this.Text = "Leave Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPermissions;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSurname;
        private System.Windows.Forms.Label labelBirthDate;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelPesel;
        private System.Windows.Forms.Label labelEMail;
        private System.Windows.Forms.Label labelPossition;
        private System.Windows.Forms.Label labelNumberOfLeaveDays;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxPermissions;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.DateTimePicker dateTimePickerBirthDate;
        private System.Windows.Forms.TextBox textBoxPesel;
        private System.Windows.Forms.TextBox textBoxEMail;
        private System.Windows.Forms.ComboBox comboBoxPossition;
        private System.Windows.Forms.TextBox textBoxNumberOfLeaveDays;
    }
}