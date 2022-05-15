using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barangay_Management_System
{
    public partial class repfilter : Form
    {
        
        DBConn DB = new DBConn();
        public repfilter()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            report_new a = new report_new();
            a.refno = this.textBox1.Text;
            a.name = this.txtlastname.Text;
            a.pob = this.txtpob.Text;
            a.sex = this.cmbsex.Text;
            a.civil = this.cmbcivilstatus.Text;
            a.purok = this.cmbpurok.Text;
            a.occupation = this.comboBox1.Text;
            a.ips = this.cmbips.Text;
            a.voter = this.cmbvoter.Text;
            a.sector = this.cmbsector.Text;
            a.student = this.cmbstudent.Text;
            a.dobf = this.dobf.Text;
            a.dobt = this.dobt.Text;
            a.note = this.richTextBox1.Text;
            a.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dobf.Value = this.dobf.MinDate;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dobf.Value = this.dobf.MaxDate;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dobt.Value = this.dobt.MinDate;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.dobt.Value = this.dobt.MaxDate;
        }
    }
    }


