using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myexperi
{
    
    internal class Class2
    {
       
        String connectionString = @"Data Source=DESKTOP-H44FGRV;Initial Catalog=Train;Integrated Security=True;";
        SqlConnection cnn;
       public Class2()
        {
             cnn = new SqlConnection(connectionString);
             cnn.Open();
             
            
        }

        public void insertdata(String Username, String UserPassword, String PreferedName, String NationalID, bool IsAdmin, String Email, int MobileNo)
        {
            int val;
            if(IsAdmin==true)
            {
                val= 1; 
            }
            else
            {
                val= 0;
            }
            
            String support = Username + "',' " + UserPassword + "','" + PreferedName +"','"+NationalID+"',"+val+", '"+ Email+"'," + MobileNo;
            //String sql = "INSERT INTO UsersInfo (Username,UserPassword,PreferedName,NationalID,IsAdmin,Email,MobileNo) VALUES ('harsha','haha','Kavindu', '992231130V', 5,'hjkhk',50000);";
           
            String sql = "INSERT INTO UsersInfo (Username,UserPassword,PreferedName,NationalID,IsAdmin,Email,MobileNo) VALUES ('" + support+");";
            MessageBox.Show(sql);
            SqlCommand command = new SqlCommand(sql, cnn);
            //SqlDataReader reader = command.ExecuteReader();
            command.ExecuteNonQuery();

            //reader.Close();
            command.Dispose();
            cnn.Close();    

        }
        public void insertdata()
        {
            String sql = "INSERT INTO UsersInfo (Username,UserPassword,PreferedName,NationalID,IsAdmin,Email,MobileNo) VALUES ('Kaviya','Kavi','Kavindu', '992231130V', 5,'hjkhk',50000);";
            
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader reader = command.ExecuteReader();

            reader.Close();
            command.Dispose();
            cnn.Close();
        }



    }
}
