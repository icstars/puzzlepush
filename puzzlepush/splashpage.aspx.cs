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
    public partial class splashpage : System.Web.UI.Page
    {

        static SqlCommand insertname;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                Response.Write(todatabase("2013-06-30"));
                
            }
            catch (Exception ex)
            {
                string json = JsonConvert.SerializeObject(ex);
                Console.WriteLine(json);
            }
        }


        [WebMethod]
        public static string todatabase(string datestring)
        {
            string json;
            try
            {
                string ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=puzzlepush;Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
                SqlConnection objConn = new SqlConnection(ConnectionString);
                
                
                //open the connection to the database
                objConn.Open();
                string inser = "insertimagesbyname";
                //string inser = "insertimages";
                insertname = new SqlCommand(inser, objConn);
               
                string sip = HttpContext.Current.Request.UserHostAddress;
                json = JsonConvert.SerializeObject(sip);

                SqlParameter parmdate = new SqlParameter("date",datestring);
                SqlParameter parmip = new SqlParameter("ip", sip);
                insertname.Parameters.Add(parmdate);
                insertname.Parameters.Add(parmip);
                //insertname.ExecuteNonQuery();
                
            }

            catch (Exception ex)
            {
                //write any error messages
                Console.WriteLine(ex.Message);
                json = JsonConvert.SerializeObject(ex);

            }
            return json;
        }
    }
}