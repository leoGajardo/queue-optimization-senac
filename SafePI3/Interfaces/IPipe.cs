using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePI3.Classes;
using SafePI3.UserControls;

namespace SafePI3.Interfaces
{
    public interface IPipe
    {
        string Name { get; set; }

        string Label { get; set;  }

        int PipesQuantity { get; set; }

        int ServiceDesksQuantity { get; set; }

        int OperatorsQuantity { get; set; }

        List<Client> Clients { get; set; }
        
        PipeUC PipeUserControl { get; set; }
    }
}
