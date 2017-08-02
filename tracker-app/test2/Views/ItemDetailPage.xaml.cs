using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Android.Widget;
using Xamarin.Forms;

namespace test2
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

		public ItemDetailPage()
        {
            InitializeComponent();

        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            
            BindingContext = this.viewModel = viewModel;
			this.ST.ItemsSource = viewModel.St_names;


		}
        


        void delete_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        async void send_Clicked(object sender, System.EventArgs e)
        {

            string st = "";
            string folio = "";
            string note;
            if (this.ST.SelectedIndex >= 0)
                st = viewModel.StItems[this.ST.SelectedIndex].st;
            else
                return;
            if (this.Folio.SelectedIndex >= 0)
                folio = viewModel.StItems[this.ST.SelectedIndex].folios[this.Folio.SelectedIndex].number;
            else
                return;

            note = notes.Text;
            //System.Diagnostics.Debugger.Break();

            var result = await viewModel.SaveItems(st, folio, note);
            if (result)
            {
                await viewModel.DataStore.UpdateStateAsync(true,"#E0F8E0", viewModel.Item);
                Toast.MakeText(Forms.Context, "Foto enviada correctamente", ToastLength.Long).Show();
            }
            else
            {
                await viewModel.DataStore.UpdateStateAsync(false,"#FFFFFF", viewModel.Item);
				Toast.MakeText(Forms.Context, "Foto NO fue enviada", ToastLength.Long).Show();
            }
            var x = 0;
            App.GoToMainPage();
            //await Navigation.PushAsync(new ItemsPage(App.picService));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.StItems.Count == 0)
            {
                
                viewModel.LoadItemsCommand.Execute(null);
                string st = "";
                string folio = "";
				if (this.ST.SelectedIndex >= 0)
					st = viewModel.StItems[this.ST.SelectedIndex].st;
				if (this.Folio.SelectedIndex >= 0)
					folio = viewModel.StItems[this.ST.SelectedIndex].folios[this.Folio.SelectedIndex].number;

                this.ST.Title = st;
                this.Folio.Title = folio;

				//Aqui se asignan las ST para el picker ST
				this.ST.ItemsSource = viewModel.St_names;

                this.ST.SelectedItem = viewModel.selectedST;
                //this.Folio.ItemsSource = viewModel.St_names;

            }
        }


        /* Unmerged change from project 'test2'
        Before:
                void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        After:
                void Handle_SelectedIndexChangedAsync(object sender, System.EventArgs e)
        */
        async void Handle_SelectedIndexChangedAsync(object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            var selectItem = picker.SelectedIndex;
            viewModel.Item.ST_string = picker.Items[selectItem];
            var folios = getST(picker.Items[selectItem]).folios;
            this.Folio.ItemsSource = folios.Select(x => x.number).ToArray();
            viewModel.selectedST = new ST(picker.Items[selectItem]);
            viewModel.Item.ST_string = picker.Items[selectItem];
            Console.WriteLine(picker.Items[selectItem]);
            //viewModel.DataStore.UpdateItemAsync(picker.Items[selectItem], "", this.notes.Text, viewModel.Item);
            await viewModel.SaveItemAsync(picker.Items[selectItem], "", this.notes.Text, viewModel.Item);

        }


        /* Unmerged change from project 'test2'
        Before:
                void Handle_SelectedIndexChangedFolio(object sender, System.EventArgs e) {
        After:
                void Handle_SelectedIndexChangedFolioAsync(object sender, System.EventArgs e) {
        */
        async void Handle_SelectedIndexChangedFolioAsync(object sender, System.EventArgs e)
        {
            if (ST.SelectedIndex < 0)
                return;

            var picker = (Picker)sender;
            var selectItem = picker.SelectedIndex;
            await viewModel.SaveItemAsync(viewModel.Item.ST_string, picker.Items[selectItem], this.notes.Text, viewModel.Item);
        }
        public ST getST(string st)
		{
			var t = viewModel.StItems.Find(x => x.st == st);
			return t;
		}
    }
}
