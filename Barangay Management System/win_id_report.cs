using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barangay_Management_System
{
    public partial class win_id_report : Form
    {
        DBConn DB = new DBConn();
        public win_id_report()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        public string qry;
        public void bindreport()
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(DB.connstring))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(qry, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, ds.Tables["residents"].TableName);
                id_card crt = new id_card();
                crt.SetDataSource(ds);
                this.crystalReportViewer1.ReportSource = crt;
            }
        }
        private void win_id_report_Load(object sender, EventArgs e)
        {
            bindreport();
        }
    }
}
