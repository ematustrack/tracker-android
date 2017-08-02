using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Media;
using SQLite.Net.Async;
using Xamarin.Forms;
using XamarinForms.SQLite.Droid.SQLite;
using SQLite.Net;


namespace test2
{
	public partial class NewItemPage : ContentPage
	{
		public PicItem Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();

			//TakePicture();

			BindingContext = this;
		}
        /*
		async Task<Image> TakePicture()
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
			{
				//DisplayAlert("No Camera", ":( No camera avaialble.", "OK");

			}
			var fileName = System.DateTime.Now.Ticks.ToString();
			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{

				Directory = "Sample",
				Name = fileName,
				PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
				CompressionQuality = 80
			});

			if (file == null)
				return new Image();

			//DisplayAlert("File Location", file.Path, "OK");
			Console.WriteLine(file.Path);
			var image = new Image { Aspect = Aspect.AspectFit };
			image.Source = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});

			var stream_ = file.GetStream();
			byte[] bytes = new byte[stream_.Length];
			await stream_.ReadAsync(bytes, 0, (int)stream_.Length);
			var base64 = System.Convert.ToBase64String(bytes);

            var picService = new PictureService();

			Item = new PicItem
			{
				FileName = base64,
				Sent = false,
				CreatedOn = DateTime.Now
			};

			picService.AddItemAsync(Item);
			//MessagingCenter.Send(this, "AddItem", Item);
			//await Navigation.PopToRootAsync();

			return image;
		}
*/

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddItem", Item);
			await Navigation.PopToRootAsync();
		}
	}
}
