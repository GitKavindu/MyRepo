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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SIGNUP LOADING = new SIGNUP();
            LOADING.Show();
        }

        private void button1_Click(object sender, EventArgs e)  ///login button
        {
            DatabaseConnect Connect = new DatabaseConnect();
            string output=Connect.selectone("UserPassword", "UsersInfo", "Username",textBox4.Text);
            if (output != "")
            {
                if (output == textBox1.Text) 
                {
                    output = Connect.selectone("userstat", "UsersInfo", "Username", textBox4.Text);
                    if(output == "True") //means account is valid
                    {
                        output = Connect.selectone("IsAdmin", "UsersInfo", "Username", textBox4.Text);
                        if (output == "False") //login type
                        {
                            Class1.adminname = textBox4.Text;
                            PassengerLogin form7 = new PassengerLogin();
                            form7.Show();
                        }
                        else
                        {
                            Class1.adminname = textBox4.Text;
                            Form6d form6 = new Form6d();
                            form6.Show();
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("oops, your account is not valid now");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect password");
                }
            }
            else
            {
                MessageBox.Show("Incorrect username");
            }
        }
    }
}
