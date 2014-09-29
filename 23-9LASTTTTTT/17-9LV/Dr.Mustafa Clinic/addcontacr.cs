using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dr.Mustafa_Clinic
{
    public partial class contacts : Form
    {
        public string number;
        public contacts()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            number = textBox1.Text;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
