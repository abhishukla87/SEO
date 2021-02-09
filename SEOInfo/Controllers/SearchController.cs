using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOInfo.Factory;
using SEOInfo.Model;
using System;
using System.Threading.Tasks;

namespace SEOInfo.Controllers
{
    [ApiController]   
    public class SearchController : ControllerBase
    {
        private readonly SearchServiceFactory _SearchServiceFactory;

        public SearchController(SearchServiceFactory SearchServiceFactory)
        {
            _SearchServiceFactory = SearchServiceFactory;
        }

        [Route("Search")]
        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]      
        public async Task<ActionResult<DataModel>> Get(string keyword="e-statement", string searchEngine = "Google")
        {
            try
            {
                var data = await _SearchServiceFactory.GetSearchService(searchEngine).GetData(keyword);

                return Ok(data);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Issue in processing request");
            }
        }
    }
}
