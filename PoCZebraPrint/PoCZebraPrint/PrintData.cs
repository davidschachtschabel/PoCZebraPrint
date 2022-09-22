using System.Collections.Generic;

namespace PoCZebraPrint
{
	public class PrintData
	{
		public int BegleitscheinNr { get; set; }
		public string LagerOrt { get; set; }
		public string LinkBegleitschein { get; set; }
		public Dictionary<string, int> ArtikelMitAnzahl { get; set; } = new Dictionary<string, int>();
	}
}