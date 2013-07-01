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
    public partial class randomarray : System.Web.UI.Page
    {
        //List<string> mylist = new List<string>();
        string[,] mylist = new string[5, 5];
        Random number = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            arrayer();
            string json = JsonConvert.SerializeObject(mylist);
            Response.Write(json);
        }

        public void arrayer()
        {
            List<string> names = new List<string>() { "Here", "Are", "Some", "Random", "Strings", "Six" };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string n = names[randomnumber()];
                    //mylist.Add(n);
                    mylist[i, j] = n;
                };
            }

        }

        public int randomnumber()
        {
            int rn = number.Next(0, 6);
            return rn;
        }
    }
}