using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Deployment.Application;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace Barangay_Management_System
{
    public partial class Login : Form
    {
        DBConn DB = new DBConn();
        public Login()
        {
            InitializeComponent();
            DB.getDataSource();
        }
        private void login()
        {
            try
            {
                string query = @"select username, password
                                      from accounts
                                      where username = @username and password = @password";
                MySqlConnection conn = new MySqlConnection(DB.connstring);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", this.textBox1.Text);
                cmd.Parameters.AddWithValue("@password", this.textBox2.Text);
                MySqlDataReader myreader;
                conn.Open();
                myreader = cmd.ExecuteReader();
                int count = 0;
                while (myreader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    //Settings a = new Settings();
                    dashboard a = new dashboard();
                    this.Hide();
                    a.Show();
                    //a.label2.Text = this.textBox1.Text;
                }
                else
                {
                    MessageBox.Show("Incorrect Credentials");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Settings a = new Settings();
            a.ShowDialog(this);
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            string lisence = null;
            string barangay = null;
            string result;
            RegistryKey APT = null;
            try
            {
                APT = Registry.CurrentUser.OpenSubKey(@"APT\BMS\DECLARATIONS");
                if (APT != null)
                {
                    lisence = APT.GetValue("lisence").ToString();
                    barangay = APT.GetValue("barangay").ToString();
                    APT.Close();
                }


                using (MD5 hash = MD5.Create())
                {
                    result = String.Join
                    (
                        "",
                        from ba in hash.ComputeHash
                        (
                            Encoding.UTF8.GetBytes(barangay)
                        )
                        select ba.ToString("x2")
                    );

                    if (result != lisence)
                    {
                        label3.Visible = true;
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (APT != null)
                {
                    APT.Close();
                }
            }

            
        }
    }
}
