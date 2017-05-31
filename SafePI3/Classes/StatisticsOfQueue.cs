using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace SafePI3.Classes
{
    public class StatisticsOfQueue
    {
        public int Operators { get; set; }
        public List<Client> AllClients { get;  set; }
        public int CurrentTurn { get;  set; }
        public double ProbabilityOfIdle { get;  set; }
        public double QueueCost { get;  set; }


        public StatisticsOfQueue(int _operators, List<Client> _clients, int _currentTurn, int _queueCost)
        {
            Operators = _operators;
            AllClients = _clients;
            CurrentTurn = _currentTurn;
            QueueCost = _queueCost;
            
            UpdateStatistics();  
        }

        public void UpdateStatistics()
        {
            double alpha = AllClients.Count() / QueueCost;
            double mi = 1;

            ProbabilityOfIdle = 0;

            if (Operators == 0)
            {
                ProbabilityOfIdle = 0;
                return;
            }
            if (AllClients.Count() == 0)
            {
                ProbabilityOfIdle = double.MaxValue;
                return;
            }

            for (int n = 0; n < Operators; n++)
            {
                ProbabilityOfIdle += Math.Pow((Math.Pow((alpha / mi), n) / SpecialFunctions.Factorial(n))
                    + ((Math.Pow((alpha / mi), Operators)) / (SpecialFunctions.Factorial(Operators) * (1 - (alpha / (Operators * mi))))),-1);
            }

            ProbabilityOfIdle *= 100;
            if (double.IsInfinity(ProbabilityOfIdle))
                ProbabilityOfIdle = double.MaxValue;
        }
    }
}
