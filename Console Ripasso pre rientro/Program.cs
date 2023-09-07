using Form_Ripasso_pre_rientro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Ripasso_pre_rientro
{
	internal class Program
	{		
		static void Main(string[] args)
		{
			#region Dichiarazioni
			Funzioni f = new Funzioni();
			string path = "rota.csv";
			string pathTEMP = @"rotaTEMP.csv";
			#endregion
			bool continua = true;
			string options = "1) Aggiungi 'Mio valore'\n2) Conta campi\n3) Lunghezza record/campi\n4) Aggiungi record in coda\n5) Visualzza\n6) Ricerca\n7) Modifica\n8) Cancellazione logica\n9) Esci";
			do
			{
				if (f.CheckLughezzaFissa(path) == false)
					f.CreateLunghezzaFissa(path, pathTEMP);
				f.ChangeChar(path, pathTEMP);
				Console.Clear();
				Console.WriteLine(options);
				string scelta;
				Console.Write("\nSeleziona: ");
				scelta = Console.ReadLine();
				switch (scelta)
				{
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write("\nScelta non valida!");
						Console.ResetColor();
						Console.ReadKey();
						break;
					case "1":
						if (!f.CheckMioValore(path))
							f.CreateMyValue(path, pathTEMP);
						else
						{ 
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("\nI campi 'Mio valore' e 'cancellazione logica' sono già presenti!");
							Console.ResetColor();
						}
						break;
					case "2":
						int nCampi = f.ContaCampi(path);
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"Il numero dei campi è: {nCampi}\n Premere un tasto per continuare...");
						Console.ResetColor();
						Console.ReadKey();
						break;
					case "3":
						int fields = f.ContaCampi(path);
						break;
					case "4":
						
						break;
					case "5":
						break;
					case "6":
						break;
					case "7":
						break;
					case "8":
						break;
					case "9":
						Console.Write("Vuoi chiudere davvero il programma? (Y/N) ");
						string answ = Console.ReadLine();
						if (answ.ToUpper() == "Y")
							continua = false;
						if (answ.ToUpper() != "N" || answ.ToUpper() != "Y")
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("\nScelta non valida!");
							Console.ResetColor();
							Console.ReadKey();
						}
						break;
				}
			} while (continua);

		}
	}
}
