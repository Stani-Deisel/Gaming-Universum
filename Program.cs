using System.Text.Json;
namespace _GamingUniversum
{
	public class GamingUniversum
	{
		public static void Main(string[] args)
		{
			// UTF-8 für korrekte Anzeige des Euro-Zeichens
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Artikel[] alleGames = ErhalteAlleArtikel();

			// Warenkorb als Dictionary mit Spielname und Anzahl
			Dictionary<Artikel, int> cart = [];

			// Hauptprogramm-Schleife für den Einkauf
			bool shopping = true;

			while (shopping)
			{
				Console.Clear();
				Console.WriteLine("Willkommen bei Gaming Universum!\n");
				Console.WriteLine("Wählen Sie eine Konsole aus:");
				Console.WriteLine("1. PlayStation");
				Console.WriteLine("2. Xbox");
				Console.WriteLine("3. Nintendo");
				Console.WriteLine("4. Warenkorb ansehen");
				Console.WriteLine("5. Bestellung abschließen");
				Console.WriteLine("6. Beenden\n");

				string choice = Console.ReadLine();
				int.TryParse(choice, out int choiceNr);

				switch (choiceNr)
				{
					case 1:
					
					case 2:
					
					case 3:
						
						DisplayGames(choiceNr, alleGames, cart);
						break;
					case 4:
						
						ViewCart(cart);
						break;
					case 5:
						
						Checkout(cart);
						shopping = false;
						break;
					case 6:
						shopping = false;
						break;
					default:
						Console.WriteLine("Ungültige Auswahl. Bitte erneut versuchen.");
						break;
				}
			}

			Console.WriteLine("Vielen Dank für Ihren Einkauf bei Gaming Universum!");
		}

		
		static void SpeicherAlleArtikel(string speicherOrt, Artikel[] artikel)
		{
			if (artikel != null && artikel.Length > 0)
			{
				string dateiInhalt = JsonSerializer.Serialize(artikel);
				File.WriteAllText(speicherOrt, dateiInhalt);
			}
			else
			{
				Console.WriteLine("Beim Versuch die Artikel als Datei zu speichern, ist etwas schief gegangen!");
			}
		}

		static Artikel[] ErhalteAlleArtikel()
		{
			Artikel[] alleGames = null;
			string artikelDatei = Path.Combine(Environment.CurrentDirectory, "AlleArtikel.json");

			if (File.Exists(artikelDatei))
			{
				string dateiInhalt = File.ReadAllText(artikelDatei);
				alleGames = JsonSerializer.Deserialize<Artikel[]>(dateiInhalt);
			}

			if (alleGames == null || alleGames.Length == 0)
			{
				alleGames = [
					new Artikel(1, "God of War: Ragnarok", 69.99m, 0.1m), // 10% Rabatt
	                new Artikel(1, "Spider-Man: Miles Morales", 49.99m, 0.0m), // kein Rabatt
	                new Artikel(1, "Horizon Forbidden West", 59.99m, 0.15m), // 15% Rabatt

	                new Artikel(2,"Halo Infinite", 59.99m, 0.05m), // 5% Rabatt
	                new Artikel(2,"Forza Horizon 5", 49.99m, 0.0m), // kein Rabatt
	                new Artikel(2,"Gears 5", 39.99m, 0.2m), // 20% Rabatt

	                new Artikel(3,"Zelda: Breath of the Wild", 59.99m, 0.1m), // 10% Rabatt
	                new Artikel(3,"Mario Kart 8 Deluxe", 49.99m, 0.0m), // kein Rabatt
	                new Artikel(3,"Animal Crossing: New Horizons", 54.99m, 0.05m), // 5% Rabatt
	            ];

				SpeicherAlleArtikel(artikelDatei, alleGames);
			}

			return alleGames;
		}

		// Methode zur Anzeige der Spiele pro Konsole
		static void DisplayGames(int consoleID, Artikel[] allgames, Dictionary<Artikel, int> cart)
		{
			Console.Clear();
			Console.WriteLine($"Verfügbare Spiele für {Artikel.KategorieByID(consoleID)}:");

			List<Artikel> games = [];

			int i = 1;
			foreach (Artikel game in allgames)
			{
				if (game.KonsolenID == consoleID)
				{
					games.Add(game);
					Console.WriteLine($"{i}. {game}");
					i++;
				}
			}

			Console.Write("\nGeben Sie die Nummer des Spiels ein, das Sie kaufen möchten, oder drücken Sie Enter, um zurückzukehren: ");
			string input = Console.ReadLine();

			if (int.TryParse(input, out int gameIndex) && gameIndex > 0 && gameIndex <= games.Count)
			{
				// string selectedGame = new List<string>(games.Keys)[gameIndex - 1];
				Artikel selectedGame = games[gameIndex - 1];

				if (cart.ContainsKey(selectedGame))
				{
					cart[selectedGame]++;
					Console.WriteLine($"{selectedGame.Name} wurde erneut hinzugefügt. Aktuelle Menge: {cart[selectedGame]}.");
				}
				else
				{
					cart[selectedGame] = 1;
					Console.WriteLine($"{selectedGame.Name} wurde zum Warenkorb hinzugefügt.");
				}
			}
			else
			{
				Console.WriteLine("Ungültige Eingabe. Zurück zum Menü.");
			}

			Console.WriteLine("\nDrücken Sie eine beliebige Taste, um fortzufahren...");
			Console.ReadKey();
		}

		// Methode zur Anzeige des Warenkorbs und Entfernen von Artikeln oder Ändern der Menge
		static void ViewCart(Dictionary<Artikel, int> cart /*, List<Artikel> playstationGames, List<Artikel> xboxGames, List<Artikel> nintendoGames*/)
		{
			Console.Clear();
			if (cart.Count == 0)
			{
				Console.WriteLine("Ihr Warenkorb ist leer.");
			}
			else
			{
				Console.WriteLine("Ihr Warenkorb enthält folgende Spiele:");
				decimal totalPrice = 0m;

				int index = 1;
				foreach (var item in cart)
				{
					// Preisberechnung für jedes Produkt
					

					decimal totalItemPrice = item.Key.VerkaufsPreis * item.Value;
					totalPrice += totalItemPrice;
					Console.WriteLine($"{index}. {item.Key.Name} - Anzahl: {item.Value}, Einzelpreis: {item.Key.VerkaufsPreis:C}, Gesamt: {totalItemPrice:C}");
					index++;
				}

				Console.WriteLine($"\nGesamtsumme: {totalPrice:C}");

				// Möglichkeit, Spiele zu entfernen oder Menge zu ändern
				Console.Write("\nGeben Sie die Nummer des Spiels ein, das Sie ändern möchten, oder drücken Sie Enter, um zurückzukehren: ");
				string input = Console.ReadLine();

				if (int.TryParse(input, out int gameIndex) && gameIndex > 0 && gameIndex <= cart.Count)
				{
					//string selectedGame = cart.ElementAt(gameIndex - 1).Key;
					Artikel selectedGame = cart.ElementAt(gameIndex - 1).Key;
					Console.Write("Geben Sie die neue Menge ein (0, um das Spiel zu entfernen): ");
					string quantityInput = Console.ReadLine();

					if (int.TryParse(quantityInput, out int newQuantity))
					{
						if (newQuantity == 0)
						{
							cart.Remove(selectedGame);
							Console.WriteLine($"{selectedGame.Name} wurde aus dem Warenkorb entfernt.");
						}
						else
						{
							cart[selectedGame] = newQuantity;
							Console.WriteLine($"Die Menge von {selectedGame.Name} wurde auf {newQuantity} geändert.");
						}
					}
					else
					{
						Console.WriteLine("Ungültige Menge.");
					}
				}
				else if (!string.IsNullOrEmpty(input))
				{
					Console.WriteLine("Ungültige Eingabe. Zurück zum Menü.");
				}
			}

			Console.WriteLine("\nDrücken Sie eine beliebige Taste, um fortzufahren...");
			Console.ReadKey();
		}

		// Methode zum Abschluss der Bestellung
		static void Checkout(Dictionary<Artikel, int> cart /*, List<Artikel> playstationGames, List<Artikel> xboxGames, List<Artikel> nintendoGames*/)
		{
			Console.Clear();
			if (cart.Count == 0)
			{
				Console.WriteLine("Ihr Warenkorb ist leer. Sie können keine Bestellung abschließen.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Wie möchten Sie bezahlen?");
			Console.WriteLine("1. Kreditkarte");
			Console.WriteLine("2. PayPal");
			Console.WriteLine("3. Girokarte");

			string paymentChoice = Console.ReadLine();
			switch (paymentChoice)
			{
				case "1":
					Console.WriteLine("Sie haben Kreditkarte als Zahlungsmethode gewählt.");
					break;
				case "2":
					Console.WriteLine("Sie haben PayPal als Zahlungsmethode gewählt.");
					break;
				case "3":
					Console.WriteLine("Sie haben Girokarte als Zahlungsmethode gewählt.");
					break;
				default:
					Console.WriteLine("Ungültige Zahlungsmethode. Bestellung abgebrochen.");
					return;
			}

			// Gesamtsumme berechnen
			decimal totalPrice = 0m;
			foreach (var item in cart)
			{
				decimal price = item.Value * item.Key.VerkaufsPreis;
			
				totalPrice += price;

			}

			Console.WriteLine($"\nDanke für ihre Zahlung von {totalPrice:C}.\nDie Bestellung wird schnellstmöglich bearbeitet und versendet.");
		}
	}


}
