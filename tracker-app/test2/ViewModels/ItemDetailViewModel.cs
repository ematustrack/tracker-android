using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V7.App;
using Android.Widget;
using Xamarin.Forms;

namespace test2
{
	public class ItemDetailViewModel : BaseViewModel
	{
        private readonly object postResult;

        public PicItem Item { get; set; }
		public Command LoadItemsCommand { get; set; }
        public List<ST> StItems { get; set; }
        public List<Folio> FolioItems { get; set; }
		public ObservableRangeCollection<string> St_names { get; set; }
        public List<string> st_names2 { get; set; }
		public ST selectedST { get; set; }
        public bool result_insert { get; set; }
        internal bool PostResult { get; set; }
        public PictureService picService;

        public ItemDetailViewModel(PictureService _picService, PicItem item = null)
		{
           
			Item = item;
            picService = _picService;
			StItems = new List<ST>();
            FolioItems = new List<Folio>();
			St_names = new ObservableRangeCollection<string>();
            result_insert = PostResult;
            //stDataService = _stDataService;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());

		}

		public async Task<bool> SaveItems(string st, string folio, string note)
		{
            var result = await picService.UpdateItemAsync(st, folio, note, Item);
            PostResult = await picService.SendPicItemAsync(result);
            return PostResult;
		}


        /* Unmerged change from project 'test2'
        Before:
                public bool SaveItem(string st, string folio, string note, PicItem item)
        After:
                public bool SaveItemAsync(string st, string folio, string note, PicItem item)
        */
        public async Task<bool> SaveItemAsync(string st, string folio, string note, PicItem item)
        {

            await picService.UpdateItemAsync(st, folio, note, item);
            return true;
        }

        async Task ExecuteLoadItems() {
            if (IsBusy)
				return;
            IsBusy = true;

            try {
                var items = await STDataStore.GetSTItemsAsync();
                StItems = (List<ST>)items;
			foreach(ST s in StItems)
				{ 
					St_names.Add(s.st);

                    //StItems.Add(s);

				}
				//StItems.ForEach((obj) => st_names2.Add(obj.st));
			}
			catch (Exception ex) 
            { //Ignore this
				var e = 0;
			}
			finally 
            {
				IsBusy = false;
			}
		}

	}
}
		
