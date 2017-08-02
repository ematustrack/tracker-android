using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Plugin.Media;
using Xamarin.Forms;
using System.Linq;
using Plugin.Geolocator;
using Acr.UserDialogs;

namespace test2
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableRangeCollection<PicItem> Items { get; set; }
		public Command LoadItemsCommand { get; set; }
		public PicItem Item { get; set; }
		public List<ST> StItems { get; set; }
		public string ST { get; set; }
		public string Folio { get; set; }
        public PictureService picService;


		public ItemsViewModel(PictureService _picService)
		{
			Title = "Fotografias";
            picService = _picService;
			Items = new ObservableRangeCollection<PicItem>();
			StItems = new List<ST>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			//RemoveItemCommand = new Command(async () => await ExecuteRemoveItemCommand());

			MessagingCenter.Subscribe<NewItemPage, PicItem>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as PicItem;
				Items.Add(_item);
                Items.OrderByDescending(x => x.CreatedOn);
                await picService.AddItemAsync(_item);
			});
           
		}

		public async Task RemoveItemCommand(PicItem item)
		{
            await picService.DeleteItemAsync(item);
			return;
		}

		public async Task<Image> TakePicture()
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
			{
				//DisplayAlert("No Camera", ":( No camera avaialble.", "OK");

			}

           UserDialogs.Instance.ShowLoading("Obteniendo posicion", MaskType.Black);

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 25;
			var position = new Plugin.Geolocator.Abstractions.Position();
			try
			{

				position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

				if (position == null)
				{

				}
                UserDialogs.Instance.HideLoading();


			}
			catch (Exception ex) {
                UserDialogs.Instance.HideLoading();
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
			var base64 = Convert.ToBase64String(bytes);


            Item = new PicItem
            {
                FileName = base64,
                Sent = false,
                CreatedOn = DateTime.UtcNow.ToString("s") + "Z",
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Color = "#FFFFFF"
			};
            Item.ST_string = "";
            Item.Folio_string = "";
			await picService.AddItemAsync(Item);
			Items.Add(Item); //ESTO LO COMENTE RECIÉN
           
			MessagingCenter.Send(this, "AddItem", Item);

			//await Navigation.PopToRootAsync();

			return image;
		}

		public ST getST(int index)
		{
            var t = StItems.Find(x => x.pk == index);
            return t;
		}

        public ST getST(string st) {
            var t = StItems.Find(x => x.st == st);
            return t;
        }

		public Folio getFolio(int index, ST st)
		{
			return st.folios.Find(x => x.pk == index);
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
                var items = await DataStore.GetItemsAsync(false);
				//StItems = (List<ST>)await STDataStore.GetSTItemsAsync(true);

				Items.ReplaceRange(items);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
