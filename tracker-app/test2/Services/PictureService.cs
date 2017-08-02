using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using SQLite.Net;
using Xamarin.Forms;

namespace test2
{
	public class PictureService : IPictureStore<PicItem>
	{

        private readonly SQLiteConnection sql;

        public PictureService(SQLiteConnection sqlCon)
		{
            sql = sqlCon;
           
            //client.BaseAddress = new Uri($"{App.BackendUrl}/");

		}

        public PictureService()
        {
            sql = App.picService.sql;     

            //client.BaseAddress = new Uri($"{App.BackendUrl}/");
        }

		public async Task<bool> AddItemAsync(PicItem item)
		{
           
            sql.RunInTransaction(() => { 

                sql.Insert(item);            
            });
			return await Task.FromResult(true);
		}

		public Task<bool> DeleteItemAsync(PicItem item)
		{
            
            sql.Delete(item);
			
			return Task.FromResult(true);
		}

		public Task<PicItem> GetItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<PicItem>> GetItemsAsync(bool forceRefresh = false)
		{
            var result = sql.Table<PicItem>();

            return (IEnumerable<test2.PicItem>)await Task.FromResult(sql.Table<PicItem>().OrderByDescending(x => x.CreatedOn));
		}

		public Task<PicItem> UpdateItemAsync(string st, string folio, string note, PicItem item)
		{
            item.Folio_string = folio;
            item.ST_string = st;
            item.Note = note;
            sql.Update(item);

			return Task.FromResult(item);
		}

        Task<PicItem> IPictureStore<PicItem>.UpdateStateAsync(bool state, string color, PicItem item)
        {
            item.Color = color;
            item.Sent = state;
            sql.Update(item);

            return Task.FromResult(item);
        }

        public async Task<bool> SendPicItemAsync(PicItem item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            HttpClientHandler handler = new HttpClientHandler();

			using(var client = new HttpClient(handler, true)){

				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				//var uri = new Uri(string.Format(App.BackendUrl, string.Empty));
                var uri = new Uri(string.Format(App.BackendUrl+"/server/insert_data/"));
				var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(uri, content);
                string jsonString = await resp.Content.ReadAsStringAsync();
                Response response = JsonConvert.DeserializeObject<Response>(jsonString);

                return response.status == 200;
            }

        }
    }
}
