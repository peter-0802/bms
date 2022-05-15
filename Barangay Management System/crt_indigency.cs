using Microsoft.Win32;
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
    public partial class crt_residency : Form
    {
        DBConn DB = new DBConn();
        public crt_residency()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        //Global Variables here ===============================================
        string purok = "purok";
        string sex = "sex";
        //Global Variables here ===============================================
        private void brgy_certification_Load(object sender, EventArgs e)
        {
            //brgy_cert crt = new brgy_cert();
            //this.crystalReportViewer1.ReportSource = crt;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(name1.Text) || String.IsNullOrWhiteSpace(namex.Text) || String.IsNullOrWhiteSpace(date.Text) || String.IsNullOrWhiteSpace(namex.Text) || String.IsNullOrWhiteSpace(purpose.Text) || String.IsNullOrWhiteSpace(content.Text) || String.IsNullOrWhiteSpace(sexx.Text))
            {
                MessageBox.Show(this, "Oops, looks like some fields are empty", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cert_ind crt = new cert_ind();
                crt.DataDefinition.FormulaFields["name"].Text = " '" + name1.Text + "'";
                crt.DataDefinition.FormulaFields["date"].Text = " '" + date.Text + "'";
                crt.DataDefinition.FormulaFields["issuedto"].Text = " '" + namex.Text + "'";
                crt.DataDefinition.FormulaFields["purpose"].Text = " '" + purpose.Text + "'";
                crt.DataDefinition.FormulaFields["content"].Text = " '" + content.Text + "'";
                crt.DataDefinition.FormulaFields["sex"].Text = " '" + sexx.Text + "'";
                this.crystalReportViewer1.ReportSource = crt;
            }
        }

        private void content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.Handled = true;
        }
    }
    }
    

