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
    public partial class id_generator : Form
    {
        DBConn DB = new DBConn();
        public id_generator()
        {
            InitializeComponent();
            DB.getDataSource();
            loadContent();
        }
        
        public void loadContent()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"SELECT code `CODE`,
                                    lastname `LAST NAME`,
                                    firstname `FIRST NAME`,
                                    middlename `MIDDLE NAME`,
                                    TIMESTAMPDIFF(YEAR, dob, CURDATE()) `AGE`
                                    FROM residents
                                    where TIMESTAMPDIFF(YEAR, dob, CURDATE()) >= 18
                                    and 
                                    (code like @code
                                    or lastname like @lastname
                                    or firstname like @firstname
                                    or middlename like @mi)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@code", "%"+this.textBox1.Text+"%");
                    cmd.Parameters.AddWithValue("@lastname", "%" + this.textBox1.Text + "%");
                    cmd.Parameters.AddWithValue("@firstname", "%" + this.textBox1.Text + "%");
                    cmd.Parameters.AddWithValue("@mi", "%" + this.textBox1.Text + "%");
                    //cmd.Parameters.AddWithValue("@lastname", this.textBox1.Text);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = dataTable;
                    dataGridView1.DataSource = bindingSource;
                    //hides the id coloumn of the dgv before populating
                    //this.dataGridView1.Columns[0].Visible = false;

                    //Add a CheckBox Column to the DataGridView at the first position.
                    DataGridViewButtonColumn checkBoxColumn = new DataGridViewButtonColumn();
                    checkBoxColumn.UseColumnTextForButtonValue = true;
                    checkBoxColumn.HeaderText = "ACTION";
                    checkBoxColumn.Width = 30;
                    checkBoxColumn.Name = "bntcolumn";
                    checkBoxColumn.Text = "Generate ID";
                    dataGridView1.Columns.Insert(5, checkBoxColumn);

                    adapter.Update(dataTable);
                    try
                    {
                        this.dataGridView1.Rows[0].Cells[0].Selected = false;
                    }
                    catch (Exception)
                    {

                        return;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadContent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadContent();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (Int16.Parse(dataGridView1.SelectedRows[0].Cells["AGE"].Value.ToString()) < 18)
                    {
                        MessageBox.Show("Resident must be 18 and above to have an ID");
                    }
                    else
                    {
                        string selectedValue = dataGridView1.SelectedRows[0].Cells["CODE"].Value.ToString();
                        enter_idno a = new enter_idno();
                        a.label4.Text = selectedValue;
                        a.ShowDialog();
                    }
                }
            }
        }

        private void id_generator_Load(object sender, EventArgs e)
        {

        }
    }
}
