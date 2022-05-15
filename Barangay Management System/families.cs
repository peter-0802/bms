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

namespace Barangay_Management_System
{
    public partial class families : Form
    {
        DBConn DB = new DBConn();
        public families()
        {
            InitializeComponent();
            DB.getDataSource();
            loadContent();
        }

        public void loadContent()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"select
                                    code `CODE`,
                                    count(resident_id) `COUNT`
                                    from family_members
                                      inner join families on families.id = family_members.family_id
                                    group by family_id";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = dataTable;
                    dataGridView1.DataSource = bindingSource;
                    adapter.Update(dataTable);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_family a = new add_family();
            a.ShowDialog();
            loadContent();
        }

 

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            add_family a = new add_family();
            a.transcode = row.Cells[0].Value.ToString();
            a.textBox2.Text = row.Cells[0].Value.ToString();
            a.ShowDialog();
        }

        private void families_Load(object sender, EventArgs e)
        {
            loadContent();
        }

        

        private void families_Activated(object sender, EventArgs e)
        {
            loadContent();
        }
    }
}
