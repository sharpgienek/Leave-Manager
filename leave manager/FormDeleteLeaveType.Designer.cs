namespace leave_manager
{
    partial class FormDeleteLeaveType
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
            this.comboBoxLeaveTypes = new System.Windows.Forms.ComboBox();
            this.labelNewPosition = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonReplace = new System.Windows.Forms.RadioButton();
            this.radioButtonDelete = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(13, 13);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(259, 39);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "You may specify a new type for the leaves\r\nthat are labeled with the type ment to" +
    " be deleted or \r\nyou may delete all leave entries that use deleted type.";
            // 
            // comboBoxLeaveTypes
            // 
            this.comboBoxLeaveTypes.FormattingEnabled = true;
            this.comboBoxLeaveTypes.Location = new System.Drawing.Point(89, 102);
            this.comboBoxLeaveTypes.Name = "comboBoxLeaveTypes";
            this.comboBoxLeaveTypes.Size = new System.Drawing.Size(208, 21);
            this.comboBoxLeaveTypes.TabIndex = 1;
            // 
            // labelNewPosition
            // 
            this.labelNewPosition.AutoSize = true;
            this.labelNewPosition.Location = new System.Drawing.Point(12, 105);
            this.labelNewPosition.Name = "labelNewPosition";
            this.labelNewPosition.Size = new System.Drawing.Size(71, 13);
            this.labelNewPosition.TabIndex = 2;
            this.labelNewPosition.Text = "New position:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(38, 138);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(208, 138);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioButtonReplace
            // 
            this.radioButtonReplace.AutoSize = true;
            this.radioButtonReplace.Location = new System.Drawing.Point(16, 66);
            this.radioButtonReplace.Name = "radioButtonReplace";
            this.radioButtonReplace.Size = new System.Drawing.Size(65, 17);
            this.radioButtonReplace.TabIndex = 5;
            this.radioButtonReplace.TabStop = true;
            this.radioButtonReplace.Text = "Replace";
            this.radioButtonReplace.UseVisualStyleBackColor = true;
            this.radioButtonReplace.CheckedChanged += new System.EventHandler(this.radioButtonReplace_CheckedChanged);
            // 
            // radioButtonDelete
            // 
            this.radioButtonDelete.AutoSize = true;
            this.radioButtonDelete.Location = new System.Drawing.Point(130, 66);
            this.radioButtonDelete.Name = "radioButtonDelete";
            this.radioButtonDelete.Size = new System.Drawing.Size(56, 17);
            this.radioButtonDelete.TabIndex = 6;
            this.radioButtonDelete.TabStop = true;
            this.radioButtonDelete.Text = "Delete";
            this.radioButtonDelete.UseVisualStyleBackColor = true;
            // 
            // FormDeleteLeaveType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 177);
            this.Controls.Add(this.radioButtonDelete);
            this.Controls.Add(this.radioButtonReplace);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelNewPosition);
            this.Controls.Add(this.comboBoxLeaveTypes);
            this.Controls.Add(this.labelInfo);
            this.Name = "FormDeleteLeaveType";
            this.Text = "Leave Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ComboBox comboBoxLeaveTypes;
        private System.Windows.Forms.Label labelNewPosition;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonReplace;
        private System.Windows.Forms.RadioButton radioButtonDelete;
    }
}