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
    public partial class Form1 : Form
    {
        messaging messag;
        Form2 newownerform;
        reminder schedule;
        Ownersearch osearch;
        Petsearch psearch;
        aboutus aboutfrm;
        int xbtngroup;
        int ybtngroup;
        int Fwellcom1 = 0;
        int Fwellcom2 = 0;
        int Fwellcom3 = 0;
        int Fwellcom4 = 0; 
        int btngroupflag = 0;
        Timer t = new Timer();
        
        NotifyIcon icon = new NotifyIcon();

        SqlConnection con;
        SqlCommand command;
        SqlDataReader reader;
        string conString;
        String query;
        public Form1()
        {
            InitializeComponent();
            xbtngroup = Width / 2 - 70;
            ybtngroup = -mainbtngroup.Height;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            icon.Icon = new System.Drawing.Icon(@"C:\Users\Ahmed\Kernel\23-9LASTTTTTT\17-9LV\Dr.Mustafa Clinic\Resources\dog.ico");
            icon.Visible = true;
            icon.Text = "Pet Clinic";
            icon.ShowBalloonTip(30000, "Pet Clinic Notification", "Pet Clinic Notification", ToolTipIcon.None);
        }
        void messaging()
        {
            List<int> customer_list = new List<int>();
            DateTime date = DateTime.Today.AddDays(1);
            conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlConnection(conString);
            con.Open();
            query = "select Custid,Dates from Vaccins where Dates = '" + date + "' ";
            command = new SqlCommand(query, con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer_list.Add(reader.GetInt32(0));
            }
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            messaging();
            comboBox1.SelectedIndex = 0;
            setfullscreen();
            btngrouplocation();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
            
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit", "App Close", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
            }
            else
            {
                icon.Visible = false;
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newownerform = new Form2();
            //newpetfrm.MdiParent = this;
            newownerform.BringToFront();
            newownerform.Show();
            }
        void wellcom()
        {
            //say Hi To The Dr On Time & display BackGround

            if (DateTime.Now.Hour < 13 && DateTime.Now.Hour >= 7)
            {
                label2.Text = "Good Morning Dr.Mustafa";
                if (Fwellcom1 == 0)
                {

                    this.BackgroundImage = global::Dr.Mustafa_Clinic.Properties.Resources.Moon_wallpaper;
                    Fwellcom1 = 1;
                }
            }
            else
            {
                Fwellcom1 = 0;
            }

            /////////////////////////////

            if (DateTime.Now.Hour < 19 && DateTime.Now.Hour >= 13)
            {
                label2.Text = "Good AfterNoon Dr.Mustafa";
                if (Fwellcom2 == 0)
                {

                    this.BackgroundImage = global::Dr.Mustafa_Clinic.Properties.Resources.Moon_wallpaper;
                    Fwellcom2 = 1;
                }

            }
            else
            {
                Fwellcom2 = 0;
            }


            /////////////////////////////
            if (DateTime.Now.Hour < 24 && DateTime.Now.Hour >= 19)
            {
                
                label2.Text = "Good evening Dr.Mustafa";
                if (Fwellcom3 == 0)
                {

                    this.BackgroundImage = global::Dr.Mustafa_Clinic.Properties.Resources.land;
                    Fwellcom3 = 1;
                }
            }
            else
            {
                Fwellcom3 = 0;
            }

            //////////////////////////////
            if (DateTime.Now.Hour < 7 && DateTime.Now.Hour >= 0)
            {
                label2.Text = "Good Night Dr.Mustafa";
                if (Fwellcom4 == 0)
                {
                    this.BackgroundImage = global::Dr.Mustafa_Clinic.Properties.Resources.Moon_wallpaper;
                    Fwellcom4 = 1;
                }
            }
            else
            {
                Fwellcom4 = 0;
            }

        }



        private void btngrouplocation()
        {
            int x = (this.Width / 2) - (mainbtngroup.Size.Width /2);
            mainbtngroup.Location = new Point(x);
        }

        private void setfullscreen()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            Location = new Point(0, 0);
            Size = new Size(width, height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
        
            schedule = new reminder();
            schedule.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss tt");
            wellcom();
        }
        private void vaccin_Click(object sender, EventArgs e)
        {
            }
        void animate()
        {

            /////// Button Group Animation
            if (Cursor.Position.Y <= 10 + mainbtngroup.Height)
            {
                if (mainbtngroup.Location.Y <= 0 && btngroupflag == 0)
                {
                    ybtngroup += 10;
                    this.mainbtngroup.Location = new System.Drawing.Point(xbtngroup, ybtngroup);
                }
                else
                {
                    btngroupflag = 1;
                }

            }
            else
            {
                /*   if (mainbtngroup.Location.Y >= -mainbtngroup.Height && flag == 1)
                   {
                       ybtngroup -= 10;
                       this.mainbtngroup.Location = new System.Drawing.Point(xbtngroup, ybtngroup);
                   }
                   else
                   {
                       flag = 0;
                   }
                 */
                ybtngroup = -mainbtngroup.Height;
                this.mainbtngroup.Location = new System.Drawing.Point(xbtngroup, ybtngroup);
                btngroupflag = 0;
            }
            /////////

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                osearch = new Ownersearch(textBox1.Text);
                osearch.Show();

            }
            if (comboBox1.SelectedIndex == 1)
            {
                psearch = new Petsearch(textBox1.Text);
                psearch.Show();

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            animate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            aboutfrm = new aboutus();
            aboutfrm.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //send the message
            messag = new messaging();
            messag.Show();
        }



        


    }
}
