using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafePI3.Classes
{
    public class QueueManager
    {
        public Dictionary<string, Queue> Queues { get; private set; }
        private int ChangeFee;

        public QueueManager()
        {
            Queues = new Dictionary<string, Queue>();
        }

        public void LoadSetupFile()
        {
            if (!File.Exists(Application.StartupPath + "\\Configs\\Setup.txt"))
            {
                MessageBox.Show("Arquivo de Setup não existente, por favor selecione um", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StreamReader file = new StreamReader(Application.StartupPath + "\\Configs\\Setup.txt");
            string line = "";
            // primeira linha
            line = file.ReadLine();
            int atendentesNumero = 0;
            if (!Int32.TryParse(line, out atendentesNumero))
            {
                MessageBox.Show("Problema na leitura do número de atendentes", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return ;
            }
            // segunda linha
            List<char> postos = new List<char>();
            line = file.ReadLine();
            postos.AddRange(line.Split(':')[1].ToCharArray());
            if (postos.Count() < 5 || postos.Count() > 20)
            {
                MessageBox.Show("Número de postos não atende as regras", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return ;
            }

            //terceira linha
            List<char> atendentes = new List<char>();
            line = file.ReadLine();
            atendentes.AddRange(line.Split(':')[1].ToCharArray());
            if (atendentes.Count() != atendentesNumero)
            {
                MessageBox.Show("Número de atendentes não corresponde a disposição dos mesmos nos postos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return ;
            }

            //quarta linha
            line = file.ReadLine();
            int troca = 0;
            if (!Int32.TryParse(line.Split(':')[1], out troca))
            {
                MessageBox.Show("Problema na leitura do valor da troca", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
                return ;
            }

            ChangeFee = troca;

            //postos
            while ((line = file.ReadLine()) != null)
            {
                char posto;
                int tempo = 0;
                string label = "";

                if (!Char.TryParse(line.Split(':')[0].Substring(0, 1), out posto) || !postos.Contains(posto))
                {
                    MessageBox.Show("Problema na leitura dos postos e seus tempos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.Close();
                    return ;
                }

                if (!Int32.TryParse(line.Split(':')[0].Substring(1), out tempo))
                {
                    MessageBox.Show("Problema na leitura dos postos e seus tempos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.Close();
                    return ;
                }

                if (line.Split(':').Count() > 1)
                    label = line.Split(':')[1];

                Queue novaFila = new Queue(label, posto.ToString(), atendentes.Count(a => a == posto), 1, postos.Count(a => a == posto), new List<Client>() , tempo);
                Queues.Add(posto.ToString(), novaFila);
            }
            file.Close();


        }


        public void StartQueue()
        {

        }



    }
}
