using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Barangay_Management_System
{
    public partial class tester : Form
    {
        public tester()
        {
            InitializeComponent();
        }
       
        private void Settings_Load(object sender, EventArgs e)
        {

        }

        // Save setings on registry
        private void savesettingsonreg()
        {
            RegistryKey apt = Registry.CurrentUser.CreateSubKey(@"APT\BMS\DECLARATIONS");
            apt.SetValue("Datasource", textBox1.Text);
            apt.SetValue("Database", textBox2.Text);
            apt.SetValue("Port", textBox3.Text);
            apt.SetValue("Username", textBox4.Text);
            apt.SetValue("Password", textBox5.Text);
            apt.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            savesettingsonreg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboard sale = new dashboard();
            sale.MdiParent = this;
            sale.Show();
        }
    }
}
