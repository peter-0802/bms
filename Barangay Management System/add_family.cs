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
    public partial class add_family : Form
    {
        DBConn DB = new DBConn();
        public add_family()
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
                                    middlename `MIDDLE NAME` 
                                    FROM residents
                                    where (code like @code
                                    or lastname like @lastname
                                    or firstname like @firstname
                                    or middlename like @mi)
                                    and infamily = 0";
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
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    checkBoxColumn.HeaderText = "SELECT";
                    checkBoxColumn.Width = 30;
                    checkBoxColumn.Name = "checkBoxColumn";
                    dataGridView1.Columns.Insert(0, checkBoxColumn);

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

        public void loadmembers()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"SELECT
                                    residents.code `CODE`,
                                    lastname `LAST NAME`,
                                    firstname `FIRST NAME`,
                                    middlename `MIDDLE NAME`
                                    FROM family_members

                                    inner join residents on residents.id = family_members.resident_id
                                    where family_id = (select id from families where code = @code)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@code", this.transcode);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = dataTable;
                    dataGridView2.DataSource = bindingSource;

                    //Add a CheckBox Column to the DataGridView at the first position.
                    //DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    //checkBoxColumn.HeaderText = "SELECT";
                    //checkBoxColumn.Width = 30;
                    //checkBoxColumn.Name = "checkBoxColumn";
                    //dataGridView2.Columns.Insert(0, checkBoxColumn);

                    adapter.Update(dataTable);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                string _query1 = @"insert into families (code) values (@code)";
                string query1 = string.Format(_query1);
                myCommand.Parameters.AddWithValue("@code", textBox2.Text);
                myCommand.CommandText = query1;
                myCommand.ExecuteNonQuery();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    myCommand = conn.CreateCommand();
                    if (row.IsNewRow) continue;
                    myCommand.Parameters.AddWithValue("@code", row.Cells[0].Value);
                    string _query2 = @"insert into family_members
                                       (family_id, resident_id)
                                       values
                                       ((select id from families order by id desc limit 1),
                                        (select id from residents where code = @code));

                                        update residents set infamily = 1 where code = @code";
                    string query2 = string.Format(_query2);
                    myCommand.CommandText = query2;
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();
                MessageBox.Show("Household Added");
                dataGridView1.Columns.RemoveAt(0);
                loadContent();
                loadmembers();
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

        private void update()
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
                string upfcode = @"update families set code = @ncode where code = @f2code";
                string upfcode1 = string.Format(upfcode);
                myCommand.Parameters.AddWithValue("@ncode", this.textBox2.Text);
                myCommand.Parameters.AddWithValue("@f2code", this.transcode);
                myCommand.CommandText = upfcode1;
                myCommand.ExecuteNonQuery();

                string _upto0 = @"update
                                    family_members
                                      left join families on families.id = family_members.family_id
                                      left join residents on residents.id = family_members.resident_id
                                    set infamily = 0 where families.code = @fcode";
                string upto0 = string.Format(_upto0);
                myCommand.Parameters.AddWithValue("@fcode", this.textBox2.Text);
                myCommand.CommandText = upto0;
                myCommand.ExecuteNonQuery();

                string _query1 = @"delete from family_members where family_id = (select id from families where code = @fcode)";
                string query1 = string.Format(_query1);
                myCommand.CommandText = query1;
                myCommand.ExecuteNonQuery();



                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    myCommand = conn.CreateCommand();
                    if (row.IsNewRow) continue;
                    myCommand.Parameters.AddWithValue("@fcode", this.textBox2.Text);
                    myCommand.Parameters.AddWithValue("@code", row.Cells[0].Value);
                    string _query2 = @"insert into family_members
                                       (family_id, resident_id)
                                       values
                                       ((select id from families where code = @fcode),
                                        (select id from residents where code = @code));

                                        update residents set infamily = 1 where code = @code";
                    string query2 = string.Format(_query2);
                    myCommand.CommandText = query2;
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();
                MessageBox.Show("Family Updated");
                transcode = textBox2.Text;
                button2.Text = "Update " + transcode;
                dataGridView1.Columns.RemoveAt(0);
                if (dataGridView2.Rows.Count == 0)
                {
                    delet_empty_fam();
                    this.Close();
                }
                else
                {
                    loadContent();
                    loadmembers();
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;

            DataTable dt = new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("LAST NAME");
            dt.Columns.Add("FIRST NAME");
            dt.Columns.Add("MIDDLE NAME");

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                if (isSelected)
                {
                    dt.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value);
                }
            }
            dataGridView2.DataSource = dt;
        }

        private void add_family_Load(object sender, EventArgs e)
        {

        }

        private void delet_empty_fam()
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
                string _del = @"delete from families where code = @fcode";
                string del = string.Format(_del);
                myCommand.Parameters.AddWithValue("@fcode", this.transcode);
                myCommand.CommandText = del;
                myCommand.ExecuteNonQuery();
                
                myTrans.Commit();
                MessageBox.Show("Family has been deleted because it has no members");

                dataGridView1.Columns.RemoveAt(0);
                loadContent();

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                MessageBox.Show("Oops, familiy code field is empty.");
            }
            else
            {
                if (button2.Text != "Save")
                {
                    update();
                }
                else
                {
                    insert();
                    this.Close();
                }
            }
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dataGridView2.SelectedCells)
            {
                if (oneCell.Selected)
                    dataGridView2.Rows.RemoveAt(oneCell.RowIndex);
            }
        }

        private void add_family_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void add_family_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadContent();
        }
        public string transcode = "~code~";
        private void add_family_Load_1(object sender, EventArgs e)
        {
            if (transcode == "~code~")
            {
                button2.Text = "Save";
            }
            else
            {
                button2.Text = "Update " + transcode;
            }
            loadmembers();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
