using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            string ConnectionString;
            ConnectionString = "Password=!31497Oo;User ID=dbdev;Initial Catalog=HW;Integrated Security=True;Trusted_Connection=No;Data Source=ics-c28-02.cloudapp.net";

            SqlConnection objConn = new SqlConnection(ConnectionString);

            SqlDataAdapter MyAdapter = new SqlDataAdapter("dbo.hello_world", objConn);



            DataSet ds = new DataSet();
            try
            {
                objConn.Open();
                MyAdapter.Fill(ds, "hw");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            // DataRow drow = ds.Tables["hw"].Rows[0];
            // string retval = drow["username"].ToString();

            string json = JsonConvert.SerializeObject(ds);
            Response.Write(json);
        }
    }
}