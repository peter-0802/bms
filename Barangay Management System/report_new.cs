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
    public partial class report_new : Form
    {
        DBConn DB = new DBConn();
        public report_new()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        public string refno = "";
        public string name = "";
        public string pob = "";
        public string sex = "";
        public string civil = "";
        public string purok = "";
        public string occupation = "";
        public string ips = "";
        public string voter = "";
        public string sector = "";
        public string student = "";
        public string dobf = "";
        public string dobt = "";
        public string note = "";


        public string qry = @"select
                                `CODE`,
                                concat(`LAST NAME`, ', ', `FIRST NAME`, ' ', `M.I.`) `NAME`,
                                `POB`,
                                `SEX` SEX,
                                `CIVIL STATUS` `CIVIL`,
                                `PUROK`,
                                `OCCUPATION`,
                                IPS,
                                VOTER,
                                SECTOR,
                                STUDENT,
                                CONTACT,
                                DOB
                                from v_residents2

                                where
                                concat(`LAST NAME`, ', ', `FIRST NAME`, ' ', `M.I.`) like @name
                                and `CODE` like @code
                                and `POB` like @pob
                                and `SEX` like @sex
                                and `CIVIL STATUS` like @civil
                                and `PUROK` like @purok
                                and `OCCUPATION` like @occupation
                                and `IPS` like @ips
                                and `VOTER` like @voter
                                and `SECTOR` like @sector
                                and `STUDENT` like @student
                                and `DOB` between @dobf and @dobt
                                ";
        public void bindreport()
        {

            repfilter a = new repfilter();
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(DB.connstring))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(qry, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                cmd.Parameters.AddWithValue("@code", "%"+ refno +"%");

                

                cmd.Parameters.AddWithValue("@pob", "%" + pob + "%");
                cmd.Parameters.AddWithValue("@sex", "%" + sex + "%");
                cmd.Parameters.AddWithValue("@civil", "%" + civil + "%");
                cmd.Parameters.AddWithValue("@purok", "%" + purok);
                cmd.Parameters.AddWithValue("@occupation", "%" + occupation + "%");
                cmd.Parameters.AddWithValue("@ips", "%" + ips + "%");
                cmd.Parameters.AddWithValue("@voter", "%" + voter + "%");
                cmd.Parameters.AddWithValue("@sector", "%" + sector + "%");
                cmd.Parameters.AddWithValue("@student", "%" + student + "%");

                cmd.Parameters.AddWithValue("@dobf", dobf);
                cmd.Parameters.AddWithValue("@dobt", dobt);

                adapter.SelectCommand = cmd;
                adapter.Fill(ds, ds.Tables["residents2"].TableName);
                rpt_summary_new crt = new rpt_summary_new();
                crt.SetDataSource(ds);
                //crt.DataDefinition.FormulaFields["notes"].Text = " TEST ";
                crt.DataDefinition.FormulaFields["notes"].Text = " '" + note + "'";
                this.crystalReportViewer1.ReportSource = crt;
            }
        }
        private void report_Load(object sender, EventArgs e)
        {
            bindreport();
        }
    }
}
