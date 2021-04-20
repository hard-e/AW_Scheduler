using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AdamWest_Scheduler_c969
{
    public partial class MainScreen : Form
    {
        public DataTable dtAppointment = new DataTable();
        public DataTable dtCustomer = new DataTable();
        DateTime date;
        string strDate;
        string userName;
        int userID;
        int currentAppointmentID;
        int nextAppointmentID;
        int nextUserID;
        int nextCustomerID;
        int nextAddressID;
        int nextCityID;

        public MainScreen(int currentUserID, string currentUserName)
        {            
            InitializeComponent();
            lblAppointments.Text = "Appointments for User: " + currentUserName;
            userName = currentUserName;
            userID = currentUserID;
            date = DateTime.Now;
            strDate = date.ToString("yyyy-MM-dd");
            calendar.AddBoldedDate(date);
            getCustomers("SELECT * FROM customer");
            displayDay();
            formatDGV(dgv);
            getAppointmentAlerts();
            getNext();
        }

        public void formatDGV(DataGridView d)
        {
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            d.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            d.RowHeadersVisible = false;
            foreach (DataGridViewColumn column in d.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            d.Refresh();
        }

        public void getAppointmentAlerts()
        {
            // alert the user if there is an appointment within the next 15 minutes
            int i = 0;
            while (i <= (dtAppointment.Rows.Count - 1))
            {
                if (((DateTime)dtAppointment.Rows[i][9] >= DateTime.Now) &&
                        ((DateTime)dtAppointment.Rows[i][9] - DateTime.Now) <= TimeSpan.FromMinutes(15))
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Upcoming appointment at " + dtAppointment.Rows[i][9].ToString());
                }
                i++;
            }
        }

        public void getNext() 
        {
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            // get the next appointment id
            string sqlString = "SELECT appointmentId FROM appointment ORDER BY appointmentId DESC LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            var dr = cmd.ExecuteReader();
            dr.Read();
            nextAppointmentID = (dr.GetInt32(0) + 1);
            dr.Close();

            // get the next customer id
            sqlString = "SELECT customerId FROM customer ORDER BY customerId DESC LIMIT 1;";
            cmd = new MySqlCommand(sqlString, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            nextCustomerID = (dr.GetInt32(0) + 1);
            dr.Close();

            // get the next user id
            sqlString = "SELECT userId FROM user ORDER BY userId DESC LIMIT 1;";
            cmd = new MySqlCommand(sqlString, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            nextUserID = (dr.GetInt32(0) + 1);
            dr.Close();

            // get the next address id
            sqlString = "SELECT addressId FROM address ORDER BY addressId DESC LIMIT 1;";
            cmd = new MySqlCommand(sqlString, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            nextAddressID = (dr.GetInt32(0) + 1);
            dr.Close();

            // get the next city id
            sqlString = "SELECT cityId FROM city ORDER BY cityId DESC LIMIT 1;";
            cmd = new MySqlCommand(sqlString, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            nextCityID = (dr.GetInt32(0) + 1);
            con.Close();
        }

        public void getAppointments(string sqlString)
        {
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtAppointment);
            // convert to local time
            for (int i = 0; i < dtAppointment.Rows.Count; i++)
            {
                dtAppointment.Rows[i]["start"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dtAppointment.Rows[i]["start"], TimeZoneInfo.Local).ToString();
                dtAppointment.Rows[i]["end"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dtAppointment.Rows[i]["end"], TimeZoneInfo.Local).ToString();
                dtAppointment.Rows[i]["createDate"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dtAppointment.Rows[i]["createDate"], TimeZoneInfo.Local).ToString();
                dtAppointment.Rows[i]["lastUpdate"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dtAppointment.Rows[i]["lastUpdate"], TimeZoneInfo.Local).ToString();
            }
            dgv.DataSource = dtAppointment;
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            con.Close();
        }

        public void getCustomers(string sqlString)
        {
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtCustomer);
            con.Close();
        }

        private void displayDay()
        {
            calendar.RemoveAllBoldedDates();
            calendar.AddBoldedDate(date);
            calendar.UpdateBoldedDates();
            dtAppointment.Clear();
            getAppointments("SELECT * FROM appointment WHERE userId = " + userID +
                " AND (DATE(start) = '" + strDate + "' OR DATE(end) = '" + strDate + "')");
            dgv.DataSource = dtAppointment;            
        }

        private void displayWeek()
        {
            calendar.RemoveAllBoldedDates();
            dtAppointment.Clear();
            int day = (int)date.DayOfWeek;
            string start = date.AddDays(-day).ToString("yyyy-MM-dd");
            DateTime temp = Convert.ToDateTime(start);
            for (int i = 0; i < 7; i++)
            {
                calendar.AddBoldedDate(temp.AddDays(i));
            }
            calendar.UpdateBoldedDates();
            string end = date.AddDays(7 - day).ToString("yyyy-MM-dd");
            getAppointments("SELECT * FROM appointment WHERE userId = " + userID + " AND " +
                "DATE(start) BETWEEN '" + start + "' AND '" + end + "'");
            dgv.DataSource = dtAppointment;
        }

        private void displayMonth()
        {
            calendar.RemoveAllBoldedDates();
            dtAppointment.Clear();
            DateTime temp;
            int month = date.Month;
            int year = date.Year;
            int days = 0;
            string start = year.ToString() + "-" + month.ToString() + "-" + "01";

            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                    days = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                case 12:
                    days = 30;
                    break;
                case 2:
                    days = 29;
                    break;
            }

            temp = DateTime.Parse(start);
            for (int i = 0; i < days; i++)
            {
                calendar.AddBoldedDate(temp.AddDays(i));
            }
            calendar.UpdateBoldedDates();
            string end = year.ToString() + "-" + month.ToString() + "-" + days.ToString();
            getAppointments("SELECT * FROM appointment WHERE userId = " + userID + " AND " +
                "DATE(start) BETWEEN '" + start + "' AND '" + end + "'");
            dgv.DataSource = dtAppointment;
        }

        public void deleteAppointment()
        {
            // capture and log the appointment type and customer record
            captureType();

            string sqlString = "DELETE FROM appointment WHERE appointmentId = '" + 
                currentAppointmentID + "'";
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            var dw = cmd.ExecuteNonQuery();
            con.Close();
        }

        public void captureType()
        {
            StreamWriter streamWriter;
            string fileName = "typeCapture.txt"; // project bin\debug\net5.0-windows folder

            // if the log file does not yet exist, create it
            if (!File.Exists(fileName))
            {
                var output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                // set file to where data is written
                streamWriter = new StreamWriter(output);
            }
            else
            {
                // open file for writing
                var output = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                // set file to where data is written
                streamWriter = new StreamWriter(output);
            }

            // write appointment type and userID to file
            streamWriter.WriteLine("Action: DELETE | Appointment Type: " + 
                dgv.CurrentRow.Cells[7].Value + " | Customer ID: " + dgv.CurrentRow.Cells[1].Value +
                " | Date/Time: " + DateTime.Now);
            streamWriter.Close();
        }

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            displayDay();
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            displayWeek();
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            displayMonth();
        }

        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            AppointmentScreen appointmentScreen = new AppointmentScreen(this, true, nextAppointmentID, userID, userName);
            {
                DateTime start = new DateTime();
                DateTime end = new DateTime();
                start = DateTime.Now;

                // verify next 15 min interval
                if (start.Minute % 15 != 0)
                {
                    int over = start.Minute % 15;
                    start = start.AddMinutes(15 - over);
                }
                end = start.AddMinutes(15);

                // load default values
                appointmentScreen.dtpStart.MinDate = start;
                appointmentScreen.dtpStart.Value = start;
                appointmentScreen.dtpEnd.MinDate = end;
                appointmentScreen.dtpEnd.Value = end;

                appointmentScreen.cboCustomer.DataSource = dtCustomer;
                appointmentScreen.cboCustomer.DisplayMember = "customerName";

                appointmentScreen.Show();
            }
        }

        private void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            // verify a row has been selected
            if (currentAppointmentID > 0)
            {
                AppointmentScreen appointmentScreen = new AppointmentScreen(this, false, currentAppointmentID, userID, userName);

                // load currently selected appointment dates/times
                appointmentScreen.dtpStart.Value = (DateTime)dgv.CurrentRow.Cells[9].Value;
                appointmentScreen.dtpEnd.Value = (DateTime)dgv.CurrentRow.Cells[10].Value;

                // load all customers
                appointmentScreen.cboCustomer.DataSource = dtCustomer;
                appointmentScreen.cboCustomer.DisplayMember = "customerName";
                // select the customer name for the current appointment
                int i = 0;
                while (i <= (dtCustomer.Rows.Count - 1))
                {
                    if ((int)dtCustomer.Rows[i][0] == (int)dgv.CurrentRow.Cells[1].Value)
                    {
                        appointmentScreen.cboCustomer.SelectedIndex = i;
                        break;
                    }
                    i++;
                }

                // load other values of currently selected appointment
                appointmentScreen.txtTitle.Text = dgv.CurrentRow.Cells[3].Value.ToString();
                appointmentScreen.txtDescription.Text = dgv.CurrentRow.Cells[4].Value.ToString();
                appointmentScreen.txtLocation.Text = dgv.CurrentRow.Cells[5].Value.ToString();
                appointmentScreen.txtContact.Text = dgv.CurrentRow.Cells[6].Value.ToString();
                appointmentScreen.txtType.Text = dgv.CurrentRow.Cells[7].Value.ToString();
                appointmentScreen.txtURL.Text = dgv.CurrentRow.Cells[8].Value.ToString();

                appointmentScreen.Show();
            }
            else
            {
                MessageBox.Show("Please selected an appointment to update");
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
            {
                currentAppointmentID = (int)dgv.CurrentRow.Cells[0].Value;
                dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            }
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            // verify a row has been selected
            if (currentAppointmentID > 0)
            {
                // alert the user
                SystemSounds.Beep.Play();
                var result = MessageBox.Show("Are you sure you want to delete this appointment?", "Delete?"
                , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // delete the appointment from the database
                    deleteAppointment();
                    calendar.SelectionStart = DateTime.Now;
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment for deletion");
            }
        }

        private void calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            date = calendar.SelectionStart;
            strDate = calendar.SelectionStart.ToString("yyyy-MM-dd");
            if (rbDay.Checked)
            {
                displayDay();
            }
            else if (rbWeek.Checked)
            {
                displayWeek();
            }
            else if (rbMonth.Checked)
            {
                displayMonth();
            }
        }

        private void btnEditUsers_Click(object sender, EventArgs e)
        {
            UserScreen userScreen = new UserScreen(userID, nextUserID, userName);
            userScreen.Show();
        }

        private void btnEditCustomers_Click(object sender, EventArgs e)
        {
            CustomerScreen customerScreen = new CustomerScreen(this, userID, dtCustomer, 
                nextCustomerID, nextAddressID, nextCityID, userName);
            customerScreen.Show();
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            ReportScreen reportScreen = new ReportScreen();
            reportScreen.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var m = MessageBox.Show(this,"Are you sure you want to exit AW Scheduler?", "Exit?", MessageBoxButtons.YesNo);
            if (m == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            var m = MessageBox.Show(this,"Are you sure you want to switch users?", "Exit?", MessageBoxButtons.YesNo);
            if (m == DialogResult.Yes)
            {
                Login login = new Login();
                this.Hide();
                login.Show();                  
            }
        }
    }
}
