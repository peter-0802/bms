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
    public partial class fam_report : Form
    {
        DBConn DB = new DBConn();
        public fam_report()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        public string qry = @"select
                                families.code `FAMILY_CODE`,
                                residents.code `CODE`,
                                concat(residents.lastname, ', ', residents.firstname, ' ', residents.middlename) `NAME`
                                from residents
                                inner join family_members on family_members.resident_id = residents.id
                                inner join families on families.id = family_members.family_id
                                ";
        public void bindreport()
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(DB.connstring))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(qry, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, ds.Tables["family_report"].TableName);
                rpt_fam_report crt = new rpt_fam_report();
                crt.SetDataSource(ds);
                this.crystalReportViewer1.ReportSource = crt;
            }
        }
        private void fam_report_Load(object sender, EventArgs e)
        {
            bindreport();
        }
    }
}
