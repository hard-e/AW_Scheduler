using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamWest_Scheduler_c969
{
    public partial class AppointmentScreen : Form
    {
        bool createAppointment = false;
        bool allValid = false;
        string currentUserName;
        int currentUserID;
        int currentAppointmentID;
        DataTable dtAppointment = new DataTable();
        ToolTip ttType = new ToolTip();
        MainScreen reloadMainScreen;
        /* LAMBDA */
        delegate bool IsValid(DateTime start, DateTime end);
        /* LAMBDA */
        delegate void ValidateForm(string text);

        public AppointmentScreen(MainScreen mainScreen, bool newAppointment, int appointmentID, int userID, string userName)
        {
            InitializeComponent();
            reloadMainScreen = mainScreen;
            createAppointment = newAppointment;
            currentAppointmentID = appointmentID;
            currentUserID = userID;
            currentUserName = userName;
        }

        public void saveAppointment()
        {
            // convert dates/times to UTC
            dtpStart.Value = TimeZoneInfo.ConvertTimeToUtc(dtpStart.Value);
            dtpEnd.Value = TimeZoneInfo.ConvertTimeToUtc(dtpEnd.Value);
            // if it is a new appointment, add it to the database
            if (createAppointment)
            {
                string sqlString = "INSERT INTO appointment VALUES (" + currentAppointmentID + ", " 
                    + (cboCustomer.SelectedIndex + 1) + ", " + currentUserID + ", '" +
                    txtTitle.Text + "', '" + txtDescription.Text + "', '" +
                    txtLocation.Text + "', '" + txtContact.Text + "', '" + txtType.Text +
                    "', '" + txtURL.Text + "', '" + formatDate(dtpStart.Value) + "', '" + 
                    formatDate(dtpEnd.Value) + "', NOW(), '" + 
                    currentUserName + "', NOW(), '" + 
                    currentUserName + "')";
                MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else // if updating an appointment
            {
                // save the updated record
                string sqlString = "UPDATE appointment " +
                    "SET customerId = '" + (cboCustomer.SelectedIndex + 1) + "'," +
                    "title = '" + txtTitle.Text + "', description = '" + txtDescription.Text + 
                    "', location = '" + txtLocation.Text + "', contact = '" + txtContact.Text + 
                    "', type = '" + txtType.Text + "', url = '" + txtURL.Text + "', start = '" + 
                    formatDate(dtpStart.Value) + "', end = '" + formatDate(dtpEnd.Value) + 
                    "', lastUpdate = NOW(), lastUpdateBy = '" +
                    currentUserName + "' WHERE appointmentId = '" + currentAppointmentID + "'";
                MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MainScreen mainScreen = new MainScreen(currentUserID, currentUserName);
            mainScreen.Show();
            reloadMainScreen.Hide();
            this.Close();
        }

        public string formatDate(DateTime dt)
        {
            string newDT = "";
            newDT = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return newDT;
        }

        /* REPLACED WITH LAMBDA */
        //public bool validateDates()
        //{
        //    // verify the start date is not later than the end date
        //    if (dtpStart.Value > dtpEnd.Value) 
        //    {
        //        // alert the user
        //        MessageBox.Show("Start date/time cannot be later than end date/time");
        //        return false;
        //    }
        //    // verify start and end dates are not on weekends
        //    else if (dtpStart.Value.DayOfWeek == DayOfWeek.Saturday ||
        //        dtpStart.Value.DayOfWeek == DayOfWeek.Sunday ||
        //        dtpEnd.Value.DayOfWeek == DayOfWeek.Saturday ||
        //        dtpEnd.Value.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        // alert the user
        //        MessageBox.Show("Start and end dates must be weekdays");
        //        return false;
        //    }
        //    // verify start and end times are not outside business hours (9am - 5pm)
        //    else if (dtpStart.Value.Hour < 9 || dtpEnd.Value.Hour < 9 || dtpStart.Value.Hour > 16
        //        || dtpEnd.Value.Hour > 16)
        //    {
        //        // alert the user
        //        MessageBox.Show("Start and end times must be within buesiness hours of 9am - 5pm");
        //        return false;
        //    }

        //    // check for overlapping dates/times
        //    if (checkDuplicates())
        //    {
        //        MessageBox.Show("This dates/times you selected overlap with an existing appointment");
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        private bool checkDuplicates()
        {
            DateTime start;
            DateTime end;
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            string sqlString = "SELECT start, end FROM appointment WHERE userId = " + currentUserID;
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtAppointment);
            for (int i = 0; i < dtAppointment.Rows.Count; i++)
            {
                start = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dtAppointment.Rows[i][0], TimeZoneInfo.Local);
                end = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dtAppointment.Rows[i][1], TimeZoneInfo.Local);
                // if an overlapping appointment is found
                if (dtpStart.Value >= start && dtpStart.Value <= end)
                {
                    con.Close();
                    return true;
                }
                else if (dtpEnd.Value >= end && dtpEnd.Value <= start)
                {
                    con.Close();
                    return true;
                }
            }
            con.Close();
            return false;
        }

        public void validateForm()
        {
            /* LAMBDA : TO SIMPLIFY THE FUNCTION 'validateForm()' -
            THE FOLLOWING FUNCTION CODE (lines 177-204) HAS BEEN 
            COMMENTED OUT*/
            ValidateForm validateForm = text => { if (text == "") { text = "not needed"; } };

            validateForm(txtTitle.Text);
            validateForm(txtDescription.Text);
            validateForm(txtContact.Text);
            validateForm(txtLocation.Text);
            validateForm(txtURL.Text);

            /* REPLACED WITH LAMBDA */
            //string title = txtTitle.Text.Trim();
            //string description = txtDescription.Text.Trim();
            //string customer = cboCustomer.SelectedItem.ToString();
            //string contact = txtContact.Text.Trim();
            //string location = txtLocation.Text.Trim();
            //string type = txtType.Text.Trim();
            //string url = txtURL.Text.Trim();

            //if (title == "")
            //{
            //    txtTitle.Text = "not needed";
            ////}
            //if (description == "")
            //{
            //    txtDescription.Text = "not needed";
            //}
            //if (contact == "")
            //{
            //    txtContact.Text = "not needed";
            //}
            //if (location == "")
            //{
            //    txtLocation.Text = "not needed";
            //}
            //if (url == "")
            //{
            //    txtURL.Text = "not needed";
            //}

            if (txtType.Text == "")
            {
                txtType.BackColor = System.Drawing.Color.LightSalmon;
                ttType.SetToolTip(txtType, "Enter a type");
                txtType.Focus();
                allValid = false;
            }
            else
            {
                allValid = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // alphanumeric try/catch
            try
            {
                // first, validate the form
                validateForm();

                /* LAMBDA : TO SIMPLIFY THE FUNCTION 'validateDates()' -
                            BOTH THE FOLLOWING CALL AND FUNCTION CODE (lines 93-131) HAVE BEEN 
                            COMMENTED OUT*/
                IsValid isValid = (start, end) => { return 
                    start < end && start.DayOfWeek != DayOfWeek.Saturday &&
                    start.DayOfWeek != DayOfWeek.Sunday && end.DayOfWeek != DayOfWeek.Saturday &&
                    end.DayOfWeek != DayOfWeek.Sunday && start.Hour > 9 && end.Hour > 9 &&
                    start.Hour < 16 && end.Hour < 16; };

                if (!isValid(dtpStart.Value, dtpEnd.Value))
                //if (!validateDates())
                {
                    MessageBox.Show("Start date/time must be earlier than end date/time, and both" +
                        " must be on weekdays between the business hours of 9am - 5pm.");
                    return;
                }
                else
                {
                    if (checkDuplicates())
                    {
                        var m = MessageBox.Show(this, "This dates/times you selected overlap with an existing appointment." +
                            "Do you wish to save anyway?", "Possible Overlap", MessageBoxButtons.YesNo);
                        if (m == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                // save and close if all fields are valid
                if (allValid)
                {
                    captureType(createAppointment);
                    saveAppointment();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter an appointment type");
                }
            }
            catch
            {
                MessageBox.Show("Please enter alphanumeric characters only");
                txtTitle.Focus();
            }
        }

        public void captureType(bool action)
        {
            string strAction;

            if (action)
            {
                strAction = "CREATE";
            }
            else
            {
                strAction = "UPDATE";
            }

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
            streamWriter.WriteLine("Action: " + strAction + " | Appointment Type: " +
                txtType.Text + " | Customer ID: " + (cboCustomer.SelectedIndex + 1) +
                " | Date/Time: " + DateTime.Now);
            streamWriter.Close();
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            if (txtType.BackColor == System.Drawing.Color.LightSalmon)
            {
                txtType.BackColor = System.Drawing.Color.White;
                ttType.RemoveAll();
            }
        }
    }
}
