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
                Timer MyTimer = new Timer();
                MyTimer.Interval = (10 * 60 * 1000); // 10 mins
                MyTimer.Tick += new EventHandler(MyTimer_Tick);
                MyTimer.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                DataSave();
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DataSave()
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


                DataTable dt3 = new DataTable();
                MySqlDataAdapter da3 = new MySqlDataAdapter("select * from wpfa_users", DatabaseConnectionForm.mysqlcon);
                da3.Fill(dt3);

                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    try
                    {
                        SqlCommand cmdchk3 = new SqlCommand("select * from wpfa_users Where ID ='" + Convert.ToInt64(dt3.Rows[i]["ID"]) + "' ", DatabaseConnectionForm.con);
                        DataTable dtchk3 = new DataTable();
                        SqlDataAdapter dachk3 = new SqlDataAdapter(cmdchk3);
                        dachk3.Fill(dtchk3);

                        if (dtchk3.Rows.Count == 0)
                        {
                            SqlCommand cmd3 = new SqlCommand("insert into wpfa_users (ID,user_login,user_pass,user_nicename,user_email,user_url,user_registered,user_activation_key,user_status,display_name) values (@ID,@user_login,@user_pass,@user_nicename,@user_email,@user_url,@user_registered,@user_activation_key,@user_status,@display_name)", DatabaseConnectionForm.con);
                            cmd3.Parameters.AddWithValue("@ID", Convert.ToInt64(dt3.Rows[i]["ID"]));
                            cmd3.Parameters.AddWithValue("@user_login", dt3.Rows[i]["user_login"]);
                            cmd3.Parameters.AddWithValue("@user_pass", dt3.Rows[i]["user_pass"]);
                            cmd3.Parameters.AddWithValue("@user_nicename", dt3.Rows[i]["user_nicename"]);
                            cmd3.Parameters.AddWithValue("@user_email", dt3.Rows[i]["user_email"]);
                            cmd3.Parameters.AddWithValue("@user_url", dt3.Rows[i]["user_url"]);
                            cmd3.Parameters.AddWithValue("@user_registered", Convert.ToDateTime(dt3.Rows[i]["user_registered"]));
                            cmd3.Parameters.AddWithValue("@user_activation_key", dt3.Rows[i]["user_activation_key"]);
                            cmd3.Parameters.AddWithValue("@user_status", Convert.ToInt32(dt3.Rows[i]["user_status"]));
                            cmd3.Parameters.AddWithValue("@display_name", dt3.Rows[i]["display_name"]);
                            cmd3.ExecuteNonQuery();
                        }
                        this.Text = "wpfa_users Table Data Checking & Updating....";
                    }

                    catch { }
                }

                DataTable dt4 = new DataTable();
                MySqlDataAdapter da4 = new MySqlDataAdapter("select * from wpfa_usermeta", DatabaseConnectionForm.mysqlcon);
                da4.Fill(dt4);

                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    try
                    {
                        SqlCommand cmdchk4 = new SqlCommand("select * from wpfa_usermeta Where umeta_id ='" + Convert.ToInt64(dt4.Rows[i]["umeta_id"]) + "' ", DatabaseConnectionForm.con);
                        DataTable dtchk4 = new DataTable();
                        SqlDataAdapter dachk4 = new SqlDataAdapter(cmdchk4);
                        dachk4.Fill(dtchk4);

                        if (dtchk4.Rows.Count == 0)
                        {
                            SqlCommand cmd4 = new SqlCommand("insert into wpfa_usermeta (umeta_id,user_id,meta_key,meta_value) values (@umeta_id,@user_id,@meta_key,@meta_value)", DatabaseConnectionForm.con);
                            cmd4.Parameters.AddWithValue("@umeta_id", Convert.ToInt64(dt4.Rows[i]["umeta_id"]));
                            cmd4.Parameters.AddWithValue("@user_id", Convert.ToInt64(dt4.Rows[i]["user_id"]));
                            cmd4.Parameters.AddWithValue("@meta_key", dt4.Rows[i]["meta_key"]);
                            cmd4.Parameters.AddWithValue("@meta_value", dt4.Rows[i]["meta_value"]);
                            cmd4.ExecuteNonQuery();
                        }
                        this.Text = "wpfa_usermeta Table Data Checking & Updating....";
                    }

                    catch { }
                }

                this.Text = "Server Data List";
                MessageBox.Show("Data updating successful", "Data Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnUploadData_Click(object sender, EventArgs e)
        {
           
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

                SqlCommand cmdcus = new SqlCommand("select * from wpfa_signups ", DatabaseConnectionForm.con);
                DataTable dtcus = new DataTable();
                SqlDataAdapter dacus = new SqlDataAdapter(cmdcus);
                dacus.Fill(dtcus);

                SqlCommand cmdc = new SqlCommand("select * from wpfa_users ", DatabaseConnectionForm.con);
                DataTable dtc = new DataTable();
                SqlDataAdapter dac = new SqlDataAdapter(cmdc);
                dac.Fill(dtc);

                SqlCommand cmdm = new SqlCommand("select * from wpfa_usermeta ", DatabaseConnectionForm.con);
                DataTable dtm = new DataTable();
                SqlDataAdapter dam = new SqlDataAdapter(cmdm);
                dam.Fill(dtm);

                lblOrder.Text = dt.Rows.Count.ToString();
                lblCust.Text = dtc.Rows.Count.ToString();
                lblUsermeta.Text = dtm.Rows.Count.ToString();
                dgvOrderList.DataSource = dt;
                dgvCustomerList.DataSource = dtcus;
                dgvUserList.DataSource = dtc;
                dgvMetauser.DataSource = dtm;
            }
            catch { }
        }
    }
}
