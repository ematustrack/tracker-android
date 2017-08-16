﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace test2
{
    public class STDataService : ISTDataService<ST>
	{
		HttpClient client;
		string url;
		public STDataService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
            client.Timeout = TimeSpan.FromMinutes(3);
            url = @"http://54.175.253.151";//@"http://54.175.253.151";
		}

		public Task<bool> AddItemAsync(ST item)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteItemAsync(ST item)
		{
			throw new NotImplementedException();
		}

		public Task<ST> GetItemAsync(string id)
		{
			throw new NotImplementedException();
		}


		public async Task<IEnumerable<ST>> GetSTItemsAsync(bool forceRefresh = false)
		{
			string mPhoneNumber = "";

			var ctx = Forms.Context;
			Android.Telephony.TelephonyManager tMgr = (Android.Telephony.TelephonyManager)ctx.GetSystemService(Android.Content.Context.TelephonyService);
			mPhoneNumber = tMgr.Line1Number;

			using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(url, string.Empty));

                //System.Diagnostics.Debugger.Break();
                client.DefaultRequestHeaders.Add("Token-Number", mPhoneNumber);
                var response = await client.GetAsync(uri + "server/getSTFolios/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<List<ST>>(content);
                    return Item;

                }
                return new List<ST>();
            }
		}

		public Task<bool> UpdateItemAsync(ST item)
		{
			throw new NotImplementedException();
		}

        public Task<bool> AddItemAsync(Folio item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Folio item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Folio item)
        {
            throw new NotImplementedException();
        }


    }
}
