using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GamingUniversum
{
	[Serializable]
	public class Artikel
	{
		private int _konsolenID;
		private string _name;
		private decimal _preis;
		private decimal _rabatt;

		public Artikel()
		{
		}

		public Artikel(int konsole, string name, decimal preis, decimal rabatt)
		{
			_konsolenID = konsole;
			_name = name;
			_preis = preis;
			_rabatt = rabatt;
		}

		public int KonsolenID { get => _konsolenID; set => _konsolenID = value; }
		public string Name { get => _name; set => _name = value; }
		public decimal Preis { get => _preis; set => _preis = value; }
		public decimal Rabatt { get => _rabatt; set => _rabatt = value; }
		public decimal VerkaufsPreis => _preis * (1 - _rabatt);

		public static string KategorieByID(int consoleId)
		{
			if (consoleId == 1)
				return "PlayStation";
			else if (consoleId == 2)
				return "Xbox";
			else if (consoleId == 2)
				return "Nintendo";
			else
				return "Unbekannt";
		}

		public override string ToString() => $"{_name} - Originalpreis: \x1b[9m{_preis:C}\x1b[0m, Preis nach Rabatt: {VerkaufsPreis:C}"; //$"{_name} - Originalpreis: {_preis:C}, Preis nach Rabatt: {VerkaufsPreis:C}";
	}
}
