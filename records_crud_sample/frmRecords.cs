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



        // WinForm Functons

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string query = "select * from records";

            //OleDbDataAdapter adapter = new OleDbDataAdapter(query, _db.Connection);
            _db.Connection.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(query, _db.Connection);

            adapter.Fill(dt);
            
            grdData.DataSource = dt;

            _db.Connection.Close();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // Open connection here

            using (_db.Connection)
            {
                _db.Connection.Open();

                string query = "select * from records where (first_name like ? or last_name like ?)";

                OdbcCommand command = new OdbcCommand(query, _db.Connection);
                command.Parameters.AddWithValue("?", txtSearch.Text + "%");
                command.Parameters.AddWithValue("?", txtSearch.Text + "%");
                //lblQuery.Text = command.CommandText;

                
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(command)) // Use OdbcDataAdapter if Odbc driver
                {
                    adapter.Fill(dt);
                }
            }

            grdData.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAdd = new frmAdd(_db);
            frmAdd.ShowDialog();
        }
    }
}
