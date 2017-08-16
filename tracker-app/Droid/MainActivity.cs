using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite.Net;

using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace test2.Droid
{
	[Activity(Label = "trackerApp", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);
            
			global::Xamarin.Forms.Forms.Init(this, bundle);

			var dbPath = FileAccessHelper.GetLocalFilePath("Order.db");
			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var param = new SQLiteConnectionString(dbPath, false);
            var connection = new SQLiteConnection(platform, dbPath, true, null, null, null, null);

            //connection.DropTable<PicItem>();
            connection.CreateTable<PicItem>();

            UserDialogs.Init(this);
            //var result = TableExistsAsync<PicItem>(connection);
			LoadApplication(new App(connection));
		}

       

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
		}
	}
}
