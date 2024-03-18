using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myexperi
{
    public partial class Verify : Form
    {
        public Verify()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SIGNUP LOADING = new SIGNUP();
            LOADING.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (USERNAME.Text== Class1.GetUsername() && PASSWORD.Text==Class1.GetUserPassword())
            {
                this.Hide();
                REGISTER LOADING = new REGISTER();
                LOADING.Show();

            }
            else
            {
                label6.Text = "Incorrect Username or password";
            }
        }
    }
}
