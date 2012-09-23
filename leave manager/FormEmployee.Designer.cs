namespace leave_manager
{
    partial class FormEmployee
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
            this.buttonChangeLoginOrPassword = new System.Windows.Forms.Button();
            this.buttonDeleteLeave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonTakeLeave
            // 
            this.buttonTakeLeave.Location = new System.Drawing.Point(126, 324);
            this.buttonTakeLeave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTakeLeave.Name = "buttonTakeLeave";
            this.buttonTakeLeave.Size = new System.Drawing.Size(127, 49);
            this.buttonTakeLeave.TabIndex = 4;
            this.buttonTakeLeave.Text = "Take a leave";
            this.buttonTakeLeave.UseVisualStyleBackColor = true;
            this.buttonTakeLeave.Click += new System.EventHandler(this.buttonTakeLeave_Click);
            // 
            // labelDaysToUseValue
            // 
            this.labelDaysToUseValue.AutoSize = true;
            this.labelDaysToUseValue.Location = new System.Drawing.Point(532, 46);
            this.labelDaysToUseValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDaysToUseValue.Name = "labelDaysToUseValue";
            this.labelDaysToUseValue.Size = new System.Drawing.Size(24, 17);
            this.labelDaysToUseValue.TabIndex = 11;
            this.labelDaysToUseValue.Text = "10";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(24, 74);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(62, 17);
            this.labelPosition.TabIndex = 1;
            this.labelPosition.Text = "Position:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(24, 47);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(28, 109);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(724, 199);
            this.dataGridView.TabIndex = 6;
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(104, 74);
            this.labelPositionValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(0, 17);
            this.labelPositionValue.TabIndex = 8;
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Location = new System.Drawing.Point(104, 47);
            this.labelNameValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(0, 17);
            this.labelNameValue.TabIndex = 9;
            // 
            // labelDaysToUse
            // 
            this.labelDaysToUse.AutoSize = true;
            this.labelDaysToUse.Location = new System.Drawing.Point(376, 46);
            this.labelDaysToUse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDaysToUse.Name = "labelDaysToUse";
            this.labelDaysToUse.Size = new System.Drawing.Size(146, 17);
            this.labelDaysToUse.TabIndex = 10;
            this.labelDaysToUse.Text = "Available days to use:";
            // 
            // buttonChangeLoginOrPassword
            // 
            this.buttonChangeLoginOrPassword.Location = new System.Drawing.Point(491, 324);
            this.buttonChangeLoginOrPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonChangeLoginOrPassword.Name = "buttonChangeLoginOrPassword";
            this.buttonChangeLoginOrPassword.Size = new System.Drawing.Size(127, 49);
            this.buttonChangeLoginOrPassword.TabIndex = 12;
            this.buttonChangeLoginOrPassword.Text = "Change login or password";
            this.buttonChangeLoginOrPassword.UseVisualStyleBackColor = true;
            this.buttonChangeLoginOrPassword.Click += new System.EventHandler(this.buttonChangeLoginOrPassword_Click);
            // 
            // buttonDeleteLeave
            // 
            this.buttonDeleteLeave.Location = new System.Drawing.Point(310, 324);
            this.buttonDeleteLeave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDeleteLeave.Name = "buttonDeleteLeave";
            this.buttonDeleteLeave.Size = new System.Drawing.Size(127, 49);
            this.buttonDeleteLeave.TabIndex = 13;
            this.buttonDeleteLeave.Text = "Delete selected leaves";
            this.buttonDeleteLeave.UseVisualStyleBackColor = true;
            this.buttonDeleteLeave.Click += new System.EventHandler(this.buttonDeleteLeave_Click);
            // 
            // FormEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 422);
            this.Controls.Add(this.buttonDeleteLeave);
            this.Controls.Add(this.buttonChangeLoginOrPassword);
            this.Controls.Add(this.labelDaysToUseValue);
            this.Controls.Add(this.labelDaysToUse);
            this.Controls.Add(this.labelNameValue);
            this.Controls.Add(this.labelPositionValue);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonTakeLeave);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormEmployee";
            this.Text = "Leave Manager";
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.labelPosition, 0);
            this.Controls.SetChildIndex(this.buttonTakeLeave, 0);
            this.Controls.SetChildIndex(this.dataGridView, 0);
            this.Controls.SetChildIndex(this.labelPositionValue, 0);
            this.Controls.SetChildIndex(this.labelNameValue, 0);
            this.Controls.SetChildIndex(this.labelDaysToUse, 0);
            this.Controls.SetChildIndex(this.labelDaysToUseValue, 0);
            this.Controls.SetChildIndex(this.buttonChangeLoginOrPassword, 0);
            this.Controls.SetChildIndex(this.buttonDeleteLeave, 0);
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
        private System.Windows.Forms.Button buttonChangeLoginOrPassword;
        private System.Windows.Forms.Button buttonDeleteLeave;
    }
}