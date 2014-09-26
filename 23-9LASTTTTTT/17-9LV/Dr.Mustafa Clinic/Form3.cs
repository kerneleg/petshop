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
    public partial class Form3 : Form
    {
        int function;
        vaccination vac;
        int saveFlag = 0;
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        string conString;
        DataSet ds;
        DataTable petTable;
        string ID;
        string ownerID;
        public Form3(string owner_id)
        {
            ID = "";
            function = 0;
            ownerID = owner_id;
            InitializeComponent();
            fill(ownerID);
        }
        public Form3(string pet_id, string owner_id)
        {
            function = 1;
            InitializeComponent();
            vaccin_button.Visible = false;
            ID = pet_id;
            ownerID = owner_id;
            fill(ownerID);
            try
            {
                /////fill data
                conString = Properties.Settings.Default.Database1ConnectionString;
                StringBuilder Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT * FROM Pet WHERE Petid='" + pet_id + "' ");

                SqlConnection con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "pet");
                petTable = ds.Tables["pet"];
                con.Close();
                name.Text = petTable.Rows[0][1].ToString();
                days.Text = petTable.Rows[0][2].ToString();
                weeks.Text = petTable.Rows[0][3].ToString();
                months.Text = petTable.Rows[0][4].ToString();
                years.Text = petTable.Rows[0][5].ToString();
                if (petTable.Rows[0][6].ToString() == "True")
                {
                    sex.Text = "Male";
                }
                else
                {
                    sex.Text = "Female";
                }
                type.Text = petTable.Rows[0][7].ToString();
                breed.Text = petTable.Rows[0][8].ToString();
                color.Text = petTable.Rows[0][9].ToString();
                markings.Text = petTable.Rows[0][10].ToString();
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }
        void fill(string id)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query = "select Name,Mob from Customers where Customerid = '" + ownerID + "' ";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Oname.Text = reader.GetString(0);
                Omob.Text = reader.GetString(1).ToString();
            }
            con.Close();
        }
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void vaccin_button_Click(object sender, EventArgs e)
        {

            if (saveFlag == 1)
            {
                if (ID == "")
                {

                    conString = Properties.Settings.Default.Database1ConnectionString;
                    StringBuilder Sqlquery = new StringBuilder();
                    Sqlquery.Append("SELECT Petid FROM Pet");

                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    sCommand = new SqlCommand(Sqlquery.ToString(), con);
                    SqlDataReader reader;
                    reader = sCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ID = reader.GetValue(0).ToString();
                    }
                    con.Close();
                    vac = new vaccination(ownerID, ID);
                    vac.Show();
                }
            }
            else
            {
                MessageBox.Show("Click Button Save First");
            }
        }
        int checkers()
        {
            if ((name.Text == "") || (type.Text == "") || (breed.Text == ""))
            {
                MessageBox.Show("Please fill the mandatory fields");
                return 0;
            }
            if ((years.Text == "") && (months.Text == "") && (weeks.Text == "") && (days.Text == ""))
            {
                MessageBox.Show("Please fill the mandatory fields");
                return 0;
            }
            int n;
            bool test, test2, test3, test4;
            test = int.TryParse(years.Text, out n);
            test2 = int.TryParse(months.Text, out n);
            test3 = int.TryParse(weeks.Text, out n);
            test4 = int.TryParse(days.Text, out n);
            if (years.Text == "")
            {
                test = true;
            }
            if (months.Text == "")
            {
                test2 = true;
            }
            if (weeks.Text == "")
            {
                test3 = true;
            }
            if (days.Text == "")
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
                        //insert
                        if (saveFlag == 0)
                        {
                            saveFlag = 1;
                            conString = Properties.Settings.Default.Database1ConnectionString;
                            SqlConnection con = new SqlConnection(conString);
                            con.Open();
                            string owner_name = "";
                            string query = "select Name from Customers where Customerid = '" + ownerID + "' ";
                            SqlCommand command2 = new SqlCommand(query, con);
                            SqlDataReader reader;
                            reader = command2.ExecuteReader();

                            while (reader.Read())
                            {
                                owner_name = reader.GetString(0);
                            }
                            con.Close();
                            con.Open();
                            query = "INSERT INTO Pet (Name,Agebydays,Agebyweeks,Agebymonths,Agebyyears,sex,Type,breed,color,Marking,Customerid,Custname) VALUES(@Name,@Agebydays,@Agebyweeks,@Agebymonths,@Agebyyears,@sex,@Type,@breed,@color,@Marking,@Customerid,@Custname)";
                            SqlCommand command = new SqlCommand(query, con);
                            command = new SqlCommand(query, con);
                            command.Parameters.AddWithValue("@Name", name.Text);
                            command.Parameters.AddWithValue("@Agebydays", days.Text);
                            command.Parameters.AddWithValue("@Agebyweeks", weeks.Text);
                            command.Parameters.AddWithValue("@Agebymonths", months.Text);
                            command.Parameters.AddWithValue("@Agebyyears", years.Text);
                            command.Parameters.AddWithValue("@sex", sex.SelectedIndex);
                            command.Parameters.AddWithValue("@Type", type.Text);
                            command.Parameters.AddWithValue("@breed", breed.Text);
                            command.Parameters.AddWithValue("@color", color.Text);
                            command.Parameters.AddWithValue("@Marking", markings.Text);
                            command.Parameters.AddWithValue("@Customerid", ownerID);
                            command.Parameters.AddWithValue("@Custname", owner_name);
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Record Saved");
                        }
                    }

                    else
                    {
                        //update
                        saveFlag = 1;
                        conString = Properties.Settings.Default.Database1ConnectionString;
                        //String query = "update Pet set Name= '" + name.Text + "' ,Agebydays= '" + days.Text + "',Agebyweeks= '" + weeks.Text + "',Agebymonths= '" + months.Text + "',Agebyyears= '" + years.Text + "',sex= '" + sex.SelectedIndex + "',Type= '" + type.Text + "',breed= '" + breed.Text + "',color= '" + color.Text + "',Marking= '" + markings.Text + "' where Petid='" + ID + "' ";
                        String query = "update Pet set Name= @Name,Agebydays= @Agebydays,Agebyweeks=@Agebyweeks,Agebymonths=@Agebymonths,Agebyyears= @Agebyyears,sex=@sex,Type= @Type,breed= @breed,color= @color,Marking= @Marking where Petid='" + ID + "' ";
                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        sCommand = new SqlCommand(query.ToString(), con);
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@Name", name.Text);
                        command.Parameters.AddWithValue("@Agebydays", days.Text);
                        command.Parameters.AddWithValue("@Agebyweeks", weeks.Text);
                        command.Parameters.AddWithValue("@Agebymonths", months.Text);
                        command.Parameters.AddWithValue("@Agebyyears", years.Text);
                        command.Parameters.AddWithValue("@sex", sex.SelectedIndex);
                        command.Parameters.AddWithValue("@Type", type.Text);
                        command.Parameters.AddWithValue("@breed", breed.Text);
                        command.Parameters.AddWithValue("@color", color.Text);
                        command.Parameters.AddWithValue("@Marking", markings.Text);
                        command.ExecuteNonQuery();
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

