using System;
using System.Windows.Input;
using PoCZebraPrint.Services;
using Xamarin.Forms;

namespace PoCZebraPrint
{
	public class MainPageViewModel
	{
		private readonly IPrintService _printService;

		public MainPageViewModel(IPrintService printService)
		{
			_printService = printService;

			PrintCommand = new Command(OnExecutePrintCommand);
		}

		public ICommand PrintCommand { get; }

		private void OnExecutePrintCommand()
		{
			try
			{
				_printService.SendDataToPrinter(new PrintData
				{
					ArtikelMitAnzahl =
					{
						{"ArtikelName", 1}
					},
					BegleitscheinNr = 1111,
					LagerOrt = "Endoskopie",
					LinkBegleitschein = $"https://www.webadresse.de/dokument/{Guid.NewGuid()}"
				}, "192.168.178.36", 9100);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}