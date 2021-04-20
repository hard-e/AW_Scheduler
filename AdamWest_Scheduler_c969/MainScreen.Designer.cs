
namespace AdamWest_Scheduler_c969
{
    partial class MainScreen
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbWeek = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.lblAppointments = new System.Windows.Forms.Label();
            this.btnNewAppointment = new System.Windows.Forms.Button();
            this.btnUpdateAppointment = new System.Windows.Forms.Button();
            this.btnDeleteAppointment = new System.Windows.Forms.Button();
            this.btnEditCustomers = new System.Windows.Forms.Button();
            this.btnEditUsers = new System.Windows.Forms.Button();
            this.btnViewReports = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(25, 243);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowTemplate.Height = 25;
            this.dgv.Size = new System.Drawing.Size(763, 309);
            this.dgv.TabIndex = 1;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // calendar
            // 
            this.calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.calendar.Location = new System.Drawing.Point(55, 23);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 10;
            this.calendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateChanged);
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Checked = true;
            this.rbDay.Location = new System.Drawing.Point(303, 36);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(45, 19);
            this.rbDay.TabIndex = 7;
            this.rbDay.TabStop = true;
            this.rbDay.Text = "Day";
            this.rbDay.UseVisualStyleBackColor = true;
            this.rbDay.CheckedChanged += new System.EventHandler(this.rbDay_CheckedChanged);
            // 
            // rbWeek
            // 
            this.rbWeek.AutoSize = true;
            this.rbWeek.Location = new System.Drawing.Point(303, 76);
            this.rbWeek.Name = "rbWeek";
            this.rbWeek.Size = new System.Drawing.Size(54, 19);
            this.rbWeek.TabIndex = 8;
            this.rbWeek.Text = "Week";
            this.rbWeek.UseVisualStyleBackColor = true;
            this.rbWeek.CheckedChanged += new System.EventHandler(this.rbWeek_CheckedChanged);
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(303, 116);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(61, 19);
            this.rbMonth.TabIndex = 9;
            this.rbMonth.Text = "Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // lblAppointments
            // 
            this.lblAppointments.AutoSize = true;
            this.lblAppointments.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAppointments.Location = new System.Drawing.Point(25, 219);
            this.lblAppointments.Name = "lblAppointments";
            this.lblAppointments.Size = new System.Drawing.Size(129, 18);
            this.lblAppointments.TabIndex = 6;
            this.lblAppointments.Text = "Appointments";
            // 
            // btnNewAppointment
            // 
            this.btnNewAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNewAppointment.Location = new System.Drawing.Point(387, 23);
            this.btnNewAppointment.Name = "btnNewAppointment";
            this.btnNewAppointment.Size = new System.Drawing.Size(100, 55);
            this.btnNewAppointment.TabIndex = 0;
            this.btnNewAppointment.Text = "New Appointment";
            this.btnNewAppointment.UseVisualStyleBackColor = false;
            this.btnNewAppointment.Click += new System.EventHandler(this.btnNewAppointment_Click);
            // 
            // btnUpdateAppointment
            // 
            this.btnUpdateAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUpdateAppointment.Location = new System.Drawing.Point(387, 107);
            this.btnUpdateAppointment.Name = "btnUpdateAppointment";
            this.btnUpdateAppointment.Size = new System.Drawing.Size(100, 37);
            this.btnUpdateAppointment.TabIndex = 2;
            this.btnUpdateAppointment.Text = "Update";
            this.btnUpdateAppointment.UseVisualStyleBackColor = false;
            this.btnUpdateAppointment.Click += new System.EventHandler(this.btnUpdateAppointment_Click);
            // 
            // btnDeleteAppointment
            // 
            this.btnDeleteAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDeleteAppointment.Location = new System.Drawing.Point(387, 150);
            this.btnDeleteAppointment.Name = "btnDeleteAppointment";
            this.btnDeleteAppointment.Size = new System.Drawing.Size(100, 37);
            this.btnDeleteAppointment.TabIndex = 3;
            this.btnDeleteAppointment.Text = "Delete";
            this.btnDeleteAppointment.UseVisualStyleBackColor = false;
            this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDeleteAppointment_Click);
            // 
            // btnEditCustomers
            // 
            this.btnEditCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEditCustomers.Location = new System.Drawing.Point(585, 94);
            this.btnEditCustomers.Name = "btnEditCustomers";
            this.btnEditCustomers.Size = new System.Drawing.Size(100, 55);
            this.btnEditCustomers.TabIndex = 5;
            this.btnEditCustomers.Text = "Edit Customers";
            this.btnEditCustomers.UseVisualStyleBackColor = false;
            this.btnEditCustomers.Click += new System.EventHandler(this.btnEditCustomers_Click);
            // 
            // btnEditUsers
            // 
            this.btnEditUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEditUsers.Location = new System.Drawing.Point(585, 23);
            this.btnEditUsers.Name = "btnEditUsers";
            this.btnEditUsers.Size = new System.Drawing.Size(100, 55);
            this.btnEditUsers.TabIndex = 4;
            this.btnEditUsers.Text = "Edit Users";
            this.btnEditUsers.UseVisualStyleBackColor = false;
            this.btnEditUsers.Click += new System.EventHandler(this.btnEditUsers_Click);
            // 
            // btnViewReports
            // 
            this.btnViewReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnViewReports.Location = new System.Drawing.Point(585, 167);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Size = new System.Drawing.Size(100, 55);
            this.btnViewReports.TabIndex = 6;
            this.btnViewReports.Text = "View Reports";
            this.btnViewReports.UseVisualStyleBackColor = false;
            this.btnViewReports.Click += new System.EventHandler(this.btnViewReports_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightCoral;
            this.btnExit.Location = new System.Drawing.Point(715, 150);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(73, 72);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Exit Scheduler";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUser
            // 
            this.btnUser.BackColor = System.Drawing.Color.LightCoral;
            this.btnUser.Location = new System.Drawing.Point(715, 23);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(73, 121);
            this.btnUser.TabIndex = 12;
            this.btnUser.Text = "Switch User";
            this.btnUser.UseVisualStyleBackColor = false;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 574);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnViewReports);
            this.Controls.Add(this.btnEditUsers);
            this.Controls.Add(this.btnEditCustomers);
            this.Controls.Add(this.btnDeleteAppointment);
            this.Controls.Add(this.btnUpdateAppointment);
            this.Controls.Add(this.btnNewAppointment);
            this.Controls.Add(this.lblAppointments);
            this.Controls.Add(this.rbMonth);
            this.Controls.Add(this.rbWeek);
            this.Controls.Add(this.rbDay);
            this.Controls.Add(this.calendar);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AW Scheduler";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainScreen_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbWeek;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.Label lblAppointments;
        private System.Windows.Forms.Button btnNewAppointment;
        private System.Windows.Forms.Button btnUpdateAppointment;
        private System.Windows.Forms.Button btnDeleteAppointment;
        private System.Windows.Forms.Button btnEditCustomers;
        //private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem mnuAddUser;
        public System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnEditUsers;
        private System.Windows.Forms.Button btnViewReports;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUser;
    }
}