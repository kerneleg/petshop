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
    public partial class pet_form : Form
    {
        vaccination vac;
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        string conString;
        DataSet ds;
        DataTable petTable;
        StringBuilder Sqlquery;
        SqlConnection con;
        Form3 edit_pet;
        string pet_id;
        string owner_id;
        public pet_form(string number, string OwnerID)
        {
            InitializeComponent();
            owner_id = OwnerID;
            pet_id = number;
            refresh();
        }
        void refresh()
        {
            try
            {              
                conString = Properties.Settings.Default.Database1ConnectionString;
                Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT * FROM Pet WHERE Petid='" + pet_id + "' ");
                con = new SqlConnection(conString);
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
                if (petTable.Rows[0][6].ToString() == "False")
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
                if ((petTable.Rows[0][7].ToString() != "dog") && (petTable.Rows[0][7].ToString() != "Dog") && (petTable.Rows[0][7].ToString() != "كلب"))
                {
                    rabies_date.Visible = false;
                    label6.Visible = false;
                }
                ////////////////////////////////////////
                Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT Name,Mob FROM Customers WHERE Customerid='" + owner_id + "' ");

                con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                SqlDataReader reader;
                reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    owner_name.Text = reader.GetString(0);
                    owner_mobile.Text = reader.GetString(1);
                }
                con.Close();
                ///////////////////////////////////
                Sqlquery = new StringBuilder();
                Sqlquery.Append("SELECT dates,type FROM Vaccins WHERE pet_ID='" + pet_id + "' ");
                con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(1) == "Main")
                    {
                        main_date.Text = reader.GetValue(0).ToString();
                    }
                    if (reader.GetString(1) == "Insects")
                    {
                        insects_date.Text = reader.GetValue(0).ToString();
                    }
                    if (reader.GetString(1) == "Worms")
                    {
                        worms_date.Text = reader.GetValue(0).ToString();
                    }
                    if (reader.GetString(1) == "Rabies")
                    {
                        rabies_date.Text = reader.GetValue(0).ToString();
                    }
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            edit_pet = new Form3(pet_id,owner_id);
            edit_pet.ShowDialog();
            refresh();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            String query = "delete Vaccins WHERE pet_ID='" + pet_id + "' ";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();

            query = "delete Pet WHERE Petid='" + pet_id + "' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("This Record Deleted");
        }

        private void vaccin_edit_Click(object sender, EventArgs e)
        {
            vac = new vaccination(owner_id, pet_id);
            vac.ShowDialog();
            refresh();
        }
    }
}
