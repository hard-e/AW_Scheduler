using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace AdamWest_Scheduler_c969
{
    public partial class Login : Form
    {
        private DataTable dtCurrentUser = new DataTable();

        // for recording user log-ins
        private StreamWriter streamWriter;
        string fileName = "userlog.txt"; // project bin\debug\net5.0-windows folder

        // get country/region name
        string crName = RegionInfo.CurrentRegion.EnglishName;

        public Login()
        {
            displayLocalLanguage();
            InitializeComponent();
        }

        private void displayLocalLanguage()
        {
            switch (crName)
            {
                case "United States":
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    break;
                case "Mexico":
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
                    break;

                default:
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    break;
            }
        }

        private void saveLog()
        {
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

            // write user and time to file
            streamWriter.WriteLine("UserID: " + dtCurrentUser.Rows[0][0]
                + " | Username: " + dtCurrentUser.Rows[0][1] + " | Date/Time: " + DateTime.Now);
            streamWriter.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private bool verifyNamePassword()
        {
            string userName = txtUsername.Text.ToLower();
            string password = txtPassword.Text;
            string sqlString = "SELECT userId, userName FROM user WHERE userName = '"
                + userName + "' AND password = '" + password + "';";

            // alphanumeric try/catch
            try
            {
                // connect to database
                MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                con.Open();
                // search 'user' table for match
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dtCurrentUser);
                con.Close();

                if (dtCurrentUser.Rows.Count != 0)
                {
                    saveLog();
                    return true;
                }
                else
                {
                    if (crName == "Mexico")
                    {
                        MessageBox.Show("El nombre de usuario y la contraseña que ingresó son incorrectos. Inténtalo de nuevo.");
                    }
                    else
                    {
                        MessageBox.Show("The username and password you entered are incorrect. Please try again.");
                    }
                    txtUsername.Focus();
                    return false;
                }
            }
            catch
            {
                if (crName == "Mexico")
                {
                    MessageBox.Show("Ingrese solo caracteres alfanuméricos");
                }
                else
                {
                    MessageBox.Show("Please enter alphanumeric characters only");
                }
                txtUsername.Focus();
                return false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (verifyNamePassword())
            {
                MainScreen mainScreen = new MainScreen((int)dtCurrentUser.Rows[0][0], txtUsername.Text);
                mainScreen.Show();
                this.Hide();
            }
        }
    }
}
