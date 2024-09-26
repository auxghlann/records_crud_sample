using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
//using System.Data.OleDb;
using System.Data.Odbc;
namespace records_crud_sample
{
    internal class Database
    {


        //private OleDbConnection connection;
        //private const string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/User/khest/Documents/C#app_dev/records_crud_sample/db/records.mdb";
        private const string conn = "Dsn=mdb_records_db";

        //public OleDbConnection Connection { get { return new OleDbConnection(conn); } }
        public OdbcConnection Connection { get { return new OdbcConnection(conn); } }


    }
}
