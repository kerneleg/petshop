using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dr.Mustafa_Clinic
{
    public partial class Ownersearch : Form
    {
        owner_form client;
        DBConnection objConnect = new DBConnection();
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        string conString;
        DataSet ds;
        DataTable sTable;


        public Ownersearch(string name)
        {
            InitializeComponent();
            if (name == "")
            {
                try
                {

                    conString = Properties.Settings.Default.Database1ConnectionString;
                    objConnect.connection_string = conString;
                    string sql = "SELECT Customerid,Name,Mob,Unpaid FROM Customers";
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    sCommand = new SqlCommand(sql, con);
                    sAdapter = new SqlDataAdapter(sCommand);
                    sBuilder = new SqlCommandBuilder(sAdapter);
                    ds = new DataSet();
                    sAdapter.Fill(ds, "Customers");
                    sTable = ds.Tables["Customers"];
                    con.Close();
                    OwnSearchDatagrid.DataSource = sTable;
                    OwnSearchDatagrid.ReadOnly = true;
                    OwnSearchDatagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    OwnSearchDatagrid.Columns[1].Visible = false;

                }
                catch (Exception err)
                {

                    MessageBox.Show(err.Message);

                }
            }
            else
            {
                conString = Properties.Settings.Default.Database1ConnectionString;
                objConnect.connection_string = conString;
                string sql = "SELECT Customerid,Name,Mob,Unpaid FROM Customers where Name LIKE @test ";
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(sql, con);
                sCommand.Parameters.AddWithValue("@test", name);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Customers");
                sTable = ds.Tables["Customers"];
                con.Close();
                OwnSearchDatagrid.DataSource = sTable;
                OwnSearchDatagrid.ReadOnly = true;
                OwnSearchDatagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                OwnSearchDatagrid.Columns[1].Visible = false;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < OwnSearchDatagrid.Rows.Count - 1)
            {
                client = new owner_form();
                client.owner_number(OwnSearchDatagrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                client.Show();
            }
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            try
            {
                conString = Properties.Settings.Default.Database1ConnectionString;
                objConnect.connection_string = conString;
                string CNSearch = owner_filter.Text.ToString();
                string MobSearch = mob_filter.Text.ToString();
                int Unpcombo = unpaid_combo.SelectedIndex;
                StringBuilder Sqlquery = new StringBuilder();
                if (CNSearch != "")
                {
                    if (MobSearch != "")
                    {
                        if (Unpcombo == -1 || Unpcombo == 0)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Name LIKE'" + CNSearch + "%' and Mob LIKE '" + MobSearch + "%' ");
                        }
                        if (Unpcombo == 1)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Name LIKE'" + CNSearch + "%' and Mob LIKE '" + MobSearch + "%' and Unpaid = 0 ");
                        }
                        if (Unpcombo == 2)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Name LIKE'" + CNSearch + "%' and Mob LIKE '" + MobSearch + "%' and Unpaid > 0 ");
                        }
                    }
                    if (MobSearch == "")
                    {
                        if (Unpcombo == -1 || Unpcombo == 0)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Name LIKE'" + CNSearch + "%' ");
                        }
                        if (Unpcombo == 1)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Name LIKE'" + CNSearch + "%' and Unpaid = 0 ");
                        }
                        if (Unpcombo == 2)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Name LIKE'" + CNSearch + "%' and Unpaid > 0 ");
                        }
                    }
                }

                if (CNSearch == "")
                {
                    if (MobSearch == "")
                    {

                        if (Unpcombo == -1 || Unpcombo == 0)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers");
                        }
                        if (Unpcombo == 1)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Unpaid = 0 ");
                        }
                        if (Unpcombo == 2)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Unpaid > 0 ");
                        }
                    }
                    else
                    {
                        if (Unpcombo == -1 || Unpcombo == 0)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Mob LIKE '" + MobSearch + "%' ");
                        }
                        if (Unpcombo == 1)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Mob LIKE '" + MobSearch + "%' and Unpaid = 0 ");
                        }
                        if (Unpcombo == 2)
                        {
                            Sqlquery.Append("SELECT Customerid,Name,Mob,Unpaid FROM Customers WHERE Mob LIKE '" + MobSearch + "%' and Unpaid > 0 ");
                        }
                    }
                }


                SqlConnection con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Customers");
                sTable = ds.Tables["Customers"];
                con.Close();
                OwnSearchDatagrid.DataSource = sTable;
                OwnSearchDatagrid.ReadOnly = true;
                OwnSearchDatagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }

         
        } 
    }

