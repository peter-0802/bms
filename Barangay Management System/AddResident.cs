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
    public partial class AddResident : Form
    {
        
        DBConn DB = new DBConn();
        string code = "";
        public AddResident()
        {
            InitializeComponent();
            DB.getDataSource();
            loadContent();
        }

        public class ExTextBox : TextBox
        {
            [DllImport("user32")]
            private static extern IntPtr GetWindowDC(IntPtr hwnd);
            private const int WM_NCPAINT = 0x85;
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_NCPAINT && this.Focused)
                {
                    var dc = GetWindowDC(Handle);
                    using (Graphics g = Graphics.FromHdc(dc))
                    {
                        g.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1);
                    }
                }
            }
        }

        public void loadContent()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"SELECT * FROM v_residents2
                                    where `CODE` like @code
                                    or `LAST NAME` like @lastname
                                    or `FIRST NAME` like @firstname
                                    or `M.I.` like @mi
                                    order by id desc";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@code", "%" + this.textBox2.Text + "%");
                    cmd.Parameters.AddWithValue("@lastname", "%" + this.textBox2.Text + "%");
                    cmd.Parameters.AddWithValue("@firstname", "%" + this.textBox2.Text + "%");
                    cmd.Parameters.AddWithValue("@mi", "%" + this.textBox2.Text + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = dataTable;
                    dataGridView1.DataSource = bindingSource;
                    //hides the id coloumn of the dgv before populating
                    this.dataGridView1.Columns[0].Visible = false;
                    adapter.Update(dataTable);
                    //this.dataGridView1.Rows[0].Cells[0].Selected = false;
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
            MySqlConnection myConnection = new MySqlConnection(DB.connstring);
            myConnection.Open();

            MySqlCommand myCommand = myConnection.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            //this is the code for adding image
            Image image = pictureBox1.Image;
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Png);
            byte[] imageBt = memoryStream.ToArray();    
                try
                {
                   

                    myCommand.CommandText = @"insert into residents
                                          (code, lastname, firstname, middlename, dob, pob, sex, civilstatus, purok, occupation, ips, voter, sector, contact, student, image)
                                          VALUES
                                          ((select if(count(id) <= 0, 'Res - 1', concat('Res - ', max(id) + 1)) code from residents as code), @lastname, @firstname, @middlename, @dob, @pob, @sex, @civilstatus, @purok, @occupation, @ips, @voter, @sector, @contact, @student, @image);";

                    //myCommand.Parameters.AddWithValue("@code", "");
                    myCommand.Parameters.AddWithValue("@lastname", txtlastname.Text);
                    myCommand.Parameters.AddWithValue("@firstname", txtfirstname.Text);
                    myCommand.Parameters.AddWithValue("@middlename", txtmiddlename.Text);
                    myCommand.Parameters.AddWithValue("@dob", dtpdob.Text);
                    myCommand.Parameters.AddWithValue("@pob", txtpob.Text);
                    myCommand.Parameters.AddWithValue("@sex", cmbsex.Text);
                    myCommand.Parameters.AddWithValue("@civilstatus", cmbcivilstatus.Text);
                    myCommand.Parameters.AddWithValue("@purok", cmbpurok.Text);
                    myCommand.Parameters.AddWithValue("@occupation", comboBox1.Text);
                    myCommand.Parameters.AddWithValue("@ips", cmbips.Text);
                    myCommand.Parameters.AddWithValue("@voter", cmbvoter.Text);
                    myCommand.Parameters.AddWithValue("@sector", cmbsector.Text);
                    myCommand.Parameters.AddWithValue("@contact", txtcontact.Text);
                    myCommand.Parameters.AddWithValue("@student", cmbstudent.Text);
                    myCommand.Parameters.AddWithValue("@image", imageBt);
                    myCommand.ExecuteNonQuery();
                    myTrans.Commit();
                    MessageBox.Show("Resident Added!");

                }
                catch (Exception e)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (MySqlException ex)
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
                    myConnection.Close();
                    imageBt = null;
                }
        }

        private void update()
        {
            MySqlConnection myConnection = new MySqlConnection(DB.connstring);
            myConnection.Open();

            MySqlCommand myCommand = myConnection.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            //this is the code for adding image
            Image image = pictureBox1.Image;
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Png);
            byte[] imageBt = memoryStream.ToArray();

            try
            {
                myCommand.CommandText = @"update residents set code = @newcode, lastname = @lastname, firstname = @firstname, middlename = @middlename,
                                          dob = @dob, pob = @pob, sex = @sex, civilstatus = @civilstatus, purok = @purok,
                                          occupation = @occupation, ips = @ips, voter = @voter, sector = @sector, contact = @contact, student = @student, image = @image
                                          where code = @oldcode;";
                myCommand.Parameters.AddWithValue("@newcode", textBox1.Text);
                myCommand.Parameters.AddWithValue("@lastname", txtlastname.Text);
                myCommand.Parameters.AddWithValue("@firstname", txtfirstname.Text);
                myCommand.Parameters.AddWithValue("@middlename", txtmiddlename.Text);
                myCommand.Parameters.AddWithValue("@dob", dtpdob.Text);
                myCommand.Parameters.AddWithValue("@pob", txtpob.Text);
                myCommand.Parameters.AddWithValue("@sex", cmbsex.Text);
                myCommand.Parameters.AddWithValue("@civilstatus", cmbcivilstatus.Text);
                myCommand.Parameters.AddWithValue("@purok", cmbpurok.Text);
                myCommand.Parameters.AddWithValue("@occupation", comboBox1.Text);
                myCommand.Parameters.AddWithValue("@ips", cmbips.Text);
                myCommand.Parameters.AddWithValue("@voter", cmbvoter.Text);
                myCommand.Parameters.AddWithValue("@sector", cmbsector.Text);
                myCommand.Parameters.AddWithValue("@contact", txtcontact.Text);
                myCommand.Parameters.AddWithValue("@student", cmbstudent.Text);
                myCommand.Parameters.AddWithValue("@image", imageBt);
                myCommand.Parameters.AddWithValue("@oldcode", code);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                MessageBox.Show("Resident Updated!");
                btnsave.Text = "Save";
                code = "";
                pictureBox1.Image = Properties.Resources._215067775_2991629604449900_6307280939636060415_n;
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
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
                myConnection.Close();
                imageBt = null;
            }
        }
        private void delete()
        {
            MySqlConnection myConnection = new MySqlConnection(DB.connstring);
            myConnection.Open();

            MySqlCommand myCommand = myConnection.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = @"delete from residents where code = @code";
                myCommand.Parameters.AddWithValue("@code", selected_code);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                MessageBox.Show("Resident Deleted!");
                loadContent();
                selected_code = "";
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
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
                myConnection.Close();
            }
        }
        private void clearall()
        {
            foreach (Control txtbox in panel1.Controls)
            {
                if (txtbox is TextBox)
                {
                    ((TextBox)txtbox).Text = string.Empty;
                }
                else if (txtbox is RichTextBox)
                {
                    ((RichTextBox)txtbox).Text = string.Empty;
                }
                else if (txtbox is ComboBox)
                {
                    ((ComboBox)txtbox).SelectedIndex = -1;
                }
                comboBox1.Text = "";
                textBox1.Text = "~autogen~";
            }
            pictureBox1.Image = Properties.Resources._215067775_2991629604449900_6307280939636060415_n;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //checks all fields if they are empty 
            if (panel1.Controls.OfType<TextBox>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                panel1.Controls.OfType<RichTextBox>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                panel1.Controls.OfType<ComboBox>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                panel1.Controls.OfType<DateTimePicker>().Any(t => string.IsNullOrEmpty(t.Text)) ||
                pictureBox1.Image == null)
            {
                MessageBox.Show(this, "Oops, looks like some fields are empty", "Wait!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //change what mode the user is in btnsave.Text == "Update"
                if (btnsave.Text.Contains("Update"))
                {
                    //updates the record
                    update();
                }
                else
                {
                    //inserts the data
                    insert();
                }
                //reloads the datagridview
                loadContent();
                //clears out all fields if insert is successfull
                clearall();
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //btnsave.Text = "Update";
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                code = "";
                code = row.Cells[1].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                txtlastname.Text = row.Cells[2].Value.ToString();
                txtfirstname.Text = row.Cells[3].Value.ToString();
                txtmiddlename.Text = row.Cells[4].Value.ToString();
                dtpdob.Text = row.Cells[5].Value.ToString();
                txtpob.Text = row.Cells[6].Value.ToString();
                cmbsex.Text = row.Cells[7].Value.ToString();
                cmbcivilstatus.Text = row.Cells[8].Value.ToString();
                cmbpurok.Text = row.Cells[9].Value.ToString();

                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox1.Text = row.Cells[10].Value.ToString();
                if (this.comboBox1.Text == "Unemployed")
                {
                    comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                }
                else
                {
                    comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                }
                cmbips.Text = row.Cells[11].Value.ToString();
                cmbvoter.Text = row.Cells[12].Value.ToString();
                cmbsector.Text = row.Cells[13].Value.ToString();
                cmbstudent.Text = row.Cells[14].Value.ToString();
                txtcontact.Text = row.Cells[15].Value.ToString();

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                    {
                        string qry = @"SELECT image FROM residents where code = @code";
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(qry, conn);
                        cmd.Parameters.AddWithValue("@code", textBox1.Text);
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        try
                        {
                            byte[] img = (byte[])dataTable.Rows[0][0];
                            MemoryStream ms = new MemoryStream(img);
                            pictureBox1.Image = Image.FromStream(ms);
                            adapter.Dispose();
                            img = null;
                            conn.Close();
                        }
                        catch
                        {
                            return;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()); 
                }
            }
            btnsave.Text = "Update " + code;
        }


        private void txtlastname_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnsave.Text.Contains("Update"))
            {
                //set dialogbox to ok and cancel
                DialogResult dialogResult = MessageBox.Show(this, "You are about to exit update mode, this will discard all changes made", "Wait!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    clearall();
                    btnsave.Text = "Save";
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        // used as the selected code holder when mouse hovers to any datagridview cell
        public string selected_code = "";
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Delete resident code \"{0}\"", selected_code), EventHandler_Click));
                }

                m.Show(dataGridView1, new Point(e.X, e.Y));

            }
        }
        //Generated event handler to handle delete item on context menu
        private void EventHandler_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            // function for getting the mouse hovered cell value
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selected_code = dataGridView1[1, e.RowIndex].Value.ToString();
                }
            }
            catch (NullReferenceException err)
            {
                return;
            }
        }

        private void AddResident_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._215067775_2991629604449900_6307280939636060415_n;
            textBox1.Enabled = false;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image files | *.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadContent();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox1.SelectedIndex == 0)
            {
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                comboBox1.SelectedIndex = -1;
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "~autogen~")
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                
            }
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
    }


