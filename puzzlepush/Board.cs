using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace puzzlepush
{
    [DataContract]
    public class Board
    {
        private string[,] array;

        [DataMember]
        public string[,] arrayboard
        {

            get { return array; }
            set { array = value; }
        }

        [DataMember]
        public int Id
        {
            get;
            set;
        }
        [DataMember]
        public int playerid
        {
            get;
            set;
        }
    }
}