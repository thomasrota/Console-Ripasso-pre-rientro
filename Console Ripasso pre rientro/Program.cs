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
			int lRecord = 504;
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
						break;
					case "3":
						int fields = f.ContaCampi(path);
						break;
					case "4":
						string[] campiNomi = f.NomeCampi(path);
						Random r = new Random();
						int campi = f.ContaCampi(path);
						string[] inputs = new string[campi];
						for (int i = 0; i < campi; i++)
						{
							if (f.CheckMioValore(path))
							{
								inputs[9] = r.Next(10, 21).ToString();
								inputs[10] = "0";
							}
							Console.Write($"Inserisci elemento per il campo '{campiNomi[i]}': ");
							inputs[i] = Console.ReadLine();
							if (i == 8)
								break;
						}
						if (!f.CheckLunghezzaInput(campi, inputs))
						{
							if (f.CheckMioValore(path) == true)
								Console.Write("\nErrore! Alcuni input sono vuoti o troppo lunghi");
							else
								Console.Write("\nErrore! Alcuni input sono vuoti o troppo lunghi");
							return;
						}
						if (!f.CheckInptChar(campi, inputs))
						{
							Console.Write("\nErrore! Alcuni input contengono caratteri non validi ('\\', ';', '#')", "Errore");
						}
						f.AggiungiInCoda(path, campi, inputs);
						break;
					case "5":
						break;
					case "6":
						Console.Clear();
						Console.Write("Inserisci l'elemento da ricercare: ");
						string searchItem = Console.ReadLine();
						Tuple<string, int> searchResult = f.Ricerca(path, searchItem, false);
						if (searchItem == "")
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("\nErrore! Inserire un valore da cercare!");
							Console.ResetColor();
						}
						else
						{
							if (searchResult.Item2 == -1)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("\nErrore! Il record cercato non è stato trovato!");
								Console.ResetColor();
							}
							else
							{
								Console.Write($"\nRecord:");
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine(searchResult.Item1);
								Console.ResetColor();
								Console.Write($"\nTrovato alla riga: ");
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine(searchResult.Item2);
								Console.ResetColor();
							}
						}
						break;
					case "7":
						string[] cN = f.NomeCampi(path);
						Random rnd = new Random();
						Console.Clear();
						Console.Write("Inserisci l'elemento da ricercare: ");
						string modSearchItem = Console.ReadLine();
						Tuple<string, int> modSearchResult = f.Ricerca(path, modSearchItem, false);
						if (modSearchItem == "")
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("\nErrore! Inserire un valore da cercare!");
							Console.ResetColor();
						}
						else
						{
							if (modSearchResult.Item2 == -1)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("\nErrore! Il record cercato non è stato trovato!");
								Console.ResetColor();
							}
							else
							{
								int nmCampi = f.ContaCampi(path);
								string[] modInpts = new string[nmCampi];
								for (int i = 0; i < nmCampi; i++)
								{
									if (f.CheckMioValore(path))
									{
										modInpts[9] = rnd.Next(10, 21).ToString();
										modInpts[10] = "0";
									}
									Console.Write($"Inserisci elemento per il campo '{cN[i]}': ");
									modInpts[i] = Console.ReadLine();
									if (i == 8)
										break;
								}
								if (f.CheckMioValore(path))
								{
									modInpts[9] = rnd.Next(10, 21).ToString();
									modInpts[10] = "0";
								}
								if (!f.CheckLunghezzaInput(nmCampi, modInpts))
								{
									if (f.CheckMioValore(path) == true)
										Console.Write("\nErrore! Alcuni input sono vuoti o troppo lunghi");
									else
										Console.Write("\nErrore! Alcuni input sono vuoti o troppo lunghi");
									return;
								}
								else if (!f.CheckInptChar(nmCampi, modInpts))
								{
									Console.Write("\nErrore! Alcuni input contengono caratteri non validi ('\\', ';', '#')", "Errore");
								}
								else
								{
									f.Modifica(path, nmCampi, modInpts, modSearchResult.Item2, lRecord);
									{
										Console.ForegroundColor = ConsoleColor.Green;
										Console.Write("\nModifica effettutata correttamente!");
										Console.ResetColor();
									}
								}
							}
						}
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
						}
						break;
				}
				Console.ReadKey();
			} while (continua);

		}
	}
}
