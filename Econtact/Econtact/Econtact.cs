using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting Data into DAtabase uing the method we created in previous episode
            bool success = c.Insert(c);
            if(success==true)
            {
                //Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                //Call the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to Add Contact
                MessageBox.Show("Failed to add New Contact. Try Again.");
            }
            //Load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //Load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Method to Clear Fields
        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the Data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            //Update DAta in Database
            bool success = c.Update(c);
            if(success==true)
            {
                //Updated Successfully
                MessageBox.Show("Contact has been successfully Updated.");
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update Contact.Try Again.");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the DAta From DAta Grid View and Load it to the textboxes respectively
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call Clear Method Here
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the Contact ID fromt eh Application
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if(success==true)
            {
                //Successfully Deleted
                MessageBox.Show("Contact successfully deleted.");
                //Refresh Data GridView
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //CAll the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to dElte
                MessageBox.Show("Failed to Delete Dontact. Try Again.");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get teh value from text box
            string keyword = txtboxSearch.Text;
            
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+"%' OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword+"%'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
