using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace puzzlepush
{
    public class Score
    {
        private string privatescore;

        public string Scores
        {
            get { return privatescore; }
            set { privatescore = value; }
        }

        public int scoreid
        {
            get;
            set;
        }
    }
}