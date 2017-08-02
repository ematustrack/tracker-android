using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Media;
using Xamarin.Forms;

namespace test2
{
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;
        PictureService p;

		public ItemsPage(PictureService picService)
		{
            InitializeComponent();

			BindingContext = this.viewModel = new ItemsViewModel(picService);
            p = picService;



		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
            var item = args.SelectedItem as PicItem;
			if (item == null)
				return;
            //viewModel.RemoveItemCommand(item);

            var itemdetail = new ItemDetailViewModel(p, item);
            await Navigation.PushAsync(new ItemDetailPage(itemdetail));

            // Manually deselect item

			ItemsListView.SelectedItem = null;
            
		}

		async void AddItem_Clicked(object sender, EventArgs e)
		{
            Console.Write("AddItem_Clicked");
            await viewModel.TakePicture();

			//await Navigation.PushAsync(new NewItemPage());
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
            if (viewModel.Items.Count == 0)
			{
                viewModel.LoadItemsCommand.Execute(null);


			}
			
		}
	}
}
