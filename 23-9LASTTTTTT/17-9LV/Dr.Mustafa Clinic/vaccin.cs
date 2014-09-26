using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
namespace Dr.Mustafa_Clinic
{
    public partial class vaccination : Form
    {
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        DataSet ds;
        DataTable petTable;
        string conString;
        String query;
        string ownerID, petID;
        SqlConnection con;
        SqlCommand command;
        SqlDataReader reader;
        public vaccination(string owner_id, string pet_id)
        {
            InitializeComponent();
            ownerID = owner_id;
            petID = pet_id;
            main_date.Enabled = false;
            insects_date.Enabled = false;
            rabies_date.Enabled = false;
            worms_date.Enabled = false;
            main_text.Enabled = false;
            insects_text.Enabled = false;
            rabies_text.Enabled = false;
            worms_text.Enabled = false;
            conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlConnection(conString);
            con.Open();
            query = "select Name,Mob from Customers where Customerid = '" + owner_id + "' ";
            command = new SqlCommand(query, con);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                ownerName.Text = reader.GetString(0);
                ownerMobile.Text = reader.GetString(1).ToString();
            }
            con.Close();
            ///////////////////////////////////////
            con = new SqlConnection(conString);
            con.Open();
            query = "SELECT dates,type,comments FROM Vaccins WHERE pet_ID='" + pet_id + "' ";
            command = new SqlCommand(query, con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(1) == "Main")
                {
                    main_date.Text = reader.GetValue(0).ToString();
                    main_text.Text = reader.GetString(2);
                }
                if (reader.GetString(1) == "Insects")
                {
                    insects_date.Text = reader.GetValue(0).ToString();
                    insects_text.Text = reader.GetString(2);
                }
                if (reader.GetString(1) == "Worms")
                {
                    worms_date.Text = reader.GetValue(0).ToString();
                    worms_text.Text = reader.GetString(2);
                }
                if (reader.GetString(1) == "Rabies")
                {
                    rabies_date.Text = reader.GetValue(0).ToString();
                    rabies_text.Text = reader.GetString(2);
                }
            }
            con.Close();
            ///////////////////////////////////////////
            query = "select Name,Type,breed,Agebydays,Agebyweeks,Agebymonths,Agebyyears from Pet where Petid = '" + pet_id + "' ";
            con = new SqlConnection(conString);
            con.Open();
            sCommand = new SqlCommand(query.ToString(), con);
            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            ds = new DataSet();
            sAdapter.Fill(ds, "pet");
            petTable = ds.Tables["pet"];
            con.Close();
            petName.Text = petTable.Rows[0][0].ToString();
            petType.Text = petTable.Rows[0][1].ToString();
            petBreed.Text = petTable.Rows[0][2].ToString();
            days.Text = petTable.Rows[0][3].ToString(); 
            weeks.Text = petTable.Rows[0][4].ToString();
            months.Text = petTable.Rows[0][5].ToString();
            years.Text = petTable.Rows[0][6].ToString();
            if ((petType.Text != "dog") && (petType.Text != "Dog") && (petType.Text != "كلب"))
            {
                rabies_date.Visible = false;
                label12.Visible = false;
                rabies_check.Visible = false;
                rabies_text.Visible = false;
                rabies_delete.Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            //update values into pet table & vaccins table
            if (main_check.CheckState == CheckState.Checked)
            {
                main_date.Value.ToString(new CultureInfo("en-US"));
                count = 0;
                query = "update Vaccins set dates= @dates,comments= @comments where Pet_ID='" + petID + "' and type= 'Main' ";
                con = new SqlConnection(conString);
                con.Open();
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@dates", main_date.Value); 
                command.Parameters.AddWithValue("@comments", main_text.Text);
                count = command.ExecuteNonQuery();
                con.Close();
                if (count == 0)
                {
                    query = "INSERT INTO Vaccins (pet_ID,type,dates,comments,Custid) VALUES(@pet_ID,@type,@dates,@comments,@Custid)";
                    con = new SqlConnection(conString);
                    con.Open();
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@pet_ID", petID);
                    command.Parameters.AddWithValue("@type", "Main");
                    command.Parameters.AddWithValue("@dates", main_date.Value);
                    command.Parameters.AddWithValue("@comments", main_text.Text);
                    command.Parameters.AddWithValue("@Custid", ownerID);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (insects_check.CheckState == CheckState.Checked)
            {
                insects_date.Value.ToString(new CultureInfo("en-US"));
                count = 0;
                query = "update Vaccins set dates= @dates,comments= @comments where Pet_ID='" + petID + "' and type= 'Insects' ";
                con = new SqlConnection(conString);
                con.Open();
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@dates", insects_date.Value);
                command.Parameters.AddWithValue("@comments", insects_text.Text);
                count = command.ExecuteNonQuery();
                con.Close();
                if (count == 0)
                {
                    query = "INSERT INTO Vaccins (pet_ID,type,dates,comments,Custid) VALUES(@pet_ID,@type,@dates,@comments,@Custid)";
                    con = new SqlConnection(conString);
                    con.Open();
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@pet_ID", petID);
                    command.Parameters.AddWithValue("@type", "Insects");
                    command.Parameters.AddWithValue("@dates", insects_date.Value);
                    command.Parameters.AddWithValue("@comments", insects_text.Text);
                    command.Parameters.AddWithValue("@Custid", ownerID);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (worms_check.CheckState == CheckState.Checked)
            {
                worms_date.Value.ToString(new CultureInfo("en-US"));
                count = 0;
                query = "update Vaccins set dates= @dates,comments= @comments where Pet_ID='" + petID + "' and type= 'Worms' ";
                con = new SqlConnection(conString);
                con.Open();
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@dates", worms_date.Value);
                command.Parameters.AddWithValue("@comments", worms_text.Text);
                count = command.ExecuteNonQuery();
                con.Close();
                if (count == 0)
                {
                    count = 0;
                    query = "INSERT INTO Vaccins (pet_ID,type,dates,comments,Custid) VALUES(@pet_ID,@type,@dates,@comments,@Custid)";
                    con = new SqlConnection(conString);
                    con.Open();
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@pet_ID", petID);
                    command.Parameters.AddWithValue("@type", "Worms");
                    command.Parameters.AddWithValue("@dates", worms_date.Value);
                    command.Parameters.AddWithValue("@comments", worms_text.Text);
                    command.Parameters.AddWithValue("@Custid", ownerID);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (rabies_check.CheckState == CheckState.Checked)
            {
                rabies_date.Value.ToString(new CultureInfo("en-US"));
                count = 0;
                query = "update Vaccins set dates= @dates,comments= @comments where Pet_ID='" + petID + "' and type= 'Rabies' ";
                con = new SqlConnection(conString);
                con.Open();
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@dates", rabies_date.Value);
                command.Parameters.AddWithValue("@comments", rabies_text.Text);
                count = command.ExecuteNonQuery();
                con.Close();
                if (count == 0)
                {
                    count = 0;
                    query = "INSERT INTO Vaccins (pet_ID,type,dates,comments,Custid) VALUES(@pet_ID,@type,@dates,@comments,@Custid)";
                    con = new SqlConnection(conString);
                    con.Open();
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@pet_ID", petID);
                    command.Parameters.AddWithValue("@type", "Rabies");
                    command.Parameters.AddWithValue("@dates", rabies_date.Value);
                    command.Parameters.AddWithValue("@comments", rabies_text.Text);
                    command.Parameters.AddWithValue("@Custid", ownerID);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            MessageBox.Show("Records Saved");
        }
        

        private void main_check_CheckedChanged(object sender, EventArgs e)
        {
            if (main_check.CheckState == CheckState.Checked)
            {
                main_date.Enabled = true;
                main_text.Enabled = true;
            }
            if (main_check.CheckState == CheckState.Unchecked)
            {
                main_date.Enabled = false;
                main_text.Enabled = false;
            }
        }

        private void worms_check_CheckedChanged(object sender, EventArgs e)
        {
            if (worms_check.CheckState == CheckState.Checked)
            {
                worms_date.Enabled = true;
                worms_text.Enabled = true;
            }
            if (worms_check.CheckState == CheckState.Unchecked)
            {
                worms_date.Enabled = false;
                worms_text.Enabled = false;
            }
        }

        private void insects_check_CheckedChanged(object sender, EventArgs e)
        {
            if (insects_check.CheckState == CheckState.Checked)
            {
                insects_date.Enabled = true;
                insects_text.Enabled = true;
            }
            if (insects_check.CheckState == CheckState.Unchecked)
            {
                insects_date.Enabled = false;
                insects_text.Enabled = false;
            }
        }

        private void rabies_check_CheckedChanged(object sender, EventArgs e)
        {
            if (rabies_check.CheckState == CheckState.Checked)
            {
                rabies_date.Enabled = true;
                rabies_text.Enabled = true;
            }
            if (rabies_check.CheckState == CheckState.Unchecked)
            {
                rabies_date.Enabled = false;
                rabies_text.Enabled = false;
            }
        }

        private void main_delete_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            query = "delete Vaccins WHERE pet_ID='" + petID + "' and type= 'Main' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("This Record Deleted");
        }

        private void worms_delete_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            query = "delete Vaccins WHERE pet_ID='" + petID + "' and type= 'Worms' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("This Record Deleted");
        }

        private void insects_delete_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            query = "delete Vaccins WHERE pet_ID='" + petID + "' and type= 'Insects' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close(); 
            MessageBox.Show("This Record Deleted");
        }

        private void rabies_delete_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.Database1ConnectionString;
            query = "delete Vaccins WHERE pet_ID='" + petID + "' and type= 'Rabies' ";
            con = new SqlConnection(conString);
            con.Open();
            command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("This Record Deleted");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}