namespace leave_manager
{
    partial class FormWorkSchedule
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSetWorkPlan = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelStartingHour = new System.Windows.Forms.Label();
            this.comboBoxMondayStartingHour = new System.Windows.Forms.ComboBox();
            this.comboBoxMondayEndHour = new System.Windows.Forms.ComboBox();
            this.labelEndHour = new System.Windows.Forms.Label();
            this.labelMonday = new System.Windows.Forms.Label();
            this.labelTuesday = new System.Windows.Forms.Label();
            this.comboBoxTuesdayEndHour = new System.Windows.Forms.ComboBox();
            this.comboBoxTuesdayStartingHour = new System.Windows.Forms.ComboBox();
            this.labelThursday = new System.Windows.Forms.Label();
            this.comboBoxThursdayEndHour = new System.Windows.Forms.ComboBox();
            this.comboBoxThursdayStartingHour = new System.Windows.Forms.ComboBox();
            this.labelWednesday = new System.Windows.Forms.Label();
            this.comboBoxWednesdayEndHour = new System.Windows.Forms.ComboBox();
            this.comboBoxWednesdayStartingHour = new System.Windows.Forms.ComboBox();
            this.labelSaturday = new System.Windows.Forms.Label();
            this.comboBoxSaturdayEndHour = new System.Windows.Forms.ComboBox();
            this.comboBoxSaturdayStartingHour = new System.Windows.Forms.ComboBox();
            this.labelFriday = new System.Windows.Forms.Label();
            this.comboBoxFridayEndHour = new System.Windows.Forms.ComboBox();
            this.comboBoxFridayStartingHour = new System.Windows.Forms.ComboBox();
            this.labelSunday = new System.Windows.Forms.Label();
            this.comboBoxSundayEndHour = new System.Windows.Forms.ComboBox();
            this.comboBoxSundayStartingHour = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(280, 301);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 32);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSetWorkPlan
            // 
            this.buttonSetWorkPlan.Location = new System.Drawing.Point(32, 301);
            this.buttonSetWorkPlan.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetWorkPlan.Name = "buttonSetWorkPlan";
            this.buttonSetWorkPlan.Size = new System.Drawing.Size(86, 32);
            this.buttonSetWorkPlan.TabIndex = 4;
            this.buttonSetWorkPlan.Text = "Set schedule";
            this.buttonSetWorkPlan.UseVisualStyleBackColor = true;
            this.buttonSetWorkPlan.Click += new System.EventHandler(this.buttonSetWorkPlan_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInfo.Location = new System.Drawing.Point(26, 20);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(294, 17);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "Here you can create or change work plan for ";
            // 
            // labelStartingHour
            // 
            this.labelStartingHour.AutoSize = true;
            this.labelStartingHour.Location = new System.Drawing.Point(113, 64);
            this.labelStartingHour.Name = "labelStartingHour";
            this.labelStartingHour.Size = new System.Drawing.Size(70, 13);
            this.labelStartingHour.TabIndex = 6;
            this.labelStartingHour.Text = "Starting hour:";
            // 
            // comboBoxMondayStartingHour
            // 
            this.comboBoxMondayStartingHour.FormattingEnabled = true;
            this.comboBoxMondayStartingHour.Location = new System.Drawing.Point(116, 80);
            this.comboBoxMondayStartingHour.Name = "comboBoxMondayStartingHour";
            this.comboBoxMondayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMondayStartingHour.TabIndex = 7;
            this.comboBoxMondayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxMondayStartingHour_SelectedIndexChanged);
            // 
            // comboBoxMondayEndHour
            // 
            this.comboBoxMondayEndHour.FormattingEnabled = true;
            this.comboBoxMondayEndHour.Location = new System.Drawing.Point(258, 80);
            this.comboBoxMondayEndHour.Name = "comboBoxMondayEndHour";
            this.comboBoxMondayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMondayEndHour.TabIndex = 8;
            this.comboBoxMondayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxMondayEndHour_SelectedIndexChanged);
            // 
            // labelEndHour
            // 
            this.labelEndHour.AutoSize = true;
            this.labelEndHour.Location = new System.Drawing.Point(255, 64);
            this.labelEndHour.Name = "labelEndHour";
            this.labelEndHour.Size = new System.Drawing.Size(53, 13);
            this.labelEndHour.TabIndex = 9;
            this.labelEndHour.Text = "End hour:";
            // 
            // labelMonday
            // 
            this.labelMonday.AutoSize = true;
            this.labelMonday.Location = new System.Drawing.Point(26, 83);
            this.labelMonday.Name = "labelMonday";
            this.labelMonday.Size = new System.Drawing.Size(45, 13);
            this.labelMonday.TabIndex = 10;
            this.labelMonday.Text = "Monday";
            // 
            // labelTuesday
            // 
            this.labelTuesday.AutoSize = true;
            this.labelTuesday.Location = new System.Drawing.Point(26, 110);
            this.labelTuesday.Name = "labelTuesday";
            this.labelTuesday.Size = new System.Drawing.Size(48, 13);
            this.labelTuesday.TabIndex = 13;
            this.labelTuesday.Text = "Tuesday";
            // 
            // comboBoxTuesdayEndHour
            // 
            this.comboBoxTuesdayEndHour.FormattingEnabled = true;
            this.comboBoxTuesdayEndHour.Location = new System.Drawing.Point(258, 107);
            this.comboBoxTuesdayEndHour.Name = "comboBoxTuesdayEndHour";
            this.comboBoxTuesdayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTuesdayEndHour.TabIndex = 12;
            this.comboBoxTuesdayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxTuesdayEndHour_SelectedIndexChanged);
            // 
            // comboBoxTuesdayStartingHour
            // 
            this.comboBoxTuesdayStartingHour.FormattingEnabled = true;
            this.comboBoxTuesdayStartingHour.Location = new System.Drawing.Point(116, 107);
            this.comboBoxTuesdayStartingHour.Name = "comboBoxTuesdayStartingHour";
            this.comboBoxTuesdayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTuesdayStartingHour.TabIndex = 11;
            this.comboBoxTuesdayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxTuesdayStartingHour_SelectedIndexChanged);
            // 
            // labelThursday
            // 
            this.labelThursday.AutoSize = true;
            this.labelThursday.Location = new System.Drawing.Point(26, 164);
            this.labelThursday.Name = "labelThursday";
            this.labelThursday.Size = new System.Drawing.Size(51, 13);
            this.labelThursday.TabIndex = 19;
            this.labelThursday.Text = "Thursday";
            // 
            // comboBoxThursdayEndHour
            // 
            this.comboBoxThursdayEndHour.FormattingEnabled = true;
            this.comboBoxThursdayEndHour.Location = new System.Drawing.Point(258, 161);
            this.comboBoxThursdayEndHour.Name = "comboBoxThursdayEndHour";
            this.comboBoxThursdayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxThursdayEndHour.TabIndex = 18;
            this.comboBoxThursdayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxThursdayEndHour_SelectedIndexChanged);
            // 
            // comboBoxThursdayStartingHour
            // 
            this.comboBoxThursdayStartingHour.FormattingEnabled = true;
            this.comboBoxThursdayStartingHour.Location = new System.Drawing.Point(116, 161);
            this.comboBoxThursdayStartingHour.Name = "comboBoxThursdayStartingHour";
            this.comboBoxThursdayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxThursdayStartingHour.TabIndex = 17;
            this.comboBoxThursdayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxThursdayStartingHour_SelectedIndexChanged);
            // 
            // labelWednesday
            // 
            this.labelWednesday.AutoSize = true;
            this.labelWednesday.Location = new System.Drawing.Point(26, 137);
            this.labelWednesday.Name = "labelWednesday";
            this.labelWednesday.Size = new System.Drawing.Size(64, 13);
            this.labelWednesday.TabIndex = 16;
            this.labelWednesday.Text = "Wednesday";
            // 
            // comboBoxWednesdayEndHour
            // 
            this.comboBoxWednesdayEndHour.FormattingEnabled = true;
            this.comboBoxWednesdayEndHour.Location = new System.Drawing.Point(258, 134);
            this.comboBoxWednesdayEndHour.Name = "comboBoxWednesdayEndHour";
            this.comboBoxWednesdayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxWednesdayEndHour.TabIndex = 15;
            this.comboBoxWednesdayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxWednesdayEndHour_SelectedIndexChanged);
            // 
            // comboBoxWednesdayStartingHour
            // 
            this.comboBoxWednesdayStartingHour.FormattingEnabled = true;
            this.comboBoxWednesdayStartingHour.Location = new System.Drawing.Point(116, 134);
            this.comboBoxWednesdayStartingHour.Name = "comboBoxWednesdayStartingHour";
            this.comboBoxWednesdayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxWednesdayStartingHour.TabIndex = 14;
            this.comboBoxWednesdayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxWednesdayStartingHour_SelectedIndexChanged);
            // 
            // labelSaturday
            // 
            this.labelSaturday.AutoSize = true;
            this.labelSaturday.Location = new System.Drawing.Point(26, 218);
            this.labelSaturday.Name = "labelSaturday";
            this.labelSaturday.Size = new System.Drawing.Size(49, 13);
            this.labelSaturday.TabIndex = 25;
            this.labelSaturday.Text = "Saturday";
            // 
            // comboBoxSaturdayEndHour
            // 
            this.comboBoxSaturdayEndHour.FormattingEnabled = true;
            this.comboBoxSaturdayEndHour.Location = new System.Drawing.Point(258, 215);
            this.comboBoxSaturdayEndHour.Name = "comboBoxSaturdayEndHour";
            this.comboBoxSaturdayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSaturdayEndHour.TabIndex = 24;
            this.comboBoxSaturdayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxSaturdayEndHour_SelectedIndexChanged);
            // 
            // comboBoxSaturdayStartingHour
            // 
            this.comboBoxSaturdayStartingHour.FormattingEnabled = true;
            this.comboBoxSaturdayStartingHour.Location = new System.Drawing.Point(116, 215);
            this.comboBoxSaturdayStartingHour.Name = "comboBoxSaturdayStartingHour";
            this.comboBoxSaturdayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSaturdayStartingHour.TabIndex = 23;
            this.comboBoxSaturdayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxSaturdayStartingHour_SelectedIndexChanged);
            // 
            // labelFriday
            // 
            this.labelFriday.AutoSize = true;
            this.labelFriday.Location = new System.Drawing.Point(26, 191);
            this.labelFriday.Name = "labelFriday";
            this.labelFriday.Size = new System.Drawing.Size(35, 13);
            this.labelFriday.TabIndex = 22;
            this.labelFriday.Text = "Friday";
            // 
            // comboBoxFridayEndHour
            // 
            this.comboBoxFridayEndHour.FormattingEnabled = true;
            this.comboBoxFridayEndHour.Location = new System.Drawing.Point(258, 188);
            this.comboBoxFridayEndHour.Name = "comboBoxFridayEndHour";
            this.comboBoxFridayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFridayEndHour.TabIndex = 21;
            this.comboBoxFridayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxFridayEndHour_SelectedIndexChanged);
            // 
            // comboBoxFridayStartingHour
            // 
            this.comboBoxFridayStartingHour.FormattingEnabled = true;
            this.comboBoxFridayStartingHour.Location = new System.Drawing.Point(116, 188);
            this.comboBoxFridayStartingHour.Name = "comboBoxFridayStartingHour";
            this.comboBoxFridayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFridayStartingHour.TabIndex = 20;
            this.comboBoxFridayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxFridayStartingHour_SelectedIndexChanged);
            // 
            // labelSunday
            // 
            this.labelSunday.AutoSize = true;
            this.labelSunday.Location = new System.Drawing.Point(26, 245);
            this.labelSunday.Name = "labelSunday";
            this.labelSunday.Size = new System.Drawing.Size(43, 13);
            this.labelSunday.TabIndex = 28;
            this.labelSunday.Text = "Sunday";
            // 
            // comboBoxSundayEndHour
            // 
            this.comboBoxSundayEndHour.FormattingEnabled = true;
            this.comboBoxSundayEndHour.Location = new System.Drawing.Point(258, 242);
            this.comboBoxSundayEndHour.Name = "comboBoxSundayEndHour";
            this.comboBoxSundayEndHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSundayEndHour.TabIndex = 27;
            this.comboBoxSundayEndHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxSundayEndHour_SelectedIndexChanged);
            // 
            // comboBoxSundayStartingHour
            // 
            this.comboBoxSundayStartingHour.FormattingEnabled = true;
            this.comboBoxSundayStartingHour.Location = new System.Drawing.Point(116, 242);
            this.comboBoxSundayStartingHour.Name = "comboBoxSundayStartingHour";
            this.comboBoxSundayStartingHour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSundayStartingHour.TabIndex = 26;
            this.comboBoxSundayStartingHour.SelectedIndexChanged += new System.EventHandler(this.comboBoxSundayStartingHour_SelectedIndexChanged);
            // 
            // FormWorkSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 378);
            this.Controls.Add(this.labelSunday);
            this.Controls.Add(this.comboBoxSundayEndHour);
            this.Controls.Add(this.comboBoxSundayStartingHour);
            this.Controls.Add(this.labelSaturday);
            this.Controls.Add(this.comboBoxSaturdayEndHour);
            this.Controls.Add(this.comboBoxSaturdayStartingHour);
            this.Controls.Add(this.labelFriday);
            this.Controls.Add(this.comboBoxFridayEndHour);
            this.Controls.Add(this.comboBoxFridayStartingHour);
            this.Controls.Add(this.labelThursday);
            this.Controls.Add(this.comboBoxThursdayEndHour);
            this.Controls.Add(this.comboBoxThursdayStartingHour);
            this.Controls.Add(this.labelWednesday);
            this.Controls.Add(this.comboBoxWednesdayEndHour);
            this.Controls.Add(this.comboBoxWednesdayStartingHour);
            this.Controls.Add(this.labelTuesday);
            this.Controls.Add(this.comboBoxTuesdayEndHour);
            this.Controls.Add(this.comboBoxTuesdayStartingHour);
            this.Controls.Add(this.labelMonday);
            this.Controls.Add(this.labelEndHour);
            this.Controls.Add(this.comboBoxMondayEndHour);
            this.Controls.Add(this.comboBoxMondayStartingHour);
            this.Controls.Add(this.labelStartingHour);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSetWorkPlan);
            this.Controls.Add(this.labelInfo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormWorkSchedule";
            this.Text = "Schedule creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonSetWorkPlan;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelStartingHour;
        private System.Windows.Forms.ComboBox comboBoxMondayStartingHour;
        private System.Windows.Forms.ComboBox comboBoxMondayEndHour;
        private System.Windows.Forms.Label labelEndHour;
        private System.Windows.Forms.Label labelMonday;
        private System.Windows.Forms.Label labelTuesday;
        private System.Windows.Forms.ComboBox comboBoxTuesdayEndHour;
        private System.Windows.Forms.ComboBox comboBoxTuesdayStartingHour;
        private System.Windows.Forms.Label labelThursday;
        private System.Windows.Forms.ComboBox comboBoxThursdayEndHour;
        private System.Windows.Forms.ComboBox comboBoxThursdayStartingHour;
        private System.Windows.Forms.Label labelWednesday;
        private System.Windows.Forms.ComboBox comboBoxWednesdayEndHour;
        private System.Windows.Forms.ComboBox comboBoxWednesdayStartingHour;
        private System.Windows.Forms.Label labelSaturday;
        private System.Windows.Forms.ComboBox comboBoxSaturdayEndHour;
        private System.Windows.Forms.ComboBox comboBoxSaturdayStartingHour;
        private System.Windows.Forms.Label labelFriday;
        private System.Windows.Forms.ComboBox comboBoxFridayEndHour;
        private System.Windows.Forms.ComboBox comboBoxFridayStartingHour;
        private System.Windows.Forms.Label labelSunday;
        private System.Windows.Forms.ComboBox comboBoxSundayEndHour;
        private System.Windows.Forms.ComboBox comboBoxSundayStartingHour;
    }
}