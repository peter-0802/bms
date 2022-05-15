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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barangay_Management_System
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        string lisence = null;

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"APT\BMS\DECLARATIONS");
                exampleRegistryKey.SetValue("barangay", textBox3.Text);
                exampleRegistryKey.SetValue("Datasource", textBox1.Text);
                exampleRegistryKey.SetValue("Database", txtlastname.Text);
                exampleRegistryKey.SetValue("Port", txtfirstname.Text);
                exampleRegistryKey.SetValue("Username", txtmiddlename.Text);
                exampleRegistryKey.SetValue("Password", txtcontact.Text);
                exampleRegistryKey.SetValue("adminpass", "P@ss");
                exampleRegistryKey.SetValue("lisence", textBox2.Text);
                exampleRegistryKey.Close();
                MessageBox.Show("Settings saved! Application will restart and apply the settings");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Application.Restart();
            }
            
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            RegistryKey APT = null;
            try
            {
                APT = Registry.CurrentUser.OpenSubKey(@"APT\BMS\DECLARATIONS");
                if (APT != null)
                {
                    this.textBox3.Text = APT.GetValue("barangay").ToString();
                    this.textBox1.Text = APT.GetValue("Datasource").ToString();
                    txtfirstname.Text = APT.GetValue("Port").ToString();
                    txtlastname.Text = APT.GetValue("Database").ToString();
                    txtmiddlename.Text = APT.GetValue("Username").ToString();
                    txtcontact.Text = APT.GetValue("Password").ToString();
                    lisence = APT.GetValue("lisence").ToString();
                    textBox2.Text = APT.GetValue("lisence").ToString();
                    APT.Close();
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

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string result;
            using (MD5 hash = MD5.Create())
            {
                result = String.Join
                (
                    "",
                    from ba in hash.ComputeHash
                    (
                        Encoding.UTF8.GetBytes("managaaptbansalanaptdavaodelsur")
                    )
                    select ba.ToString("x2")
                );
                if (result == lisence)
                {
                    MessageBox.Show("Yes");
                }
                else
                {
                    MessageBox.Show("no");
                }
                
            }
        }
    }
    }


