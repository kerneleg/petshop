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
    public partial class Petsearch : Form
    {
        pet_form pet;
        DBConnection objConnect = new DBConnection();
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        string conString;
        DataSet ds;
        DataTable sTable;
        DataTable dtCloned;
        SqlConnection con, con2;
        SqlCommand command, command2;
        SqlDataReader reader;
        //string conString;
        String query, query2;
        string name;
        public Petsearch(string temp_name)
        {
            InitializeComponent();
            name = temp_name;
            refresh();
        }
        void refresh()
        {
            ///////// this week vaccinations
            conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlConnection(conString);
            con.Open();
            query = "select Dates,pet_ID from Vaccins";
            command = new SqlCommand(query, con);
            reader = command.ExecuteReader();
            List<int> IDS = new List<int>();
            while (reader.Read())
            {
                int check = weekly_vaccin(reader.GetDateTime(0));
                int pet_id = reader.GetInt32(1);
                if (check == 1)
                {
                    con2 = new SqlConnection(conString);
                    con2.Open();
                    query2 = "update Pet set ThisWeek_Vaccin='" + 1 + "' where Petid= '" + pet_id + "' ";
                    command2 = new SqlCommand(query2, con2);
                    command2.ExecuteNonQuery();
                    con2.Close();
                    IDS.Add(pet_id);
                }
                else
                {
                    int IDS_flag = 0;
                    for (int i = 0; i < IDS.Count; i++)
                    {
                        if (IDS[i] == pet_id)
                        {
                            IDS_flag = 1;
                        }
                    }
                    if (IDS_flag == 0)
                    {
                        con2 = new SqlConnection(conString);
                        con2.Open();
                        query2 = "update Pet set ThisWeek_Vaccin='" + 0 + "' where Petid= '" + pet_id + "' ";
                        command2 = new SqlCommand(query2, con2);
                        command2.ExecuteNonQuery();
                        con2.Close();
                    }
                }
            }
            con.Close();
            if (name == "")
            {
                try
                {
                    objConnect.connection_string = conString;
                    string sql = "SELECT Petid,Customerid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet";


                    con = new SqlConnection(conString);
                    con.Open();
                    sCommand = new SqlCommand(sql, con);
                    sAdapter = new SqlDataAdapter(sCommand);
                    sBuilder = new SqlCommandBuilder(sAdapter);
                    ds = new DataSet();
                    sAdapter.Fill(ds, "Pet");
                    sTable = ds.Tables["Pet"];
                    con.Close();
                    dtCloned = sTable.Clone();
                    foreach (DataRow row in sTable.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }
                    PetSearchdataGrid.DataSource = sTable;
                    PetSearchdataGrid.ReadOnly = true;
                    PetSearchdataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    PetSearchdataGrid.Columns[1].Visible = false;
                    PetSearchdataGrid.Columns[2].Visible = false;
                }
                catch (Exception err)
                {

                    MessageBox.Show(err.Message);

                }
            }
            else
            {
                objConnect.connection_string = conString;
                string sql = "SELECT Petid,Customerid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet where Name LIKE @test ";


                con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(sql, con);
                sCommand.Parameters.AddWithValue("@test", name);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Pet");
                sTable = ds.Tables["Pet"];
                con.Close();
                dtCloned = sTable.Clone();
                foreach (DataRow row in sTable.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                PetSearchdataGrid.DataSource = sTable;
                PetSearchdataGrid.ReadOnly = true;
                PetSearchdataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                PetSearchdataGrid.Columns[1].Visible = false;
                PetSearchdataGrid.Columns[2].Visible = false;
            }
        }
        int weekly_vaccin(DateTime date)
        {
            if (date.Date >= DateTime.Today && date.Date <= DateTime.Today.AddDays(7))
            {
                return 1;
            }
            return 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                conString = Properties.Settings.Default.Database1ConnectionString;
                objConnect.connection_string = conString;
                string cust_name = owner_filter.Text.ToString();
                string pet_name = pet_filter.Text.ToString();
                string type = type_filter.Text;
                string breed = breed_filter.Text;
                StringBuilder Sqlquery = new StringBuilder();
                if (cust_name != "")
                {
                    if (pet_name != "")
                    {
                        if (type != "")
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                        else
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Name LIKE '" + pet_name + "%' and ThisWeek_Vaccin = 1");
                                }
                             }
                        }
                    }
                    else
                    {
                        if (type != "")
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Type LIKE '" + type + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and Type LIKE '" + type + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                        else
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Custname LIKE'" + cust_name + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (pet_name != "")
                    {
                        if (type != "")
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and Type LIKE '" + type + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                        else
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Name LIKE '" + pet_name + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (type != "")
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Type LIKE '" + type + "%' and breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Type LIKE '" + type + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE Type LIKE '" + type + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                        else
                        {
                            if (breed != "")
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE breed LIKE '" + breed + "%' ");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE breed LIKE '" + breed + "%' and ThisWeek_Vaccin = 1");
                                }
                            }
                            else
                            {
                                if (vaccin_filter.CheckState == CheckState.Unchecked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet");
                                }
                                if (vaccin_filter.CheckState == CheckState.Checked)
                                {
                                    Sqlquery.Append("SELECT Petid,Custname,Name,Type,breed,ThisWeek_Vaccin FROM Pet WHERE ThisWeek_Vaccin = 1");
                                }
                            }
                        }
                    }
                }

                SqlConnection con = new SqlConnection(conString);
                con.Open();
                sCommand = new SqlCommand(Sqlquery.ToString(), con);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                ds = new DataSet();
                sAdapter.Fill(ds, "Pet");
                sTable = ds.Tables["Pet"];
                con.Close();
                PetSearchdataGrid.DataSource = sTable;
                PetSearchdataGrid.ReadOnly = true;
                PetSearchdataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }

        private void PetSearchdataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < PetSearchdataGrid.Rows.Count - 1)
            {
                pet = new pet_form(PetSearchdataGrid.Rows[e.RowIndex].Cells[1].Value.ToString(), PetSearchdataGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
                pet.ShowDialog();
                refresh();
            }
        }
    }
}
