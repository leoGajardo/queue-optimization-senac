using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePI3.Classes;
using SafePI3.UserControls;
using System.Collections.ObjectModel;

namespace SafePI3.Interfaces
{
    public interface IQueue
    {
        string Name { get; set; }

        string Label { get; set;  }

        int QueuesQuantity { get; set; }

        int ServiceDesksQuantity { get; set; }

        int OperatorsQuantity { get; set; }

        ObservableCollection<Client> Clients { get; set; }
        
        QueueUC QueueUserControl { get; set; }
    }
}
