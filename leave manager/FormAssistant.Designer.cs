namespace leave_manager
{
    partial class FormAssistant
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
            this.buttonRejectPendingAplication = new System.Windows.Forms.Button();
            this.buttonConsiderPendingAplication = new System.Windows.Forms.Button();
            this.labelTabNeedsActionInfo = new System.Windows.Forms.Label();
            this.dataGridViewPendingAplications = new System.Windows.Forms.DataGridView();
            this.ColumnPendingAplicationsStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPendingAplicationsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPendingAplicationsPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPendingAplicationsType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPendingAplicationsFirstDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPendingAplicationsLastDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.buttonEmployeesDetailedData = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.ColumnEmployeesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEmployeesSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEmployeesBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEmployeesAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEmployeesPesel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEmployeesPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.ColumnReplacementsDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplacementsWorkersNeeded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplacementsWorkersAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplacementsPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabPageNeedsAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPendingAplications)).BeginInit();
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
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(711, 418);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageNeedsAction
            // 
            this.tabPageNeedsAction.Controls.Add(this.buttonRejectPendingAplication);
            this.tabPageNeedsAction.Controls.Add(this.buttonConsiderPendingAplication);
            this.tabPageNeedsAction.Controls.Add(this.labelTabNeedsActionInfo);
            this.tabPageNeedsAction.Controls.Add(this.dataGridViewPendingAplications);
            this.tabPageNeedsAction.Location = new System.Drawing.Point(4, 22);
            this.tabPageNeedsAction.Name = "tabPageNeedsAction";
            this.tabPageNeedsAction.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNeedsAction.Size = new System.Drawing.Size(703, 392);
            this.tabPageNeedsAction.TabIndex = 0;
            this.tabPageNeedsAction.Text = "Needs your action";
            this.tabPageNeedsAction.UseVisualStyleBackColor = true;
            this.tabPageNeedsAction.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // buttonRejectPendingAplication
            // 
            this.buttonRejectPendingAplication.Location = new System.Drawing.Point(480, 330);
            this.buttonRejectPendingAplication.Name = "buttonRejectPendingAplication";
            this.buttonRejectPendingAplication.Size = new System.Drawing.Size(91, 41);
            this.buttonRejectPendingAplication.TabIndex = 3;
            this.buttonRejectPendingAplication.Text = "Reject without consideration";
            this.buttonRejectPendingAplication.UseVisualStyleBackColor = true;
            // 
            // buttonConsiderPendingAplication
            // 
            this.buttonConsiderPendingAplication.Location = new System.Drawing.Point(86, 339);
            this.buttonConsiderPendingAplication.Name = "buttonConsiderPendingAplication";
            this.buttonConsiderPendingAplication.Size = new System.Drawing.Size(75, 23);
            this.buttonConsiderPendingAplication.TabIndex = 2;
            this.buttonConsiderPendingAplication.Text = "Consider";
            this.buttonConsiderPendingAplication.UseVisualStyleBackColor = true;
            this.buttonConsiderPendingAplication.Click += new System.EventHandler(this.buttonConsiderPendingAplication_Click);
            // 
            // labelTabNeedsActionInfo
            // 
            this.labelTabNeedsActionInfo.AutoSize = true;
            this.labelTabNeedsActionInfo.Location = new System.Drawing.Point(6, 22);
            this.labelTabNeedsActionInfo.Name = "labelTabNeedsActionInfo";
            this.labelTabNeedsActionInfo.Size = new System.Drawing.Size(188, 13);
            this.labelTabNeedsActionInfo.TabIndex = 1;
            this.labelTabNeedsActionInfo.Text = "Here you can see pending aplications.";
            // 
            // dataGridViewPendingAplications
            // 
            this.dataGridViewPendingAplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPendingAplications.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPendingAplicationsStatus,
            this.ColumnPendingAplicationsName,
            this.ColumnPendingAplicationsPosition,
            this.ColumnPendingAplicationsType,
            this.ColumnPendingAplicationsFirstDay,
            this.ColumnPendingAplicationsLastDay});
            this.dataGridViewPendingAplications.Location = new System.Drawing.Point(9, 49);
            this.dataGridViewPendingAplications.Name = "dataGridViewPendingAplications";
            this.dataGridViewPendingAplications.Size = new System.Drawing.Size(643, 266);
            this.dataGridViewPendingAplications.TabIndex = 0;
            // 
            // ColumnPendingAplicationsStatus
            // 
            this.ColumnPendingAplicationsStatus.HeaderText = "Status";
            this.ColumnPendingAplicationsStatus.Name = "ColumnPendingAplicationsStatus";
            // 
            // ColumnPendingAplicationsName
            // 
            this.ColumnPendingAplicationsName.HeaderText = "Name";
            this.ColumnPendingAplicationsName.Name = "ColumnPendingAplicationsName";
            // 
            // ColumnPendingAplicationsPosition
            // 
            this.ColumnPendingAplicationsPosition.HeaderText = "Position";
            this.ColumnPendingAplicationsPosition.Name = "ColumnPendingAplicationsPosition";
            // 
            // ColumnPendingAplicationsType
            // 
            this.ColumnPendingAplicationsType.HeaderText = "Type";
            this.ColumnPendingAplicationsType.Name = "ColumnPendingAplicationsType";
            // 
            // ColumnPendingAplicationsFirstDay
            // 
            this.ColumnPendingAplicationsFirstDay.HeaderText = "First Day";
            this.ColumnPendingAplicationsFirstDay.Name = "ColumnPendingAplicationsFirstDay";
            // 
            // ColumnPendingAplicationsLastDay
            // 
            this.ColumnPendingAplicationsLastDay.HeaderText = "Last Day";
            this.ColumnPendingAplicationsLastDay.Name = "ColumnPendingAplicationsLastDay";
            // 
            // tabPageEmployees
            // 
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
            this.tabPageEmployees.Size = new System.Drawing.Size(703, 392);
            this.tabPageEmployees.TabIndex = 1;
            this.tabPageEmployees.Text = "Employees";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
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
            this.dataGridViewEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEmployeesName,
            this.ColumnEmployeesSurname,
            this.ColumnEmployeesBirthDate,
            this.ColumnEmployeesAdress,
            this.ColumnEmployeesPesel,
            this.ColumnEmployeesPosition});
            this.dataGridViewEmployees.Location = new System.Drawing.Point(16, 124);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.Size = new System.Drawing.Size(643, 194);
            this.dataGridViewEmployees.TabIndex = 7;
            // 
            // ColumnEmployeesName
            // 
            this.ColumnEmployeesName.HeaderText = "Name";
            this.ColumnEmployeesName.Name = "ColumnEmployeesName";
            // 
            // ColumnEmployeesSurname
            // 
            this.ColumnEmployeesSurname.HeaderText = "Surname";
            this.ColumnEmployeesSurname.Name = "ColumnEmployeesSurname";
            // 
            // ColumnEmployeesBirthDate
            // 
            this.ColumnEmployeesBirthDate.HeaderText = "Birth date";
            this.ColumnEmployeesBirthDate.Name = "ColumnEmployeesBirthDate";
            // 
            // ColumnEmployeesAdress
            // 
            this.ColumnEmployeesAdress.HeaderText = "Adress";
            this.ColumnEmployeesAdress.Name = "ColumnEmployeesAdress";
            // 
            // ColumnEmployeesPesel
            // 
            this.ColumnEmployeesPesel.HeaderText = "PESEL";
            this.ColumnEmployeesPesel.Name = "ColumnEmployeesPesel";
            // 
            // ColumnEmployeesPosition
            // 
            this.ColumnEmployeesPosition.HeaderText = "Position";
            this.ColumnEmployeesPosition.Name = "ColumnEmployeesPosition";
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
            this.labelEmployeesPosition.Click += new System.EventHandler(this.label4_Click);
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
            this.labelEmployeesInfo.Size = new System.Drawing.Size(176, 13);
            this.labelEmployeesInfo.TabIndex = 0;
            this.labelEmployeesInfo.Text = "Here you can view employees data.";
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
            this.tabPageReplacements.Size = new System.Drawing.Size(703, 392);
            this.tabPageReplacements.TabIndex = 2;
            this.tabPageReplacements.Text = "Replacements";
            this.tabPageReplacements.UseVisualStyleBackColor = true;
            // 
            // labelReplacementsPositionFilter
            // 
            this.labelReplacementsPositionFilter.AutoSize = true;
            this.labelReplacementsPositionFilter.Location = new System.Drawing.Point(115, 39);
            this.labelReplacementsPositionFilter.Name = "labelReplacementsPositionFilter";
            this.labelReplacementsPositionFilter.Size = new System.Drawing.Size(85, 13);
            this.labelReplacementsPositionFilter.TabIndex = 4;
            this.labelReplacementsPositionFilter.Text = "Filter by position:";
            // 
            // labelReplacementsInfo
            // 
            this.labelReplacementsInfo.AutoSize = true;
            this.labelReplacementsInfo.Location = new System.Drawing.Point(8, 12);
            this.labelReplacementsInfo.Name = "labelReplacementsInfo";
            this.labelReplacementsInfo.Size = new System.Drawing.Size(181, 13);
            this.labelReplacementsInfo.TabIndex = 3;
            this.labelReplacementsInfo.Text = "Here you can manage replacements.";
            // 
            // comboBoxReplacementsPosition
            // 
            this.comboBoxReplacementsPosition.FormattingEnabled = true;
            this.comboBoxReplacementsPosition.Location = new System.Drawing.Point(221, 36);
            this.comboBoxReplacementsPosition.Name = "comboBoxReplacementsPosition";
            this.comboBoxReplacementsPosition.Size = new System.Drawing.Size(121, 21);
            this.comboBoxReplacementsPosition.TabIndex = 2;
            // 
            // buttonReplacementsManage
            // 
            this.buttonReplacementsManage.Location = new System.Drawing.Point(288, 309);
            this.buttonReplacementsManage.Name = "buttonReplacementsManage";
            this.buttonReplacementsManage.Size = new System.Drawing.Size(91, 37);
            this.buttonReplacementsManage.TabIndex = 1;
            this.buttonReplacementsManage.Text = "Manage replacement";
            this.buttonReplacementsManage.UseVisualStyleBackColor = true;
            this.buttonReplacementsManage.Click += new System.EventHandler(this.button6_Click);
            // 
            // dataGridViewReplacements
            // 
            this.dataGridViewReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReplacements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnReplacementsDate,
            this.ColumnReplacementsWorkersNeeded,
            this.ColumnReplacementsWorkersAvailable,
            this.ColumnReplacementsPosition});
            this.dataGridViewReplacements.Location = new System.Drawing.Point(118, 63);
            this.dataGridViewReplacements.Name = "dataGridViewReplacements";
            this.dataGridViewReplacements.Size = new System.Drawing.Size(443, 229);
            this.dataGridViewReplacements.TabIndex = 0;
            // 
            // ColumnReplacementsDate
            // 
            this.ColumnReplacementsDate.HeaderText = "Date";
            this.ColumnReplacementsDate.Name = "ColumnReplacementsDate";
            // 
            // ColumnReplacementsWorkersNeeded
            // 
            this.ColumnReplacementsWorkersNeeded.HeaderText = "No. workers needed";
            this.ColumnReplacementsWorkersNeeded.Name = "ColumnReplacementsWorkersNeeded";
            // 
            // ColumnReplacementsWorkersAvailable
            // 
            this.ColumnReplacementsWorkersAvailable.HeaderText = "No. workers available";
            this.ColumnReplacementsWorkersAvailable.Name = "ColumnReplacementsWorkersAvailable";
            // 
            // ColumnReplacementsPosition
            // 
            this.ColumnReplacementsPosition.HeaderText = "position";
            this.ColumnReplacementsPosition.Name = "ColumnReplacementsPosition";
            // 
            // FormAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 418);
            this.Controls.Add(this.tabControl);
            this.Name = "FormAssistant";
            this.Text = "Leave Manager";
            this.tabControl.ResumeLayout(false);
            this.tabPageNeedsAction.ResumeLayout(false);
            this.tabPageNeedsAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPendingAplications)).EndInit();
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
        private System.Windows.Forms.TabPage tabPageEmployees;
        private System.Windows.Forms.DataGridView dataGridViewPendingAplications;
        private System.Windows.Forms.Button buttonConsiderPendingAplication;
        private System.Windows.Forms.Label labelTabNeedsActionInfo;
        private System.Windows.Forms.Button buttonRejectPendingAplication;
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
        private System.Windows.Forms.ComboBox comboBoxReplacementsPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPendingAplicationsStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPendingAplicationsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPendingAplicationsPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPendingAplicationsType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPendingAplicationsFirstDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPendingAplicationsLastDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmployeesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmployeesSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmployeesBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmployeesAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmployeesPesel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmployeesPosition;
        private System.Windows.Forms.Label labelReplacementsPositionFilter;
        private System.Windows.Forms.Label labelReplacementsInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementsDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementsWorkersNeeded;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementsWorkersAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementsPosition;
    }
}