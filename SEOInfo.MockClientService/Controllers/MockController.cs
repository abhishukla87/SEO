using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEOInfo.MockClientService.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SEOInfo.MockClientService.Controllers
{
    [ApiController]
    public class MockController : ControllerBase
    {
        
        [Route("GetGoogle")]
        [HttpGet]
        public ActionResult<List<Data>> GetGoogle(string searchCriteria, int limit)
        {
            return LoadJson("MockGoogle.json", searchCriteria, limit);
        }

        [Route("GetBing")]
        [HttpGet]
        public ActionResult<List<Data>> GetBing(string searchCriteria, int limit)
        {
            return LoadJson("MockBing.json", searchCriteria, limit);
        }

        private List<Data> LoadJson(string filename, string searchCriteria, int limit)
        {
            List<Data> items;

            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) +
                               @"\MockJson\" + filename;

            using (StreamReader r = new StreamReader(_filePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Data>>(json);
            }

            return items.Where(t => t.result.Contains(searchCriteria)).Take(limit).ToList();
        }


    }
}
