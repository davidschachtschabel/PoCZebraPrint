using System;
using System.Net.Sockets;
using System.Text;
using PoCZebraPrint.Services;

namespace PoCZebraPrint
{
	public class PrintService : IPrintService
	{
		private TcpClient _tcpClient;
		public const int DefaultZplPort = 9100;

		/// <summary>
		///     Sendet die Daten, die gedruckt werden sollen, an einen Drucker.
		/// </summary>
		/// <param name="data">Daten für den Druck</param>
		/// <param name="ipAdresse">IP-Adresse des Druckers</param>
		/// <param name="port">Port vom Drucker</param>
		public void SendDataToPrinter(PrintData data, string ipAdresse, int port)
		{
			try
			{
				_tcpClient = new TcpClient();

				if (port is 0) port = DefaultZplPort;

				_tcpClient.Connect(ipAdresse, port);

				var template =
					@"^XA
					^A0,30,30^FO25,25^FD#### WE Belgeitschein^FS
					^A0,30,30^FO310,25^FD<BegleitscheinNr>^FS
					^A0,30,30^FO375,25^FD####^FS
					^A0,30,30^FO25,70^FDLager:^FS
					^A0,30,30^FO105,70^FD<Lagerort>^FS
					^FO700,25^BQN,2,2,^FDQA,<LinkBegleitschein>^FS
					^A0,30,30^FO25,145^FDArtikel^FS
					^A0,30,30^FO680,145^FDAnzahl^FS
					^A0,25,25^FO25,185^FD<ArtikelMitAnzahl>^FS
					^XZ";

				if (template.Contains("<BegleitscheinNr>"))
				{
					Console.WriteLine("<BegleitscheinNr> Gefunden!");
					template = template.Replace("<BegleitscheinNr>", data.BegleitscheinNr.ToString());
				}
				else
					Console.WriteLine("Nicht gefunden!");

				if (template.Contains("<Lagerort>"))
				{
					Console.WriteLine("<Lagerort> Gefunden!");
					template = template.Replace("<Lagerort>", data.LagerOrt);
				}
				else
					Console.WriteLine("Nicht gefunden!");

				if (template.Contains("<LinkBegleitschein>"))
				{
					Console.WriteLine("<LinkBegleitschein> Gefunden!");
					template = template.Replace("<LinkBegleitschein>", data.LinkBegleitschein);
				}
				else
					Console.WriteLine("Nicht gefunden!");

				if (template.Contains("<ArtikelMitAnzahl>"))
				{
					Console.WriteLine("<ArtikelMitAnzahl> Gefunden!");

					var artikelMitAnzahl = new StringBuilder();
					artikelMitAnzahl.AppendLine("^A0,25,25^FO25,185^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine("^A0,25,25^FO715,185^FD1^FS");
					artikelMitAnzahl.AppendLine(
						$"^A0,25,25^FO25,{185 + 30}^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine($"^A0,25,25^FO715,{185 + 30}^FD2^FS");
					artikelMitAnzahl.AppendLine(
						$"^A0,25,25^FO25,{185 + 60}^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine($"^A0,25,25^FO715,{185 + 60}^FD5^FS");
					artikelMitAnzahl.AppendLine(
						$"^A0,25,25^FO25,{185 + 90}^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine($"^A0,25,25^FO715,{185 + 90}^FD5^FS");
					artikelMitAnzahl.AppendLine(
						$"^A0,25,25^FO25,{185 + 120}^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine($"^A0,25,25^FO715,{185 + 120}^FD8^FS");
					artikelMitAnzahl.AppendLine(
						$"^A0,25,25^FO25,{185 + 150}^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine($"^A0,25,25^FO715,{185 + 150}^FD6^FS");
					artikelMitAnzahl.AppendLine(
						$"^A0,25,25^FO25,{185 + 180}^FDSuporaflex Cruz 2.5mm x 26mm^FS");
					artikelMitAnzahl.AppendLine($"^A0,25,25^FO715,{185 + 180}^FD9^FS");
					template = template.Replace("<ArtikelMitAnzahl>", artikelMitAnzahl.ToString());
				}
				else
					Console.WriteLine("Nicht gefunden!");

				var stream = _tcpClient.GetStream();
				var printData = Encoding.UTF8.GetBytes(template);

				stream.Write(printData, 0, printData.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				_tcpClient.Close();
			}
		}
	}
}