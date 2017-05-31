using SafePI3.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafePI3.Classes
{
    public class QueueManager : IDisposable
    {
        public Dictionary<string, Queue> Queues { get; private set; }
        private int ChangeFee;
        public int CurrentTurn { get; private set; }

        private MainForm form;

        private List<Change> ListChanges;
        private List<Change> LogListChanges;

        private Dictionary<string, StatisticsOfQueue> statictics;
        
        private StreamReader QueueFile;
        private string CurrentLine;
        private bool arquivoAcabou;
        private bool PausedQueue;

        TurnSpeed speed;

        static System.Windows.Forms.Timer GeneralTimer = new System.Windows.Forms.Timer();

        List<Client> FinishedClients = new List<Client>();

        public QueueManager(MainForm _form)
        {
            Queues = new Dictionary<string, Queue>();

            form = _form;
            ListChanges = new List<Change>();
            LogListChanges = new List<Change>();
            statictics = new Dictionary<string, StatisticsOfQueue>();

            if (File.Exists(Application.StartupPath + "\\LogChanges.txt"))
            {
                File.Delete(Application.StartupPath + "\\LogChanges.txt");
                File.CreateText(Application.StartupPath + "\\LogChanges.txt").Close();
            }

            if (File.Exists(Application.StartupPath + "\\LogIddleRatings.txt"))
            {
                File.Delete(Application.StartupPath + "\\LogIddleRatings.txt");
                File.CreateText(Application.StartupPath + "\\LogIddleRatings.txt").Close();
            }
                
        }

        public bool LoadSetupFile()
        {
            if (!File.Exists(Application.StartupPath + "\\Configs\\Setup.txt"))
            {
                MessageBox.Show("Arquivo de Setup não existente, por favor selecione um", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Queues = new Dictionary<string, Queue>();

            StreamReader file = new StreamReader(Application.StartupPath + "\\Configs\\Setup.txt", Encoding.Default);
            string line = "";
            // primeira linha
            line = file.ReadLine();
            int atendentesNumero = 0;
            if (!Int32.TryParse(line, out atendentesNumero))
            {
                MessageBox.Show("Problema na leitura do número de atendentes", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return false;
            }
            // segunda linha
            List<char> postos = new List<char>();
            line = file.ReadLine();
            postos.AddRange(line.Split(':')[1].ToCharArray());
            if (postos.Count() < 5 || postos.Count() > 21)
            {
                MessageBox.Show("Número de postos não atende as regras", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return false;
            }

            //terceira linha
            List<char> atendentes = new List<char>();
            line = file.ReadLine();
            atendentes.AddRange(line.Split(':')[1].ToCharArray());
            if (atendentes.Count() != atendentesNumero)
            {
                MessageBox.Show("Número de atendentes não corresponde a disposição dos mesmos nos postos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return false;
            }

            //quarta linha
            line = file.ReadLine();
            int troca = 0;
            if (!Int32.TryParse(line.Split(':')[1], out troca))
            {
                MessageBox.Show("Problema na leitura do valor da troca", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return false;
            }

            ChangeFee = troca;

            //postos
            while ((line = file.ReadLine()) != null)
            {
                char posto;
                int tempo = 0;
                byte[] label = null;

                if (!Char.TryParse(line.Split(':')[0].Substring(0, 1), out posto) || !postos.Contains(posto))
                {
                    MessageBox.Show("Problema na leitura dos postos e seus tempos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.Close();
                    return false;
                }

                if (!Int32.TryParse(line.Split(':')[0].Substring(1), out tempo))
                {
                    MessageBox.Show("Problema na leitura dos postos e seus tempos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.Close();
                    return false;
                }

                if (line.Split(':').Count() > 1)
                    label = Encoding.Default.GetBytes(line.Split(':')[1]);

                Queue novaFila = new Queue(Encoding.Default.GetString(label), posto.ToString(), atendentes.Count(a => a == posto), 1, postos.Count(a => a == posto), tempo);
                Queues.Add(posto.ToString(), novaFila);
            }
            file.Close();
            return true;
        }


        public void StartQueue(TurnSpeed _speed)
        {
            CurrentTurn = 1;
            QueueFile = new StreamReader(Application.StartupPath + "//Configs//Queue.txt");
            form.UpdateTurn(CurrentTurn.ToString());
            Application.DoEvents();
            speed = _speed;
            arquivoAcabou = false;
            CurrentLine = "";
            PausedQueue = false;
            GeneralTimer.Tick += new EventHandler(NextStep);
            GeneralTimer.Interval = 1000/(int)speed;
            GeneralTimer.Start();

        }

        public void NextStep(Object myObject, EventArgs myEventArgs)
        {

            DateTime tempo = DateTime.Now;
            
            tempo = tempo.AddMilliseconds(1000 / (int)speed);
            ClientDTO currentClientDTO;

            //Tratando entrada

            if (!arquivoAcabou)
            {
                if (String.IsNullOrWhiteSpace(CurrentLine))
                    CurrentLine = QueueFile.ReadLine();

                currentClientDTO = new ClientDTO(CurrentLine);


                while (currentClientDTO.EntranceTurn == CurrentTurn && !arquivoAcabou)
                {
                    Queues[currentClientDTO.QueueSequence[0].ToString()].Clients.Add(
                        new Client()
                        {
                            ID = currentClientDTO.ID,
                            QueueSequence = currentClientDTO.QueueSequence,
                            EntranceTurn = currentClientDTO.EntranceTurn,
                            currentQueueSequence = 0,
                            atendimento = false,
                            TurnsInSystem = 0,
                            QueueAllEntrances = new List<int>() { CurrentTurn }
                        });

                    Queues[currentClientDTO.QueueSequence[0].ToString()].AllClients.Add(
                        new Client()
                        {
                            ID = currentClientDTO.ID,
                            QueueSequence = currentClientDTO.QueueSequence,
                            EntranceTurn = currentClientDTO.EntranceTurn,
                            currentQueueSequence = 0,
                            atendimento = false,
                            TurnsInSystem = 0,
                            QueueAllEntrances = new List<int>() { CurrentTurn }
                        });

                    CurrentLine = QueueFile.ReadLine();
                    if (CurrentLine != null)
                        currentClientDTO = new ClientDTO(CurrentLine);
                    else { 
                        arquivoAcabou = true;
                        QueueFile.Close();
                    }
                }
            }


            //tratando movimentação de Clientes em atendimento
            foreach (Queue q in Queues.Where(q => q.Value.Clients.Count() > 0).Select(a => a.Value))
            {
                List<Client> clientsToRemove = new List<Client>();
                foreach (Client c in q.Clients.Where(a => a.atendimento)){
                    if(CurrentTurn - c.beginAtendimento  == q.TimePerClient)
                    {
                        c.atendimento = false;
                        if (c.currentQueueSequence == 0)
                            c.QueueAllEntrances.Add(CurrentTurn);
                        else
                            c.QueueAllEntrances.Add(CurrentTurn - c.QueueAllEntrances[c.currentQueueSequence - 1]);

                        if (c.currentQueueSequence + 1 == c.QueueSequence.Count)
                        {
                            //Tirando Clientes do sistema
                            c.TurnsInSystem = CurrentTurn - c.EntranceTurn ;
                            FinishedClients.Add(c);
                            clientsToRemove.Add(c);
                        }
                        else
                        {
                            //passando pra proxima fila
                            c.currentQueueSequence++;
                            Queues[c.QueueSequence[c.currentQueueSequence].ToString()].Clients.Add(c);
                            Queues[c.QueueSequence[c.currentQueueSequence].ToString()].AllClients.Add(c);
                            clientsToRemove.Add(c);
                        }
                        
                    }
                }
                q.Clients.RemoveAll(a => clientsToRemove.Contains(a));
            }

            // Chegada de Atendente

            if (ListChanges.Count(c => c.RoundArrival == CurrentTurn) > 0)
            {
                IList<Change> ArrivingChanges = new List<Change>();
                ArrivingChanges = ListChanges.Where(c => c.RoundArrival == CurrentTurn).ToList();

                foreach (Change item in ArrivingChanges)
                {
                    Queues[item.ToQueue].OperatorsQuantity += 1;
                    LogListChanges.Add(item);
                }

                
                ListChanges.RemoveAll(c => ArrivingChanges.Contains(c));
                
                form.UpdateChangesPanel(ListChanges);
            }


            // Otimização

            if (ChangeFee > 0 && Queues.Sum(q => q.Value.AllClients.Count()) > 20)
            {
                //Checar qual fila pode disponibilizar um atendente

                IList<Queue> AvaiableOperators = Queues
                                                    .Where(q => q.Value.OperatorsQuantity > q.Value.Clients.Count(c => c.atendimento))
                                                    .Select(q => q.Value)
                                                    .ToList();

                if (AvaiableOperators.Count() > 0)
                {
                    // Criar Estatisticas na primeira passada
                    if (statictics.Count() == 0)
                    {
                        foreach (Queue q in Queues.Select(q => q.Value))
                        {
                            statictics.Add(q.Name, new StatisticsOfQueue(q.OperatorsQuantity, q.AllClients.ToList(), CurrentTurn, q.TimePerClient));
                            statictics[q.Name].UpdateStatistics();
                        }
                    }

                    //Atualizando valores das estatisticas
                    foreach (var sq  in statictics)
	                {
		                sq.Value.Operators = Queues[sq.Key].OperatorsQuantity;
                        sq.Value.AllClients = Queues[sq.Key].AllClients.Where(
                                    c =>
                                        CurrentTurn - c.QueueAllEntrances[c.QueueSequence.IndexOf(sq.Key.ToCharArray()[0])] < 30)
                                        .ToList();
                        sq.Value.AllClients.AddRange(Queues[sq.Key].Clients);
                        sq.Value.AllClients = sq.Value.AllClients.Distinct().ToList();
                        sq.Value.CurrentTurn = CurrentTurn;
                        sq.Value.QueueCost = Queues[sq.Key].TimePerClient;
                        
                        sq.Value.UpdateStatistics();

                        StreamWriter logger = File.AppendText(Application.StartupPath + "\\Configs\\LogIddleRatings.txt");
                        logger.WriteLine(
                                    "Queue :" + sq.Key +
                                    "   IddleRating " + sq.Value.ProbabilityOfIdle +
                                    "   ClientsNumber " + sq.Value.AllClients.Count +
                                    "   CurrentTurn " + sq.Value.CurrentTurn);

                        logger.Close();
	                }
                    
                    var bla = statictics.Where(s => (AvaiableOperators.Count(ao => ao.Name == s.Key) > 0 || (s.Key == "A" && Queues["A"].Clients.Count() == 0 && arquivoAcabou) && s.Value.Operators > 1 )).OrderByDescending(s => s.Value.ProbabilityOfIdle).ThenByDescending(s => s.Value.Operators);

                    //Checar os cenários e os indices de cada fila com operador disponivel para mudança
                    foreach (var sq in bla.Where(q => q.Value.ProbabilityOfIdle > 0))
                    {
                        if (sq.Value.Operators > 0)
                        {

                            //Checar qual fila precisa de um atendente
                            var s1 = statictics.Where(
                                            s => (s.Value.ProbabilityOfIdle <= 0) || (s.Value.ProbabilityOfIdle / sq.Value.ProbabilityOfIdle <= 0.15) || (sq.Key == "A" && Queues["A"].Clients.Count() == 0 && arquivoAcabou));

                            s1 = s1.Where(s => ChangeFee / (Queues[s.Key].Clients.Count * s.Value.QueueCost) < 0.35 || (Queues[s.Key].Clients.Count > 0 && s.Value.Operators == 0));
                            //s1 = s1.Where(s => (Queues[s.Key].Clients.Count * s.Value.QueueCost) / s.Value.Operators > (Queues[sq.Key].Clients.Count * sq.Value.QueueCost) / sq.Value.Operators);

                            //|| (s.Value.Operators == 0 && s.Key != "A" && !arquivoAcabou)

                            s1 = s1.Where(s => Queues[s.Key].OperatorsQuantity + ListChanges.Count(lc => lc.ToQueue == s.Key) < Queues[s.Key].ServiceDesksQuantity);
                            s1 = s1.Where(s => ((arquivoAcabou && Queues["A"].Clients.Count == 0 && s.Key == "A") ? 0 : 1) == 1);
                            if (s1.Count() > 0) 
                            {
                                var min = s1.Min(s => s.Value.ProbabilityOfIdle);
                                s1 = s1.Where(s => s.Value.ProbabilityOfIdle == min);
                                s1 = s1.Where(s => s.Key != sq.Key);
                            }
                            
                            
                            

                            if (s1.Count() > 0)
                            {
                                ListChanges.Add(new Change(sq.Key, s1.First().Key , CurrentTurn, CurrentTurn + ChangeFee));
                                Queues[sq.Key].OperatorsQuantity -= 1;

                                //Log de trocas no sistema

                                StreamWriter f = File.AppendText(Application.StartupPath + "\\Configs\\LogChanges.txt");
                                LogListChanges.ForEach(
                                    (c) =>
                                        f.WriteLine(
                                            "Atendente saindo de " + c.FromQueue +
                                            " no round " + c.RoundDeparture +
                                            " e chegando em " + c.ToQueue +
                                            " no round " + c.RoundArrival +
                                            " que tem " + Queues[c.ToQueue].OperatorsQuantity + " operadores"));

                                f.Close();
                            }

                            
                        }
                    }

                    form.UpdateChangesPanel(ListChanges);
                }
            }

            //Colocando novos Clientes em atendimento
            foreach (Queue q in Queues.Where(q => q.Value.Clients.Count() > 0).Select(a => a.Value))
            {
                if (q.OperatorsQuantity < q.Clients.Count(c => c.atendimento)
                    || q.OperatorsQuantity == q.Clients.Count(c => c.atendimento))
                    continue;

                foreach (Client c in q.Clients
                    .Where(a => !a.atendimento)
                    .OrderBy(a => (a.currentQueueSequence == 0 ? a.EntranceTurn : a.QueueAllEntrances[a.currentQueueSequence - 1]))
                    .Take(q.OperatorsQuantity - q.Clients.Count(a => a.atendimento)))
                {
                    c.atendimento = true;
                    c.beginAtendimento = CurrentTurn;
                }
            }

            Console.WriteLine(Queues.SelectMany(a => a.Value.Clients).Count(a => a.TurnsInSystem == 0));
            if (Queues.SelectMany(a => a.Value.Clients).Count(a => a.TurnsInSystem == 0) > 0 || !arquivoAcabou)
            {
                if (!PausedQueue) { 
                    CurrentTurn++;
                    form.UpdateTurn(CurrentTurn.ToString());
                    form.Invalidate();
                    Application.DoEvents();
                    
                }
            }
            else
            {
                //Calculo de indicadores
                GeneralTimer.Stop();
                double mediumTimeSystem = FinishedClients.Average(a => a.TurnsInSystem);
                Dictionary<string, double> mediumPerQueue = new Dictionary<string, double>(Queues.Count);
                foreach (Queue q in Queues.Select(a => a.Value))
                {
                    mediumPerQueue.Add(q.Name,
                        FinishedClients
                            .Where(a => a.QueueSequence.IndexOf(char.Parse(q.Name)) > -1)
                            .Average(a => a.QueueAllEntrances[a.QueueSequence.IndexOf(char.Parse(q.Name))]));
                }
                Client MostWaitUser = FinishedClients.OrderByDescending(a => a.TurnsInSystem).First();

                Dictionary<string, double> mediumPerCombination = new Dictionary<string, double>();

                foreach (string sequence in FinishedClients.Select(a => a.QueueSequence).Select(a => string.Join("|", a)).Distinct())
                {
                    mediumPerCombination.Add(sequence , FinishedClients
                                                    .Where(a => String.Join("|", a.QueueSequence) == sequence)
                                                    .Average(a => a.TurnsInSystem));
                }



                form.PauseButton_Click(this, null);
                Results resultado = new Results();
                resultado.LoadResults(mediumTimeSystem, mediumPerQueue, MostWaitUser, mediumPerCombination, CurrentTurn);
                resultado.Show();
                


            }
                

        }
        
        public void PauseQueue()
        {
            PausedQueue = true;
        }

        public void SetSpeed(TurnSpeed _speed)
        {
            speed = _speed;
            GeneralTimer.Interval = 1000 / (int)speed;
        }
        
        public void Dispose()
        {
            try
            {
                if (QueueFile != null)
                    QueueFile.Close();
            }
            catch (Exception e)
            {

            }
           
        }
    }
}
