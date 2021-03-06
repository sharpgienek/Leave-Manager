﻿namespace leave_manager
{
    partial class FormEmployeeData
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
            this.labelDaysToUseValue = new System.Windows.Forms.Label();
            this.labelDaysToUse = new System.Windows.Forms.Label();
            this.labelNameValue = new System.Windows.Forms.Label();
            this.labelPositionValue = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDaysToUseValue
            // 
            this.labelDaysToUseValue.AutoSize = true;
            this.labelDaysToUseValue.Location = new System.Drawing.Point(524, 57);
            this.labelDaysToUseValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDaysToUseValue.Name = "labelDaysToUseValue";
            this.labelDaysToUseValue.Size = new System.Drawing.Size(24, 17);
            this.labelDaysToUseValue.TabIndex = 20;
            this.labelDaysToUseValue.Text = "10";
            // 
            // labelDaysToUse
            // 
            this.labelDaysToUse.AutoSize = true;
            this.labelDaysToUse.Location = new System.Drawing.Point(368, 57);
            this.labelDaysToUse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDaysToUse.Name = "labelDaysToUse";
            this.labelDaysToUse.Size = new System.Drawing.Size(146, 17);
            this.labelDaysToUse.TabIndex = 19;
            this.labelDaysToUse.Text = "Available days to use:";
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Location = new System.Drawing.Point(96, 58);
            this.labelNameValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(89, 17);
            this.labelNameValue.TabIndex = 18;
            this.labelNameValue.Text = "Jan Kowalski";
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(96, 85);
            this.labelPositionValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(50, 17);
            this.labelPositionValue.TabIndex = 17;
            this.labelPositionValue.Text = "Doctor";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(16, 133);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(724, 407);
            this.dataGridView.TabIndex = 15;
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(16, 85);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(62, 17);
            this.labelPosition.TabIndex = 13;
            this.labelPosition.Text = "Position:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(16, 58);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Name:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(17, 28);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(298, 17);
            this.labelInfo.TabIndex = 21;
            this.labelInfo.Text = "You are viewing detailed leave dataLeaves of:";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(20, 561);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(117, 47);
            this.buttonEdit.TabIndex = 22;
            this.buttonEdit.Text = "Edit leave entry";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(145, 561);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(116, 47);
            this.buttonAdd.TabIndex = 23;
            this.buttonAdd.Text = "Add new leave entry";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(269, 561);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(116, 47);
            this.buttonDelete.TabIndex = 24;
            this.buttonDelete.Text = "Delete leave entry";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // FormEmployeeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 623);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelDaysToUseValue);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelDaysToUse);
            this.Controls.Add(this.labelNameValue);
            this.Controls.Add(this.labelPositionValue);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormEmployeeData";
            this.Text = "Leave Manager";
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.labelPosition, 0);
            this.Controls.SetChildIndex(this.dataGridView, 0);
            this.Controls.SetChildIndex(this.labelPositionValue, 0);
            this.Controls.SetChildIndex(this.labelNameValue, 0);
            this.Controls.SetChildIndex(this.labelDaysToUse, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.labelDaysToUseValue, 0);
            this.Controls.SetChildIndex(this.buttonEdit, 0);
            this.Controls.SetChildIndex(this.buttonAdd, 0);
            this.Controls.SetChildIndex(this.buttonDelete, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDaysToUseValue;
        private System.Windows.Forms.Label labelDaysToUse;
        private System.Windows.Forms.Label labelNameValue;
        private System.Windows.Forms.Label labelPositionValue;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
    }
}