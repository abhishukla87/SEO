using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SEOInfo.Helper;
using SEOInfo.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SEOInfo.Service
{
    public class BingService : ISearchService
    {
        private AppSettings _appsettings;
       
        public BingService(IOptions<AppSettings> appsettings)
        {
            _appsettings = appsettings.Value;
        }
        public async Task<DataModel> GetData(string searchCriteria)
        {

            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format(_appsettings.MockBingURL,
                                                 _appsettings.MockBingKey,
                                                 searchCriteria,
                                                 _appsettings.NumberOFRecordsRead));

                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                return new DataModel
                {
                    SearchResult = JsonConvert.DeserializeObject<List<SearchModel>>(json).FilterRecord(searchCriteria)
                };

            }
        }
     }
 }

