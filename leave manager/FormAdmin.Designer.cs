namespace leave_manager
{
    partial class FormAdmin
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
            this.tabDataSource = new System.Windows.Forms.TabPage();
            this.groupBoxDataSourceLocal = new System.Windows.Forms.GroupBox();
            this.buttonDataSourceLocalBrowse = new System.Windows.Forms.Button();
            this.textBoxDataSourceLocalPath = new System.Windows.Forms.TextBox();
            this.groupBoxDataSourceRemote = new System.Windows.Forms.GroupBox();
            this.labelDataSourceRemotePortColon = new System.Windows.Forms.Label();
            this.labelDataSourceIpPort = new System.Windows.Forms.Label();
            this.labelDataSourceDatabase = new System.Windows.Forms.Label();
            this.textBoxDataSourceDatabase = new System.Windows.Forms.TextBox();
            this.textBoxDataSourceIp1 = new System.Windows.Forms.TextBox();
            this.textBoxDataSourcePort = new System.Windows.Forms.TextBox();
            this.textBoxDataSourceIp2 = new System.Windows.Forms.TextBox();
            this.textBoxDataSourceIp4 = new System.Windows.Forms.TextBox();
            this.textBoxDataSourceIp3 = new System.Windows.Forms.TextBox();
            this.buttonDataSourceAccept = new System.Windows.Forms.Button();
            this.buttonDataSourceTestConnection = new System.Windows.Forms.Button();
            this.labelDataSourceInfo = new System.Windows.Forms.Label();
            this.radioButtonDataSourceRemote = new System.Windows.Forms.RadioButton();
            this.radioButtonDataSourceLocal = new System.Windows.Forms.RadioButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabNewEmployee = new System.Windows.Forms.TabPage();
            this.buttonNewEmployeesInformed = new System.Windows.Forms.Button();
            this.labelNewEmployeesInfo = new System.Windows.Forms.Label();
            this.dataGridViewNewEmployees = new System.Windows.Forms.DataGridView();
            this.tabPageDictionaries = new System.Windows.Forms.TabPage();
            this.tabControlDictionaries = new System.Windows.Forms.TabControl();
            this.tabPagePositions = new System.Windows.Forms.TabPage();
            this.buttonDeletePosition = new System.Windows.Forms.Button();
            this.buttonAddNewPosition = new System.Windows.Forms.Button();
            this.labelPositionsInfo = new System.Windows.Forms.Label();
            this.dataGridViewPositions = new System.Windows.Forms.DataGridView();
            this.tabPageLeaveTypes = new System.Windows.Forms.TabPage();
            this.buttonDeleteLeaveType = new System.Windows.Forms.Button();
            this.buttonAddLeaveType = new System.Windows.Forms.Button();
            this.labelLeaveTypesInfo = new System.Windows.Forms.Label();
            this.dataGridViewLeaveTypes = new System.Windows.Forms.DataGridView();
            this.tabDataSource.SuspendLayout();
            this.groupBoxDataSourceLocal.SuspendLayout();
            this.groupBoxDataSourceRemote.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabNewEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNewEmployees)).BeginInit();
            this.tabPageDictionaries.SuspendLayout();
            this.tabControlDictionaries.SuspendLayout();
            this.tabPagePositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPositions)).BeginInit();
            this.tabPageLeaveTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeaveTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // tabDataSource
            // 
            this.tabDataSource.Controls.Add(this.groupBoxDataSourceLocal);
            this.tabDataSource.Controls.Add(this.groupBoxDataSourceRemote);
            this.tabDataSource.Controls.Add(this.buttonDataSourceAccept);
            this.tabDataSource.Controls.Add(this.buttonDataSourceTestConnection);
            this.tabDataSource.Controls.Add(this.labelDataSourceInfo);
            this.tabDataSource.Controls.Add(this.radioButtonDataSourceRemote);
            this.tabDataSource.Controls.Add(this.radioButtonDataSourceLocal);
            this.tabDataSource.Location = new System.Drawing.Point(4, 25);
            this.tabDataSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabDataSource.Name = "tabDataSource";
            this.tabDataSource.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabDataSource.Size = new System.Drawing.Size(851, 521);
            this.tabDataSource.TabIndex = 1;
            this.tabDataSource.Text = "Data Source";
            this.tabDataSource.UseVisualStyleBackColor = true;
            // 
            // groupBoxDataSourceLocal
            // 
            this.groupBoxDataSourceLocal.Controls.Add(this.buttonDataSourceLocalBrowse);
            this.groupBoxDataSourceLocal.Controls.Add(this.textBoxDataSourceLocalPath);
            this.groupBoxDataSourceLocal.Location = new System.Drawing.Point(15, 108);
            this.groupBoxDataSourceLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDataSourceLocal.Name = "groupBoxDataSourceLocal";
            this.groupBoxDataSourceLocal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDataSourceLocal.Size = new System.Drawing.Size(405, 132);
            this.groupBoxDataSourceLocal.TabIndex = 17;
            this.groupBoxDataSourceLocal.TabStop = false;
            this.groupBoxDataSourceLocal.Text = "Select database path.";
            this.groupBoxDataSourceLocal.Visible = false;
            // 
            // buttonDataSourceLocalBrowse
            // 
            this.buttonDataSourceLocalBrowse.Location = new System.Drawing.Point(140, 64);
            this.buttonDataSourceLocalBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDataSourceLocalBrowse.Name = "buttonDataSourceLocalBrowse";
            this.buttonDataSourceLocalBrowse.Size = new System.Drawing.Size(100, 28);
            this.buttonDataSourceLocalBrowse.TabIndex = 1;
            this.buttonDataSourceLocalBrowse.Text = "Browse";
            this.buttonDataSourceLocalBrowse.UseVisualStyleBackColor = true;
            this.buttonDataSourceLocalBrowse.Click += new System.EventHandler(this.buttonDataSourceLocalBrowse_Click);
            // 
            // textBoxDataSourceLocalPath
            // 
            this.textBoxDataSourceLocalPath.Location = new System.Drawing.Point(12, 32);
            this.textBoxDataSourceLocalPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourceLocalPath.Name = "textBoxDataSourceLocalPath";
            this.textBoxDataSourceLocalPath.Size = new System.Drawing.Size(373, 22);
            this.textBoxDataSourceLocalPath.TabIndex = 0;
            // 
            // groupBoxDataSourceRemote
            // 
            this.groupBoxDataSourceRemote.Controls.Add(this.labelDataSourceRemotePortColon);
            this.groupBoxDataSourceRemote.Controls.Add(this.labelDataSourceIpPort);
            this.groupBoxDataSourceRemote.Controls.Add(this.labelDataSourceDatabase);
            this.groupBoxDataSourceRemote.Controls.Add(this.textBoxDataSourceDatabase);
            this.groupBoxDataSourceRemote.Controls.Add(this.textBoxDataSourceIp1);
            this.groupBoxDataSourceRemote.Controls.Add(this.textBoxDataSourcePort);
            this.groupBoxDataSourceRemote.Controls.Add(this.textBoxDataSourceIp2);
            this.groupBoxDataSourceRemote.Controls.Add(this.textBoxDataSourceIp4);
            this.groupBoxDataSourceRemote.Controls.Add(this.textBoxDataSourceIp3);
            this.groupBoxDataSourceRemote.Location = new System.Drawing.Point(15, 108);
            this.groupBoxDataSourceRemote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDataSourceRemote.Name = "groupBoxDataSourceRemote";
            this.groupBoxDataSourceRemote.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxDataSourceRemote.Size = new System.Drawing.Size(405, 132);
            this.groupBoxDataSourceRemote.TabIndex = 23;
            this.groupBoxDataSourceRemote.TabStop = false;
            this.groupBoxDataSourceRemote.Text = "Specify it details.";
            // 
            // labelDataSourceRemotePortColon
            // 
            this.labelDataSourceRemotePortColon.AutoSize = true;
            this.labelDataSourceRemotePortColon.Location = new System.Drawing.Point(299, 37);
            this.labelDataSourceRemotePortColon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataSourceRemotePortColon.Name = "labelDataSourceRemotePortColon";
            this.labelDataSourceRemotePortColon.Size = new System.Drawing.Size(12, 17);
            this.labelDataSourceRemotePortColon.TabIndex = 13;
            this.labelDataSourceRemotePortColon.Text = ":";
            // 
            // labelDataSourceIpPort
            // 
            this.labelDataSourceIpPort.AutoSize = true;
            this.labelDataSourceIpPort.Location = new System.Drawing.Point(8, 37);
            this.labelDataSourceIpPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataSourceIpPort.Name = "labelDataSourceIpPort";
            this.labelDataSourceIpPort.Size = new System.Drawing.Size(65, 17);
            this.labelDataSourceIpPort.TabIndex = 5;
            this.labelDataSourceIpPort.Text = "IP : port: ";
            // 
            // labelDataSourceDatabase
            // 
            this.labelDataSourceDatabase.AutoSize = true;
            this.labelDataSourceDatabase.Location = new System.Drawing.Point(4, 73);
            this.labelDataSourceDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataSourceDatabase.Name = "labelDataSourceDatabase";
            this.labelDataSourceDatabase.Size = new System.Drawing.Size(77, 17);
            this.labelDataSourceDatabase.TabIndex = 6;
            this.labelDataSourceDatabase.Text = "Database: ";
            // 
            // textBoxDataSourceDatabase
            // 
            this.textBoxDataSourceDatabase.Location = new System.Drawing.Point(91, 69);
            this.textBoxDataSourceDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourceDatabase.Name = "textBoxDataSourceDatabase";
            this.textBoxDataSourceDatabase.Size = new System.Drawing.Size(225, 22);
            this.textBoxDataSourceDatabase.TabIndex = 7;
            // 
            // textBoxDataSourceIp1
            // 
            this.textBoxDataSourceIp1.Location = new System.Drawing.Point(91, 33);
            this.textBoxDataSourceIp1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourceIp1.Name = "textBoxDataSourceIp1";
            this.textBoxDataSourceIp1.Size = new System.Drawing.Size(43, 22);
            this.textBoxDataSourceIp1.TabIndex = 8;
            // 
            // textBoxDataSourcePort
            // 
            this.textBoxDataSourcePort.Location = new System.Drawing.Point(320, 33);
            this.textBoxDataSourcePort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourcePort.Name = "textBoxDataSourcePort";
            this.textBoxDataSourcePort.Size = new System.Drawing.Size(65, 22);
            this.textBoxDataSourcePort.TabIndex = 12;
            // 
            // textBoxDataSourceIp2
            // 
            this.textBoxDataSourceIp2.Location = new System.Drawing.Point(143, 33);
            this.textBoxDataSourceIp2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourceIp2.Name = "textBoxDataSourceIp2";
            this.textBoxDataSourceIp2.Size = new System.Drawing.Size(43, 22);
            this.textBoxDataSourceIp2.TabIndex = 9;
            // 
            // textBoxDataSourceIp4
            // 
            this.textBoxDataSourceIp4.Location = new System.Drawing.Point(247, 33);
            this.textBoxDataSourceIp4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourceIp4.Name = "textBoxDataSourceIp4";
            this.textBoxDataSourceIp4.Size = new System.Drawing.Size(43, 22);
            this.textBoxDataSourceIp4.TabIndex = 11;
            // 
            // textBoxDataSourceIp3
            // 
            this.textBoxDataSourceIp3.Location = new System.Drawing.Point(195, 33);
            this.textBoxDataSourceIp3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDataSourceIp3.Name = "textBoxDataSourceIp3";
            this.textBoxDataSourceIp3.Size = new System.Drawing.Size(43, 22);
            this.textBoxDataSourceIp3.TabIndex = 10;
            // 
            // buttonDataSourceAccept
            // 
            this.buttonDataSourceAccept.Location = new System.Drawing.Point(153, 279);
            this.buttonDataSourceAccept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDataSourceAccept.Name = "buttonDataSourceAccept";
            this.buttonDataSourceAccept.Size = new System.Drawing.Size(100, 28);
            this.buttonDataSourceAccept.TabIndex = 21;
            this.buttonDataSourceAccept.Text = "Accept";
            this.buttonDataSourceAccept.UseVisualStyleBackColor = true;
            this.buttonDataSourceAccept.Click += new System.EventHandler(this.buttonDataSourceAccept_Click);
            // 
            // buttonDataSourceTestConnection
            // 
            this.buttonDataSourceTestConnection.Location = new System.Drawing.Point(45, 268);
            this.buttonDataSourceTestConnection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDataSourceTestConnection.Name = "buttonDataSourceTestConnection";
            this.buttonDataSourceTestConnection.Size = new System.Drawing.Size(100, 50);
            this.buttonDataSourceTestConnection.TabIndex = 20;
            this.buttonDataSourceTestConnection.Text = "Test connection";
            this.buttonDataSourceTestConnection.UseVisualStyleBackColor = true;
            this.buttonDataSourceTestConnection.Click += new System.EventHandler(this.buttonDataSourceTestConnection_Click);
            // 
            // labelDataSourceInfo
            // 
            this.labelDataSourceInfo.AutoSize = true;
            this.labelDataSourceInfo.Location = new System.Drawing.Point(11, 16);
            this.labelDataSourceInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataSourceInfo.Name = "labelDataSourceInfo";
            this.labelDataSourceInfo.Size = new System.Drawing.Size(185, 17);
            this.labelDataSourceInfo.TabIndex = 19;
            this.labelDataSourceInfo.Text = "Choose dataLeaves source.";
            // 
            // radioButtonDataSourceRemote
            // 
            this.radioButtonDataSourceRemote.AutoSize = true;
            this.radioButtonDataSourceRemote.Checked = true;
            this.radioButtonDataSourceRemote.Location = new System.Drawing.Point(11, 68);
            this.radioButtonDataSourceRemote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonDataSourceRemote.Name = "radioButtonDataSourceRemote";
            this.radioButtonDataSourceRemote.Size = new System.Drawing.Size(104, 26);
            this.radioButtonDataSourceRemote.TabIndex = 18;
            this.radioButtonDataSourceRemote.TabStop = true;
            this.radioButtonDataSourceRemote.Text = "Remote";
            this.radioButtonDataSourceRemote.UseVisualStyleBackColor = true;
            // 
            // radioButtonDataSourceLocal
            // 
            this.radioButtonDataSourceLocal.AutoSize = true;
            this.radioButtonDataSourceLocal.Location = new System.Drawing.Point(11, 39);
            this.radioButtonDataSourceLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonDataSourceLocal.Name = "radioButtonDataSourceLocal";
            this.radioButtonDataSourceLocal.Size = new System.Drawing.Size(211, 26);
            this.radioButtonDataSourceLocal.TabIndex = 17;
            this.radioButtonDataSourceLocal.Text = "Local (from .mdf file)";
            this.radioButtonDataSourceLocal.UseVisualStyleBackColor = true;
            this.radioButtonDataSourceLocal.CheckedChanged += new System.EventHandler(this.radioButtonDataSourceLocal_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabNewEmployee);
            this.tabControl.Controls.Add(this.tabDataSource);
            this.tabControl.Controls.Add(this.tabPageDictionaries);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(859, 550);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabNewEmployee
            // 
            this.tabNewEmployee.Controls.Add(this.buttonNewEmployeesInformed);
            this.tabNewEmployee.Controls.Add(this.labelNewEmployeesInfo);
            this.tabNewEmployee.Controls.Add(this.dataGridViewNewEmployees);
            this.tabNewEmployee.Location = new System.Drawing.Point(4, 25);
            this.tabNewEmployee.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabNewEmployee.Name = "tabNewEmployee";
            this.tabNewEmployee.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabNewEmployee.Size = new System.Drawing.Size(851, 521);
            this.tabNewEmployee.TabIndex = 0;
            this.tabNewEmployee.Text = "New Employees";
            this.tabNewEmployee.UseVisualStyleBackColor = true;
            // 
            // buttonNewEmployeesInformed
            // 
            this.buttonNewEmployeesInformed.Location = new System.Drawing.Point(65, 276);
            this.buttonNewEmployeesInformed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonNewEmployeesInformed.Name = "buttonNewEmployeesInformed";
            this.buttonNewEmployeesInformed.Size = new System.Drawing.Size(268, 44);
            this.buttonNewEmployeesInformed.TabIndex = 2;
            this.buttonNewEmployeesInformed.Text = "Informed (delete selected from list)";
            this.buttonNewEmployeesInformed.UseVisualStyleBackColor = true;
            this.buttonNewEmployeesInformed.Click += new System.EventHandler(this.buttonNewEmployeesInformed_Click);
            // 
            // labelNewEmployeesInfo
            // 
            this.labelNewEmployeesInfo.AutoSize = true;
            this.labelNewEmployeesInfo.Location = new System.Drawing.Point(11, 16);
            this.labelNewEmployeesInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNewEmployeesInfo.Name = "labelNewEmployeesInfo";
            this.labelNewEmployeesInfo.Size = new System.Drawing.Size(589, 17);
            this.labelNewEmployeesInfo.TabIndex = 1;
            this.labelNewEmployeesInfo.Text = "Here is the list of new dataLeaves that needs to be informed about their login an" +
                "d password.";
            // 
            // dataGridViewNewEmployees
            // 
            this.dataGridViewNewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNewEmployees.Location = new System.Drawing.Point(15, 65);
            this.dataGridViewNewEmployees.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewNewEmployees.Name = "dataGridViewNewEmployees";
            this.dataGridViewNewEmployees.Size = new System.Drawing.Size(797, 185);
            this.dataGridViewNewEmployees.TabIndex = 0;
            // 
            // tabPageDictionaries
            // 
            this.tabPageDictionaries.Controls.Add(this.tabControlDictionaries);
            this.tabPageDictionaries.Location = new System.Drawing.Point(4, 25);
            this.tabPageDictionaries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageDictionaries.Name = "tabPageDictionaries";
            this.tabPageDictionaries.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageDictionaries.Size = new System.Drawing.Size(851, 521);
            this.tabPageDictionaries.TabIndex = 2;
            this.tabPageDictionaries.Text = "Dictionaries";
            this.tabPageDictionaries.UseVisualStyleBackColor = true;
            // 
            // tabControlDictionaries
            // 
            this.tabControlDictionaries.Controls.Add(this.tabPagePositions);
            this.tabControlDictionaries.Controls.Add(this.tabPageLeaveTypes);
            this.tabControlDictionaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDictionaries.Location = new System.Drawing.Point(4, 4);
            this.tabControlDictionaries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlDictionaries.Name = "tabControlDictionaries";
            this.tabControlDictionaries.SelectedIndex = 0;
            this.tabControlDictionaries.Size = new System.Drawing.Size(843, 513);
            this.tabControlDictionaries.TabIndex = 0;
            this.tabControlDictionaries.SelectedIndexChanged += new System.EventHandler(this.tabControlDictionaries_SelectedIndexChanged);
            // 
            // tabPagePositions
            // 
            this.tabPagePositions.Controls.Add(this.buttonDeletePosition);
            this.tabPagePositions.Controls.Add(this.buttonAddNewPosition);
            this.tabPagePositions.Controls.Add(this.labelPositionsInfo);
            this.tabPagePositions.Controls.Add(this.dataGridViewPositions);
            this.tabPagePositions.Location = new System.Drawing.Point(4, 25);
            this.tabPagePositions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePositions.Name = "tabPagePositions";
            this.tabPagePositions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePositions.Size = new System.Drawing.Size(835, 484);
            this.tabPagePositions.TabIndex = 0;
            this.tabPagePositions.Text = "Positions";
            this.tabPagePositions.UseVisualStyleBackColor = true;
            // 
            // buttonDeletePosition
            // 
            this.buttonDeletePosition.Location = new System.Drawing.Point(548, 332);
            this.buttonDeletePosition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDeletePosition.Name = "buttonDeletePosition";
            this.buttonDeletePosition.Size = new System.Drawing.Size(128, 54);
            this.buttonDeletePosition.TabIndex = 3;
            this.buttonDeletePosition.Text = "Delete selected entry";
            this.buttonDeletePosition.UseVisualStyleBackColor = true;
            this.buttonDeletePosition.Click += new System.EventHandler(this.buttonDeletePosition_Click);
            // 
            // buttonAddNewPosition
            // 
            this.buttonAddNewPosition.Location = new System.Drawing.Point(145, 332);
            this.buttonAddNewPosition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddNewPosition.Name = "buttonAddNewPosition";
            this.buttonAddNewPosition.Size = new System.Drawing.Size(128, 54);
            this.buttonAddNewPosition.TabIndex = 2;
            this.buttonAddNewPosition.Text = "Add new entry";
            this.buttonAddNewPosition.UseVisualStyleBackColor = true;
            this.buttonAddNewPosition.Click += new System.EventHandler(this.buttonAddNewPosition_Click);
            // 
            // labelPositionsInfo
            // 
            this.labelPositionsInfo.AutoSize = true;
            this.labelPositionsInfo.Location = new System.Drawing.Point(8, 4);
            this.labelPositionsInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPositionsInfo.Name = "labelPositionsInfo";
            this.labelPositionsInfo.Size = new System.Drawing.Size(215, 17);
            this.labelPositionsInfo.TabIndex = 1;
            this.labelPositionsInfo.Text = "Here you can edit position types.";
            // 
            // dataGridViewPositions
            // 
            this.dataGridViewPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPositions.Location = new System.Drawing.Point(221, 90);
            this.dataGridViewPositions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewPositions.Name = "dataGridViewPositions";
            this.dataGridViewPositions.Size = new System.Drawing.Size(391, 185);
            this.dataGridViewPositions.TabIndex = 0;
            // 
            // tabPageLeaveTypes
            // 
            this.tabPageLeaveTypes.Controls.Add(this.buttonDeleteLeaveType);
            this.tabPageLeaveTypes.Controls.Add(this.buttonAddLeaveType);
            this.tabPageLeaveTypes.Controls.Add(this.labelLeaveTypesInfo);
            this.tabPageLeaveTypes.Controls.Add(this.dataGridViewLeaveTypes);
            this.tabPageLeaveTypes.Location = new System.Drawing.Point(4, 25);
            this.tabPageLeaveTypes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageLeaveTypes.Name = "tabPageLeaveTypes";
            this.tabPageLeaveTypes.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageLeaveTypes.Size = new System.Drawing.Size(832, 482);
            this.tabPageLeaveTypes.TabIndex = 1;
            this.tabPageLeaveTypes.Text = "Leave types";
            this.tabPageLeaveTypes.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteLeaveType
            // 
            this.buttonDeleteLeaveType.Location = new System.Drawing.Point(548, 332);
            this.buttonDeleteLeaveType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDeleteLeaveType.Name = "buttonDeleteLeaveType";
            this.buttonDeleteLeaveType.Size = new System.Drawing.Size(128, 54);
            this.buttonDeleteLeaveType.TabIndex = 7;
            this.buttonDeleteLeaveType.Text = "Delete selected entry";
            this.buttonDeleteLeaveType.UseVisualStyleBackColor = true;
            this.buttonDeleteLeaveType.Click += new System.EventHandler(this.buttonDeleteLeaveType_Click);
            // 
            // buttonAddLeaveType
            // 
            this.buttonAddLeaveType.Location = new System.Drawing.Point(145, 332);
            this.buttonAddLeaveType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddLeaveType.Name = "buttonAddLeaveType";
            this.buttonAddLeaveType.Size = new System.Drawing.Size(128, 54);
            this.buttonAddLeaveType.TabIndex = 6;
            this.buttonAddLeaveType.Text = "Add new entry";
            this.buttonAddLeaveType.UseVisualStyleBackColor = true;
            this.buttonAddLeaveType.Click += new System.EventHandler(this.buttonAddLeaveType_Click);
            // 
            // labelLeaveTypesInfo
            // 
            this.labelLeaveTypesInfo.AutoSize = true;
            this.labelLeaveTypesInfo.Location = new System.Drawing.Point(8, 4);
            this.labelLeaveTypesInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLeaveTypesInfo.Name = "labelLeaveTypesInfo";
            this.labelLeaveTypesInfo.Size = new System.Drawing.Size(215, 17);
            this.labelLeaveTypesInfo.TabIndex = 5;
            this.labelLeaveTypesInfo.Text = "Here you can edit position types.";
            // 
            // dataGridViewLeaveTypes
            // 
            this.dataGridViewLeaveTypes.AllowUserToAddRows = false;
            this.dataGridViewLeaveTypes.AllowUserToDeleteRows = false;
            this.dataGridViewLeaveTypes.AllowUserToOrderColumns = true;
            this.dataGridViewLeaveTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewLeaveTypes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewLeaveTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLeaveTypes.Location = new System.Drawing.Point(109, 90);
            this.dataGridViewLeaveTypes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewLeaveTypes.Name = "dataGridViewLeaveTypes";
            this.dataGridViewLeaveTypes.ReadOnly = true;
            this.dataGridViewLeaveTypes.Size = new System.Drawing.Size(609, 185);
            this.dataGridViewLeaveTypes.TabIndex = 4;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 550);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAdmin";
            this.Text = "Leave Manager";
            this.tabDataSource.ResumeLayout(false);
            this.tabDataSource.PerformLayout();
            this.groupBoxDataSourceLocal.ResumeLayout(false);
            this.groupBoxDataSourceLocal.PerformLayout();
            this.groupBoxDataSourceRemote.ResumeLayout(false);
            this.groupBoxDataSourceRemote.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabNewEmployee.ResumeLayout(false);
            this.tabNewEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNewEmployees)).EndInit();
            this.tabPageDictionaries.ResumeLayout(false);
            this.tabControlDictionaries.ResumeLayout(false);
            this.tabPagePositions.ResumeLayout(false);
            this.tabPagePositions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPositions)).EndInit();
            this.tabPageLeaveTypes.ResumeLayout(false);
            this.tabPageLeaveTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeaveTypes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabDataSource;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabNewEmployee;
        private System.Windows.Forms.GroupBox groupBoxDataSourceRemote;
        private System.Windows.Forms.GroupBox groupBoxDataSourceLocal;
        private System.Windows.Forms.Button buttonDataSourceLocalBrowse;
        private System.Windows.Forms.TextBox textBoxDataSourceLocalPath;
        private System.Windows.Forms.Label labelDataSourceRemotePortColon;
        private System.Windows.Forms.Label labelDataSourceIpPort;
        private System.Windows.Forms.Label labelDataSourceDatabase;
        private System.Windows.Forms.TextBox textBoxDataSourceDatabase;
        private System.Windows.Forms.TextBox textBoxDataSourceIp1;
        private System.Windows.Forms.TextBox textBoxDataSourcePort;
        private System.Windows.Forms.TextBox textBoxDataSourceIp2;
        private System.Windows.Forms.TextBox textBoxDataSourceIp4;
        private System.Windows.Forms.TextBox textBoxDataSourceIp3;
        private System.Windows.Forms.Button buttonDataSourceAccept;
        private System.Windows.Forms.Button buttonDataSourceTestConnection;
        private System.Windows.Forms.Label labelDataSourceInfo;
        private System.Windows.Forms.RadioButton radioButtonDataSourceRemote;
        private System.Windows.Forms.RadioButton radioButtonDataSourceLocal;
        private System.Windows.Forms.Label labelNewEmployeesInfo;
        private System.Windows.Forms.DataGridView dataGridViewNewEmployees;
        private System.Windows.Forms.Button buttonNewEmployeesInformed;
        private System.Windows.Forms.TabPage tabPageDictionaries;
        private System.Windows.Forms.TabControl tabControlDictionaries;
        private System.Windows.Forms.TabPage tabPagePositions;
        private System.Windows.Forms.Label labelPositionsInfo;
        private System.Windows.Forms.DataGridView dataGridViewPositions;
        private System.Windows.Forms.TabPage tabPageLeaveTypes;
        private System.Windows.Forms.Button buttonAddNewPosition;
        private System.Windows.Forms.Button buttonDeletePosition;
        private System.Windows.Forms.Button buttonDeleteLeaveType;
        private System.Windows.Forms.Button buttonAddLeaveType;
        private System.Windows.Forms.Label labelLeaveTypesInfo;
        private System.Windows.Forms.DataGridView dataGridViewLeaveTypes;
    }
}