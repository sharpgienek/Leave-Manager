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
            this.radioButtonLocal.Location = new System.Drawing.Point(17, 37);
            this.radioButtonLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonLocal.Name = "radioButtonLocal";
            this.radioButtonLocal.Size = new System.Drawing.Size(158, 21);
            this.radioButtonLocal.TabIndex = 2;
            this.radioButtonLocal.Text = "Local (from .mdf file)";
            this.radioButtonLocal.UseVisualStyleBackColor = true;
            this.radioButtonLocal.CheckedChanged += new System.EventHandler(this.radioButtonLocal_CheckedChanged);
            // 
            // radioButtonRemote
            // 
            this.radioButtonRemote.AutoSize = true;
            this.radioButtonRemote.Checked = true;
            this.radioButtonRemote.Location = new System.Drawing.Point(17, 65);
            this.radioButtonRemote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonRemote.Name = "radioButtonRemote";
            this.radioButtonRemote.Size = new System.Drawing.Size(78, 21);
            this.radioButtonRemote.TabIndex = 3;
            this.radioButtonRemote.TabStop = true;
            this.radioButtonRemote.Text = "Remote";
            this.radioButtonRemote.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(17, 14);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(185, 17);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "Choose dataLeaves source.";
            // 
            // labelIpPort
            // 
            this.labelIpPort.AutoSize = true;
            this.labelIpPort.Location = new System.Drawing.Point(8, 37);
            this.labelIpPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIpPort.Name = "labelIpPort";
            this.labelIpPort.Size = new System.Drawing.Size(65, 17);
            this.labelIpPort.TabIndex = 5;
            this.labelIpPort.Text = "IP : port: ";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(4, 73);
            this.labelDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(77, 17);
            this.labelDatabase.TabIndex = 6;
            this.labelDatabase.Text = "Database: ";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(91, 69);
            this.textBoxDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(225, 22);
            this.textBoxDatabase.TabIndex = 7;
            // 
            // textBoxIp1
            // 
            this.textBoxIp1.Location = new System.Drawing.Point(91, 33);
            this.textBoxIp1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIp1.Name = "textBoxIp1";
            this.textBoxIp1.Size = new System.Drawing.Size(43, 22);
            this.textBoxIp1.TabIndex = 8;
            // 
            // textBoxIp2
            // 
            this.textBoxIp2.Location = new System.Drawing.Point(143, 33);
            this.textBoxIp2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIp2.Name = "textBoxIp2";
            this.textBoxIp2.Size = new System.Drawing.Size(43, 22);
            this.textBoxIp2.TabIndex = 9;
            // 
            // textBoxIp3
            // 
            this.textBoxIp3.Location = new System.Drawing.Point(195, 33);
            this.textBoxIp3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIp3.Name = "textBoxIp3";
            this.textBoxIp3.Size = new System.Drawing.Size(43, 22);
            this.textBoxIp3.TabIndex = 10;
            // 
            // textBoxIp4
            // 
            this.textBoxIp4.Location = new System.Drawing.Point(247, 33);
            this.textBoxIp4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIp4.Name = "textBoxIp4";
            this.textBoxIp4.Size = new System.Drawing.Size(43, 22);
            this.textBoxIp4.TabIndex = 11;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(320, 33);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(65, 22);
            this.textBoxPort.TabIndex = 12;
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(52, 266);
            this.buttonTestConnection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(100, 50);
            this.buttonTestConnection.TabIndex = 13;
            this.buttonTestConnection.Text = "Test connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(160, 277);
            this.buttonAccept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(100, 28);
            this.buttonAccept.TabIndex = 14;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(268, 277);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 28);
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
            this.groupBoxRemote.Location = new System.Drawing.Point(16, 112);
            this.groupBoxRemote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxRemote.Name = "groupBoxRemote";
            this.groupBoxRemote.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxRemote.Size = new System.Drawing.Size(405, 132);
            this.groupBoxRemote.TabIndex = 16;
            this.groupBoxRemote.TabStop = false;
            this.groupBoxRemote.Text = "Specify it details.";
            // 
            // labelRemotePortColon
            // 
            this.labelRemotePortColon.AutoSize = true;
            this.labelRemotePortColon.Location = new System.Drawing.Point(299, 37);
            this.labelRemotePortColon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRemotePortColon.Name = "labelRemotePortColon";
            this.labelRemotePortColon.Size = new System.Drawing.Size(12, 17);
            this.labelRemotePortColon.TabIndex = 13;
            this.labelRemotePortColon.Text = ":";
            // 
            // groupBoxLocal
            // 
            this.groupBoxLocal.Controls.Add(this.buttonLocalBrowse);
            this.groupBoxLocal.Controls.Add(this.textBoxLocalPath);
            this.groupBoxLocal.Location = new System.Drawing.Point(16, 112);
            this.groupBoxLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxLocal.Name = "groupBoxLocal";
            this.groupBoxLocal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxLocal.Size = new System.Drawing.Size(405, 132);
            this.groupBoxLocal.TabIndex = 17;
            this.groupBoxLocal.TabStop = false;
            this.groupBoxLocal.Text = "Select database path.";
            this.groupBoxLocal.Visible = false;
            // 
            // buttonLocalBrowse
            // 
            this.buttonLocalBrowse.Location = new System.Drawing.Point(124, 81);
            this.buttonLocalBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLocalBrowse.Name = "buttonLocalBrowse";
            this.buttonLocalBrowse.Size = new System.Drawing.Size(100, 28);
            this.buttonLocalBrowse.TabIndex = 1;
            this.buttonLocalBrowse.Text = "Browse";
            this.buttonLocalBrowse.UseVisualStyleBackColor = true;
            this.buttonLocalBrowse.Click += new System.EventHandler(this.buttonLocalBrowse_Click);
            // 
            // textBoxLocalPath
            // 
            this.textBoxLocalPath.Location = new System.Drawing.Point(12, 32);
            this.textBoxLocalPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLocalPath.Name = "textBoxLocalPath";
            this.textBoxLocalPath.Size = new System.Drawing.Size(373, 22);
            this.textBoxLocalPath.TabIndex = 0;
            // 
            // FormDefineDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 342);
            this.Controls.Add(this.groupBoxRemote);
            this.Controls.Add(this.groupBoxLocal);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.radioButtonRemote);
            this.Controls.Add(this.radioButtonLocal);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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