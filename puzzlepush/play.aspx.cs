using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace puzzlepush
{
    

    public partial class play : System.Web.UI.Page
    {
        static Random number = new Random();
        
        static SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string json;
           
            try
            {
                //Response.Write(recordstart("2013-06-30"));
                //Response.Write(gettrivia());
                //json = getarray();
                //Response.Write(json);
                //Response.Write(getjson());
                string[,] board = new string[,] { { "1", "2", "3", "4", "5" }, { "6", "7", "8", "9", "10" }, { "11", "12", "13", "14", "15" }, { "16", "17", "18", "19", "20" }, { "21", "22", "23", "24", "25" } };
                //string[,] board1 = arrayer();
                Response.Write(saveboard(board));
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(ex);
                Console.WriteLine(json);
            }
        }


        [WebMethod]
        public static string recordstart(string datestring)
        {
            string json;
            try
            {
                connect("puzzlepush");

                string insert = "addip";
                SqlCommand insertname = new SqlCommand(insert, conn);
                insertname.CommandType = CommandType.StoredProcedure;
                
                // gets the IP from the jQuery request
                string sip = HttpContext.Current.Request.UserHostAddress;
                json = JsonConvert.SerializeObject(sip);
                
                // add the IP and date we get as parameters to a stored procedure
                SqlParameter parmdate = new SqlParameter("@startdate",datestring);
                SqlParameter parmip = new SqlParameter("@ip", sip);
                insertname.Parameters.Add(parmip);
                insertname.Parameters.Add(parmdate);
                // run the stored procedure, which updates the database with user start time and IP
                insertname.ExecuteNonQuery();
                conn.Close();
            }

            catch (Exception ex)
            {
                //write any error messages
                json = JsonConvert.SerializeObject(ex);
                Console.WriteLine("recordstart" + ex.Message);
            }
            return gettrivia();
        }

        

        public static void connect(string db)
        {
            try
            {
                string ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog="+db+";Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
                conn = new SqlConnection(ConnectionString);

                //open the connection to the database
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [WebMethod]
        public static string gettrivia()
        {
            string json;
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            try
            {
                connect("puzzlepush");
                //gets a piece of trivia from the database returns it as a JSON string
                da = new SqlDataAdapter("selecttriviatip", conn);
                da.Fill(ds, "trivia");

                json = JsonConvert.SerializeObject(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(ex);
                Console.WriteLine("gettrivia"+ex.Message);
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
                Console.WriteLine(json);
            }

            return json;
        }

       
        [WebMethod]
        public static string saveboard(string[,] arrayboard)
        {
            string json;
           
            string query="";
            SqlCommand insertname = new SqlCommand();
            
            List<string> listboard = new List<string>();
            List<string> listcol = new List<string>();

            try
            {
                

                foreach (string image in arrayboard)
                {
                    listboard.Add(image);
                }
                string header = "INSERT INTO poz ";
                string cols = "(poz0";
                string vals = "VALUES ('@" + listboard[0] + "'";

                for (int i = 1; i < listboard.Count; i++)
                {
                    
                    cols += ",poz" + i;
                    vals += ",'@" + listboard[i] + "'";

                }
                cols += ") ";
                vals += ")";
                
                query = header + cols + vals;
                
                insertname = new SqlCommand(query, conn);
                for(int k = 0; k<listboard.Count;k++)
                {
                    string parm = "@1,"+listboard[k+1]+"";
                    insertname.Parameters.AddWithValue(parm, listboard[k]);
                }

                json = JsonConvert.SerializeObject(insertname);
                insertname.CommandType = CommandType.Text;
                
                connect("puzzlepush");
                insertname.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject("saveboard"+ex);
                Console.WriteLine(json);
            }
            
            
            return query;


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
                Console.WriteLine("arrayer" + ex.Message);
            }
            
            return myarray;
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
                Console.WriteLine(ex.Message);
            }
            return stringlist;
        }

        public static int randomnumber()
        {
            //Return a random number between 0 and 4
            int rn = number.Next(0, 5);
            return rn;
        }

        public static DataTable getimagenames()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                connect("puzzlepush");
                //connects to the database and retrieves the image names stores it in a Datatable
                SqlDataAdapter Adapterimgnames = new SqlDataAdapter("select name from images", conn);
                Adapterimgnames.Fill(ds, "imgnames");
                dt = ds.Tables["imgnames"];
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;

        }

        [WebMethod]
        public static string getjson()
        {
            string json;
            // define a connection to the database
            try
            {
                connect("HW");
                //run an sql query and create a dataset to store the result
                SqlDataAdapter MyAdapter = new SqlDataAdapter("select * from HW", conn);
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
                //Console.Write(ex.Message);
                json = JsonConvert.SerializeObject(ex);
                Console.WriteLine(json);
            }

            return json;


        }

    }
}