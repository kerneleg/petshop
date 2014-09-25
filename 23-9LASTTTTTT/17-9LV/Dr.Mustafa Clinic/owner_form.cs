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
    public partial class owner_form : Form
    {

        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        SqlConnection con;
        string conString;
        DataSet ds;
        DataTable custTable;
        DataTable petTable;
        string cust_id, sql;
        Form2 edit;
        pet_form pet;
        StringBuilder Sqlquery;
        public owner_form()
        {
            InitializeComponent();
        }
        public void owner_number(string number)
        {
            try
            {

                conString = Properties.Settings.Default.Database1ConnectionString;
                Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT * FROM Customers WHERE Customerid='" + number + "' ");

                con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Cust");
                custTable = ds.Tables["Cust"];
                con.Close();
                cust_id = number;
                lname.Text = custTable.Rows[0][1].ToString();
                lmob.Text = custTable.Rows[0][2].ToString();
                laddress.Text = custTable.Rows[0][3].ToString();
                lage.Text = custTable.Rows[0][4].ToString();
                if (custTable.Rows[0][5].ToString() == "True")
                {
                    lgender.Text = "Male";
                }
                else
                {
                    lgender.Text = "Female";
                }
                lphone.Text = custTable.Rows[0][6].ToString();
                lunpaid.Text = custTable.Rows[0][7].ToString();

                //////////////////////////////////////////
                Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT Petid,Name,Type,breed FROM Pet WHERE Customerid='" + cust_id + "' ");


                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Pet");
                petTable = ds.Tables["Pet"];

                con.Close();
                Pet_Table.DataSource = petTable;
                Pet_Table.ReadOnly = true;
                Pet_Table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Pet_Table.Columns[1].Visible = false;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }


        }

        private void cbut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editing_Click(object sender, EventArgs e)
        {
            edit = new Form2(cust_id);
            edit.Show();
        }

        private void Pet_Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Pet_Table.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                pet = new pet_form(Pet_Table.Rows[e.RowIndex].Cells[1].Value.ToString(), cust_id);
                pet.Show();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            String query = "delete Vaccins WHERE Custid='" + cust_id + "' ";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();

            query = "delete Pet WHERE Customerid='" + cust_id + "' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();

            query = "delete Customers WHERE Customerid='" + cust_id + "' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("This Record Deleted");
        }
    }
}
