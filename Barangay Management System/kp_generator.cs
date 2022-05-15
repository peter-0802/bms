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
    public partial class kp_generator : Form
    {
        DBConn DB = new DBConn();
        public kp_generator()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        public void showkp()
        {
            if(comboBox1.SelectedItem.ToString() == "KP Form 1")
            {
                kp_form_1 kp = new kp_form_1();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 2")
            {
                kp_form_2 kp = new kp_form_2();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 3")
            {
                kp_form_3 kp = new kp_form_3();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 4")
            {
                kp_form_4 kp = new kp_form_4();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 5")
            {
                kp_form_5 kp = new kp_form_5();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 6")
            {
                kp_form_6a kp = new kp_form_6a();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 7")
            {
                kp_form_7 kp = new kp_form_7();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 8")
            {
                kp_form_8 kp = new kp_form_8();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 9")
            {

                kp_form_9 kp = new kp_form_9();
                this.crystalReportViewer1.ShowPageNavigateButtons = true;
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 10")
            {

                kp_form_10 kp = new kp_form_10();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 11")
            {

                kp_form_11 kp = new kp_form_11();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 12")
            {

                kp_form_12 kp = new kp_form_12();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 13")
            {

                kp_form_13 kp = new kp_form_13();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 14")
            {

                kp_form_14 kp = new kp_form_14();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 15")
            {

                kp_form_15 kp = new kp_form_15();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 16")
            {

                kp_form_16 kp = new kp_form_16();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 17")
            {

                kp_form_17 kp = new kp_form_17();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 18")
            {

                kp_form_18 kp = new kp_form_18();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 19")
            {

                kp_form_19 kp = new kp_form_19();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 20")
            {

                kp_form_20 kp = new kp_form_20();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 20-A")
            {

                kp_form_20a kp = new kp_form_20a();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 20-B")
            {

                kp_form_20a kp = new kp_form_20a();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 21")
            {

                kp_form_21 kp = new kp_form_21();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 22")
            {

                kp_form_22 kp = new kp_form_22();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 23")
            {

                kp_form_23 kp = new kp_form_23();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 24")
            {

                kp_form_24 kp = new kp_form_24();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else if (comboBox1.SelectedItem.ToString() == "KP Form 25")
            {

                kp_form_25 kp = new kp_form_25();
                this.crystalReportViewer1.ReportSource = kp;
            }
            else
            {
                MessageBox.Show("No Data");
            }
                
        }
        private void kp_generator_Load(object sender, EventArgs e)
        {
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            showkp();
        }
    }
    }


