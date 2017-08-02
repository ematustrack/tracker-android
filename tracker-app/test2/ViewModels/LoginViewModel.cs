using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Xamarin.Forms;


namespace test2
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel()
		{
			SignInCommand = new Command(async () => await SignIn());
			NotNowCommand = new Command(async (obj) => await TakePicture());
		}

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
				Name = fileName
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

			var picService = new PictureService();
			picService.AddItemAsync(new PicItem
			{
				FileName = fileName,
				Sent = false,
				CreatedOn = DateTime.Now
			});

			return image;
		}
		string message = string.Empty;
		public string Message
		{
			get { return message; }
			set { message = value; OnPropertyChanged(); }
		}

		public ICommand NotNowCommand { get; }
		public ICommand SignInCommand { get; }

		async Task SignIn()
		{
			try
			{
				IsBusy = true;
				Message = "Signing In...";

				// Log the user in
				await TryLoginAsync();
			}
			finally
			{
				Message = string.Empty;
				IsBusy = false;

				if (Settings.IsLoggedIn)
					App.GoToMainPage();
			}
		}

		public static async Task<bool> TryLoginAsync()
		{
			return true;
		}
	}
}
