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

    public partial class enter_idno : Form
    {
        DBConn DB = new DBConn();
        public enter_idno()
        {
            InitializeComponent();
            DB.getDataSource();
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

            try
            {
                myCommand.CommandText = @"insert into generated_id
                                          (resident_id, id_no)
                                          VALUES
                                          ((select id from residents where code = @code), @lastname);";

                myCommand.Parameters.AddWithValue("@code", label4.Text);
                myCommand.Parameters.AddWithValue("@lastname", textBox1.Text);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();

                win_id_report a = new win_id_report();
                a.qry = "SELECT * FROM idquery where CODE = '" + this.label4.Text + "'";
                a.ShowDialog();
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

            try
            {
                myCommand.CommandText = @"update generated_id set id_no = @lastname where resident_id = (select id from residents where code = @code)";
                myCommand.Parameters.AddWithValue("@code", label4.Text);
                myCommand.Parameters.AddWithValue("@lastname", textBox1.Text);
                myCommand.ExecuteNonQuery();
                myTrans.Commit();

                win_id_report a = new win_id_report();
                a.qry = "SELECT * FROM idquery where CODE = '" + this.label4.Text + "'";
                a.ShowDialog();
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Oops, looks like ID no field is empty!");
            }
            else
            {
                try
                {
                    if (btnsave.Text != "Print")
                    {
                        update();
                    }
                    else
                    {
                        insert();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());

                }
            }
        }

        private void enter_idno_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"select resident_id, id_no from generated_id where resident_id = (select id from residents where code = @code)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@code", label4.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        btnsave.Text = "Re-print";
                        textBox1.Text = reader.GetString("id_no");
                    }
                    else
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

        private void enter_idno_Activated(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DB.connstring))
                {
                    string qry = @"select resident_id, id_no from generated_id where resident_id = (select id from residents where code = @code)";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@code", label4.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        btnsave.Text = "Re-print";
                        textBox1.Text = reader.GetString("id_no");
                    }
                    else
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
    }
    }


