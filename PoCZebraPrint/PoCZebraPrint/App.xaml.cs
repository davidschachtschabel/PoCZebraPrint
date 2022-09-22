using PoCZebraPrint.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace PoCZebraPrint
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			DependencyService.Register<IPrintService, PrintService>();

			MainPage = new MainPage
			{
				BindingContext = new MainPageViewModel(DependencyService.Get<IPrintService>())
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}