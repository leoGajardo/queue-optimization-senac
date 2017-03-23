using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SafePI3.Classes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace SafePI3.Utils
{
    public static class Utils
    {

        static public string PickFile(string filters, string title)
        {
            OpenFileDialog FilePicker = new OpenFileDialog();
            FilePicker.Filter = filters;
            FilePicker.Title = title;

            if (FilePicker.ShowDialog() == DialogResult.OK)
                return FilePicker.FileName;
            else
                return null;

        }

        static public void CopyFile(string filename, string source, string dest)
        {
            try
            {
                Directory.CreateDirectory(dest);
                File.Copy(source, dest + filename, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("Houve um erro ao tentar copiar o arquivo de configuração: " + e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        static public bool ValidateSetupFile()
        {
            if (!File.Exists(Application.StartupPath + "\\Configs\\Setup.txt"))
            {
                MessageBox.Show("Arquivo de Setup não existente, por favor selecione um", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

            //postos
            while ((line = file.ReadLine()) != null)
            {
                char posto;
                int tempo = 0;

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

            }
            file.Close();
            return true;
        }

        static public bool ValidateQueueFile()
        {
            if (!File.Exists(Application.StartupPath + "\\Configs\\Queue.txt"))
            {
                MessageBox.Show("Arquivo de Fila não existente, por favor selecione um", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!File.Exists(Application.StartupPath + "\\Configs\\Setup.txt"))
            {
                MessageBox.Show("Arquivo de Setup não existente, é preciso selecionar o Setup antes do arquivo de fila", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            StreamReader file = new StreamReader(Application.StartupPath + "\\Configs\\Queue.txt");

            string line = "";
            string pattern = "";

            StreamReader setup = new StreamReader(Application.StartupPath + "\\Configs\\Setup.txt");
            // primeira linha
            line = setup.ReadLine();
            line = setup.ReadLine();
            List<char> postos = new List<char>();
            postos.AddRange(line.Split(':')[1].ToCharArray().Distinct().Skip(1));
            setup.Close();


            string patternEnd = @"+C[\d]+A[" + string.Join("{1}]?[", postos) + @"{1}]?$";
            int UNumber = 1;
            while ((line = file.ReadLine()) != null)
            {
                pattern = "^U" + UNumber + patternEnd;
                if (!Regex.Match(line.Trim(), pattern).Success)
                {
                    MessageBox.Show("Existe alguma inconsistencia no arquivo de fila na linha: " + UNumber, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.Close();
                    return false;
                }
                UNumber++;
            }
            file.Close();
            return true;
        }

        public static int RemoveAll<T>(
                        this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }

    }

}
