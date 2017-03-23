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

        private StreamReader QueueFile;
        private string CurrentLine;
        private bool arquivoAcabou;
        private bool PausedQueue;

        TurnSpeed speed;

        List<Client> FinishedClients = new List<Client>();

        public QueueManager(MainForm _form)
        {
            Queues = new Dictionary<string, Queue>();

            form = _form;
        }

        public bool LoadSetupFile()
        {
            if (!File.Exists(Application.StartupPath + "\\Configs\\Setup.txt"))
            {
                MessageBox.Show("Arquivo de Setup não existente, por favor selecione um", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Queues = new Dictionary<string, Queue>();

            StreamReader file = new StreamReader(Application.StartupPath + "\\Configs\\Setup.txt");
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
            if (postos.Count() < 5 || postos.Count() > 20)
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
                    label = Encoding.UTF8.GetBytes(line.Split(':')[1]);

                Queue novaFila = new Queue(Encoding.UTF8.GetString(label), posto.ToString(), atendentes.Count(a => a == posto), 1, postos.Count(a => a == posto), tempo);
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
            //ScheduleProcessTurnJob(speed);
            Application.DoEvents();
            speed = _speed;
            NextStep();
        }

        public void NextStep()
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


                    CurrentLine = QueueFile.ReadLine();
                    if (CurrentLine != null)
                        currentClientDTO = new ClientDTO(CurrentLine);
                    else
                        arquivoAcabou = true;
                }
            }


            //tratando movimentação de Clientes em atendimento
            foreach (Queue q in Queues.Select(a => a.Value))
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
                            c.TurnsInSystem = c.EntranceTurn - CurrentTurn;
                            FinishedClients.Add(c);
                            clientsToRemove.Add(c);
                        }
                        else
                        {
                            //passando pra proxima fila
                            c.currentQueueSequence++;
                            Queues[c.QueueSequence[c.currentQueueSequence].ToString()].Clients.Add(c);
                            clientsToRemove.Add(c);
                        }
                        
                    }
                }
                q.Clients.RemoveAll(a => clientsToRemove.Contains(a));
            }

            //Colocando novos Clientes em atendimento
            foreach (Queue q in Queues.Select(a => a.Value))
            {

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
                    if((tempo - DateTime.Now)> TimeSpan.Zero)
                        Thread.Sleep(tempo - DateTime.Now);
                    NextStep();
                }
            }
            else
            {
                double mediumTimeSystem = FinishedClients.Average(a => a.TurnsInSystem);
                Dictionary<string, double> mediumPerQueue = new Dictionary<string, double>(Queues.Count);
                foreach (Queue q in Queues.Select(a => a.Value))
                {
                    mediumPerQueue.Add(q.Name,
                        FinishedClients
                            .Where(a => a.QueueSequence.IndexOf(char.Parse(q.Name)) > -1)
                            .Average(a => a.QueueAllEntrances[a.QueueSequence.IndexOf(char.Parse(q.Name))]));
                }
                int IDMostWaitUser = FinishedClients.OrderByDescending(a => a.TurnsInSystem).First().ID;

                Dictionary<string, double> mediumPerCombination = new Dictionary<string, double>();
                foreach (List<char> sequence in FinishedClients.Select(a => a.QueueSequence).Distinct())
                {
                    mediumPerCombination.Add( string.Join("|", sequence) , FinishedClients
                                                    .Where(a => a.QueueSequence == sequence)
                                                    .Average(a => a.TurnsInSystem));
                }

                

            }
                

        }
        
        public void PauseQueue()
        {
            PausedQueue = true;
        }

        public void SetSpeed(TurnSpeed _speed)
        {
            speed = _speed;
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
