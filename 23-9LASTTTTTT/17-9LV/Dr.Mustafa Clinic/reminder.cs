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
    public partial class reminder : Form
    {

        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        string conString;
        DataSet ds;
        DataTable sTable;
        public reminder()
        {
            InitializeComponent();
            vaccin_table.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            vaccin_table.Visible = true;
            this.monthCalendar1.MaxSelectionCount = 1;
            DateTime day = new DateTime();
            day = this.monthCalendar1.SelectionRange.Start.Date;
            conString = Properties.Settings.Default.Database1ConnectionString;
            StringBuilder Sqlquery = new StringBuilder();
            day.ToString(new CultureInfo("en-US"));
            
            Sqlquery.Append("select Vaccins.Id,Vaccins.type,Vaccins.comments,Pet.Name,Customers.Name,Customers.Mob from Vaccins join Pet on Vaccins.pet_ID = Pet.Petid join Customers on Vaccins.Custid = Customers.Customerid where Vaccins.dates= '" + day + "' ");

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            sCommand = new SqlCommand(Sqlquery.ToString(), con);
            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            ds = new DataSet();
            sAdapter.Fill(ds, "vaccin");
            sTable = ds.Tables["vaccin"];
            con.Close();
            vaccin_table.DataSource = sTable;
            vaccin_table.ReadOnly = true;
            vaccin_table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            vaccin_table.Columns[0].Visible = false;
            vaccin_table.Columns[1].HeaderText = "Type";
            vaccin_table.Columns[2].HeaderText = "Comments";
            vaccin_table.Columns[3].HeaderText = "Pet Name";
            vaccin_table.Columns[4].HeaderText = "Customer Name";
            vaccin_table.Columns[5].HeaderText = "Customer Mobile";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
