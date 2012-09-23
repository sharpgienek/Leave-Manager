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
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(17, 37);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(320, 34);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "You need to specify a new type for the leaves\r\nthat are labeled with the type men" +
                "t to be deleted. ";
            // 
            // comboBoxLeaveTypes
            // 
            this.comboBoxLeaveTypes.FormattingEnabled = true;
            this.comboBoxLeaveTypes.Location = new System.Drawing.Point(120, 90);
            this.comboBoxLeaveTypes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxLeaveTypes.Name = "comboBoxLeaveTypes";
            this.comboBoxLeaveTypes.Size = new System.Drawing.Size(276, 24);
            this.comboBoxLeaveTypes.TabIndex = 1;
            // 
            // labelNewPosition
            // 
            this.labelNewPosition.AutoSize = true;
            this.labelNewPosition.Location = new System.Drawing.Point(17, 94);
            this.labelNewPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewPosition.Name = "labelNewPosition";
            this.labelNewPosition.Size = new System.Drawing.Size(92, 17);
            this.labelNewPosition.TabIndex = 2;
            this.labelNewPosition.Text = "New position:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(48, 149);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(275, 149);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormDeleteLeaveType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 206);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelNewPosition);
            this.Controls.Add(this.comboBoxLeaveTypes);
            this.Controls.Add(this.labelInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormDeleteLeaveType";
            this.Text = "Leave Manager";
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.comboBoxLeaveTypes, 0);
            this.Controls.SetChildIndex(this.labelNewPosition, 0);
            this.Controls.SetChildIndex(this.buttonOk, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ComboBox comboBoxLeaveTypes;
        private System.Windows.Forms.Label labelNewPosition;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}