using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SafePI3
{
    public class ClientDTO
    {
        public int ID { get; set; }
        public int EntranceTurn{ get; set; }
        public List<char> QueueSequence { get; set; }
        public ClientDTO(string line)
        {

            string pattern = @"^U([\d]+)C([\d]+)(\w+)$";
            Match result = Regex.Match(line.Trim(), pattern);
            try
            {
                ID = Int32.Parse(result.Groups[1].Value);
                EntranceTurn = Int32.Parse(result.Groups[2].Value);
                QueueSequence = result.Groups[3].Value.ToCharArray().ToList();
            }
            catch (Exception)
            {

            }
            

        }

    }
}
