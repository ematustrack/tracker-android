using Xamarin.Forms;

namespace test2
{
	public class BaseViewModel : ObservableObject
	{
		/// <summary>
		/// Get the azure service instance
		/// </summary>
		public IPictureStore<PicItem> DataStore => DependencyService.Get<IPictureStore<PicItem>>();
		public ISTDataService<ST> STDataStore => DependencyService.Get<ISTDataService<ST>>();

		bool isBusy = false;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}
		bool isBusy_ = false;
		public bool IsBusy_
		{
			get { return isBusy_; }
			set { SetProperty(ref isBusy_, value); }
		}
		/// <summary>
		/// Private backing field to hold the title
		/// </summary>
		string title = string.Empty;
		/// <summary>
		/// Public property to set and get the title of the item
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}
	}
}

