using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Diagnostics;
using Newtonsoft.Json;

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
        public Board Get(int id)
        {
           
            string query = "SELECT * FROM poz1 WHERE poz_id=@poz_id";
            SqlCommand insertname = new SqlCommand();
            List<string> listboard = new List<string>();
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            var board = new Board { Id = id };
            try
            {
                insertname.CommandType = CommandType.Text;
                insertname.CommandText = query;

                var conn = connect("puzzlepush");
   
                insertname = new SqlCommand(query, conn);
                insertname.Parameters.Add(new SqlParameter("@poz_id", id));
                
                da = new SqlDataAdapter(query, conn);
                da.Fill(ds, "loadboard");
                DataTable dt = new DataTable();
                dt = ds.Tables["loadboard"];
                
                conn.Close();

                List<string> stringlist = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = (string)dt.Rows[i]["loadboard"];
                    stringlist.Add(s);

                }

                //makes a 2D array from the dataTable List
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        String n = stringlist[i];
                        board.arrayboard[i, j] = n;
                    };
                }
            }

            catch (Exception ex)
            {
                //json = JsonConvert.SerializeObject("saveboard" + ex);
                Debug.WriteLine(ex.Message);
                
            }

            return board;            
        }

        // POST api/<controller>
        public void Post(Board board)
        {
            
            List<string> listboard = new List<string>();

            try
            {
                foreach (string image in board.arrayboard)
                {
                    listboard.Add(image);
                }
                
                var conn = connect("puzzlepush");
                SqlCommand insertname = new SqlCommand("insertboard", conn);
                insertname.CommandType = CommandType.StoredProcedure;
                Debug.Write(insertname);  
                insertname.Parameters.Add(new SqlParameter("@P_id",board.playerid));
                for (int k = 0; k < listboard.Count; k++)
                {
                    string valparm = "@poz" + k + "";
                    //insertname.Parameters.AddWithValue(valparm, listboard[k]);

                    insertname.Parameters.Add(new SqlParameter(valparm, listboard[k]));
                }

                
                var boardID  = insertname.ExecuteScalar();
                board.Id = Convert.ToInt32(boardID);
                conn.Close();

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