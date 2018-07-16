using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMigration
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void databaseConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Program.dbcf = new DatabaseConnectionForm();
                Program.dbcf.MdiParent = this;
                Program.dbcf.Show();
            }
            catch { }
        }

        private void getOnlineDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Program.svrdata = new ServerDataForm();
                Program.svrdata.MdiParent = this;
                Program.svrdata.Show();
            }
            catch { }  
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to DataMigration software.\nTo save your online mysql database data to sql server 1st connect your mysql database with this project.\nTo connect mysql goto Settings >> database Connection,fill up all input box with your mysql connection info.\nThen click Connect button till show this message 'Online database connected successfully'.\nYour server doesn't respond all time so try more times to connect.\nAfter connection goto View >> Get Online Data There you will show two list of data that save from mysql to sql server.\nTo save data or update with new data just click Refresh button till show this message 'Data updating successful.'");
        }
    }
}
