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
    public partial class ServerDataForm : Form
    {
        public ServerDataForm()
        {
            InitializeComponent();
        }

        private void ServerDataForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseConnectionForm.mysqlcon = new MySqlConnection("Data Source='" + DatabaseConnectionForm.serverName + "'; Database='" + DatabaseConnectionForm.databaseName + "' ; Uid='" + DatabaseConnectionForm.dbUserName + "'; Pwd='" + DatabaseConnectionForm.dbUserPass + "'; ");
                DatabaseConnectionForm.mysqlcon.Open();

                DatabaseConnectionForm.con = new SqlConnection("Data Source='.'; Database='techvill_wp809_st1' ; Uid='sa'; Pwd='123'; ");
                DatabaseConnectionForm.con.Open();


                DataTable dt2 = new DataTable();
                MySqlDataAdapter da2 = new MySqlDataAdapter("select * from wpfa_signups", DatabaseConnectionForm.mysqlcon);
                da2.Fill(dt2);

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    try
                    {
                        SqlCommand cmdchk2 = new SqlCommand("select * from wpfa_signups Where signup_id ='" + Convert.ToInt64(dt2.Rows[i]["signup_id"]) + "' ", DatabaseConnectionForm.con);
                        DataTable dtchk2 = new DataTable();
                        SqlDataAdapter dachk2 = new SqlDataAdapter(cmdchk2);
                        dachk2.Fill(dtchk2);

                        if (dtchk2.Rows.Count == 0)
                        {
                            SqlCommand cmd2 = new SqlCommand("insert into wpfa_signups (signup_id,domain,path,title,user_login,user_email,registered,activated,active,activation_key,meta) values (@signup_id,@domain,@path,@title,@user_login,@user_email,@registered,@activated,@active,@activation_key,@meta)", DatabaseConnectionForm.con);
                            cmd2.Parameters.AddWithValue("@signup_id", Convert.ToInt64(dt2.Rows[i]["signup_id"]));
                            cmd2.Parameters.AddWithValue("@domain", dt2.Rows[i]["domain"]);
                            cmd2.Parameters.AddWithValue("@path", dt2.Rows[i]["path"]);
                            cmd2.Parameters.AddWithValue("@title", dt2.Rows[i]["title"]);
                            cmd2.Parameters.AddWithValue("@user_login", dt2.Rows[i]["user_login"]);
                            cmd2.Parameters.AddWithValue("@user_email", dt2.Rows[i]["user_email"]);
                            cmd2.Parameters.AddWithValue("@registered", Convert.ToDateTime(dt2.Rows[i]["registered"]));
                            cmd2.Parameters.AddWithValue("@activated", Convert.ToDateTime(dt2.Rows[i]["activated"]));
                            cmd2.Parameters.AddWithValue("@active", Convert.ToInt16(dt2.Rows[i]["active"]));
                            cmd2.Parameters.AddWithValue("@activation_key", dt2.Rows[i]["activation_key"]);
                            cmd2.Parameters.AddWithValue("@meta", dt2.Rows[i]["meta"]);
                            cmd2.ExecuteNonQuery();
                        }
                        this.Text = "wpfa_signups Table Data Checking & Updating....";
                    }

                    catch { }
                }

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("select * from wpfa_woocommerce_order_items", DatabaseConnectionForm.mysqlcon);
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        SqlCommand cmdchk = new SqlCommand("select * from wpfa_woocommerce_order_items Where order_item_id ='" + Convert.ToInt64(dt.Rows[i]["order_item_id"]) + "' ", DatabaseConnectionForm.con);
                        DataTable dtchk = new DataTable();
                        SqlDataAdapter dachk = new SqlDataAdapter(cmdchk);
                        dachk.Fill(dtchk);

                        if (dtchk.Rows.Count == 0)
                        {
                            SqlCommand cmd = new SqlCommand("insert into wpfa_woocommerce_order_items (order_item_id,order_item_name,order_item_type,order_id) values (@order_item_id,@order_item_name,@order_item_type,@order_id)", DatabaseConnectionForm.con);
                            cmd.Parameters.AddWithValue("@order_item_id", Convert.ToInt64(dt.Rows[i]["order_item_id"]));
                            cmd.Parameters.AddWithValue("@order_item_name", dt.Rows[i]["order_item_name"]);
                            cmd.Parameters.AddWithValue("@order_item_type", dt.Rows[i]["order_item_type"]);
                            cmd.Parameters.AddWithValue("@order_id", Convert.ToInt64(dt.Rows[i]["order_id"]));
                            cmd.ExecuteNonQuery();
                        }
                        this.Text = "wpfa_woocommerce_order_items Table Data Checking & Updating....";
                    }

                    catch { }
                }

                this.Text = "Server Data List";
                MessageBox.Show("Data updating successful","Data Update",MessageBoxButtons.OK,MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadData()
        {
            try
            {

                DatabaseConnectionForm.con = new SqlConnection("Data Source='.'; Database='techvill_wp809_st1' ; Uid='sa'; Pwd='123'; ");
                DatabaseConnectionForm.con.Open();

                SqlCommand cmd = new SqlCommand("select * from wpfa_woocommerce_order_items ", DatabaseConnectionForm.con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                SqlCommand cmdc = new SqlCommand("select * from wpfa_signups ", DatabaseConnectionForm.con);
                DataTable dtc = new DataTable();
                SqlDataAdapter dac = new SqlDataAdapter(cmdc);
                dac.Fill(dtc);

                lblOrder.Text = dt.Rows.Count.ToString();
                lblCust.Text = dtc.Rows.Count.ToString();
                dgvOrderList.DataSource = dt;
                dgvCustomerList.DataSource = dtc;
            }
            catch { }
        }
    }
}
