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
using System.Windows.Forms.DataVisualization.Charting;

namespace Barangay_Management_System
{
    public partial class dashboard : Form
    {
        DBConn DB = new DBConn();
        public dashboard()
        {
            DB.getDataSource();
            InitializeComponent();
            loadContent();
            StartTimer();
        }
        System.Windows.Forms.Timer tmr = null;
        private void StartTimer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            label17.Text = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
        }

        public void loadContent()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"SELECT * FROM v_dashboard";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        label13.Text = reader.GetString("tcount");

                        chart1.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tmale")));
                        chart1.Series["Total Population"].Points.AddXY("Femele", int.Parse(reader.GetString("tfmale")));
                        

                        label60.Text = reader.GetString("tips");

                        chart2.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tmips")));
                        chart2.Series["Total Population"].Points.AddXY("Femele", int.Parse(reader.GetString("tfips")));

                        label62.Text = reader.GetString("tnips");

                        chart3.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tmnips")));
                        chart3.Series["Total Population"].Points.AddXY("Femele", int.Parse(reader.GetString("tfnips")));

                        label18.Text = reader.GetString("tpwd");
                        chart5.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tpwdm")));
                        chart5.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("tpwdf")));

                        label20.Text = reader.GetString("tsc");
                        chart6.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tscm")));
                        chart6.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("tscf")));

                        label25.Text = reader.GetString("tsp");
                        chart8.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tspm")));
                        chart8.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("tspf")));

                        label23.Text = reader.GetString("osy");
                        chart7.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("osym")));
                        chart7.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("osyf")));

                        label29.Text = reader.GetString("unemp");
                        chart4.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("unempm")));
                        chart4.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("unempf")));

                        label31.Text = reader.GetString("y");
                        chart9.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("ym")));
                        chart9.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("yf")));

                        label27.Text = reader.GetString("tw");

                        label33.Text = reader.GetString("fam");

                        label3.Text = reader.GetString("tminor");
                        chatminor.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tmminor")));
                        chatminor.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("tfminor")));

                        label1.Text = reader.GetString("tass");
                        chartassembly.Series["Total Population"].Points.AddXY("Male", int.Parse(reader.GetString("tmass")));
                        chartassembly.Series["Total Population"].Points.AddXY("Female", int.Parse(reader.GetString("tfass")));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addResidentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddResident a = new AddResident();
            a.ShowDialog();
            this.Activate();
        }

        private void FamilyHouseholdProfileform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void addFamilyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            families a = new families();
            a.ShowDialog();
            this.Activate();
        }

        private void viewReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            families a = new families();
            a.ShowDialog();
        }

        private void generateListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repfilter a = new repfilter();
            a.ShowDialog();
            this.Activate();
        }

        private void generateFamilyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fam_report a = new fam_report();
            a.ShowDialog();
            this.Activate();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            menuStrip1.ForeColor = ColorTranslator.FromHtml("#ffffff");
        }

        private void generateIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id_generator a = new id_generator();
            a.ShowDialog();
            this.Activate();
        }
        

        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart3.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart4.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart5.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart6.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart7.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart8.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart9.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chatminor.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartassembly.Series)
            {
                series.Points.Clear();
            }
            loadContent();
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Dispose();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void brgyCertificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brgy_certification a = new brgy_certification();
            a.ShowDialog();
        }

        private void certificateOfIndigencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crt_indigency a = new crt_indigency();
            a.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            brgy_certification a = new brgy_certification();
            a.ShowDialog();
        }
        

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart3.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart4.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart5.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart6.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart7.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart8.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart9.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chatminor.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartassembly.Series)
            {
                series.Points.Clear();
            }
            loadContent();
        }

        private void kPFormsGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kp_generator a = new kp_generator();
            a.ShowDialog();
        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            crt_indigency a = new crt_indigency();
            a.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            crt_residency a = new crt_residency();
            a.ShowDialog();
        }
    }
}
