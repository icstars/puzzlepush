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

        static SqlCommand insertname;
        static SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                //Response.Write(recordstart("2013-06-30"));
                Response.Write(gettrivia());
            }
            catch (Exception ex)
            {
                string json = JsonConvert.SerializeObject(ex);
                Console.WriteLine(json);
            }
        }


        [WebMethod]
        public static string recordstart(string datestring)
        {
            string json;
            try
            {
                connect();

                string inser = "addip";
                insertname = new SqlCommand(inser, conn);
                insertname.CommandType = CommandType.StoredProcedure;

                string sip = HttpContext.Current.Request.UserHostAddress;
                json = JsonConvert.SerializeObject(sip);

                SqlParameter parmdate = new SqlParameter("@startdate",datestring);
                SqlParameter parmip = new SqlParameter("@ip", sip);
                insertname.Parameters.Add(parmip);
                insertname.Parameters.Add(parmdate);
                //insertname.ExecuteNonQuery();
                
            }

            catch (Exception ex)
            {
                //write any error messages
                json = JsonConvert.SerializeObject(ex);

            }
            return json;
        }

        public static void connect()
        {
            string ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=puzzlepush;Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";
            conn = new SqlConnection(ConnectionString);

            //open the connection to the database
            conn.Open();
        }

        [WebMethod]
        public static string gettrivia()
        {
            string json;
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            try
            {
                connect();
                da = new SqlDataAdapter("select * from trivia where id = 1", conn);
                da.Fill(ds, "trivia");

                json = JsonConvert.SerializeObject(ds);

            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(ex);
            }

            return json;
        }
    }
}