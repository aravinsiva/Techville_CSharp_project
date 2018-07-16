using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMigration
{
    public partial class DatabaseConnectionForm : Form
    {
        public static SqlConnection con = null;
        public static MySqlConnection mysqlcon = null;
        public static string serverName = "";
        public static string databaseName = "";
        public static string dbUserName = "";
        public static string dbUserPass = "";

        public DatabaseConnectionForm()
        {
            InitializeComponent();
        }

        private void DatabaseConnectionForm_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                serverName = txtServerName.Text.Trim();
                databaseName = txtDatabaseName.Text.Trim();
                dbUserName = txtDbUserName.Text.Trim();
                dbUserPass = txtDbUserPass.Text.Trim();

                mysqlcon = new MySqlConnection("Data Source='" + serverName + "';Database='" + databaseName + "' ; Uid='" + dbUserName + "'; Pwd='" + dbUserPass + "'; ");
                mysqlcon.Open();

                MessageBox.Show("Online database connected successfully","Database Connection",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
