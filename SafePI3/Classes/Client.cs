using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePI3.Interfaces;

namespace SafePI3.Classes
{
    public class Client : IClient
    {
        public int ID { get; set; }
        public int EntranceTurn { get; set; }


        public List<char> QueueSequence { get; set; }
        public List<int> QueueAllEntrances { get; set; }

        public int TurnsInSystem { get; set; }


        public int currentQueueSequence { get; set; }
        
        public bool atendimento { get; set; }
        public int beginAtendimento { get; set; }
    }
}
