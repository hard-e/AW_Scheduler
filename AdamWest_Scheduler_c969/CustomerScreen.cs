using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamWest_Scheduler_c969
{
    public partial class CustomerScreen : Form
    {        
        DataTable dtCustomer = new DataTable();
        DataTable dtAddress = new DataTable();
        DataTable dtCity = new DataTable();
        DataTable dtCountry = new DataTable();
        MainScreen lastMainScreen;
        bool isNewCustomer;
        bool isError = false;
        int selectedCustomerID;
        int currentUserID;
        int saveCustomerID;
        int saveAddressID;
        int saveCityID;
        int saveCountryID;
        int newAddressID;
        int newCustomerID;
        int newCityID;
        string currentUserName;
        string currentAddress;
        string currentAddress2;
        string currentCity;
        string currentPostalCode;
        string currentPhone;

        public CustomerScreen(MainScreen mainScreen, int userID, DataTable dt, int nextCustomerID, int nextAddressID, int nextCityID, string userName)
        {
            InitializeComponent();
            currentUserID = userID;
            currentUserName = userName;
            newCustomerID = nextCustomerID;
            newAddressID = nextAddressID;
            newCityID = nextCityID;
            dtCustomer = dt;
            lastMainScreen = mainScreen;
            
            dgv.DataSource = dtCustomer;
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            formatDGV(dgv);
        }

        public void verifyForm()
        {
            currentAddress = txtAddress.Text.Trim();
            currentAddress2 = txtAddress2.Text.Trim();
            currentCity = txtCity.Text.Trim();
            currentPostalCode = txtPostalCode.Text.Trim();
            currentPhone = txtPhone.Text.Trim();

            // set the countryId
            saveCountryID = (cboCountry.SelectedIndex + 1);

            // set the cityId
            getSetCityID();
        }

        public void saveCustomer()
        {
            // verify all required fields
            if (txtName.Text.Trim() == "" || txtAddress.Text.Trim() == "" || txtCity.Text.Trim() == ""
                || txtPostalCode.Text.Trim() == "" || txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Every customer record must include a name, address, city, " +
                    "postal code, and phone number.");
                return;
            }

            // alpha-numberic try/catch 
            try
            {
                verifyForm();

                // if creating a new customer...
                if (isNewCustomer)
                {
                    // verify customer doesn't already exist
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (txtName.Text == dgv.Rows[i].Cells[1].Value.ToString())
                        {
                            // alert the user and abort
                            MessageBox.Show("Customer already exists");
                            return;
                        }
                    }
                    // assign customerID
                    saveCustomerID = newCustomerID;
                    newCustomerID += 1;

                    // assign addressID
                    saveAddressID = newAddressID;
                    newAddressID += 1;

                    // save the address information
                    string sqlString = "INSERT INTO address VALUES(" + saveAddressID + ", '" +
                        currentAddress + "', '" + currentAddress2 + "', " + saveCityID + ", '" +
                        currentPostalCode + "', '" + currentPhone + "', NOW(), '" +
                        currentUserName + "', NOW(), '" +
                        currentUserName + "')";
                    MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlString, con);
                    cmd.ExecuteNonQuery();

                    // save the customer information
                    sqlString = "INSERT INTO customer VALUES(" + saveCustomerID + ", '"
                        + txtName.Text + "', " + saveAddressID + ", " +
                        1 + ", NOW(), '" + currentUserName + "', NOW(), '" + currentUserName + "')";
                    cmd = new MySqlCommand(sqlString, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else // if updating a customer
                {
                    // save the updated city
                    string sqlString = "UPDATE city SET city = '" + currentCity + "', countryId = " +
                        saveCountryID + ", lastUpdate = NOW(), lastUpdateBy = '" + currentUserName +
                        "' WHERE cityId = " + saveCityID;
                    MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlString, con);
                    cmd.ExecuteNonQuery();

                    saveAddressID = (int)dgv.CurrentRow.Cells[2].Value;
                    // save the updated address
                    sqlString = "UPDATE address SET address = '" + currentAddress +
                        "', address2 = '" + currentAddress2 + "', cityId = " + saveCityID +
                        ", postalCode = '" + currentPostalCode + "', phone = '" + currentPhone +
                        "', lastUpdate = NOW(), lastUpdateBy = '" + currentUserName +
                        "' WHERE addressId = " + saveAddressID;
                    con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                    con.Open();
                    cmd = new MySqlCommand(sqlString, con);
                    cmd.ExecuteNonQuery();

                    // save the updated customer
                    sqlString = "UPDATE customer " +
                        "SET customerName = '" + txtName.Text + "'," +
                        " active = " + 1 + ", lastUpdate = NOW(), lastUpdateBy = '" + currentUserName +
                        "' WHERE customerID = " + saveCustomerID;
                    con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                    con.Open();
                    cmd = new MySqlCommand(sqlString, con);
                    var dw = cmd.ExecuteNonQuery();
                    con.Close();
                }

                // update dgv
                selectedCustomerID = -1;
                getCustomers("SELECT * FROM customer");
                dgv.DataSource = dtCustomer;
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                formatDGV(dgv);
                isError = false;
            }
            catch
            {
                isError = true;
                MessageBox.Show("Please enter alphanumeric characters only");
            }
        }

        public string formatDate(DateTime dt)
        {
            string newDT = "";
            newDT = dt.ToString("yyyy-MM-dd HH:mm:ss");
            newDT = "STR_TO_DATE('" + newDT + "', '%Y-%m-%d %H:%i:%s')";
            MessageBox.Show(newDT);
            return newDT;
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

        public void getCustomers(string sqlString)
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.Refresh();
            dtCustomer.Clear();

            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtCustomer);
            con.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
            {
                // clear the form
                if (txtName.Enabled)
                {
                    resetForm();
                }

                selectedCustomerID = (int)dgv.CurrentRow.Cells[0].Value;
                saveCustomerID = selectedCustomerID;
                dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        public void getCountry()
        {
            dtCountry.Clear();
            string sqlString = "SELECT countryId, country FROM country";
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtCountry);
            con.Close();
        }

        public void getSetCityID()
        {
            if (isNewCustomer)
            {
                dtCity.Clear();
                string sqlString = "SELECT cityId FROM city WHERE city = '" + currentCity + "'";
                MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dtCity);

                // if city not found...
                if (dtCity.Rows.Count < 1)
                {
                    saveCityID = newCityID;
                    newCityID += 1;
                    // create new city
                    sqlString = "INSERT INTO city VALUES(" + saveCityID + ", '" +
                        currentCity + "', " + saveCountryID + ", NOW(), " +
                        "'" + currentUserName + "', NOW(), '" +
                        currentUserName + "')";
                    cmd = new MySqlCommand(sqlString, con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    saveCityID = (int)dtCity.Rows[0][0];
                }
            }
            else
            {
                saveCityID = (int)dgv.CurrentRow.Cells[2].Value;
            }
        }

        public void getAddress(int addressID)
        {
            dtAddress.Clear();
            dtCity.Clear();
            string sqlString = "SELECT * FROM address WHERE addressId = " + addressID;
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtAddress);
            sqlString = "SELECT * FROM city WHERE cityId = " + (int)dtAddress.Rows[0]["cityId"];
            cmd = new MySqlCommand(sqlString, con);
            adp = new MySqlDataAdapter(cmd);
            adp.Fill(dtCity);
            con.Close();
            for (int i = 0; i < dtCountry.Rows.Count; i++)
            {
                if ((int)dtCity.Rows[0][2] == (int)dtCountry.Rows[i][0])
                {
                    cboCountry.SelectedIndex = ((int)dtCountry.Rows[i][0] - 1);
                }
            }
            saveAddressID = addressID;
            currentAddress = dtAddress.Rows[0]["address"].ToString();
            currentAddress2 = dtAddress.Rows[0]["address2"].ToString();
            currentCity = dtCity.Rows[0]["city"].ToString();
            currentPostalCode = dtAddress.Rows[0]["postalCode"].ToString();
            currentPhone = dtAddress.Rows[0]["phone"].ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // verify a row has been selected
            if (selectedCustomerID > 0)
            {
                // load countries
                getCountry();
                cboCountry.DataSource = dtCountry;
                cboCountry.DisplayMember = "country";

                // get customer's address
                getAddress((int)dgv.CurrentRow.Cells[2].Value);

                // load customer info into text boxes and enable editing
                txtName.Text = dgv.CurrentRow.Cells[1].Value.ToString();
                txtAddress.Text = currentAddress;
                txtAddress2.Text = currentAddress2;
                txtCity.Text = currentCity;
                txtPostalCode.Text = currentPostalCode;
                txtPhone.Text = currentPhone;

                txtName.Enabled = true;
                txtAddress.Enabled = true;
                txtAddress2.Enabled = true;
                txtCity.Enabled = true;
                txtPostalCode.Enabled = true;
                txtPhone.Enabled = true;
                cboCountry.Enabled = true;
                btnSave.Enabled = true;
                isNewCustomer = false;
                isError = false;
            }
        }

        private void resetForm()
        {
            // reset the blank form
            txtName.Text = "";
            txtAddress.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtPostalCode.Text = "";
            txtPhone.Text = "";
            cboCountry.DataSource = null;
            dtCountry.Clear();
            txtName.Enabled = false;
            txtAddress.Enabled = false;
            txtAddress2.Enabled = false;
            txtCity.Enabled = false;
            txtPostalCode.Enabled = false;
            txtPhone.Enabled = false;
            cboCountry.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            isNewCustomer = false;

            currentAddress = "";
            currentAddress2 = "";
            currentCity = "";
            currentPostalCode = "";
            currentPhone = "";
            saveAddressID = 0;
            saveCityID = 0;
            saveCountryID = 0;
            saveCustomerID = 0;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // reset the blank form and enable
            resetForm();

            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtAddress2.Enabled = true;
            txtCity.Enabled = true;
            txtPostalCode.Enabled = true;
            txtPhone.Enabled = true;
            cboCountry.Enabled = true;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            isNewCustomer = true;

            // get countries
            getCountry();
            cboCountry.DataSource = dtCountry;
            cboCountry.DisplayMember = "country";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveCustomer();

            if (!isError)
            {
                // reset form and buttons
                resetForm();
            }
            else
            {
                txtName.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // verify a row has been selected
            if (selectedCustomerID > 0)
            {
                // alert the user
                SystemSounds.Beep.Play();
                var result = MessageBox.Show("Are you sure you want to delete this customer?", "Delete?"
                , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // delete the customer from the database
                    deleteCustomer();
                    getCustomers("SELECT * FROM customer");
                    dgv.DataSource = dtCustomer;
                    dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    formatDGV(dgv);

                    resetForm();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer for deletion");
            }

        }

        public void deleteCustomer()
        {
            // delete customer and all corresponding appointments
            string sqlString = "DELETE FROM appointment WHERE customerId = " + selectedCustomerID;
            MySqlConnection con = new MySqlConnection("server=wgudb.ucertify.com;user id=U08JKI;password=53689304986;persistsecurityinfo=True;database=U08JKI");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlString, con);
            cmd.ExecuteNonQuery();
            sqlString = "DELETE FROM customer WHERE customerId = " +
                selectedCustomerID;
            cmd = new MySqlCommand(sqlString, con);
            cmd.ExecuteNonQuery();
            con.Close();

            MainScreen newMainScreen = new MainScreen(currentUserID, currentUserName);
            newMainScreen.Show();
            lastMainScreen.Hide();
            this.Focus();
        }
    }
}
