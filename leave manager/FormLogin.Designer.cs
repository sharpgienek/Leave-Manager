namespace leave_manager
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.labelLoginInfo = new System.Windows.Forms.Label();
            this.labelLoginLogin = new System.Windows.Forms.Label();
            this.labelLoginPassword = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonLoginLogin = new System.Windows.Forms.Button();
            this.buttonLoginExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelLoginInfo
            // 
            resources.ApplyResources(this.labelLoginInfo, "labelLoginInfo");
            this.labelLoginInfo.Name = "labelLoginInfo";
            // 
            // labelLoginLogin
            // 
            resources.ApplyResources(this.labelLoginLogin, "labelLoginLogin");
            this.labelLoginLogin.Name = "labelLoginLogin";
            this.labelLoginLogin.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelLoginPassword
            // 
            resources.ApplyResources(this.labelLoginPassword, "labelLoginPassword");
            this.labelLoginPassword.Name = "labelLoginPassword";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // buttonLoginLogin
            // 
            resources.ApplyResources(this.buttonLoginLogin, "buttonLoginLogin");
            this.buttonLoginLogin.Name = "buttonLoginLogin";
            this.buttonLoginLogin.UseVisualStyleBackColor = true;
            // 
            // buttonLoginExit
            // 
            resources.ApplyResources(this.buttonLoginExit, "buttonLoginExit");
            this.buttonLoginExit.Name = "buttonLoginExit";
            this.buttonLoginExit.UseVisualStyleBackColor = true;
            // 
            // FormLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLoginExit);
            this.Controls.Add(this.buttonLoginLogin);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelLoginPassword);
            this.Controls.Add(this.labelLoginLogin);
            this.Controls.Add(this.labelLoginInfo);
            this.Name = "FormLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLoginInfo;
        private System.Windows.Forms.Label labelLoginLogin;
        private System.Windows.Forms.Label labelLoginPassword;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonLoginLogin;
        private System.Windows.Forms.Button buttonLoginExit;

    }
}

