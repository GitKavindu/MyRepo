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
    public partial class SIGNUP : Form
    {
        public SIGNUP()
        {
            InitializeComponent();
            textBox2.Text=Class1.GetUsername();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Class1.setvalues(textBox2.Text, PASSWORD.Text, NAME.Text, NATIONALID.Text, true, EMAIL.Text, Int32.Parse(MOBILENO.Text));
                this.Hide();
                Verify LOADING = new Verify();
                LOADING.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("please fill the details correctly");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn LOADING = new SignIn();
             LOADING.Show();
        }
    }
}
