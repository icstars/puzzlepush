using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Diagnostics;

namespace puzzlepush
{
    public class BoardController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post(SerialArray array)
        {
            Console.Write("hello");
            //string json;
            string query = "";
            SqlCommand insertname = new SqlCommand();
            List<string> listboard = new List<string>();

            try
            {
                foreach (string image in array.arrayboard)
                {
                    listboard.Add(image);
                }
                string header = "INSERT INTO poz ";
                string cols = "(poz0";
                string vals = "VALUES (@" + listboard[0] + "";

                for (int i = 1; i < listboard.Count; i++)
                {
                    cols += ",poz" + i;
                    vals += ",@" + listboard[i] + "";
                }
                cols += ") ";
                vals += ")";

                query = header + cols + vals;
                insertname.CommandType = CommandType.Text;
                insertname.CommandText = query;



                var conn = connect("puzzlepush");
                insertname = new SqlCommand(query, conn);
                Debug.Write(insertname);  

                for (int k = 0; k < listboard.Count; k++)
                {
                    string valparm = "@" + k + "";
                    //insertname.Parameters.AddWithValue(valparm, listboard[k]);

                    insertname.Parameters.Add(new SqlParameter(valparm, listboard[k]));
                }

                
                //int recordsin = insertname.ExecuteNonQuery();

                conn.Close();

                //json = JsonConvert.SerializeObject(insertname);

            }
            catch (Exception ex)
            {
                //json = JsonConvert.SerializeObject("saveboard" + ex);
                Debug.WriteLine(ex.Message);
                //Console.WriteLine();
            }


            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        
        public static SqlConnection connect(string db)
        {
            SqlConnection conn = null;
            try
            {
                string ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=" + db + ";Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
                conn = new SqlConnection(ConnectionString);

                //open the connection to the database
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return conn;
        }
    }
}