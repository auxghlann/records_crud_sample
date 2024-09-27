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

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO records VALUES (?, ?, ?, ?, ?)";

            _db = new Database();

            using (_db.Connection)
            {
                OdbcCommand command = new OdbcCommand(query, _db.Connection);
                command.Parameters.AddWithValue("?", 101);
                command.Parameters.AddWithValue("?", txtFname.Text);
                command.Parameters.AddWithValue("?", txtLname.Text);
                command.Parameters.AddWithValue("?", txtEmail.Text);
                command.Parameters.AddWithValue("?", txtGender.Text);



                if (_db.Connection.State != ConnectionState.Open)
                {
                    _db.Connection.Open();
                }


                int res = command.ExecuteNonQuery();
                if (res != 0)
                {
                    DialogResult dgRes = MessageBox.Show("Record Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dgRes.Equals(DialogResult.OK))
                    {
                        this.Close();
                    }
                    
                } else
                {
                    MessageBox.Show("Failed to add to the Database", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                _db.Connection.Close();
            }

            
        }
    }
}
