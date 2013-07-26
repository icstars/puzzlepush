using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace puzzlepush
{
    

    public partial class play : System.Web.UI.Page
    {
        static Random number = new Random();
        static SqlConnection conn;
        static int recordsin;

        protected void Page_Load(object sender, EventArgs e)
        {
            string json;

           
            try
            {
                //Response.Write(recordstart("30-12-1111"));
                //string[,] board = new string[,] { { "1", "2", "3", "4", "5" }, { "6", "7", "8", "9", "10" }, { "11", "12", "13", "14", "15" }, { "16", "17", "18", "19", "20" }, { "21", "22", "23", "24", "25" } };
                //string[,] board1 = arrayer();
                //Response.Write(saveboard(board));
                //Response.Write(recordscore("45"));
                //json = JsonConvert.SerializeObject(saveboard(board));
                //Response.Write(getjson());
                //Response.Write(recordsin.ToString());


            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(ex);
                Debug.WriteLine(json);
            }
        }




        [WebMethod]
        public static string recordstart(string datestring)
        {
            string json;
            try
            {
                string insert = "addip";
                SqlCommand insertname = new SqlCommand(insert, connect("puzzlepush"));
                insertname.CommandType = CommandType.StoredProcedure;
                
                // gets the IP from the jQuery request
                string sip = HttpContext.Current.Request.UserHostAddress;
                json = JsonConvert.SerializeObject(sip);
                
                // add the IP and date we get as parameters to a stored procedure
                SqlParameter dateparm = new SqlParameter("@startdate",datestring);
                SqlParameter ipparm = new SqlParameter("@ip", sip);
                insertname.Parameters.Add(ipparm);
                insertname.Parameters.Add(dateparm);
                // run the stored procedure, which updates the database with user start time and IP
                insertname.ExecuteNonQuery();
                conn.Close();
            }

            catch (Exception ex)
            {
                //write any error messages
                json = JsonConvert.SerializeObject(ex);
                Debug.WriteLine("recordstart" + ex.Message);
            }
            return gettrivia();
        }

        [WebMethod]
        public static string gettrivia()
        {
            string json;
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            try
            {
                
                //gets a piece of trivia from the database returns it as a JSON string
                da = new SqlDataAdapter("selecttriviatip", connect("puzzlepush"));
                da.Fill(ds, "trivia");

                json = JsonConvert.SerializeObject(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(ex);
                Debug.WriteLine("gettrivia"+ex.Message);
            }

            return json;
        }

        [WebMethod]
        public static string postboard(List<string> board)
        {
            string json;
            List<string> listboard = new List<string>();

            try
            {
                foreach (string image in board)
                {
                    listboard.Add(image);
                }
                
                var conn = connect("puzzlepush");
                SqlCommand insertname = new SqlCommand("insertboard", conn);
                insertname.CommandType = CommandType.StoredProcedure;
                Debug.Write(insertname);
                string id = getpid().ToString();
                insertname.Parameters.Add(new SqlParameter("@P_Id","3"));
                for (int k = 0; k < listboard.Count; k++)
                {
                    string valparm = "@poz" + k + "";

                    insertname.Parameters.Add(new SqlParameter(valparm, listboard[k]));
                }

                
                var recordsin  = insertname.ExecuteNonQuery();
                
                conn.Close();
                json = "success";

            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject("postboard" + ex);
                Debug.WriteLine(ex.Message);
                //Console.WriteLine();
            }


            return json;
        }

        [WebMethod]
        public static string loadboard(int id)
        {
            string json;
            string query = "SELECT * FROM poz1 WHERE P_id = 2";
            SqlCommand insertname = new SqlCommand();
            List<string> listboard = new List<string>();
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            //var board = new Board { boardid = id };
            try
            {
                insertname.CommandType = CommandType.Text;
                insertname.CommandText = query;

                var conn = connect("puzzlepush");

                insertname = new SqlCommand(query, conn);
                insertname.Parameters.Add(new SqlParameter("@P_Id", "1"));

                da = new SqlDataAdapter(query, conn);
                da.Fill(ds, "loadboard");
                DataTable dt = new DataTable();
                dt = ds.Tables["loadboard"];

                conn.Close();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = (string)dt.Rows[i]["loadboard"];
                    listboard.Add(s);

                }

                json = JsonConvert.SerializeObject(listboard);
            }

            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject("saveboard" + ex);
                Debug.WriteLine(ex.Message);

            }

            return json;
        }

        [WebMethod]
        public static string recordscore(int id, int score)
        {
            string json;
            

            try
            {
                SqlConnection conn = connect("puzzlepush");
            
                SqlCommand insertname = new SqlCommand("UpdateScore", conn);
                insertname.CommandType = CommandType.StoredProcedure;
            
                int playerscore = score;
                insertname.Parameters.Add(new SqlParameter("@P_Id", id));
                insertname.Parameters.Add(new SqlParameter("@score", score));
                insertname.ExecuteNonQuery();
                conn.Close();

                json = "success";
            }

            catch (Exception ex)
            {
                //write any error messages
                json = JsonConvert.SerializeObject(ex);
                Debug.WriteLine("recordstart" + ex.Message);
            }

            return json;
        
        }


        [WebMethod]
        public static string getarray()
        {
            string json;
            try
            {
                //use arrayer to make random array of 25 elements, return as JSON
                json = JsonConvert.SerializeObject(arrayer());

            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject("getarray" + ex.Message);
                Debug.WriteLine(json);
            }

            return json;
        }

        public static string[,] arrayer()
        {
            string[,] myarray = new string[5, 5];
            List<String> names = makelist();
            
            try
            {
                //makes a 2D array and adds a random element from the dataTable List to it
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        String n = names[randomnumber()];
                        myarray[i, j] = n;
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("arrayer" + ex.Message);
            }
            
            return myarray;
        }

        [WebMethod]
        public static string getjson()
        {
            string json;
            // define a connection to the database
            try
            {
               
                //run an sql query and create a dataset to store the result
                SqlDataAdapter MyAdapter = new SqlDataAdapter("select * from scores", connect("puzzlepush")); 
                DataSet ds = new DataSet();
                
                //fill the dataset with the results from the sql query and name the table 'hw'
                
                MyAdapter.Fill(ds, "hw");
                //Using Newtonsoft's JSON.NET, convert our dataset object from C# to JSON (JavaScript Object Notation)
                json = JsonConvert.SerializeObject(ds);
                conn.Close();
            }

            catch (Exception ex)
            {
                //write any error messages
                //Debug.Write(ex.Message);
                json = JsonConvert.SerializeObject(ex);
                Debug.WriteLine(json);
            }

            return json;
        }
        [WebMethod]
        public static string getpid()
        {
            string json;
            //int pid;
            // define a connection to the database
            try
            {
                string sip = HttpContext.Current.Request.UserHostAddress;
                //json = JsonConvert.SerializeObject(sip);
                //run an sql query and create a dataset to store the result
                SqlDataAdapter MyAdapter = new SqlDataAdapter("select P_Id from splashPage WHERE ipAddress ="+sip+"", connect("puzzlepush"));
                DataSet ds = new DataSet();

                //fill the dataset with the results from the sql query and name the table 'hw'

                MyAdapter.Fill(ds, "hw");
                json = JsonConvert.SerializeObject(ds);
                //pid = ds.Tables["hw"].Rows["P_Id"][0];
                //Using Newtonsoft's JSON.NET, convert our dataset object from C# to JSON (JavaScript Object Notation)
                
                conn.Close();
            }

            catch (Exception ex)
            {
                //write any error messages
                //Debug.Write(ex.Message);
                json = JsonConvert.SerializeObject(ex);
                Debug.WriteLine(json);
            }

            return json;
        }
        public static SqlConnection connect(string db)
        {
            try
            {
                string ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=" + db + ";Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
                conn = new SqlConnection(ConnectionString);

                //open the connection to the database
                conn.Open();
            }
            catch (Exception ex)
            {      
                Debug.WriteLine(ex.Message);
            }

            return conn;
        }

        public static List<String> makelist()
        {
            //takes the DataTable and casts it to a string List
            List<String> stringlist = new List<String>();
            DataTable dt = getimagenames();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = (string)dt.Rows[i]["name"];
                    stringlist.Add(s);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return stringlist;
        }

        public static DataTable getimagenames()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                //connects to the database and retrieves the image names stores it in a Datatable
                SqlDataAdapter Adapterimgnames = new SqlDataAdapter("select name from images", connect("puzzlepush"));
                Adapterimgnames.Fill(ds, "imgnames");
                dt = ds.Tables["imgnames"];
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return dt;

        }

        public static int randomnumber()
        {
            //Return a random number between 0 and 4
            int rn = number.Next(0, 5);
            return rn;
        }

    }
}