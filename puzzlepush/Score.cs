using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace puzzlepush
{
    [DataContract]
    public class Score
    {
        private int privatescore;

        [DataMember]
        public int thescore
        {
            get { return privatescore; }
            set { privatescore = value; }
        }

        [DataMember]
        public int playerid
        {
            get;
            set;
        }
    }
}