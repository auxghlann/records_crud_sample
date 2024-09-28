using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace records_crud_sample
{
    public partial class frmAdd : Form
    {
        private Database _db;

        public frmAdd(Database db)
        {
            InitializeComponent();
            this._db = db;
        }




        // Helper Functions

        private int GetLastValueInID(Database db)
        {
            if (db.Connection.State != ConnectionState.Open)
            {
                db.Connection.Open();
            }

            // Get the total number of records
            string query = "SELECT MAX(id) FROM records";
            int recordCount = -1;

            using (OdbcCommand countCommand = new OdbcCommand(query, db.Connection))
            {
                object result = countCommand.ExecuteScalar();
                if (result != null)
                {
                    recordCount = Convert.ToInt32(result);
                }
            }

            //db.Connection.Close();
            // Add 1 to the count
            return recordCount;

        }

        // winForm functios
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO records VALUES (?, ?, ?, ?, ?)";


            if (_db.Connection.State != ConnectionState.Open)
            {
                _db.Connection.Open();
            }
            using (OdbcCommand command = new OdbcCommand(query, _db.Connection))
            {
                command.Parameters.AddWithValue("?", GetLastValueInID(_db) + 1);
                command.Parameters.AddWithValue("?", txtFname.Text);
                command.Parameters.AddWithValue("?", txtLname.Text);
                command.Parameters.AddWithValue("?", txtEmail.Text);
                command.Parameters.AddWithValue("?", txtGender.Text);

                int res = command.ExecuteNonQuery();
                if (res != 0)
                {
                    DialogResult dgRes = MessageBox.Show("Record Added Successfully. Please refresh your datagrid", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dgRes.Equals(DialogResult.OK))
                    {
                        _db.Connection.Close();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Failed to add to the Database", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                _db.Connection.Close();
            }
            
        }
    }
}
