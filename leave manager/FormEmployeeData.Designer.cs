namespace leave_manager
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
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFirstDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUsedDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDaysToUseValue
            // 
            this.labelDaysToUseValue.AutoSize = true;
            this.labelDaysToUseValue.Location = new System.Drawing.Point(393, 46);
            this.labelDaysToUseValue.Name = "labelDaysToUseValue";
            this.labelDaysToUseValue.Size = new System.Drawing.Size(19, 13);
            this.labelDaysToUseValue.TabIndex = 20;
            this.labelDaysToUseValue.Text = "10";
            // 
            // labelDaysToUse
            // 
            this.labelDaysToUse.AutoSize = true;
            this.labelDaysToUse.Location = new System.Drawing.Point(276, 46);
            this.labelDaysToUse.Name = "labelDaysToUse";
            this.labelDaysToUse.Size = new System.Drawing.Size(110, 13);
            this.labelDaysToUse.TabIndex = 19;
            this.labelDaysToUse.Text = "Available days to use:";
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Location = new System.Drawing.Point(72, 47);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(69, 13);
            this.labelNameValue.TabIndex = 18;
            this.labelNameValue.Text = "Jan Kowalski";
            // 
            // labelPositionValue
            // 
            this.labelPositionValue.AutoSize = true;
            this.labelPositionValue.Location = new System.Drawing.Point(72, 69);
            this.labelPositionValue.Name = "labelPositionValue";
            this.labelPositionValue.Size = new System.Drawing.Size(39, 13);
            this.labelPositionValue.TabIndex = 17;
            this.labelPositionValue.Text = "Doctor";
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
            this.dataGridView.Location = new System.Drawing.Point(12, 108);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(543, 331);
            this.dataGridView.TabIndex = 15;
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(12, 69);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(47, 13);
            this.labelPosition.TabIndex = 13;
            this.labelPosition.Text = "Position:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 47);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Name:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(15, 13);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(191, 13);
            this.labelInfo.TabIndex = 21;
            this.labelInfo.Text = "You are viewing detailed leave data of:";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(15, 456);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(88, 38);
            this.buttonEdit.TabIndex = 22;
            this.buttonEdit.Text = "Edit leave entry";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(109, 456);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(87, 38);
            this.buttonAdd.TabIndex = 23;
            this.buttonAdd.Text = "Add new leave entry";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(202, 456);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(87, 38);
            this.buttonDelete.TabIndex = 24;
            this.buttonDelete.Text = "Delete leave entry";
            this.buttonDelete.UseVisualStyleBackColor = true;
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
            // FormEmployeeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 506);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelDaysToUseValue);
            this.Controls.Add(this.labelDaysToUse);
            this.Controls.Add(this.labelNameValue);
            this.Controls.Add(this.labelPositionValue);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelName);
            this.Name = "FormEmployeeData";
            this.Text = "Leave Manager";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFirstDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUsedDays;
    }
}