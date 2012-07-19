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
            this.dataGridViewWorkPlan = new System.Windows.Forms.DataGridView();
            this.comboBoxWeek = new System.Windows.Forms.ComboBox();
            this.labelChooseWeek = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(544, 484);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 40);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSetWorkPlan
            // 
            this.buttonSetWorkPlan.Location = new System.Drawing.Point(213, 484);
            this.buttonSetWorkPlan.Name = "buttonSetWorkPlan";
            this.buttonSetWorkPlan.Size = new System.Drawing.Size(115, 40);
            this.buttonSetWorkPlan.TabIndex = 4;
            this.buttonSetWorkPlan.Text = "Set schedule";
            this.buttonSetWorkPlan.UseVisualStyleBackColor = true;
            // 
            // dataGridViewWorkPlan
            // 
            this.dataGridViewWorkPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWorkPlan.Location = new System.Drawing.Point(75, 118);
            this.dataGridViewWorkPlan.Name = "dataGridViewWorkPlan";
            this.dataGridViewWorkPlan.ReadOnly = true;
            this.dataGridViewWorkPlan.RowHeadersWidth = 80;
            this.dataGridViewWorkPlan.RowTemplate.Height = 24;
            this.dataGridViewWorkPlan.Size = new System.Drawing.Size(804, 340);
            this.dataGridViewWorkPlan.TabIndex = 3;
            // 
            // comboBoxWeek
            // 
            this.comboBoxWeek.FormattingEnabled = true;
            this.comboBoxWeek.Location = new System.Drawing.Point(206, 61);
            this.comboBoxWeek.Name = "comboBoxWeek";
            this.comboBoxWeek.Size = new System.Drawing.Size(121, 24);
            this.comboBoxWeek.TabIndex = 2;
            // 
            // labelChooseWeek
            // 
            this.labelChooseWeek.AutoSize = true;
            this.labelChooseWeek.Location = new System.Drawing.Point(34, 61);
            this.labelChooseWeek.Name = "labelChooseWeek";
            this.labelChooseWeek.Size = new System.Drawing.Size(124, 17);
            this.labelChooseWeek.TabIndex = 1;
            this.labelChooseWeek.Text = "Choose one week:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInfo.Location = new System.Drawing.Point(34, 24);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(346, 20);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "Here you can create or change work plan for ";
            // 
            // FormWorkSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 535);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSetWorkPlan);
            this.Controls.Add(this.dataGridViewWorkPlan);
            this.Controls.Add(this.comboBoxWeek);
            this.Controls.Add(this.labelChooseWeek);
            this.Controls.Add(this.labelInfo);
            this.Name = "FormWorkSchedule";
            this.Text = "Schedule creator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkPlan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelChooseWeek;
        private System.Windows.Forms.ComboBox comboBoxWeek;
        private System.Windows.Forms.DataGridView dataGridViewWorkPlan;
        private System.Windows.Forms.Button buttonSetWorkPlan;
        private System.Windows.Forms.Button buttonCancel;
    }
}