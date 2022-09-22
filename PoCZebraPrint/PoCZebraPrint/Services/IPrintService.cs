namespace PoCZebraPrint.Services
{
	public interface IPrintService
	{
		void SendDataToPrinter(PrintData data, string ipAdresse, int port);
	}
}