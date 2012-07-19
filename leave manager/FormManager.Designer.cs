﻿namespace leave_manager
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
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.buttonEmployeeEdit = new System.Windows.Forms.Button();
            this.textBoxEmployeesPesel = new System.Windows.Forms.TextBox();
            this.textBoxEmployeesSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Surname = new System.Windows.Forms.Label();
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
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(952, 533);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageNeedsAction
            // 
            this.tabPageNeedsAction.Controls.Add(this.buttonReject);
            this.tabPageNeedsAction.Controls.Add(this.buttonConsider);
            this.tabPageNeedsAction.Controls.Add(this.labelNeedsActionInfo);
            this.tabPageNeedsAction.Controls.Add(this.dataGridViewNeedsAction);
            this.tabPageNeedsAction.Location = new System.Drawing.Point(4, 25);
            this.tabPageNeedsAction.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageNeedsAction.Name = "tabPageNeedsAction";
            this.tabPageNeedsAction.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageNeedsAction.Size = new System.Drawing.Size(944, 504);
            this.tabPageNeedsAction.TabIndex = 0;
            this.tabPageNeedsAction.Text = "Needs your action";
            this.tabPageNeedsAction.UseVisualStyleBackColor = true;
            // 
            // buttonReject
            // 
            this.buttonReject.Location = new System.Drawing.Point(640, 406);
            this.buttonReject.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(121, 50);
            this.buttonReject.TabIndex = 3;
            this.buttonReject.Text = "Reject without consideration";
            this.buttonReject.UseVisualStyleBackColor = true;
            // 
            // buttonConsider
            // 
            this.buttonConsider.Location = new System.Drawing.Point(115, 417);
            this.buttonConsider.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConsider.Name = "buttonConsider";
            this.buttonConsider.Size = new System.Drawing.Size(100, 28);
            this.buttonConsider.TabIndex = 2;
            this.buttonConsider.Text = "Consider";
            this.buttonConsider.UseVisualStyleBackColor = true;
            this.buttonConsider.Click += new System.EventHandler(this.buttonConsider_Click);
            // 
            // labelNeedsActionInfo
            // 
            this.labelNeedsActionInfo.AutoSize = true;
            this.labelNeedsActionInfo.Location = new System.Drawing.Point(8, 27);
            this.labelNeedsActionInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNeedsActionInfo.Name = "labelNeedsActionInfo";
            this.labelNeedsActionInfo.Size = new System.Drawing.Size(250, 17);
            this.labelNeedsActionInfo.TabIndex = 1;
            this.labelNeedsActionInfo.Text = "Here you can see pending aplications.";
            // 
            // dataGridViewNeedsAction
            // 
            this.dataGridViewNeedsAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNeedsAction.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewNeedsAction.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewNeedsAction.Name = "dataGridViewNeedsAction";
            this.dataGridViewNeedsAction.RowTemplate.Height = 24;
            this.dataGridViewNeedsAction.Size = new System.Drawing.Size(857, 327);
            this.dataGridViewNeedsAction.TabIndex = 0;
            // 
            // tabPageEmployees
            // 
            this.tabPageEmployees.Controls.Add(this.buttonEmployeeEdit);
            this.tabPageEmployees.Controls.Add(this.textBoxEmployeesPesel);
            this.tabPageEmployees.Controls.Add(this.textBoxEmployeesSurname);
            this.tabPageEmployees.Controls.Add(this.label2);
            this.tabPageEmployees.Controls.Add(this.Surname);
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
            this.tabPageEmployees.Location = new System.Drawing.Point(4, 25);
            this.tabPageEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageEmployees.Name = "tabPageEmployees";
            this.tabPageEmployees.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageEmployees.Size = new System.Drawing.Size(944, 504);
            this.tabPageEmployees.TabIndex = 1;
            this.tabPageEmployees.Text = "Employees";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // buttonEmployeeEdit
            // 
            this.buttonEmployeeEdit.Location = new System.Drawing.Point(631, 437);
            this.buttonEmployeeEdit.Name = "buttonEmployeeEdit";
            this.buttonEmployeeEdit.Size = new System.Drawing.Size(115, 59);
            this.buttonEmployeeEdit.TabIndex = 14;
            this.buttonEmployeeEdit.Text = "Edit employee";
            this.buttonEmployeeEdit.UseVisualStyleBackColor = true;
            this.buttonEmployeeEdit.Click += new System.EventHandler(this.buttonEmployeeEdit_Click);
            // 
            // textBoxEmployeesPesel
            // 
            this.textBoxEmployeesPesel.Location = new System.Drawing.Point(156, 82);
            this.textBoxEmployeesPesel.Name = "textBoxEmployeesPesel";
            this.textBoxEmployeesPesel.Size = new System.Drawing.Size(183, 22);
            this.textBoxEmployeesPesel.TabIndex = 13;
            // 
            // textBoxEmployeesSurname
            // 
            this.textBoxEmployeesSurname.Location = new System.Drawing.Point(612, 46);
            this.textBoxEmployeesSurname.Name = "textBoxEmployeesSurname";
            this.textBoxEmployeesSurname.Size = new System.Drawing.Size(184, 22);
            this.textBoxEmployeesSurname.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "PESEL";
            // 
            // Surname
            // 
            this.Surname.AutoSize = true;
            this.Surname.Location = new System.Drawing.Point(470, 46);
            this.Surname.Name = "Surname";
            this.Surname.Size = new System.Drawing.Size(65, 17);
            this.Surname.TabIndex = 10;
            this.Surname.Text = "Surname";
            // 
            // buttonEmployeesAdd
            // 
            this.buttonEmployeesAdd.Location = new System.Drawing.Point(358, 438);
            this.buttonEmployeesAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployeesAdd.Name = "buttonEmployeesAdd";
            this.buttonEmployeesAdd.Size = new System.Drawing.Size(128, 58);
            this.buttonEmployeesAdd.TabIndex = 9;
            this.buttonEmployeesAdd.Text = "Add employee";
            this.buttonEmployeesAdd.UseVisualStyleBackColor = true;
            this.buttonEmployeesAdd.Click += new System.EventHandler(this.buttonEmployeesAdd_Click);
            // 
            // buttonEmployeesDetailedData
            // 
            this.buttonEmployeesDetailedData.Location = new System.Drawing.Point(71, 437);
            this.buttonEmployeesDetailedData.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployeesDetailedData.Name = "buttonEmployeesDetailedData";
            this.buttonEmployeesDetailedData.Size = new System.Drawing.Size(132, 59);
            this.buttonEmployeesDetailedData.TabIndex = 8;
            this.buttonEmployeesDetailedData.Text = "View detailed leave dataLeaves";
            this.buttonEmployeesDetailedData.UseVisualStyleBackColor = true;
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(32, 191);
            this.dataGridViewEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.RowTemplate.Height = 24;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(857, 239);
            this.dataGridViewEmployees.TabIndex = 7;
            // 
            // buttonEmployeesShowAll
            // 
            this.buttonEmployeesShowAll.Location = new System.Drawing.Point(473, 125);
            this.buttonEmployeesShowAll.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployeesShowAll.Name = "buttonEmployeesShowAll";
            this.buttonEmployeesShowAll.Size = new System.Drawing.Size(100, 44);
            this.buttonEmployeesShowAll.TabIndex = 6;
            this.buttonEmployeesShowAll.Text = "Show all dataLeaves";
            this.buttonEmployeesShowAll.UseVisualStyleBackColor = true;
            // 
            // buttonEmployeesSearch
            // 
            this.buttonEmployeesSearch.Location = new System.Drawing.Point(156, 141);
            this.buttonEmployeesSearch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployeesSearch.Name = "buttonEmployeesSearch";
            this.buttonEmployeesSearch.Size = new System.Drawing.Size(100, 28);
            this.buttonEmployeesSearch.TabIndex = 5;
            this.buttonEmployeesSearch.Text = "Search";
            this.buttonEmployeesSearch.UseVisualStyleBackColor = true;
            this.buttonEmployeesSearch.Click += new System.EventHandler(this.buttonEmployeesSearch_Click);
            // 
            // comboBoxEmployeesPosition
            // 
            this.comboBoxEmployeesPosition.FormattingEnabled = true;
            this.comboBoxEmployeesPosition.Location = new System.Drawing.Point(613, 79);
            this.comboBoxEmployeesPosition.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEmployeesPosition.Name = "comboBoxEmployeesPosition";
            this.comboBoxEmployeesPosition.Size = new System.Drawing.Size(183, 24);
            this.comboBoxEmployeesPosition.TabIndex = 4;
            // 
            // labelEmployeesPosition
            // 
            this.labelEmployeesPosition.AutoSize = true;
            this.labelEmployeesPosition.Location = new System.Drawing.Point(470, 79);
            this.labelEmployeesPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesPosition.Name = "labelEmployeesPosition";
            this.labelEmployeesPosition.Size = new System.Drawing.Size(109, 17);
            this.labelEmployeesPosition.TabIndex = 3;
            this.labelEmployeesPosition.Text = "Choose position";
            // 
            // textBoxEmployeesName
            // 
            this.textBoxEmployeesName.Location = new System.Drawing.Point(156, 42);
            this.textBoxEmployeesName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEmployeesName.Name = "textBoxEmployeesName";
            this.textBoxEmployeesName.Size = new System.Drawing.Size(183, 22);
            this.textBoxEmployeesName.TabIndex = 2;
            // 
            // labelEmployeesName
            // 
            this.labelEmployeesName.AutoSize = true;
            this.labelEmployeesName.Location = new System.Drawing.Point(13, 46);
            this.labelEmployeesName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesName.Name = "labelEmployeesName";
            this.labelEmployeesName.Size = new System.Drawing.Size(85, 17);
            this.labelEmployeesName.TabIndex = 1;
            this.labelEmployeesName.Text = "Enter name:";
            // 
            // labelEmployeesInfo
            // 
            this.labelEmployeesInfo.AutoSize = true;
            this.labelEmployeesInfo.Location = new System.Drawing.Point(9, 9);
            this.labelEmployeesInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmployeesInfo.Name = "labelEmployeesInfo";
            this.labelEmployeesInfo.Size = new System.Drawing.Size(339, 17);
            this.labelEmployeesInfo.TabIndex = 0;
            this.labelEmployeesInfo.Text = "Here you can view and edit dataLeaves dataLeaves.";
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
            this.tabPageReplacements.Size = new System.Drawing.Size(944, 504);
            this.tabPageReplacements.TabIndex = 2;
            this.tabPageReplacements.Text = "Replacements";
            this.tabPageReplacements.UseVisualStyleBackColor = true;
            // 
            // labelReplacementsPositionFilter
            // 
            this.labelReplacementsPositionFilter.AutoSize = true;
            this.labelReplacementsPositionFilter.Location = new System.Drawing.Point(153, 52);
            this.labelReplacementsPositionFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReplacementsPositionFilter.Name = "labelReplacementsPositionFilter";
            this.labelReplacementsPositionFilter.Size = new System.Drawing.Size(115, 17);
            this.labelReplacementsPositionFilter.TabIndex = 7;
            this.labelReplacementsPositionFilter.Text = "Filter by position:";
            // 
            // labelReplacementsInfo
            // 
            this.labelReplacementsInfo.AutoSize = true;
            this.labelReplacementsInfo.Location = new System.Drawing.Point(11, 18);
            this.labelReplacementsInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReplacementsInfo.Name = "labelReplacementsInfo";
            this.labelReplacementsInfo.Size = new System.Drawing.Size(241, 17);
            this.labelReplacementsInfo.TabIndex = 6;
            this.labelReplacementsInfo.Text = "Here you can manage replacements.";
            // 
            // comboBoxReplacementsPosition
            // 
            this.comboBoxReplacementsPosition.FormattingEnabled = true;
            this.comboBoxReplacementsPosition.Location = new System.Drawing.Point(295, 48);
            this.comboBoxReplacementsPosition.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxReplacementsPosition.Name = "comboBoxReplacementsPosition";
            this.comboBoxReplacementsPosition.Size = new System.Drawing.Size(160, 24);
            this.comboBoxReplacementsPosition.TabIndex = 5;
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
            // 
            // dataGridViewReplacements
            // 
            this.dataGridViewReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReplacements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15});
            this.dataGridViewReplacements.Location = new System.Drawing.Point(157, 78);
            this.dataGridViewReplacements.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewReplacements.Name = "dataGridViewReplacements";
            this.dataGridViewReplacements.RowTemplate.Height = 24;
            this.dataGridViewReplacements.Size = new System.Drawing.Size(591, 282);
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
            this.tabPageReport.Location = new System.Drawing.Point(4, 25);
            this.tabPageReport.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageReport.Name = "tabPageReport";
            this.tabPageReport.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageReport.Size = new System.Drawing.Size(944, 504);
            this.tabPageReport.TabIndex = 3;
            this.tabPageReport.Text = "Report";
            this.tabPageReport.UseVisualStyleBackColor = true;
            // 
            // FormManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 533);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label labelReplacementsPositionFilter;
        private System.Windows.Forms.Label labelReplacementsInfo;
        private System.Windows.Forms.ComboBox comboBoxReplacementsPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Surname;
        private System.Windows.Forms.TextBox textBoxEmployeesSurname;
        private System.Windows.Forms.TextBox textBoxEmployeesPesel;
        private System.Windows.Forms.Button buttonEmployeeEdit;

    }
}