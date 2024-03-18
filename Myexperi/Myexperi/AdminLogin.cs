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
    public partial class Form6d : Form
    {
        System.Random random;
        DatabaseConnect db;
        int i=0;
        int []hours;
        int []minutes;
        List<Panel> listpanel = new List<Panel>();
        int index;
        bool schedulevalidity = true;
        string scheduleID;
        List<string> nList = new List<string>();

        string[] sendcol;
        string[] sendval;

        void setscheduleID()
        {
            random = new System.Random();
            scheduleID = random.Next(1000).ToString() + random.Next(1000).ToString();
            
        }
        

        public Form6d()
        {
            InitializeComponent();
            
        }

        private void View_Click(object sender, EventArgs e)
        {
            db = new DatabaseConnect();
            string[] send = { "Username", "IsAdmin", "userstat" };
            db.Datagrid("UsersInfo", dataGridView1,send);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            db = new DatabaseConnect();
            string[] send = { "Username", "IsAdmin", "userstat" };
            db.Datagrid("UsersInfo", dataGridView1, send);

            string[] stations = new string[4];
            stations[0] = "Galle";
            stations[1] = "Mathara";
            stations[2] = "Hikkaduwa";
            stations[3] = "Ambalangoda";


            for (int i = 0; i < stations.Length; i++)
            {
                nList.Add(stations[i]);
                listBox2.Items.Add(stations[i]);    
            }

            //listBox2.DataSource = nList;
         

            listpanel.Add(panel3);
            listpanel.Add(panel2);
            listpanel.Add(panel4);

            listpanel[0].BringToFront();

            
            db.Datagrid("Trains", dataGridView3);
            db.Datagrid("Schedule", dataGridView2);


        }

        private void SetAdmin_Click(object sender, EventArgs e)
        {
            // public void UpdateData(string table, string Targetfield, string Targetval, string field, string fieldval)
            db = new DatabaseConnect();
            String IsAdmin= db.selectone("IsAdmin", "UsersInfo","Username", textBox1.Text);
            if (IsAdmin == "True")
            {
                db.UpdateData("UsersInfo", "IsAdmin", "0", "Username", textBox1.Text);
            }
            else
            {
                db.UpdateData("UsersInfo", "IsAdmin", "1", "Username", textBox1.Text);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
             db = new DatabaseConnect();
            String userstat = db.selectone("userstat", "UsersInfo", "Username", textBox1.Text);
            if (userstat == "True")
            {
                db.UpdateData("UsersInfo", "userstat", "0", "Username", textBox1.Text);
            }
            else
            {
                db.UpdateData("UsersInfo", "userstat", "1", "Username", textBox1.Text);
            }
        }

        private void AddTrain_Click(object sender, EventArgs e)
        {
            sendval= new string[4];
            random=new System.Random();
            sendval[0] = random.Next(10).ToString()+random.Next(10).ToString();
            sendval[1] = textBox2.Text;
            sendval[2] = textBox4.Text;
            sendval[3] = textBox5.Text;

            sendcol = new string[4];
            sendcol[0] = "TrainId";
            sendcol[1] = "TrainName";
            sendcol[2] = "SecondClass";
            sendcol[3] = "ThirdClass";

            db = new DatabaseConnect();
            db.insertdata(sendcol, sendval,"Trains");

            MessageBox.Show("Train added Sucessfully");



        }

        private void button2_Click(object sender, EventArgs e) //remove button
        {
            try
            {
                string removed = listBox1.SelectedItem.ToString();
                listBox1.Items.Remove(removed);
                listBox2.Items.Add(removed);

                i--;
                hours = new int[i];
                minutes = new int[i];
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Plase select a item from list");
            }
            
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string add = listBox2.SelectedItem.ToString();
                listBox2.Items.Remove(add);
                listBox1.Items.Add(add);


                i++;
                hours = new int[i];
                minutes = new int[i];
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Plase select a item from list");
            }

        }

        private void Set_Click(object sender, EventArgs e)
        {
            if(int.Parse(textBox3.Text) < 24 && int.Parse(textBox3.Text) >= 0)
            {
                hours[listBox1.SelectedIndex] = int.Parse(textBox3.Text);
            }
            if (int.Parse(textBox6.Text) < 60 && int.Parse(textBox6.Text)>= 0) 
            {
                minutes[listBox1.SelectedIndex] = int.Parse(textBox6.Text);
            }
           
        }

        private void change(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = hours[listBox1.SelectedIndex].ToString();
                textBox6.Text = minutes[listBox1.SelectedIndex].ToString();
            }
            catch (Exception ex)
            {

            }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (index < listpanel.Count - 1)
            {
                
                listpanel[++index].BringToFront();
                if (index == listpanel.Count - 1)
                {
                    Next.Text = "Submit";

                }
                PassengerLogin ld = new PassengerLogin();
                ComboBox comboBox = ld.returncombo(); 
                

            }
            else if(index == listpanel.Count - 1) // submit part
            {
                if(classvalidate()==true)
                {
                    // enter database
                    sendval = new string[7];
                    sendcol = new string[7];

                    setscheduleID();
                    sendcol[0] = "ScheduleId";          sendval[0] = scheduleID;
                    sendcol[1] = "Scheduledate";        sendval[1] = dateTimePicker1.Value.ToString("yyyy-MM-dd");  MessageBox.Show(sendval[1]);
                    sendcol[2] = "ScheduledTime";       sendval[2] = textBox8.Text+":"+textBox7.Text;
                    sendcol[3] = "ScheduleAdmin";       sendval[3] = Class1.adminname; 
                    sendcol[4] = "RelatedTrain";        sendval[4] = textBox11.Text;
                   sendcol[5] = "Secondseats";          sendval[5] = textBox9.Text;
                   sendcol[6] = "Thirdseats";           sendval[6] = textBox10.Text;

                    db = new DatabaseConnect();
                    db.insertdata(sendcol,sendval,"Schedule");

                    sendval = new string[3];
                    sendcol = new string[3];

                    for (int i = 0; i < hours.Length; i++)
                    {
                        sendcol[0] = "ScheduleId";          sendval[0] = scheduleID;
                        sendcol[1] = "Schedulestation";     sendval[1] =listBox1.Items[i].ToString();
                        sendcol[2] = "ScheduledETA";        sendval[2] = (hours[i]*60+minutes[i]).ToString();
                        db.insertdata(sendcol, sendval, "ScheduleStations");
                    }


                }
            }
           
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {

               
                listpanel[--index].BringToFront();
                Next.Text = "Next";
                
            }
        }

        bool classvalidate()
        {
            bool classvalidity = true;
            db = new DatabaseConnect();
            int second = int.Parse(db.selectone("SecondClass", "Trains", "TrainId", textBox11.Text));
            int third  = int.Parse(db.selectone("ThirdClass", "Trains", "TrainId", textBox11.Text));
            if (second<int.Parse(textBox9.Text)|| third<int.Parse(textBox10.Text))
            {
                classvalidity = false;
            }
            return classvalidity;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
