using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace records_crud_sample
{
    public partial class frmUpdate : Form
    {
        private Database _db;
        private int ID;
        private string fName, lName, email, gender;

        public frmUpdate(int ID, string fName, string lName, string email, string gender, Database db)
        {
            InitializeComponent();
            this._db = db;
            this.ID = ID;
            this.fName = fName;
            this.lName = lName;
            this.email = email;
            this.gender = gender;
        }


 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "UPDATE records SET first_name=?, last_name=?, email=?, gender=? " +
                            "WHERE id=?";


            if (_db.Connection.State != ConnectionState.Open)
            {
                _db.Connection.Open();
            }
            using (OdbcCommand command = new OdbcCommand(query, _db.Connection))
            {
                command.Parameters.AddWithValue("?", txtFname.Text);
                command.Parameters.AddWithValue("?", txtLname.Text);
                command.Parameters.AddWithValue("?", txtEmail.Text);
                command.Parameters.AddWithValue("?", txtGender.Text);
                command.Parameters.AddWithValue("?", this.ID);

                int res = command.ExecuteNonQuery();
                if (res != 0)
                {
                    DialogResult dgRes = MessageBox.Show("Record Updated Successfully. Please refresh your datagrid", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dgRes.Equals(DialogResult.OK))
                    {
                        _db.Connection.Close();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Failed to update the selected Record", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                _db.Connection.Close();
            }
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            txtFname.Text = fName;
            txtLname.Text = lName;
            txtEmail.Text = email;
            txtGender.Text = gender;

        }

    }
}
