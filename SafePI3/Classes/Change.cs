using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePI3.Classes
{
    public class Change
    {
        public string FromQueue;
        public string ToQueue;
        public int RoundDeparture;
        public int RoundArrival;


        public Change(string _FromQueue, string _ToQueue, int _RoundDeparture, int _RoundArrival)
        {
            FromQueue = _FromQueue;
            ToQueue = _ToQueue;
            RoundDeparture = _RoundDeparture;
            RoundArrival = _RoundArrival;
        }



    }
}
