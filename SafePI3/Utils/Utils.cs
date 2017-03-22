using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SafePI3.Utils
{
    public static class Utils
    {

        static public int getTotalClients(this MainForm form)
        {
            return form.Queues.Sum(a => a.Value.Clients.Count);
        }

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
                MessageBox.Show("Arquivo copiado para o sistema com sucesso", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Houve um erro ao tentar copiar o arquivo de configuração: " + e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        static public void ReadSetupFile()
        {
            


        }
    }

}
