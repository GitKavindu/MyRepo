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
    public partial class PassengerLogin : Form
    {
        List<string> combo1,combo2,combo3;
        List<Panel> listpanel = new List<Panel>();
        
        System.Random random;
        
        string BookID;
        string[] sendval;
        string[] sendcol;

        DatabaseConnect db;
        string valimessege;
       

        private void Submit_Click(object sender, EventArgs e)
        {
            if (Submit.Text == "Submit")
            {
                //
                if (validate() == true)
                {
                    // MessageBox.Show(CalculatePrice().ToString());
                    listpanel[1].BringToFront();
                    Submit.Text = "Confirm";
                    //show details
                    show_details();

                   

                }
                else
                {
                    MessageBox.Show(valimessege);
                }
                //
            }
            else
            {
                // update table schedule
                updateSchedule();
                // add booking to table
                AddBookingToDB();
                MessageBox.Show("Booking Sucessful");
                PassengerLogin form7 = new PassengerLogin();
                this.Hide();
                form7.Show();

            }

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // textBox3.Text = "916947";
            string[] take;
            db = new DatabaseConnect();
            take = db.selectcolumn("ScheduleId", "Schedule", "IsActive","1");
            for (int i = 0; i < take.Length; i++)
            {
               
                combo3.Add(take[i]);
               


            }
           
            comboBox3.DataSource = combo3;


            listpanel.Add(panel1);
            listpanel.Add(panel2);
            listpanel[0].BringToFront();

            db=new DatabaseConnect();
            db.Datagrid("Schedule", dataGridView1);


        }

        public PassengerLogin()
        {
            InitializeComponent();
            returncombo();
            combo1 = new List<string>();
            combo2 = new List<string>();
            combo3 = new List<string>();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        // ComboBox comboBox1;
        public System.Windows.Forms.ComboBox returncombo()
        {
            return comboBox3;
        }
       

        public bool validate()
        {
            
            bool validate=true;
            db = new DatabaseConnect();
           // MessageBox.Show(db.selectone("SecondClass", "Trains", "TrainId", textBox3.Text));
            int secondclasses = int.Parse(db.selectone("Secondseats", "Schedule", "ScheduleId", comboBox3.SelectedItem.ToString()));
            int thirdclasses = int.Parse(db.selectone("Thirdseats", "Schedule", "ScheduleId", comboBox3.SelectedItem.ToString()));
            try
            {
                if (int.Parse(textBox2.Text) < 0 || int.Parse(textBox2.Text) > secondclasses)
                {
                    validate = false;
                    valimessege = "Please set suitable second classes";
                }
                else
                {
                    if (int.Parse(textBox1.Text) < 0 || int.Parse(textBox1.Text) > thirdclasses)
                    {
                        validate = false;
                        valimessege = "Please set a suitable third classes";


                    }
                    else
                    {
                        int one = int.Parse(db.selectone("ScheduledRank", "ScheduleStations", "Schedulestation", comboBox1.SelectedItem.ToString(), "ScheduleId", comboBox3.SelectedItem.ToString()));
                        int two = int.Parse(db.selectone("ScheduledRank", "ScheduleStations", "Schedulestation", comboBox2.SelectedItem.ToString(), "ScheduleId", comboBox3.SelectedItem.ToString()));
                        if (one >= two)
                        {
                            validate = false;
                            valimessege = "Please set a valid station";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 valimessege="Please enter a valid second and third classes";
                 validate = false;
            }
            return validate;

        }
        public int CalculatePrice()
        {
            int totalVal=0;
            int unitprice = 5;
            int time=0;
            string [] retVal;
            retVal = db.selectcolumn("ScheduledETA", "ScheduleStations", "ScheduleId", comboBox3.SelectedItem.ToString());

            for(int i = 0; i < retVal.Length; i++)
            {
                time=time+int.Parse(retVal[i].ToString());
            }
            totalVal=totalVal+(time*unitprice);
            return totalVal;
        }
        
        public void show_details()
        {
            SetBookID();
            label7.Text = GetBookID();
            label14.Text = comboBox3.SelectedItem.ToString(); 
            label15.Text = comboBox1.SelectedItem.ToString(); 
            label16.Text = comboBox2.SelectedItem.ToString();
            label17.Text = textBox2.Text; 
            label18.Text = textBox1.Text; 
            label19.Text = CalculatePrice().ToString(); // price


         }

        public void SetBookID()
        {
            random = new System.Random();
            BookID = random.Next(1000).ToString() + random.Next(1000).ToString();
        }

        public string GetBookID()
        {
            return BookID;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string[] take;
             db = new DatabaseConnect();

            comboBox1.Text = "";
            comboBox2.Text = "";
            combo1 = new List<string>();
            combo2 = new List<string>();
            
            take =db.selectcolumn("Schedulestation", "ScheduleStations", "ScheduleId", comboBox3.SelectedItem.ToString());
            
           
            for (int i = 0; i < take.Length; i++)
            {
                combo1.Add(take[i]);
                combo2.Add(take[i]);
               

            }
            comboBox1.DataSource = combo1;
            comboBox2.DataSource = combo2;


        }

        public void AddBookingToDB()
        {
            sendval = new string[7];
            sendcol = new string[7];

           
            sendcol[0] = "BookId";  sendval[0] = GetBookID();
            sendcol[1] = "ScheduleId"; sendval[1] = comboBox3.SelectedItem.ToString(); MessageBox.Show(sendval[1]);
            sendcol[2] = "Username"; sendval[2] = Class1.adminname;
            sendcol[3] = "Secondseats"; sendval[3] = textBox2.Text;
            sendcol[4] = "Thirdseats"; sendval[4] = textBox1.Text;
            sendcol[5] = "startStation"; sendval[5] = comboBox1.SelectedItem.ToString();
            sendcol[6] = "endStation"; sendval[6] = comboBox2.SelectedItem.ToString();

            db = new DatabaseConnect();
            db.insertdata(sendcol, sendval, "Bookings");
        }

        public void updateSchedule()
        {
            db = new DatabaseConnect();
            int second = int.Parse(db.selectone("Secondseats", "Schedule", "ScheduleId", comboBox3.SelectedItem.ToString()));
            int third = int.Parse(db.selectone("Thirdseats", "Schedule", "ScheduleId", comboBox3.SelectedItem.ToString()));
            
            second = second - int.Parse(textBox2.Text);
            third = third - int.Parse(textBox1.Text);

            db.UpdateData("Schedule", "Secondseats", second.ToString(), "ScheduleId", comboBox3.SelectedItem.ToString());
            db.UpdateData("Schedule", "Thirdseats", third.ToString(), "ScheduleId", comboBox3.SelectedItem.ToString());
            
        }

        
    }
}
