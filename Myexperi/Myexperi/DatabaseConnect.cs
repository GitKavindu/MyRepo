using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Myexperi
{
    internal class DatabaseConnect
    {

       
        String connectionString = @"Data Source=LOVEMOON;Initial Catalog=Train;Integrated Security=True;";
        SqlConnection cnn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;


        public DatabaseConnect()
        {
            cnn = new SqlConnection(connectionString);
            
        }

        public void insertdata(string[] column,string[] val,string db)
        {
               
            string data="";  // contain columns
            string jk="";   // contain values
           

            for (int i = 0; i < column.Length; i++)
            {
                jk = jk+"@"+ column[i];
                data =data+ column[i];
                if(i == column.Length - 1)
                {
                    jk = jk + ")";
                    data = data + ")";
                }
                else
                {
                    jk =jk + ",";
                    data = data +",";
                }
            }

            
            string sql = "INSERT INTO " + db + "("+data+"values("+jk+";";
            cnn.Open();
            cmd = new SqlCommand(sql,cnn);
            for (int i = 0; i < val.Length; i++)
            {
                cmd.Parameters.AddWithValue("@"+column[i], val[i]);
            }

            MessageBox.Show(sql);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

        }
       
        public string selectone(String outsql,String table,string inputsql,string inputval)
        {
            
            string sql = "select "+ outsql+" from "+table+" where "+inputsql+"=@"+inputsql+";";
            //MessageBox.Show(sql);
            cnn.Open();
            cmd = new SqlCommand(sql,cnn);
            cmd.Parameters.AddWithValue("@"+inputsql, inputval);
            object result=cmd.ExecuteScalar();
            cnn.Close ();
            string outval="";
            if (result != null) 
            {
                outval = result.ToString();
                //MessageBox.Show(outval);
            }

            return outval;

        }
        public string selectone(String outsql, String table, string inputsql, string inputval, string inputsql2, string inputval2)
        {

            string sql = "select " + outsql + " from " + table + " where " + inputsql + "='" + inputval + "' and " + inputsql2 + "='" + inputval2 + "';";
            // MessageBox.Show(sql);
            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@" + inputsql, inputval);
            cmd.Parameters.AddWithValue("@" + inputsql2, inputval);

            object result = cmd.ExecuteScalar();
            cnn.Close();
            string outval = "";
            if (result != null)
            {
                outval = result.ToString();
                //MessageBox.Show(outval);
            }

            return outval;

        }


        public void Datagrid(string table,DataGridView dg)
        {
            string sql = "Select * from " + table;
            cnn.Open();
            adapter = new SqlDataAdapter(sql,cnn);
           // MessageBox.Show(sql);
            dt= new DataTable();    
            adapter.Fill(dt);
            dg.DataSource = dt;
            cnn.Close();

        }
        public void Datagrid(string table, DataGridView dg,string[] attri)
        {
            string attSql="";
            for(int i = 0; i < attri.Length; i++)
            {
                attSql = attSql + attri[i];
                if (i == attri.Length - 1)
                {
                    attSql = attSql + " ";
                }
                else
                {
                    attSql = attSql + ",";
                }

            }
            string sql = "Select "+attSql +" from " + table;

            cnn.Open();
            adapter = new SqlDataAdapter(sql, cnn);
             
            dt = new DataTable();
            adapter.Fill(dt);
            dg.DataSource = dt;
            cnn.Close();


        }

        public void UpdateData(string table, string Targetfield,string Targetval, string field, string fieldval)
        {
            string sql = "update "+table+" set "+ Targetfield+" =@"+ Targetfield+" where "+field+"=@" + field + ";";
           // MessageBox.Show (sql);
            cnn.Open ();
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@" + Targetfield, Targetval);
            cmd.Parameters.AddWithValue("@" + field, fieldval);
            cmd.ExecuteNonQuery ();
            cmd.Dispose ();
            cnn.Close();
        }

        public string[] selectcolumn(String outsql, String table, string inputsql, string inputval)
        {
            string[] returnstring;
            cnn.Open();

            string sql = "select " + outsql + " from " + table + " where " + inputsql + "='" + inputval + "';";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, cnn);
            
            //cmd.Parameters.AddWithValue("@" + inputsql, inputval);
            adapter.Fill(dt);
            
            returnstring=new string[dt.Rows.Count];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                returnstring[i] = dt.Rows[i].ItemArray[0].ToString();
            }
            cnn.Close();
            return returnstring;

            

        }

        

    }

   
   
}



