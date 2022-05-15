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
    public partial class brgy_certification : Form
    {
        DBConn DB = new DBConn();
        public brgy_certification()
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
        private void insert()
        {
            MySqlConnection conn = new MySqlConnection(DB.connstring);
            conn.Open();
            MySqlCommand myCommand = conn.CreateCommand();
            MySqlTransaction myTrans;
            myTrans = conn.BeginTransaction();
            myCommand.Connection = conn;
            myCommand.Transaction = myTrans;
            try
            {
                string _query1 = @"insert brg_certificate (res_id, ctc_no, cert_no, date, place) values (@res_id, @ctc_no, @cert_no, @date, @place)";
                string query1 = string.Format(_query1);
                myCommand.Parameters.AddWithValue("@res_id", reff.Text);
                myCommand.Parameters.AddWithValue("@ctc_no", ctc.Text);
                myCommand.Parameters.AddWithValue("@cert_no", crtno.Text);
                myCommand.Parameters.AddWithValue("@date", date.Text);
                myCommand.Parameters.AddWithValue("@place", place.Text);
                myCommand.CommandText = query1;
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                MessageBox.Show("Record Added, Certificate will now be generated");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (Exception ex)
                {
                    if (myTrans.Connection != null)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (this.Controls.OfType<TextBox>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                this.Controls.OfType<RichTextBox>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                this.Controls.OfType<ComboBox>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                this.Controls.OfType<DateTimePicker>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                this.Controls.OfType<Button>().Any(t => string.IsNullOrEmpty(t.Text)))
            {
                MessageBox.Show(this, "Oops, looks like some fields are empty", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                brgy_cert crt = new brgy_cert();
                crt.DataDefinition.FormulaFields["name"].Text = " '" + btnsave.Text + "'";
                crt.DataDefinition.FormulaFields["date"].Text = " '" + date.Text + "'";
                crt.DataDefinition.FormulaFields["place"].Text = " '" + place.Text + "'";
                crt.DataDefinition.FormulaFields["ctc"].Text = " '" + ctc.Text + "'";
                crt.DataDefinition.FormulaFields["crtno"].Text = " '" + crtno.Text + "'";
                crt.DataDefinition.FormulaFields["or"].Text = " '" + oorr.Text + "'";
                crt.DataDefinition.FormulaFields["purpose"].Text = " '" + purpose.Text + "'";


                crt.DataDefinition.FormulaFields["purok"].Text = " '" + purok + "'";
                crt.DataDefinition.FormulaFields["sex"].Text = " '" + sex + "'";
                this.crystalReportViewer1.ReportSource = crt;
                //this.crystalReportViewer1.ReportSource = crt;
            }


            //    try
            //{
            //    using (MySqlConnection conn = new MySqlConnection(DB.connstring))
            //    {
            //        string qry = @"select resident_id, id_no from generated_id where resident_id = (select id from residents where code = @code)";
            //        conn.Open();
            //        MySqlCommand cmd = new MySqlCommand(qry, conn);
            //        cmd.Parameters.AddWithValue("@code", label4.Text);
            //        MySqlDataReader reader = cmd.ExecuteReader();
            //        if (reader.Read())
            //        {
            //            btnsave.Text = "Re-print";
            //            textBox1.Text = reader.GetString("id_no");
            //        }
            //        else
            //        {
            //            return;
            //        }
            //        conn.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //insert();
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"SELECT concat(lastname, ', ', firstname, ' ', middlename) `name`, sex, purok FROM residents where code = @code";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@code", reff.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show(this, "Resident Found!", "BMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnsave.Text = reader.GetString("name");
                        purok = reader.GetString("purok");
                        sex = reader.GetString("sex");
                    }
                    else
                    {
                        MessageBox.Show(this, "No Resident Found!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnsave.Text = string.Empty;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    }
    

