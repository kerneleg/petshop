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
    public partial class Form2 : Form
    {
        Form3 newpetfrm;
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        string conString;
        DataSet ds;
        DataTable custTable;
        int save_flag;
        int function;
        string ownerId;
        public Form2()
        {
            function = 0;
            save_flag = 0;
            InitializeComponent();
        }
        public Form2(string owner_id)
        {
            InitializeComponent();
            function = 1;
            save_flag = 0;
            try
            {
                ownerId = owner_id;
                conString = Properties.Settings.Default.Database1ConnectionString;
                StringBuilder Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT * FROM Customers WHERE Customerid='" + owner_id + "' ");

                SqlConnection con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Cust");
                custTable = ds.Tables["Cust"];
                con.Close();
                owner_name.Text = custTable.Rows[0][1].ToString();
                owner_mobile.Text = custTable.Rows[0][2].ToString();
                owner_addres.Text = custTable.Rows[0][3].ToString();
                owner_age.Text = custTable.Rows[0][4].ToString();
                if (custTable.Rows[0][5].ToString() == "False")
                {
                    owner_gender.Text = "Male";
                }
                else
                {
                    owner_gender.Text = "Female";
                }
                owner_phone.Text = custTable.Rows[0][6].ToString();
                unpaid.Text = custTable.Rows[0][7].ToString();
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void new_pet_Click(object sender, EventArgs e)
        {
            if (save_flag == 1)
            {
                newpetfrm = new Form3(ownerId);
                newpetfrm.Show();
            }
            else
            {
                MessageBox.Show("Click Button Save First");
            }
        }
        int checkers()
        {
            if ((owner_name.Text == "") || (owner_mobile.Text == ""))
            {
                MessageBox.Show("Please fill the mandatory fields"); 
                return 0;
            }
            int n;
            bool test, test2, test3, test4;
            test = int.TryParse(owner_age.Text, out n);
            test2 = int.TryParse(owner_mobile.Text, out n);
            test3 = int.TryParse(owner_phone.Text, out n);
            test4 = int.TryParse(unpaid.Text, out n);
            if (owner_age.Text == "")
            {
                test = true;
            }
            if (owner_phone.Text == "")
            {
                test3 = true;
            }
            if (unpaid.Text == "")
            {
                test4 = true;
            }
            if ((test == false) || (test2 == false) || (test3 == false) || (test4 == false))
            {
                MessageBox.Show("Erorr in filling the fields");
                return 0;
            }
            return 1;
        }
        private void save_Click(object sender, EventArgs e)
        {
            int check = checkers();
            if (check == 1)
            {
                try
                {
                    if (function == 0)
                    {
                        if (save_flag == 0)
                        {
                            save_flag = 1;
                            //insert///////////////////////
                            conString = Properties.Settings.Default.Database1ConnectionString;
                            String query = "INSERT INTO Customers (Name,Mob,Address,Age,Gender,Phone,Unpaid) VALUES(@Name,@Mob,@Address,@Age,@Gender,@Phone,@Unpaid)";
                            SqlConnection con = new SqlConnection(conString);
                            con.Open();
                            SqlCommand command = new SqlCommand(query, con);
                            command.Parameters.AddWithValue("@Name", owner_name.Text);
                            command.Parameters.AddWithValue("@Mob", owner_mobile.Text);
                            command.Parameters.AddWithValue("@Address", owner_addres.Text);
                            command.Parameters.AddWithValue("@Age", owner_age.Text);
                            command.Parameters.AddWithValue("@Gender", owner_gender.SelectedIndex);
                            command.Parameters.AddWithValue("@Phone", owner_phone.Text);
                            command.Parameters.AddWithValue("@Unpaid", unpaid.Text);
                            command.ExecuteNonQuery();
                            //////////////////////////////////////////////
                            query = "SELECT Customerid FROM Customers";
                            sCommand = new SqlCommand(query.ToString(), con);
                            SqlDataReader reader;
                            reader = sCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                ownerId = reader.GetValue(0).ToString();
                            }
                            con.Close();
                            MessageBox.Show("Record Saved");
                        }
                    }
                    else
                    {
                        //update/////////////////////
                        save_flag = 1;
                        conString = Properties.Settings.Default.Database1ConnectionString;
                        string query = "update Customers set Name= @Name,Mob=@Mob,Address=@Address,Age=@Age,Gender=@Gender,Phone=@Phone,Unpaid=@Unpaid where Customerid='" + ownerId + "' ";

                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@Name", owner_name.Text);
                        command.Parameters.AddWithValue("@Mob", owner_mobile.Text);
                        command.Parameters.AddWithValue("@Address", owner_addres.Text);
                        command.Parameters.AddWithValue("@Age", owner_age.Text);
                        command.Parameters.AddWithValue("@Gender", owner_gender.SelectedIndex);
                        command.Parameters.AddWithValue("@Phone", owner_phone.Text);
                        command.Parameters.AddWithValue("@Unpaid", unpaid.Text); command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record Updated");
                    }

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
    }
}

