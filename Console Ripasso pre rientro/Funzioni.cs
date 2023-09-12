using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace Form_Ripasso_pre_rientro
{
    public class Funzioni
    {
        public void ChangeChar(string path, string pathTEMP)
        {
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                using (StreamWriter sw = new StreamWriter(pathTEMP))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace('\uFFFD', '\'');
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Funzione che controlla se il file csv ha la lunghezza fissa
        public bool CheckLughezzaFissa(string path)
        {
            bool check = false;
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                line = sr.ReadLine();
                if (line.Contains("##"))
                    check = true;
                sr.Close();
            }
            return check;
        }
        // Funzione che rende il file a lunghezza fissa
        public void CreateLunghezzaFissa(string path, string pathTEMP)
        {
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        line += ";";
                        line = line.PadRight(500) + "##";
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Funzione che controlla l'esistenza di 'mio valore' e dei campi successivi
        public bool CheckMioValore(string path)
        {
            bool check = false;
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                line = sr.ReadLine();
                if (line.Contains("Mio valore;"))
                    check = true;
                sr.Close();
            }
            return check;
        }
        // Funzione aggiunta 'mio valore' e 'cancellazione logica'
        public void CreateMyValue(string path, string pathTEMP)
        {
            Random r = new Random();
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                {
                    line = sr.ReadLine();
                    string[] dati = line.Split(';');
                    line = "";
                    for (int i = 0; i < dati.Length - 1; i++)
                        line += dati[i] + ";";
                    line += "Mio valore;Cancellazione logica;";
                    line = line.PadRight(500) + "##";
                    sw.WriteLine(line);
                    while ((line = sr.ReadLine()) != null)
                    {
                        dati = line.Split(';');
                        line = "";
                        for (int i = 0; i < dati.Length - 1; i++)
                            line += dati[i] + ";";
                        line += r.Next(10, 21) + ";0;";
                        line = line.PadRight(500) + "##";
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Funzione che conta il numero di campi
        public int ContaCampi(string path)
        {
            int count;
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                line = sr.ReadLine();
                string[] dati = line.Split(';');
                count = dati.Length - 1;
                sr.Close();
            }
            return count;
        }
        public string[] LunghezzaRC(string path, int nCampi)
        {
            string[] mLen = new string[nCampi + 1];
            for (int i = 0; i < mLen.Length; i++)
                mLen[i] = "";
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] dati = line.Split(';');
                    line = "";
                    for (int i = 0; i < dati.Length; i++)
                    {
                        if (dati[i].Length > mLen[i].Length)
                            mLen[i] = dati[i];
                        line += dati[i] + ";";
                    }
                    if (line.Length > mLen[nCampi].Length)
                        mLen[nCampi] = line;
                }
                sr.Close();
            }
            return mLen;
        }
        // Funzione che controlla la dimensione dell'input e se è 'vuoto'
        public bool CheckLunghezzaInput(int nCampi, string[] inputs)
        {
            string line = ""; 
            for (int i = 0; i < nCampi; i++)
            {
                line += inputs[i] + ";";
                if (inputs[i].Length == 0)
                   return false;
            }
            if (nCampi == 11)
                if (line.Length < 500)
                    return true;
                else return false;
            else
                if (line.Length < 455)
                return true;
            return false;
        }
        public bool CheckInptChar(int nCampi, string[] inputs)
        {
            for (int i = 0; i < nCampi; i++)
            {
                if (inputs[i].Contains(';') || inputs[i].Contains('\\') || inputs[i].Contains('#'))
                    return false;
            }
            return true;
        }
        // Funzione che aggiunge dei record in coda
        public void AggiungiInCoda(string path, int nCampi, string[] inputs)
        {
            string line = "";
            for (int i = 0; i < nCampi; i++)
                line += inputs[i] + ";";
            line = line.Replace('\uFFFD', '\'');
            line = (line.PadRight(500) + "##");
            using (FileStream rw = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(line);
                rw.Seek(0, SeekOrigin.End);
                rw.Write(bytes, 0, bytes.Length);
                rw.Close();
            }
        }
        // Ricercare un record per campo chiave a scelta (se esiste, utilizzare il campo che contiene dati univoci);
        public Tuple<string, int> Ricerca(string path, string search, bool eliminato)
        {
            Tuple<string, int> p;
            using (StreamReader sr = File.OpenText(path))
            {
                string line;
                int pos = 0;
                if (ContaCampi(path) == 11)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] fields = line.Split(';');
                        if (fields[0] == search)
                        {
                            if (((fields[10] == 0.ToString() && !eliminato) || (fields[10] == 1.ToString() && eliminato)) && pos != 0)
                            {
                                sr.Close();
                                p = new Tuple<string, int>(line, pos);
                                return p;
                            }
                        }
                        pos++;
                    }
                }
                else
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] fields = line.Split(';');
                        if (fields[6] == search && pos != 0)
                        {
                            sr.Close();
                            p = new Tuple<string, int>(line, pos);
                            return p;
                        }
                        pos++;
                    }
                }
                sr.Close();
            }
            return new Tuple<string, int>("", -1);
        }
        public void Modifica(string path, int nCampi, string[] inputs, int pos, int recordLen)
        {
            string line = "";
            for (int i = 0; i < nCampi; i++)
                line += inputs[i] + ";";
            line = (line.PadRight(500) + "##");
            line = line.Replace('\uFFFD', '\'');
            using (FileStream csvRanWriter = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(line);
                csvRanWriter.Seek(pos * recordLen, SeekOrigin.Current);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public void Cancellazione(string path, int nCampi, string inputs, int pos, int recordLen)
        {
            string line = "";
            string[] fields = inputs.Split(';');
            for (int i = 0; i < nCampi - 1; i++)
                line += fields[i] + ";";
            line += "1;";
            line = line.PadRight(500) + "##";
            using (FileStream csvRanWriter = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(line);
                csvRanWriter.Seek(pos * recordLen, SeekOrigin.Current);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public void Recupera(string path, int nCampi, string inputs, int pos, int recordLen)
        {
            string line = "";
            string[] fields = inputs.Split(';');
            for (int i = 0; i < nCampi - 1; i++)
                line += fields[i] + ";";
            line += "0;";
            line = line.PadRight(500) + "##";
            using (FileStream csvRanWriter = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(line);
                csvRanWriter.Seek(pos * recordLen, SeekOrigin.Current);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public void Ricompattazione(string path, string pathTEMP)
        {
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                using (StreamWriter sw = new StreamWriter(pathTEMP))
                {
                    line = sr.ReadLine();
                    sw.WriteLine(line);
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] fields = line.Split(';');
                        if (fields[10] == "0")
                        {
                            sw.WriteLine(line);
                        }
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
		public string[] NomeCampi(string path)
		{
			int n = ContaCampi(path);
			string[] nomi = new string[n];
			string linea;
			using (StreamReader sr = File.OpenText(path))
			{
				linea = sr.ReadLine();
				string[] campo = linea.Split(';');
				for (int i = 0; i < nomi.Length; i++)
				{
					nomi[i] = campo[i];
				}
			}
			return nomi;
		}
	}
}
