namespace leave_manager
{
    partial class FormDefineDatabase
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
            this.radioButtonLocal = new System.Windows.Forms.RadioButton();
            this.radioButtonRemote = new System.Windows.Forms.RadioButton();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelIpPort = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxIp1 = new System.Windows.Forms.TextBox();
            this.textBoxIp2 = new System.Windows.Forms.TextBox();
            this.textBoxIp3 = new System.Windows.Forms.TextBox();
            this.textBoxIp4 = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxRemote = new System.Windows.Forms.GroupBox();
            this.labelRemotePortColon = new System.Windows.Forms.Label();
            this.groupBoxLocal = new System.Windows.Forms.GroupBox();
            this.buttonLocalBrowse = new System.Windows.Forms.Button();
            this.textBoxLocalPath = new System.Windows.Forms.TextBox();
            this.groupBoxRemote.SuspendLayout();
            this.groupBoxLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonLocal
            // 
            this.radioButtonLocal.AutoSize = true;
            this.radioButtonLocal.Location = new System.Drawing.Point(13, 30);
            this.radioButtonLocal.Name = "radioButtonLocal";
            this.radioButtonLocal.Size = new System.Drawing.Size(119, 17);
            this.radioButtonLocal.TabIndex = 2;
            this.radioButtonLocal.Text = "Local (from .mdf file)";
            this.radioButtonLocal.UseVisualStyleBackColor = true;
            this.radioButtonLocal.CheckedChanged += new System.EventHandler(this.radioButtonLocal_CheckedChanged);
            // 
            // radioButtonRemote
            // 
            this.radioButtonRemote.AutoSize = true;
            this.radioButtonRemote.Checked = true;
            this.radioButtonRemote.Location = new System.Drawing.Point(13, 53);
            this.radioButtonRemote.Name = "radioButtonRemote";
            this.radioButtonRemote.Size = new System.Drawing.Size(62, 17);
            this.radioButtonRemote.TabIndex = 3;
            this.radioButtonRemote.TabStop = true;
            this.radioButtonRemote.Text = "Remote";
            this.radioButtonRemote.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(13, 11);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(105, 13);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "Choose data source.";
            // 
            // labelIpPort
            // 
            this.labelIpPort.AutoSize = true;
            this.labelIpPort.Location = new System.Drawing.Point(6, 30);
            this.labelIpPort.Name = "labelIpPort";
            this.labelIpPort.Size = new System.Drawing.Size(50, 13);
            this.labelIpPort.TabIndex = 5;
            this.labelIpPort.Text = "IP : port: ";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(3, 59);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(59, 13);
            this.labelDatabase.TabIndex = 6;
            this.labelDatabase.Text = "Database: ";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(68, 56);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(170, 20);
            this.textBoxDatabase.TabIndex = 7;
            // 
            // textBoxIp1
            // 
            this.textBoxIp1.Location = new System.Drawing.Point(68, 27);
            this.textBoxIp1.Name = "textBoxIp1";
            this.textBoxIp1.Size = new System.Drawing.Size(33, 20);
            this.textBoxIp1.TabIndex = 8;
            // 
            // textBoxIp2
            // 
            this.textBoxIp2.Location = new System.Drawing.Point(107, 27);
            this.textBoxIp2.Name = "textBoxIp2";
            this.textBoxIp2.Size = new System.Drawing.Size(33, 20);
            this.textBoxIp2.TabIndex = 9;
            // 
            // textBoxIp3
            // 
            this.textBoxIp3.Location = new System.Drawing.Point(146, 27);
            this.textBoxIp3.Name = "textBoxIp3";
            this.textBoxIp3.Size = new System.Drawing.Size(33, 20);
            this.textBoxIp3.TabIndex = 10;
            // 
            // textBoxIp4
            // 
            this.textBoxIp4.Location = new System.Drawing.Point(185, 27);
            this.textBoxIp4.Name = "textBoxIp4";
            this.textBoxIp4.Size = new System.Drawing.Size(33, 20);
            this.textBoxIp4.TabIndex = 11;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(240, 27);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(50, 20);
            this.textBoxPort.TabIndex = 12;
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(39, 216);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(75, 41);
            this.buttonTestConnection.TabIndex = 13;
            this.buttonTestConnection.Text = "Test connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(120, 225);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 14;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(201, 225);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 15;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupBoxRemote
            // 
            this.groupBoxRemote.Controls.Add(this.labelRemotePortColon);
            this.groupBoxRemote.Controls.Add(this.labelIpPort);
            this.groupBoxRemote.Controls.Add(this.labelDatabase);
            this.groupBoxRemote.Controls.Add(this.textBoxDatabase);
            this.groupBoxRemote.Controls.Add(this.textBoxIp1);
            this.groupBoxRemote.Controls.Add(this.textBoxPort);
            this.groupBoxRemote.Controls.Add(this.textBoxIp2);
            this.groupBoxRemote.Controls.Add(this.textBoxIp4);
            this.groupBoxRemote.Controls.Add(this.textBoxIp3);
            this.groupBoxRemote.Location = new System.Drawing.Point(12, 91);
            this.groupBoxRemote.Name = "groupBoxRemote";
            this.groupBoxRemote.Size = new System.Drawing.Size(304, 107);
            this.groupBoxRemote.TabIndex = 16;
            this.groupBoxRemote.TabStop = false;
            this.groupBoxRemote.Text = "Specify it details.";
            // 
            // labelRemotePortColon
            // 
            this.labelRemotePortColon.AutoSize = true;
            this.labelRemotePortColon.Location = new System.Drawing.Point(224, 30);
            this.labelRemotePortColon.Name = "labelRemotePortColon";
            this.labelRemotePortColon.Size = new System.Drawing.Size(10, 13);
            this.labelRemotePortColon.TabIndex = 13;
            this.labelRemotePortColon.Text = ":";
            // 
            // groupBoxLocal
            // 
            this.groupBoxLocal.Controls.Add(this.buttonLocalBrowse);
            this.groupBoxLocal.Controls.Add(this.textBoxLocalPath);
            this.groupBoxLocal.Location = new System.Drawing.Point(12, 91);
            this.groupBoxLocal.Name = "groupBoxLocal";
            this.groupBoxLocal.Size = new System.Drawing.Size(304, 107);
            this.groupBoxLocal.TabIndex = 17;
            this.groupBoxLocal.TabStop = false;
            this.groupBoxLocal.Text = "Select database path.";
            this.groupBoxLocal.Visible = false;
            // 
            // buttonLocalBrowse
            // 
            this.buttonLocalBrowse.Location = new System.Drawing.Point(93, 66);
            this.buttonLocalBrowse.Name = "buttonLocalBrowse";
            this.buttonLocalBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonLocalBrowse.TabIndex = 1;
            this.buttonLocalBrowse.Text = "Browse";
            this.buttonLocalBrowse.UseVisualStyleBackColor = true;
            this.buttonLocalBrowse.Click += new System.EventHandler(this.buttonLocalBrowse_Click);
            // 
            // textBoxLocalPath
            // 
            this.textBoxLocalPath.Location = new System.Drawing.Point(9, 26);
            this.textBoxLocalPath.Name = "textBoxLocalPath";
            this.textBoxLocalPath.Size = new System.Drawing.Size(281, 20);
            this.textBoxLocalPath.TabIndex = 0;
            // 
            // FormDefineDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 278);
            this.Controls.Add(this.groupBoxRemote);
            this.Controls.Add(this.groupBoxLocal);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.radioButtonRemote);
            this.Controls.Add(this.radioButtonLocal);
            this.Name = "FormDefineDatabase";
            this.Text = "Leave Manager";
            this.groupBoxRemote.ResumeLayout(false);
            this.groupBoxRemote.PerformLayout();
            this.groupBoxLocal.ResumeLayout(false);
            this.groupBoxLocal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonLocal;
        private System.Windows.Forms.RadioButton radioButtonRemote;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelIpPort;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.TextBox textBoxIp1;
        private System.Windows.Forms.TextBox textBoxIp2;
        private System.Windows.Forms.TextBox textBoxIp3;
        private System.Windows.Forms.TextBox textBoxIp4;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupBoxRemote;
        private System.Windows.Forms.GroupBox groupBoxLocal;
        private System.Windows.Forms.Button buttonLocalBrowse;
        private System.Windows.Forms.TextBox textBoxLocalPath;
        private System.Windows.Forms.Label labelRemotePortColon;

    }
}