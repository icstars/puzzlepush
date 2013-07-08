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
    public partial class randomarray : System.Web.UI.Page
    {
        static Random number = new Random();
        static DataSet ds = new DataSet();
        static DataTable dt = new DataTable();



        protected void Page_Load(object sender, EventArgs e)
        {
            

            string json = JsonConvert.SerializeObject(ds);
            Response.Write(json);
        }

        [WebMethod]
        public static string getarray()
        {
            //connect and use arrayer, return as JSON
            connect();

            string json = JsonConvert.SerializeObject(arrayer());
            return json;
        }

        public static Array arrayer()
        {
            //makes a 2D array and adds each element from the DataTable list to it
            string[,] myarray = new string[5, 5];
            List<String> names = makelist();
            List<String> namestest = new List<String>() {"A","B","C","D","E" };
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    String n = names[randomnumber()];
                    //String n = namestest[randomnumber()];
                    myarray[i, j] = n;
                };
            }

            return myarray;
        }

        public static List<String> makelist()
        {
            //takes the DataTable and casts it to a String list
            List<String> stringlist = new List<String>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = (string)dt.Rows[i]["image_name"];
                stringlist.Add(s);
                
            }
            return stringlist;
        }

        public static int randomnumber()
        {
            //Return a random number between 0 and 4
            int rn = number.Next(0, 5);
            return rn;
        }

        public static void connect()
        {
            try
            {
                string ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=puzzlepush;Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
                SqlConnection objConn = new SqlConnection(ConnectionString);

                //run an sql query and create a dataset to store the result
                SqlDataAdapter MyAdapter = new SqlDataAdapter("select image_name from images", objConn);

                //open the connection to the database
                //fill the dataset with the results from the sql query and name the table 'hw'
                objConn.Open();
                MyAdapter.Fill(ds, "ra");
                dt = ds.Tables["ra"];
                //objConn.Close();
            }

            catch (Exception ex)
            {
                //write any error messages
                Console.WriteLine(ex.Message);

            }
        }
    }
}