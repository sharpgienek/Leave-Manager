namespace leave_manager
{
    partial class FormManager
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageNeedsAction = new System.Windows.Forms.TabPage();
            this.buttonReject = new System.Windows.Forms.Button();
            this.buttonConsider = new System.Windows.Forms.Button();
            this.labelNeedsActionInfo = new System.Windows.Forms.Label();
            this.dataGridViewNeedsAction = new System.Windows.Forms.DataGridView();
            this.ColumnNeedsActionStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNeedsActionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNeedsActionPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNeedsActionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNeedsActionFirstDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNeedsActionLastDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.buttonEmployeesAdd = new System.Windows.Forms.Button();
            this.buttonEmployeesDetailedData = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.buttonEmployeesShowAll = new System.Windows.Forms.Button();
            this.buttonEmployeesSearch = new System.Windows.Forms.Button();
            this.comboBoxEmployeesPosition = new System.Windows.Forms.ComboBox();
            this.labelEmployeesPosition = new System.Windows.Forms.Label();
            this.textBoxEmployeesName = new System.Windows.Forms.TextBox();
            this.labelEmployeesName = new System.Windows.Forms.Label();
            this.labelEmployeesInfo = new System.Windows.Forms.Label();
            this.tabPageReplacements = new System.Windows.Forms.TabPage();
            this.labelReplacementsPositionFilter = new System.Windows.Forms.Label();
            this.labelReplacementsInfo = new System.Windows.Forms.Label();
            this.comboBoxReplacementsPosition = new System.Windows.Forms.ComboBox();
            this.buttonReplacementsManage = new System.Windows.Forms.Button();
            this.dataGridViewReplacements = new System.Windows.Forms.DataGridView();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageReport = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPageNeedsAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNeedsAction)).BeginInit();
            this.tabPageEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.tabPageReplacements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReplacements)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageNeedsAction);
            this.tabControl.Controls.Add(this.tabPageEmployees);
            this.tabControl.Controls.Add(this.tabPageReplacements);
            this.tabControl.Controls.Add(this.tabPageReport);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(714, 433);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageNeedsAction
            // 
            this.tabPageNeedsAction.Controls.Add(this.buttonReject);
            this.tabPageNeedsAction.Controls.Add(this.buttonConsider);
            this.tabPageNeedsAction.Controls.Add(this.labelNeedsActionInfo);
            this.tabPageNeedsAction.Controls.Add(this.dataGridViewNeedsAction);
            this.tabPageNeedsAction.Location = new System.Drawing.Point(4, 22);
            this.tabPageNeedsAction.Name = "tabPageNeedsAction";
            this.tabPageNeedsAction.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNeedsAction.Size = new System.Drawing.Size(706, 407);
            this.tabPageNeedsAction.TabIndex = 0;
            this.tabPageNeedsAction.Text = "Needs your action";
            this.tabPageNeedsAction.UseVisualStyleBackColor = true;
            // 
            // buttonReject
            // 
            this.buttonReject.Location = new System.Drawing.Point(480, 330);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(91, 41);
            this.buttonReject.TabIndex = 3;
            this.buttonReject.Text = "Reject without consideration";
            this.buttonReject.UseVisualStyleBackColor = true;
            // 
            // buttonConsider
            // 
            this.buttonConsider.Location = new System.Drawing.Point(86, 339);
            this.buttonConsider.Name = "buttonConsider";
            this.buttonConsider.Size = new System.Drawing.Size(75, 23);
            this.buttonConsider.TabIndex = 2;
            this.buttonConsider.Text = "Consider";
            this.buttonConsider.UseVisualStyleBackColor = true;
            // 
            // labelNeedsActionInfo
            // 
            this.labelNeedsActionInfo.AutoSize = true;
            this.labelNeedsActionInfo.Location = new System.Drawing.Point(6, 22);
            this.labelNeedsActionInfo.Name = "labelNeedsActionInfo";
            this.labelNeedsActionInfo.Size = new System.Drawing.Size(188, 13);
            this.labelNeedsActionInfo.TabIndex = 1;
            this.labelNeedsActionInfo.Text = "Here you can see pending aplications.";
            // 
            // dataGridViewNeedsAction
            // 
            this.dataGridViewNeedsAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNeedsAction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNeedsActionStatus,
            this.ColumnNeedsActionName,
            this.ColumnNeedsActionPosition,
            this.ColumnNeedsActionType,
            this.ColumnNeedsActionFirstDay,
            this.ColumnNeedsActionLastDay});
            this.dataGridViewNeedsAction.Location = new System.Drawing.Point(9, 49);
            this.dataGridViewNeedsAction.Name = "dataGridViewNeedsAction";
            this.dataGridViewNeedsAction.Size = new System.Drawing.Size(643, 266);
            this.dataGridViewNeedsAction.TabIndex = 0;
            // 
            // ColumnNeedsActionStatus
            // 
            this.ColumnNeedsActionStatus.HeaderText = "Status";
            this.ColumnNeedsActionStatus.Name = "ColumnNeedsActionStatus";
            // 
            // ColumnNeedsActionName
            // 
            this.ColumnNeedsActionName.HeaderText = "Name";
            this.ColumnNeedsActionName.Name = "ColumnNeedsActionName";
            // 
            // ColumnNeedsActionPosition
            // 
            this.ColumnNeedsActionPosition.HeaderText = "Position";
            this.ColumnNeedsActionPosition.Name = "ColumnNeedsActionPosition";
            // 
            // ColumnNeedsActionType
            // 
            this.ColumnNeedsActionType.HeaderText = "Type";
            this.ColumnNeedsActionType.Name = "ColumnNeedsActionType";
            // 
            // ColumnNeedsActionFirstDay
            // 
            this.ColumnNeedsActionFirstDay.HeaderText = "First Day";
            this.ColumnNeedsActionFirstDay.Name = "ColumnNeedsActionFirstDay";
            // 
            // ColumnNeedsActionLastDay
            // 
            this.ColumnNeedsActionLastDay.HeaderText = "Last Day";
            this.ColumnNeedsActionLastDay.Name = "ColumnNeedsActionLastDay";
            // 
            // tabPageEmployees
            // 
            this.tabPageEmployees.Controls.Add(this.buttonEmployeesAdd);
            this.tabPageEmployees.Controls.Add(this.buttonEmployeesDetailedData);
            this.tabPageEmployees.Controls.Add(this.dataGridViewEmployees);
            this.tabPageEmployees.Controls.Add(this.buttonEmployeesShowAll);
            this.tabPageEmployees.Controls.Add(this.buttonEmployeesSearch);
            this.tabPageEmployees.Controls.Add(this.comboBoxEmployeesPosition);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesPosition);
            this.tabPageEmployees.Controls.Add(this.textBoxEmployeesName);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesName);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesInfo);
            this.tabPageEmployees.Location = new System.Drawing.Point(4, 22);
            this.tabPageEmployees.Name = "tabPageEmployees";
            this.tabPageEmployees.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEmployees.Size = new System.Drawing.Size(706, 407);
            this.tabPageEmployees.TabIndex = 1;
            this.tabPageEmployees.Text = "Employees";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // buttonEmployeesAdd
            // 
            this.buttonEmployeesAdd.Location = new System.Drawing.Point(180, 324);
            this.buttonEmployeesAdd.Name = "buttonEmployeesAdd";
            this.buttonEmployeesAdd.Size = new System.Drawing.Size(96, 48);
            this.buttonEmployeesAdd.TabIndex = 9;
            this.buttonEmployeesAdd.Text = "Add employee";
            this.buttonEmployeesAdd.UseVisualStyleBackColor = true;
            this.buttonEmployeesAdd.Click += new System.EventHandler(this.buttonEmployeesAdd_Click);
            // 
            // buttonEmployeesDetailedData
            // 
            this.buttonEmployeesDetailedData.Location = new System.Drawing.Point(45, 324);
            this.buttonEmployeesDetailedData.Name = "buttonEmployeesDetailedData";
            this.buttonEmployeesDetailedData.Size = new System.Drawing.Size(99, 48);
            this.buttonEmployeesDetailedData.TabIndex = 8;
            this.buttonEmployeesDetailedData.Text = "View detailed leave data";
            this.buttonEmployeesDetailedData.UseVisualStyleBackColor = true;
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(16, 124);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.Size = new System.Drawing.Size(643, 194);
            this.dataGridViewEmployees.TabIndex = 7;
            // 
            // buttonEmployeesShowAll
            // 
            this.buttonEmployeesShowAll.Location = new System.Drawing.Point(411, 56);
            this.buttonEmployeesShowAll.Name = "buttonEmployeesShowAll";
            this.buttonEmployeesShowAll.Size = new System.Drawing.Size(75, 36);
            this.buttonEmployeesShowAll.TabIndex = 6;
            this.buttonEmployeesShowAll.Text = "Show all employees";
            this.buttonEmployeesShowAll.UseVisualStyleBackColor = true;
            // 
            // buttonEmployeesSearch
            // 
            this.buttonEmployeesSearch.Location = new System.Drawing.Point(303, 63);
            this.buttonEmployeesSearch.Name = "buttonEmployeesSearch";
            this.buttonEmployeesSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonEmployeesSearch.TabIndex = 5;
            this.buttonEmployeesSearch.Text = "Search";
            this.buttonEmployeesSearch.UseVisualStyleBackColor = true;
            // 
            // comboBoxEmployeesPosition
            // 
            this.comboBoxEmployeesPosition.FormattingEnabled = true;
            this.comboBoxEmployeesPosition.Items.AddRange(new object[] {
            "doctor"});
            this.comboBoxEmployeesPosition.Location = new System.Drawing.Point(117, 65);
            this.comboBoxEmployeesPosition.Name = "comboBoxEmployeesPosition";
            this.comboBoxEmployeesPosition.Size = new System.Drawing.Size(138, 21);
            this.comboBoxEmployeesPosition.TabIndex = 4;
            // 
            // labelEmployeesPosition
            // 
            this.labelEmployeesPosition.AutoSize = true;
            this.labelEmployeesPosition.Location = new System.Drawing.Point(10, 65);
            this.labelEmployeesPosition.Name = "labelEmployeesPosition";
            this.labelEmployeesPosition.Size = new System.Drawing.Size(82, 13);
            this.labelEmployeesPosition.TabIndex = 3;
            this.labelEmployeesPosition.Text = "Choose position";
            // 
            // textBoxEmployeesName
            // 
            this.textBoxEmployeesName.Location = new System.Drawing.Point(117, 34);
            this.textBoxEmployeesName.Name = "textBoxEmployeesName";
            this.textBoxEmployeesName.Size = new System.Drawing.Size(138, 20);
            this.textBoxEmployeesName.TabIndex = 2;
            // 
            // labelEmployeesName
            // 
            this.labelEmployeesName.AutoSize = true;
            this.labelEmployeesName.Location = new System.Drawing.Point(10, 37);
            this.labelEmployeesName.Name = "labelEmployeesName";
            this.labelEmployeesName.Size = new System.Drawing.Size(64, 13);
            this.labelEmployeesName.TabIndex = 1;
            this.labelEmployeesName.Text = "Enter name:";
            // 
            // labelEmployeesInfo
            // 
            this.labelEmployeesInfo.AutoSize = true;
            this.labelEmployeesInfo.Location = new System.Drawing.Point(7, 7);
            this.labelEmployeesInfo.Name = "labelEmployeesInfo";
            this.labelEmployeesInfo.Size = new System.Drawing.Size(217, 13);
            this.labelEmployeesInfo.TabIndex = 0;
            this.labelEmployeesInfo.Text = "Here you can view and edit employees data.";
            // 
            // tabPageReplacements
            // 
            this.tabPageReplacements.Controls.Add(this.labelReplacementsPositionFilter);
            this.tabPageReplacements.Controls.Add(this.labelReplacementsInfo);
            this.tabPageReplacements.Controls.Add(this.comboBoxReplacementsPosition);
            this.tabPageReplacements.Controls.Add(this.buttonReplacementsManage);
            this.tabPageReplacements.Controls.Add(this.dataGridViewReplacements);
            this.tabPageReplacements.Location = new System.Drawing.Point(4, 22);
            this.tabPageReplacements.Name = "tabPageReplacements";
            this.tabPageReplacements.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReplacements.Size = new System.Drawing.Size(706, 407);
            this.tabPageReplacements.TabIndex = 2;
            this.tabPageReplacements.Text = "Replacements";
            this.tabPageReplacements.UseVisualStyleBackColor = true;
            // 
            // labelReplacementsPositionFilter
            // 
            this.labelReplacementsPositionFilter.AutoSize = true;
            this.labelReplacementsPositionFilter.Location = new System.Drawing.Point(115, 42);
            this.labelReplacementsPositionFilter.Name = "labelReplacementsPositionFilter";
            this.labelReplacementsPositionFilter.Size = new System.Drawing.Size(85, 13);
            this.labelReplacementsPositionFilter.TabIndex = 7;
            this.labelReplacementsPositionFilter.Text = "Filter by position:";
            // 
            // labelReplacementsInfo
            // 
            this.labelReplacementsInfo.AutoSize = true;
            this.labelReplacementsInfo.Location = new System.Drawing.Point(8, 15);
            this.labelReplacementsInfo.Name = "labelReplacementsInfo";
            this.labelReplacementsInfo.Size = new System.Drawing.Size(181, 13);
            this.labelReplacementsInfo.TabIndex = 6;
            this.labelReplacementsInfo.Text = "Here you can manage replacements.";
            // 
            // comboBoxReplacementsPosition
            // 
            this.comboBoxReplacementsPosition.FormattingEnabled = true;
            this.comboBoxReplacementsPosition.Location = new System.Drawing.Point(221, 39);
            this.comboBoxReplacementsPosition.Name = "comboBoxReplacementsPosition";
            this.comboBoxReplacementsPosition.Size = new System.Drawing.Size(121, 21);
            this.comboBoxReplacementsPosition.TabIndex = 5;
            // 
            // buttonReplacementsManage
            // 
            this.buttonReplacementsManage.Location = new System.Drawing.Point(288, 309);
            this.buttonReplacementsManage.Name = "buttonReplacementsManage";
            this.buttonReplacementsManage.Size = new System.Drawing.Size(91, 37);
            this.buttonReplacementsManage.TabIndex = 1;
            this.buttonReplacementsManage.Text = "Manage replacement";
            this.buttonReplacementsManage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewReplacements
            // 
            this.dataGridViewReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReplacements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15});
            this.dataGridViewReplacements.Location = new System.Drawing.Point(118, 63);
            this.dataGridViewReplacements.Name = "dataGridViewReplacements";
            this.dataGridViewReplacements.Size = new System.Drawing.Size(443, 229);
            this.dataGridViewReplacements.TabIndex = 0;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Date";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "No. workers needed";
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "No. workers available";
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "position";
            this.Column15.Name = "Column15";
            // 
            // tabPageReport
            // 
            this.tabPageReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageReport.Name = "tabPageReport";
            this.tabPageReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReport.Size = new System.Drawing.Size(706, 407);
            this.tabPageReport.TabIndex = 3;
            this.tabPageReport.Text = "Report";
            this.tabPageReport.UseVisualStyleBackColor = true;
            // 
            // FormManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 433);
            this.Controls.Add(this.tabControl);
            this.Name = "FormManager";
            this.Text = "Leave Manager";
            this.tabControl.ResumeLayout(false);
            this.tabPageNeedsAction.ResumeLayout(false);
            this.tabPageNeedsAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNeedsAction)).EndInit();
            this.tabPageEmployees.ResumeLayout(false);
            this.tabPageEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.tabPageReplacements.ResumeLayout(false);
            this.tabPageReplacements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReplacements)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageNeedsAction;
        private System.Windows.Forms.Button buttonReject;
        private System.Windows.Forms.Button buttonConsider;
        private System.Windows.Forms.Label labelNeedsActionInfo;
        private System.Windows.Forms.DataGridView dataGridViewNeedsAction;
        private System.Windows.Forms.TabPage tabPageEmployees;
        private System.Windows.Forms.Button buttonEmployeesDetailedData;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.Button buttonEmployeesShowAll;
        private System.Windows.Forms.Button buttonEmployeesSearch;
        private System.Windows.Forms.ComboBox comboBoxEmployeesPosition;
        private System.Windows.Forms.Label labelEmployeesPosition;
        private System.Windows.Forms.TextBox textBoxEmployeesName;
        private System.Windows.Forms.Label labelEmployeesName;
        private System.Windows.Forms.Label labelEmployeesInfo;
        private System.Windows.Forms.TabPage tabPageReplacements;
        private System.Windows.Forms.Button buttonReplacementsManage;
        private System.Windows.Forms.DataGridView dataGridViewReplacements;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.Button buttonEmployeesAdd;
        private System.Windows.Forms.TabPage tabPageReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNeedsActionStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNeedsActionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNeedsActionPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNeedsActionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNeedsActionFirstDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNeedsActionLastDay;
        private System.Windows.Forms.Label labelReplacementsPositionFilter;
        private System.Windows.Forms.Label labelReplacementsInfo;
        private System.Windows.Forms.ComboBox comboBoxReplacementsPosition;

    }
}