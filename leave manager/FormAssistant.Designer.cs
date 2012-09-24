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
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.textBoxEmployeesPesel = new System.Windows.Forms.TextBox();
            this.textBoxEmployeesSurname = new System.Windows.Forms.TextBox();
            this.labelEmployeesPesel = new System.Windows.Forms.Label();
            this.labelEmployeesSurname = new System.Windows.Forms.Label();
            this.buttonEmployeesDetailedData = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
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
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplacementsWorkersNeeded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplacementsWorkersAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(948, 521);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageNeedsAction
            // 
            this.tabPageNeedsAction.Controls.Add(this.buttonRejectPendingAplication);
            this.tabPageNeedsAction.Controls.Add(this.buttonConsiderPendingAplication);
            this.tabPageNeedsAction.Controls.Add(this.labelTabNeedsActionInfo);
            this.tabPageNeedsAction.Controls.Add(this.dataGridViewPendingAplications);
            this.tabPageNeedsAction.Location = new System.Drawing.Point(4, 25);
            this.tabPageNeedsAction.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageNeedsAction.Name = "tabPageNeedsAction";
            this.tabPageNeedsAction.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageNeedsAction.Size = new System.Drawing.Size(940, 492);
            this.tabPageNeedsAction.TabIndex = 0;
            this.tabPageNeedsAction.Text = "Needs your action";
            this.tabPageNeedsAction.UseVisualStyleBackColor = true;
            // 
            // buttonRejectPendingAplication
            // 
            this.buttonRejectPendingAplication.Location = new System.Drawing.Point(640, 406);
            this.buttonRejectPendingAplication.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRejectPendingAplication.Name = "buttonRejectPendingAplication";
            this.buttonRejectPendingAplication.Size = new System.Drawing.Size(121, 50);
            this.buttonRejectPendingAplication.TabIndex = 3;
            this.buttonRejectPendingAplication.Text = "Reject without consideration";
            this.buttonRejectPendingAplication.UseVisualStyleBackColor = true;
            // 
            // buttonConsiderPendingAplication
            // 
            this.buttonConsiderPendingAplication.Location = new System.Drawing.Point(115, 417);
            this.buttonConsiderPendingAplication.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConsiderPendingAplication.Name = "buttonConsiderPendingAplication";
            this.buttonConsiderPendingAplication.Size = new System.Drawing.Size(100, 28);
            this.buttonConsiderPendingAplication.TabIndex = 2;
            this.buttonConsiderPendingAplication.Text = "Consider";
            this.buttonConsiderPendingAplication.UseVisualStyleBackColor = true;
            this.buttonConsiderPendingAplication.Click += new System.EventHandler(this.buttonConsiderPendingAplication_Click);
            // 
            // labelTabNeedsActionInfo
            // 
            this.labelTabNeedsActionInfo.AutoSize = true;
            this.labelTabNeedsActionInfo.Location = new System.Drawing.Point(8, 27);
            this.labelTabNeedsActionInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTabNeedsActionInfo.Name = "labelTabNeedsActionInfo";
            this.labelTabNeedsActionInfo.Size = new System.Drawing.Size(250, 17);
            this.labelTabNeedsActionInfo.TabIndex = 1;
            this.labelTabNeedsActionInfo.Text = "Here you can see pending aplications.";
            // 
            // dataGridViewPendingAplications
            // 
            this.dataGridViewPendingAplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPendingAplications.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewPendingAplications.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewPendingAplications.Name = "dataGridViewPendingAplications";
            this.dataGridViewPendingAplications.ReadOnly = true;
            this.dataGridViewPendingAplications.RowTemplate.Height = 24;
            this.dataGridViewPendingAplications.Size = new System.Drawing.Size(857, 327);
            this.dataGridViewPendingAplications.TabIndex = 0;
            // 
            // tabPageEmployees
            // 
            this.tabPageEmployees.Controls.Add(this.textBoxEmployeesPesel);
            this.tabPageEmployees.Controls.Add(this.textBoxEmployeesSurname);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesPesel);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesSurname);
            this.tabPageEmployees.Controls.Add(this.buttonEmployeesDetailedData);
            this.tabPageEmployees.Controls.Add(this.dataGridViewEmployees);
            this.tabPageEmployees.Controls.Add(this.buttonEmployeesSearch);
            this.tabPageEmployees.Controls.Add(this.comboBoxEmployeesPosition);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesPosition);
            this.tabPageEmployees.Controls.Add(this.textBoxEmployeesName);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesName);
            this.tabPageEmployees.Controls.Add(this.labelEmployeesInfo);
            this.tabPageEmployees.Location = new System.Drawing.Point(4, 25);
            this.tabPageEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageEmployees.Name = "tabPageEmployees";
            this.tabPageEmployees.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageEmployees.Size = new System.Drawing.Size(940, 492);
            this.tabPageEmployees.TabIndex = 1;
            this.tabPageEmployees.Text = "Employees";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // textBoxEmployeesPesel
            // 
            this.textBoxEmployeesPesel.Location = new System.Drawing.Point(89, 74);
            this.textBoxEmployeesPesel.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEmployeesPesel.Name = "textBoxEmployeesPesel";
            this.textBoxEmployeesPesel.Size = new System.Drawing.Size(267, 22);
            this.textBoxEmployeesPesel.TabIndex = 12;
            // 
            // textBoxEmployeesSurname
            // 
            this.textBoxEmployeesSurname.Location = new System.Drawing.Point(557, 42);
            this.textBoxEmployeesSurname.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEmployeesSurname.Name = "textBoxEmployeesSurname";
            this.textBoxEmployeesSurname.Size = new System.Drawing.Size(225, 22);
            this.textBoxEmployeesSurname.TabIndex = 11;
            // 
            // labelEmployeesPesel
            // 
            this.labelEmployeesPesel.AutoSize = true;
            this.labelEmployeesPesel.Location = new System.Drawing.Point(13, 78);
            this.labelEmployeesPesel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesPesel.Name = "labelEmployeesPesel";
            this.labelEmployeesPesel.Size = new System.Drawing.Size(56, 17);
            this.labelEmployeesPesel.TabIndex = 10;
            this.labelEmployeesPesel.Text = "PESEL:";
            // 
            // labelEmployeesSurname
            // 
            this.labelEmployeesSurname.AutoSize = true;
            this.labelEmployeesSurname.Location = new System.Drawing.Point(440, 46);
            this.labelEmployeesSurname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesSurname.Name = "labelEmployeesSurname";
            this.labelEmployeesSurname.Size = new System.Drawing.Size(69, 17);
            this.labelEmployeesSurname.TabIndex = 9;
            this.labelEmployeesSurname.Text = "Surname:";
            // 
            // buttonEmployeesDetailedData
            // 
            this.buttonEmployeesDetailedData.Location = new System.Drawing.Point(49, 433);
            this.buttonEmployeesDetailedData.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployeesDetailedData.Name = "buttonEmployeesDetailedData";
            this.buttonEmployeesDetailedData.Size = new System.Drawing.Size(132, 59);
            this.buttonEmployeesDetailedData.TabIndex = 8;
            this.buttonEmployeesDetailedData.Text = "View detailed leave dataLeaves";
            this.buttonEmployeesDetailedData.UseVisualStyleBackColor = true;
            this.buttonEmployeesDetailedData.Click += new System.EventHandler(this.buttonEmployeesDetailedData_Click);
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(11, 187);
            this.dataGridViewEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.RowTemplate.Height = 24;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(857, 239);
            this.dataGridViewEmployees.TabIndex = 7;
            // 
            // buttonEmployeesSearch
            // 
            this.buttonEmployeesSearch.Location = new System.Drawing.Point(257, 135);
            this.buttonEmployeesSearch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployeesSearch.Name = "buttonEmployeesSearch";
            this.buttonEmployeesSearch.Size = new System.Drawing.Size(100, 44);
            this.buttonEmployeesSearch.TabIndex = 5;
            this.buttonEmployeesSearch.Text = "Search";
            this.buttonEmployeesSearch.UseVisualStyleBackColor = true;
            this.buttonEmployeesSearch.Click += new System.EventHandler(this.buttonEmployeesSearch_Click);
            // 
            // comboBoxEmployeesPosition
            // 
            this.comboBoxEmployeesPosition.FormattingEnabled = true;
            this.comboBoxEmployeesPosition.Location = new System.Drawing.Point(557, 78);
            this.comboBoxEmployeesPosition.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEmployeesPosition.Name = "comboBoxEmployeesPosition";
            this.comboBoxEmployeesPosition.Size = new System.Drawing.Size(225, 24);
            this.comboBoxEmployeesPosition.TabIndex = 4;
            // 
            // labelEmployeesPosition
            // 
            this.labelEmployeesPosition.AutoSize = true;
            this.labelEmployeesPosition.Location = new System.Drawing.Point(440, 81);
            this.labelEmployeesPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesPosition.Name = "labelEmployeesPosition";
            this.labelEmployeesPosition.Size = new System.Drawing.Size(109, 17);
            this.labelEmployeesPosition.TabIndex = 3;
            this.labelEmployeesPosition.Text = "Choose position";
            // 
            // textBoxEmployeesName
            // 
            this.textBoxEmployeesName.Location = new System.Drawing.Point(89, 42);
            this.textBoxEmployeesName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEmployeesName.Name = "textBoxEmployeesName";
            this.textBoxEmployeesName.Size = new System.Drawing.Size(267, 22);
            this.textBoxEmployeesName.TabIndex = 2;
            // 
            // labelEmployeesName
            // 
            this.labelEmployeesName.AutoSize = true;
            this.labelEmployeesName.Location = new System.Drawing.Point(13, 46);
            this.labelEmployeesName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesName.Name = "labelEmployeesName";
            this.labelEmployeesName.Size = new System.Drawing.Size(49, 17);
            this.labelEmployeesName.TabIndex = 1;
            this.labelEmployeesName.Text = "Name:";
            // 
            // labelEmployeesInfo
            // 
            this.labelEmployeesInfo.AutoSize = true;
            this.labelEmployeesInfo.Location = new System.Drawing.Point(9, 9);
            this.labelEmployeesInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesInfo.Name = "labelEmployeesInfo";
            this.labelEmployeesInfo.Size = new System.Drawing.Size(284, 17);
            this.labelEmployeesInfo.TabIndex = 0;
            this.labelEmployeesInfo.Text = "Here you can view dataLeaves dataLeaves.";
            // 
            // tabPageReplacements
            // 
            this.tabPageReplacements.Controls.Add(this.labelReplacementsPositionFilter);
            this.tabPageReplacements.Controls.Add(this.labelReplacementsInfo);
            this.tabPageReplacements.Controls.Add(this.comboBoxReplacementsPosition);
            this.tabPageReplacements.Controls.Add(this.buttonReplacementsManage);
            this.tabPageReplacements.Controls.Add(this.dataGridViewReplacements);
            this.tabPageReplacements.Location = new System.Drawing.Point(4, 25);
            this.tabPageReplacements.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageReplacements.Name = "tabPageReplacements";
            this.tabPageReplacements.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageReplacements.Size = new System.Drawing.Size(940, 492);
            this.tabPageReplacements.TabIndex = 2;
            this.tabPageReplacements.Text = "Replacements";
            this.tabPageReplacements.UseVisualStyleBackColor = true;
            // 
            // labelReplacementsPositionFilter
            // 
            this.labelReplacementsPositionFilter.AutoSize = true;
            this.labelReplacementsPositionFilter.Location = new System.Drawing.Point(153, 48);
            this.labelReplacementsPositionFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReplacementsPositionFilter.Name = "labelReplacementsPositionFilter";
            this.labelReplacementsPositionFilter.Size = new System.Drawing.Size(115, 17);
            this.labelReplacementsPositionFilter.TabIndex = 4;
            this.labelReplacementsPositionFilter.Text = "Filter by position:";
            // 
            // labelReplacementsInfo
            // 
            this.labelReplacementsInfo.AutoSize = true;
            this.labelReplacementsInfo.Location = new System.Drawing.Point(11, 15);
            this.labelReplacementsInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReplacementsInfo.Name = "labelReplacementsInfo";
            this.labelReplacementsInfo.Size = new System.Drawing.Size(241, 17);
            this.labelReplacementsInfo.TabIndex = 3;
            this.labelReplacementsInfo.Text = "Here you can manage replacements.";
            // 
            // comboBoxReplacementsPosition
            // 
            this.comboBoxReplacementsPosition.FormattingEnabled = true;
            this.comboBoxReplacementsPosition.Location = new System.Drawing.Point(295, 44);
            this.comboBoxReplacementsPosition.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxReplacementsPosition.Name = "comboBoxReplacementsPosition";
            this.comboBoxReplacementsPosition.Size = new System.Drawing.Size(160, 24);
            this.comboBoxReplacementsPosition.TabIndex = 2;
            this.comboBoxReplacementsPosition.SelectedIndexChanged += new System.EventHandler(this.comboBoxReplacementsPosition_SelectedIndexChanged);
            // 
            // buttonReplacementsManage
            // 
            this.buttonReplacementsManage.Location = new System.Drawing.Point(384, 380);
            this.buttonReplacementsManage.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReplacementsManage.Name = "buttonReplacementsManage";
            this.buttonReplacementsManage.Size = new System.Drawing.Size(121, 46);
            this.buttonReplacementsManage.TabIndex = 1;
            this.buttonReplacementsManage.Text = "Manage replacement";
            this.buttonReplacementsManage.UseVisualStyleBackColor = true;
            this.buttonReplacementsManage.Click += new System.EventHandler(this.buttonReplacementsManage_Click);
            // 
            // dataGridViewReplacements
            // 
            this.dataGridViewReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReplacements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.ColumnReplacementsWorkersNeeded,
            this.ColumnReplacementsWorkersAvailable,
            this.Position});
            this.dataGridViewReplacements.Location = new System.Drawing.Point(157, 78);
            this.dataGridViewReplacements.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewReplacements.Name = "dataGridViewReplacements";
            this.dataGridViewReplacements.ReadOnly = true;
            this.dataGridViewReplacements.RowTemplate.Height = 24;
            this.dataGridViewReplacements.Size = new System.Drawing.Size(591, 282);
            this.dataGridViewReplacements.TabIndex = 0;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // ColumnReplacementsWorkersNeeded
            // 
            this.ColumnReplacementsWorkersNeeded.HeaderText = "No. workers needed";
            this.ColumnReplacementsWorkersNeeded.Name = "ColumnReplacementsWorkersNeeded";
            this.ColumnReplacementsWorkersNeeded.ReadOnly = true;
            // 
            // ColumnReplacementsWorkersAvailable
            // 
            this.ColumnReplacementsWorkersAvailable.HeaderText = "No. workers available";
            this.ColumnReplacementsWorkersAvailable.Name = "ColumnReplacementsWorkersAvailable";
            this.ColumnReplacementsWorkersAvailable.ReadOnly = true;
            // 
            // Position
            // 
            this.Position.HeaderText = "position";
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            // 
            // FormAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 549);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAssistant";
            this.Text = "Leave Manager";
            this.Controls.SetChildIndex(this.tabControl, 0);
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
            this.PerformLayout();

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
        private System.Windows.Forms.Label labelReplacementsPositionFilter;
        private System.Windows.Forms.Label labelReplacementsInfo;
        private System.Windows.Forms.TextBox textBoxEmployeesPesel;
        private System.Windows.Forms.TextBox textBoxEmployeesSurname;
        private System.Windows.Forms.Label labelEmployeesPesel;
        private System.Windows.Forms.Label labelEmployeesSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementsWorkersNeeded;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementsWorkersAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
    }
}