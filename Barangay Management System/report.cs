using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Barangay_Management_System
{
    public partial class report : Form
    {
        DBConn DB = new DBConn();
        public report()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        public string qry = @"SELECT * FROM v_residents where code = @ref";
        public void bindreport()
        {
            repfilter a = new repfilter();
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(DB.connstring))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(qry, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                cmd.Parameters.AddWithValue("@ref", a.textBox1.Text);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, ds.Tables["residents"].TableName);
                rpt_summary crt = new rpt_summary();
                crt.SetDataSource(ds);
                this.crystalReportViewer1.ReportSource = crt;
            }
        }
        private void report_Load(object sender, EventArgs e)
        {
            bindreport();
        }
    }
}
