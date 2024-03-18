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
    public partial class REGISTER : Form
    {
        public REGISTER()
        {
            InitializeComponent();
        }

        public REGISTER(String name)
        {
            MessageBox.Show(name);
        }

            private void button1_Click(object sender, EventArgs e)
        {
            Class2 load=new Class2();

            string send= Class1.GetUsername()+Class1.GetUserPassword()+ Class1.GetPreferedName()+Class1.GettNationalID()+Class1.GetIsAdmin()+ Class1.GetEmail()+ Class1.GetMobileNo();
            string[] ar=new string[6];
            ar[0] = Class1.GetUsername();
            ar[1] = Class1.GetUserPassword();
            ar[2] = Class1.GetPreferedName();
            ar[3] = Class1.GettNationalID();
            ar[4] = Class1.GetEmail();
            ar[5] = Class1.GetMobileNo().ToString();

            //Username,UserPassword,PreferedName,NationalID,IsAdmin,Email,MobileNo
            string[] aw = new string[6];
            aw[0] = "Username";
            aw[1] = "UserPassword";
            aw[2] = "PreferedName";
            aw[3] = "NationalID";
            aw[4] = "Email";
            aw[5] = "MobileNo";

            DatabaseConnect dbm=new DatabaseConnect();
            dbm.insertdata(aw,ar, "UsersInfo");

            PassengerLogin form7 = new PassengerLogin();
            this.Hide();
            form7.Show();


        }
    }
}
