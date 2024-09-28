using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections;

namespace records_crud_sample
{
    public partial class frmRecords : Form
    {
        public frmRecords()
        {
            InitializeComponent();
        }

        // Initializion and Declaration
        Database _db = new Database();
        frmAdd frmAdd;
        frmUpdate frmUpdate;


        // Update value holder
        private int selectedID;
        private string  selectedFname, selectedLname, selectedEmail, selectedGender;

        // WinForm Functons

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string query = "select * from records";

            //OleDbDataAdapter adapter = new OleDbDataAdapter(query, _db.Connection);
            if (_db.Connection.State != ConnectionState.Open)
            {
                _db.Connection.Open();
            }
            using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, _db.Connection))
            {

                adapter.Fill(dt);

                _db.Connection.Close();

            }

            grdData.DataSource = dt;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // Open connection here

            if (_db.Connection.State != ConnectionState.Open)
            {
                _db.Connection.Open();
            }

            string query = "select * from records where (first_name like ? or last_name like ?)";

            using (OdbcCommand command = new OdbcCommand(query, _db.Connection))
            {

                command.Parameters.AddWithValue("?", txtSearch.Text + "%");
                command.Parameters.AddWithValue("?", txtSearch.Text + "%");
                //lblQuery.Text = command.CommandText;

                using (OdbcDataAdapter adapter = new OdbcDataAdapter(command)) // Use OdbcDataAdapter if Odbc driver
                {
                    adapter.Fill(dt);
                }

                _db.Connection.Close();
            }
 

            grdData.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdate = new frmUpdate(this.selectedID, this.selectedFname, this.selectedLname, this.selectedEmail, this.selectedGender, _db);
            frmUpdate.ShowDialog();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            DialogResult dg = MessageBox.Show("Are you sure you want to delete the selected record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (dg == DialogResult.Yes)
            {
                string query = "DELETE from records where id=?";


                if (_db.Connection.State != ConnectionState.Open)
                {
                    _db.Connection.Open();
                }
                using (OdbcCommand command = new OdbcCommand(query, _db.Connection))
                {
                    command.Parameters.AddWithValue("?", this.selectedID);

                    int res = command.ExecuteNonQuery();
                    if (res != 0)
                    {
                        DialogResult dgRes = MessageBox.Show("Record has been deleted successfully. Please refresh your datagrid", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (dgRes.Equals(DialogResult.OK))
                        {
                            _db.Connection.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the selected Record", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    _db.Connection.Close();
                }
            
            
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAdd = new frmAdd(_db);
            frmAdd.ShowDialog();
        }


        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count > 0) // Ensure there is a selected row
            {
                var selectedRow = grdData.SelectedRows[0].DataBoundItem as DataRowView;

                if (selectedRow != null)
                {
                    btnUpdate.Enabled = true;
                    btnDel.Enabled = true;
                    this.selectedID = Convert.ToInt32(selectedRow["id"]);
                    this.selectedFname = selectedRow["first_name"].ToString();
                    this.selectedLname = selectedRow["last_name"].ToString();
                    this.selectedEmail = selectedRow["email"].ToString();
                    this.selectedGender = selectedRow["gender"].ToString();
                }
            }
        }

        private void frmRecords_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDel.Enabled = false;
        }
    }
}
