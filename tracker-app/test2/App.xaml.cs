using System.Collections.Generic;
using Android.Widget;
using SQLite.Net;
using Xamarin.Forms;

namespace test2
{
	public partial class App : Application
	{
		public static bool UseMockDataStore = true;
		public static string BackendUrl = "https://5582bb46.ngrok.io";
        public static PictureService picService;

		public static IDictionary<string, string> LoginParameters => null;

		public App(SQLiteConnection conn)
		{
			InitializeComponent();
            picService = new PictureService(conn);

			if (UseMockDataStore)
			{
				//DependencyService.Register<MockDataStore>();
				DependencyService.Register<PictureService>();
				DependencyService.Register<STDataService>();
			}
			else
			{
				DependencyService.Register<CloudDataStore>();
			}
			SetMainPage();
		}

		public static void SetMainPage()
		{
			if (!UseMockDataStore && !Settings.IsLoggedIn)
			{
				/*Current.MainPage = new NavigationPage(new LoginPage())
				{
					BarBackgroundColor = (Color)Current.Resources["Primary"],
					BarTextColor = Color.White
				};*/
			}
			else
			{
				GoToMainPage();
			}
		}

		public static void GoToMainPage()
		{
            Current.MainPage = new TabbedPage
			{
				Children = {
					new NavigationPage(new ItemsPage(picService))
					{
						Title = "Listado"
					},
					
				}
                
			};
		}
	}
}
