using Form_Ripasso_pre_rientro;
using System;
using System.Collections.Generic;
using System.IO;
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
						Console.Write("\nScelta non vala!");
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
						int camp = f.ContaCampi(path);
						string[] maxLen = f.LunghezzaRC(path, camp);
						Console.Clear();
						Console.Write($"Il record più lungo è ");
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write($" '{maxLen[camp]}' ");
						Console.ResetColor();
						Console.Write($"composto da {maxLen[camp].Length} caratteri\n");
						for (int i = 0; i < camp; i++)
						{
							Console.Write($"Il campo {i} più lungo è");
							Console.ForegroundColor = ConsoleColor.Green;
							Console.Write($" {maxLen[i]} ");
							Console.ResetColor();
							Console.Write($"composto da {maxLen[i].Length} caratteri\n");
						}
						Console.Write("\nPremere un tasto per continuare");
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
							Console.Write("\nErrore! Alcuni input contengono caratteri non vali ('\\', ';', '#')", "Errore");
						}
						f.AggiungiInCoda(path, campi, inputs);
						break;
					case "5":
						Console.Clear();
						Console.WriteLine("Inserire numero del primo campo: ");
						string primoCampoS = Console.ReadLine();
						int primoCampo;
						if (primoCampoS.Length == 1 && primoCampoS[0] > 47 && primoCampoS[0] < 58)
							primoCampo = int.Parse(primoCampoS);
						else
						{
							Console.Clear();
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Il campo inserito non è valido");
							Console.ResetColor();
							Console.WriteLine("\nPremere un tasto per continuare\n");
							Console.ReadKey();
							break;
						}
						Console.Clear();
						Console.WriteLine("Inserire numero del primo campo: ");
						string secondoCampoS = Console.ReadLine();
						int secondoCampo;
						if (secondoCampoS.Length == 1 && secondoCampoS[0] > 47 && secondoCampoS[0] < 58)
							secondoCampo = int.Parse(secondoCampoS);
						else
						{
							Console.Clear();
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Il campo inserito non è valido");
							Console.ResetColor();
							Console.WriteLine("\nPremere un tasto per continuare\n");
							Console.ReadKey();
							break;
						}
						Console.Clear();
						Console.WriteLine("Inserire numero del primo campo: ");
						string terzoCampoS = Console.ReadLine();
						int terzoCampo;
						if (terzoCampoS.Length == 1 && terzoCampoS[0] > 47 && terzoCampoS[0] < 58)
							terzoCampo = int.Parse(terzoCampoS);
						else
						{
							Console.Clear();
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Il campo inserito non è valido");
							Console.ResetColor();
							Console.WriteLine("\nPremere un tasto per continuare\n");
							Console.ReadKey();
							break;
						}
						int numFieldsVIS = f.ContaCampi(path);
						int[] checkEd = new int[numFieldsVIS];
						for (int i = 0; i < numFieldsVIS; i++)
							checkEd[i] = 0;
						if (primoCampo == 0 || secondoCampo == 0 || terzoCampo == 0) checkEd[0] = 1;
						if (primoCampo == 1 || secondoCampo == 1 || terzoCampo == 1) checkEd[1] = 1;
						if (primoCampo == 2 || secondoCampo == 2 || terzoCampo == 2) checkEd[2] = 1;
						if (primoCampo == 3 || secondoCampo == 3 || terzoCampo == 3) checkEd[3] = 1;
						if (primoCampo == 4 || secondoCampo == 4 || terzoCampo == 4) checkEd[4] = 1;
						if (primoCampo == 5 || secondoCampo == 5 || terzoCampo == 5) checkEd[5] = 1;
						if (primoCampo == 6 || secondoCampo == 6 || terzoCampo == 6) checkEd[6] = 1;
						if (primoCampo == 7 || secondoCampo == 7 || terzoCampo == 7) checkEd[7] = 1;
						if (primoCampo == 8 || secondoCampo == 8 || terzoCampo == 8) checkEd[8] = 1;
						if (numFieldsVIS > 9)
							if (primoCampo == 9 || secondoCampo == 9 || terzoCampo == 9) checkEd[9] = 1;
							else
							if (primoCampo == 9 || secondoCampo == 9 || terzoCampo == 9)
							{
								Console.Clear();
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("Il campo Miovalore non è presente nel file");
								Console.ResetColor();
								Console.WriteLine("\nPremere un tasto per continuare\n");
								Console.ReadKey();
								break;
							}
						string[] names = f.NomeCampi(path);
						Console.Clear();
						using (StreamReader csvReader = File.OpenText(path))
						{
							string lineFromFile;
							lineFromFile = csvReader.ReadLine();
							while ((lineFromFile = csvReader.ReadLine()) != null)
							{
								string[] fieldsVIS = lineFromFile.Split(';');
								if (numFieldsVIS == 11)
								{
									if (fieldsVIS[10] == "0")
									{
										Console.WriteLine();
										for (int i = 0; i < numFieldsVIS; i++)
										{
											if (checkEd[i] == 1)
											{
												Console.Write($"{names[i]}: ");
												Console.ForegroundColor = ConsoleColor.Green;
												Console.Write($"{fieldsVIS[i]} ");
												Console.ResetColor();
											}
										}
									}
								}
								else
								{
									Console.WriteLine();
									for (int i = 0; i < numFieldsVIS; i++)
									{
										if (checkEd[i] == 1)
										{
											Console.Write($"{names[i]}: ");
											Console.ForegroundColor = ConsoleColor.Green;
											Console.Write($"{fieldsVIS[i]} ");
											Console.ResetColor();
										}
									}
								}
							}
							csvReader.Close();
							Console.WriteLine("\n\nPremere un tasto per continuare\n");
							Console.ReadKey();
						}
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
									Console.Write("\nErrore! Alcuni input contengono caratteri non vali ('\\', ';', '#')", "Errore");
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
						bool val;
						do
						{
							Console.Clear();
							Console.WriteLine("Opzioni cancellazione\n1) Cancella record\n2) Recupera record\n3) Ricompatta database\n");
							switch (Console.ReadLine())
							{
								case "1":
									val = true;
									if (!f.CheckMioValore(path))
									{
										Console.Clear();
										Console.ForegroundColor = ConsoleColor.Red;
										Console.WriteLine("Nel file non sono presenti i campi 'Mio valore' e 'Cancellazione Logica'");
										Console.ResetColor();
										Console.Write("\nPremere un tasto per continuare");
									}
									else
									{
										Console.Clear();
										Console.WriteLine("Inserire l'elemento da cancellare\n");
										string elCanc = Console.ReadLine();
										Tuple<string, int> searchResultCanc = f.Ricerca(path, elCanc, false);
										if (searchResultCanc.Item2 == -1)
										{
											Console.Clear();
											Console.ForegroundColor = ConsoleColor.Red;
											Console.WriteLine("Il record cercato non è presente");
											Console.ResetColor();
											Console.Write("\nPremere un tasto per continuare");
										}
										else
										{
											int nCampiCanc = f.ContaCampi(path);
											f.Cancellazione(path, nCampiCanc, searchResultCanc.Item1, searchResultCanc.Item2, lRecord);
										}
									}
									break;
								case "2":
									val = true;
									if (!f.CheckMioValore(path))
									{
										Console.Clear();
										Console.ForegroundColor = ConsoleColor.Red;
										Console.WriteLine("Nel file non sono presenti i campi 'Mio valore' e 'Cancellazione Logica'");
										Console.ResetColor();
										Console.Write("\nPremere un tasto per continuare");
									}
									else
									{
										Console.Clear();
										Console.WriteLine("Inserire l'elemento da cancellare\n");
										string elCanc = Console.ReadLine();
										Tuple<string, int> searchResultCanc = f.Ricerca(path, elCanc, true);
										if (searchResultCanc.Item2 == -1)
										{
											Console.Clear();
											Console.ForegroundColor = ConsoleColor.Red;
											Console.WriteLine("Il record cercato non è presente");
											Console.ResetColor();
											Console.Write("\nPremere un tasto per continuare");
										}
										else
										{
											int nCampiCanc = f.ContaCampi(path);
											f.Recupera(path, nCampiCanc, searchResultCanc.Item1, searchResultCanc.Item2, lRecord);
										}
									}
									break;
								case "3":
									val = true;
									if (!f.CheckMioValore(path))
									{
										Console.Clear();
										Console.ForegroundColor = ConsoleColor.Red;
										Console.WriteLine("Nel file non sono presenti i campi 'Mio valore' e 'Cancellazione Logica'");
										Console.ResetColor();
										Console.Write("\nPremere un tasto per continuare");
									}
									else
									{
										bool valCOMP;
										do
										{
											Console.Clear();
											Console.WriteLine("Cancellare definitivamente i record eliminati\n1) Si\n0) No\n");
											switch (Console.ReadLine())
											{
												case "1":
													valCOMP = true;
													f.Ricompattazione(path, pathTEMP);
													break;
												case "0":
													valCOMP = true;
													break;
												default:
													valCOMP = false;
													Console.Write("\nOpzione non vala\nPremere un tasto per continuare");
													break;
											}
										} while (!valCOMP);
									}
									break;
								default:
									val = false;
									Console.Write("\nOpzione non vala\nPremere un tasto per continuare");
									break;
							}
						} while (!val);
						break;
					case "9":
						Console.Write("Vuoi chiudere davvero il programma? (Y/N) ");
						string answ = Console.ReadLine();
						if (answ.ToUpper() == "Y")
							continua = false;
						if (answ.ToUpper() != "N" || answ.ToUpper() != "Y")
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("\nScelta non vala!");
							Console.ResetColor();
						}
						break;
				}
				Console.ReadKey();
			} while (continua);

		}
	}
}
