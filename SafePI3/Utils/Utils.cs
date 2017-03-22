using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePI3.Utils
{
    static class Utils
    {

        static public int getTotalClients(this MainForm form)
        {
            return form.Pipes.Sum(a => a.Value.Clients.Count);
        }

    }
}
