using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Barangay_Management_System
{
    class DBConn
    {
        public string Datasource;
        public string Port;
        public string Database;
        public string Username;
        public string Password;
        public string connstring;
#pragma warning disable CS0649 // Field 'DBConn.addminpass' is never assigned to, and will always have its default value null
        public string addminpass;
#pragma warning restore CS0649 // Field 'DBConn.addminpass' is never assigned to, and will always have its default value null
        public void getDataSource()
        {
            RegistryKey APT = null;
            try
            {
                APT = Registry.CurrentUser.OpenSubKey(@"APT\BMS\DECLARATIONS");
                if (APT != null)
                {
                    Datasource = APT.GetValue("Datasource").ToString();
                    Port = APT.GetValue("Port").ToString();
                    Database = APT.GetValue("Database").ToString();
                    Username = APT.GetValue("Username").ToString();
                    Password = APT.GetValue("Password").ToString();
                    string connect = "datasource = '" + Datasource + "'; port = '" + Port + "'; database = '" + Database + "'; username = '" + Username + "'; password = '" + Password + "'; SSL Mode = NONE; default command timeout=50";
                    connstring = connect;
                    APT.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (APT == null)
                {
                    MessageBox.Show("No Configuration");
                    Settings a = new Settings();
                    a.ShowDialog();
                    Login b = new Login();
                    for(int i = 0; i < Application.OpenForms.Count; ++i)
                    if (Application.OpenForms[i] == b)
                        Application.OpenForms[i].Close();
                    
                }
                else
                {
                    APT.Close();
                }
            }
        }
    }
}
