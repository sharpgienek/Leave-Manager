﻿namespace leave_manager
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
            this.ColumnNewEmployeesEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNewEmployeesLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNewEmployeesPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNewEmployeesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNewEmployeesSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDataSource.SuspendLayout();
            this.groupBoxDataSourceLocal.SuspendLayout();
            this.groupBoxDataSourceRemote.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabNewEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNewEmployees)).BeginInit();
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
            this.tabDataSource.Location = new System.Drawing.Point(4, 22);
            this.tabDataSource.Name = "tabDataSource";
            this.tabDataSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataSource.Size = new System.Drawing.Size(636, 421);
            this.tabDataSource.TabIndex = 1;
            this.tabDataSource.Text = "Data Source";
            this.tabDataSource.UseVisualStyleBackColor = true;
            // 
            // groupBoxDataSourceLocal
            // 
            this.groupBoxDataSourceLocal.Controls.Add(this.buttonDataSourceLocalBrowse);
            this.groupBoxDataSourceLocal.Controls.Add(this.textBoxDataSourceLocalPath);
            this.groupBoxDataSourceLocal.Location = new System.Drawing.Point(11, 88);
            this.groupBoxDataSourceLocal.Name = "groupBoxDataSourceLocal";
            this.groupBoxDataSourceLocal.Size = new System.Drawing.Size(304, 107);
            this.groupBoxDataSourceLocal.TabIndex = 17;
            this.groupBoxDataSourceLocal.TabStop = false;
            this.groupBoxDataSourceLocal.Text = "Select database path.";
            this.groupBoxDataSourceLocal.Visible = false;
            // 
            // buttonDataSourceLocalBrowse
            // 
            this.buttonDataSourceLocalBrowse.Location = new System.Drawing.Point(105, 52);
            this.buttonDataSourceLocalBrowse.Name = "buttonDataSourceLocalBrowse";
            this.buttonDataSourceLocalBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonDataSourceLocalBrowse.TabIndex = 1;
            this.buttonDataSourceLocalBrowse.Text = "Browse";
            this.buttonDataSourceLocalBrowse.UseVisualStyleBackColor = true;
            this.buttonDataSourceLocalBrowse.Click += new System.EventHandler(this.buttonDataSourceLocalBrowse_Click);
            // 
            // textBoxDataSourceLocalPath
            // 
            this.textBoxDataSourceLocalPath.Location = new System.Drawing.Point(9, 26);
            this.textBoxDataSourceLocalPath.Name = "textBoxDataSourceLocalPath";
            this.textBoxDataSourceLocalPath.Size = new System.Drawing.Size(281, 20);
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
            this.groupBoxDataSourceRemote.Location = new System.Drawing.Point(11, 88);
            this.groupBoxDataSourceRemote.Name = "groupBoxDataSourceRemote";
            this.groupBoxDataSourceRemote.Size = new System.Drawing.Size(304, 107);
            this.groupBoxDataSourceRemote.TabIndex = 23;
            this.groupBoxDataSourceRemote.TabStop = false;
            this.groupBoxDataSourceRemote.Text = "Specify it details.";
            // 
            // labelDataSourceRemotePortColon
            // 
            this.labelDataSourceRemotePortColon.AutoSize = true;
            this.labelDataSourceRemotePortColon.Location = new System.Drawing.Point(224, 30);
            this.labelDataSourceRemotePortColon.Name = "labelDataSourceRemotePortColon";
            this.labelDataSourceRemotePortColon.Size = new System.Drawing.Size(10, 13);
            this.labelDataSourceRemotePortColon.TabIndex = 13;
            this.labelDataSourceRemotePortColon.Text = ":";
            // 
            // labelDataSourceIpPort
            // 
            this.labelDataSourceIpPort.AutoSize = true;
            this.labelDataSourceIpPort.Location = new System.Drawing.Point(6, 30);
            this.labelDataSourceIpPort.Name = "labelDataSourceIpPort";
            this.labelDataSourceIpPort.Size = new System.Drawing.Size(50, 13);
            this.labelDataSourceIpPort.TabIndex = 5;
            this.labelDataSourceIpPort.Text = "IP : port: ";
            // 
            // labelDataSourceDatabase
            // 
            this.labelDataSourceDatabase.AutoSize = true;
            this.labelDataSourceDatabase.Location = new System.Drawing.Point(3, 59);
            this.labelDataSourceDatabase.Name = "labelDataSourceDatabase";
            this.labelDataSourceDatabase.Size = new System.Drawing.Size(59, 13);
            this.labelDataSourceDatabase.TabIndex = 6;
            this.labelDataSourceDatabase.Text = "Database: ";
            // 
            // textBoxDataSourceDatabase
            // 
            this.textBoxDataSourceDatabase.Location = new System.Drawing.Point(68, 56);
            this.textBoxDataSourceDatabase.Name = "textBoxDataSourceDatabase";
            this.textBoxDataSourceDatabase.Size = new System.Drawing.Size(170, 20);
            this.textBoxDataSourceDatabase.TabIndex = 7;
            // 
            // textBoxDataSourceIp1
            // 
            this.textBoxDataSourceIp1.Location = new System.Drawing.Point(68, 27);
            this.textBoxDataSourceIp1.Name = "textBoxDataSourceIp1";
            this.textBoxDataSourceIp1.Size = new System.Drawing.Size(33, 20);
            this.textBoxDataSourceIp1.TabIndex = 8;
            // 
            // textBoxDataSourcePort
            // 
            this.textBoxDataSourcePort.Location = new System.Drawing.Point(240, 27);
            this.textBoxDataSourcePort.Name = "textBoxDataSourcePort";
            this.textBoxDataSourcePort.Size = new System.Drawing.Size(50, 20);
            this.textBoxDataSourcePort.TabIndex = 12;
            // 
            // textBoxDataSourceIp2
            // 
            this.textBoxDataSourceIp2.Location = new System.Drawing.Point(107, 27);
            this.textBoxDataSourceIp2.Name = "textBoxDataSourceIp2";
            this.textBoxDataSourceIp2.Size = new System.Drawing.Size(33, 20);
            this.textBoxDataSourceIp2.TabIndex = 9;
            // 
            // textBoxDataSourceIp4
            // 
            this.textBoxDataSourceIp4.Location = new System.Drawing.Point(185, 27);
            this.textBoxDataSourceIp4.Name = "textBoxDataSourceIp4";
            this.textBoxDataSourceIp4.Size = new System.Drawing.Size(33, 20);
            this.textBoxDataSourceIp4.TabIndex = 11;
            // 
            // textBoxDataSourceIp3
            // 
            this.textBoxDataSourceIp3.Location = new System.Drawing.Point(146, 27);
            this.textBoxDataSourceIp3.Name = "textBoxDataSourceIp3";
            this.textBoxDataSourceIp3.Size = new System.Drawing.Size(33, 20);
            this.textBoxDataSourceIp3.TabIndex = 10;
            // 
            // buttonDataSourceAccept
            // 
            this.buttonDataSourceAccept.Location = new System.Drawing.Point(115, 227);
            this.buttonDataSourceAccept.Name = "buttonDataSourceAccept";
            this.buttonDataSourceAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonDataSourceAccept.TabIndex = 21;
            this.buttonDataSourceAccept.Text = "Accept";
            this.buttonDataSourceAccept.UseVisualStyleBackColor = true;
            this.buttonDataSourceAccept.Click += new System.EventHandler(this.buttonDataSourceAccept_Click);
            // 
            // buttonDataSourceTestConnection
            // 
            this.buttonDataSourceTestConnection.Location = new System.Drawing.Point(34, 218);
            this.buttonDataSourceTestConnection.Name = "buttonDataSourceTestConnection";
            this.buttonDataSourceTestConnection.Size = new System.Drawing.Size(75, 41);
            this.buttonDataSourceTestConnection.TabIndex = 20;
            this.buttonDataSourceTestConnection.Text = "Test connection";
            this.buttonDataSourceTestConnection.UseVisualStyleBackColor = true;
            this.buttonDataSourceTestConnection.Click += new System.EventHandler(this.buttonDataSourceTestConnection_Click);
            // 
            // labelDataSourceInfo
            // 
            this.labelDataSourceInfo.AutoSize = true;
            this.labelDataSourceInfo.Location = new System.Drawing.Point(8, 13);
            this.labelDataSourceInfo.Name = "labelDataSourceInfo";
            this.labelDataSourceInfo.Size = new System.Drawing.Size(105, 13);
            this.labelDataSourceInfo.TabIndex = 19;
            this.labelDataSourceInfo.Text = "Choose data source.";
            // 
            // radioButtonDataSourceRemote
            // 
            this.radioButtonDataSourceRemote.AutoSize = true;
            this.radioButtonDataSourceRemote.Checked = true;
            this.radioButtonDataSourceRemote.Location = new System.Drawing.Point(8, 55);
            this.radioButtonDataSourceRemote.Name = "radioButtonDataSourceRemote";
            this.radioButtonDataSourceRemote.Size = new System.Drawing.Size(62, 17);
            this.radioButtonDataSourceRemote.TabIndex = 18;
            this.radioButtonDataSourceRemote.TabStop = true;
            this.radioButtonDataSourceRemote.Text = "Remote";
            this.radioButtonDataSourceRemote.UseVisualStyleBackColor = true;
            // 
            // radioButtonDataSourceLocal
            // 
            this.radioButtonDataSourceLocal.AutoSize = true;
            this.radioButtonDataSourceLocal.Location = new System.Drawing.Point(8, 32);
            this.radioButtonDataSourceLocal.Name = "radioButtonDataSourceLocal";
            this.radioButtonDataSourceLocal.Size = new System.Drawing.Size(119, 17);
            this.radioButtonDataSourceLocal.TabIndex = 17;
            this.radioButtonDataSourceLocal.Text = "Local (from .mdf file)";
            this.radioButtonDataSourceLocal.UseVisualStyleBackColor = true;
            this.radioButtonDataSourceLocal.CheckedChanged += new System.EventHandler(this.radioButtonDataSourceLocal_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabNewEmployee);
            this.tabControl.Controls.Add(this.tabDataSource);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(644, 447);
            this.tabControl.TabIndex = 0;
            // 
            // tabNewEmployee
            // 
            this.tabNewEmployee.Controls.Add(this.buttonNewEmployeesInformed);
            this.tabNewEmployee.Controls.Add(this.labelNewEmployeesInfo);
            this.tabNewEmployee.Controls.Add(this.dataGridViewNewEmployees);
            this.tabNewEmployee.Location = new System.Drawing.Point(4, 22);
            this.tabNewEmployee.Name = "tabNewEmployee";
            this.tabNewEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewEmployee.Size = new System.Drawing.Size(636, 421);
            this.tabNewEmployee.TabIndex = 0;
            this.tabNewEmployee.Text = "New Employees";
            this.tabNewEmployee.UseVisualStyleBackColor = true;
            // 
            // buttonNewEmployeesInformed
            // 
            this.buttonNewEmployeesInformed.Location = new System.Drawing.Point(49, 224);
            this.buttonNewEmployeesInformed.Name = "buttonNewEmployeesInformed";
            this.buttonNewEmployeesInformed.Size = new System.Drawing.Size(201, 36);
            this.buttonNewEmployeesInformed.TabIndex = 2;
            this.buttonNewEmployeesInformed.Text = "Informed (delete selected from list)";
            this.buttonNewEmployeesInformed.UseVisualStyleBackColor = true;
            // 
            // labelNewEmployeesInfo
            // 
            this.labelNewEmployeesInfo.AutoSize = true;
            this.labelNewEmployeesInfo.Location = new System.Drawing.Point(8, 13);
            this.labelNewEmployeesInfo.Name = "labelNewEmployeesInfo";
            this.labelNewEmployeesInfo.Size = new System.Drawing.Size(434, 13);
            this.labelNewEmployeesInfo.TabIndex = 1;
            this.labelNewEmployeesInfo.Text = "Here is the list of new employees that needs to be informed about their login and" +
    " password.";
            // 
            // dataGridViewNewEmployees
            // 
            this.dataGridViewNewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNewEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNewEmployeesEmail,
            this.ColumnNewEmployeesLogin,
            this.ColumnNewEmployeesPassword,
            this.ColumnNewEmployeesName,
            this.ColumnNewEmployeesSurname});
            this.dataGridViewNewEmployees.Location = new System.Drawing.Point(11, 53);
            this.dataGridViewNewEmployees.Name = "dataGridViewNewEmployees";
            this.dataGridViewNewEmployees.Size = new System.Drawing.Size(598, 150);
            this.dataGridViewNewEmployees.TabIndex = 0;
            // 
            // ColumnNewEmployeesEmail
            // 
            this.ColumnNewEmployeesEmail.HeaderText = "e-mail";
            this.ColumnNewEmployeesEmail.Name = "ColumnNewEmployeesEmail";
            // 
            // ColumnNewEmployeesLogin
            // 
            this.ColumnNewEmployeesLogin.HeaderText = "Login";
            this.ColumnNewEmployeesLogin.Name = "ColumnNewEmployeesLogin";
            // 
            // ColumnNewEmployeesPassword
            // 
            this.ColumnNewEmployeesPassword.HeaderText = "Password";
            this.ColumnNewEmployeesPassword.Name = "ColumnNewEmployeesPassword";
            // 
            // ColumnNewEmployeesName
            // 
            this.ColumnNewEmployeesName.HeaderText = "Name";
            this.ColumnNewEmployeesName.Name = "ColumnNewEmployeesName";
            // 
            // ColumnNewEmployeesSurname
            // 
            this.ColumnNewEmployeesSurname.HeaderText = "Surname";
            this.ColumnNewEmployeesSurname.Name = "ColumnNewEmployeesSurname";
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 447);
            this.Controls.Add(this.tabControl);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNewEmployeesEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNewEmployeesLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNewEmployeesPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNewEmployeesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNewEmployeesSurname;
    }
}