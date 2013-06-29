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
    public partial class hw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // this was our first test to see if json was being serialized, the jquery test has replaced this need.
           // string j = getjson();
           // Response.Write(j);
        }

        [WebMethod]
        public static string getjson()
        {
            // define a connection to the database 
            string ConnectionString;
            ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=HW;Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
            SqlConnection objConn = new SqlConnection(ConnectionString);

            //run an sql query and create a dataset to store the result
            SqlDataAdapter MyAdapter = new SqlDataAdapter("selecthelloworldbyid'1'", objConn);
            DataSet ds = new DataSet();

            try
            {
                //open the connection to the database
                //fill the dataset with the results from the sql query and name the table 'hw'
                objConn.Open();
                MyAdapter.Fill(ds, "hw");
            }

            catch (Exception ex)
            {
                //write any error messages
                Console.Write(ex.Message);
            }

            //Using Newtonsoft's JSON.NET, convert our dataset object from C# to JSON (JavaScript Object Notation)
            string json = JsonConvert.SerializeObject(ds);
            
            return json;

            
        }
       

    }
}