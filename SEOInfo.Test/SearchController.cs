using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SEOInfo.Controllers;
using SEOInfo.Factory;
using SEOInfo.Helper;
using SEOInfo.Model;
using SEOInfo.Service;
using System;
using System.Threading.Tasks;

namespace SEOInfo.Test
{
    public class Tests
    {
        private Mock<SearchServiceFactory> _mockSearchFactory;
        private Mock<GoogleService> _mocKGoogleService;
        private Mock<BingService> _mockBingService;
        private SearchController _searchController;
        private IOptions<AppSettings> _mockappsetting;
        private DataModel _dataModel;
        private string keyword = "e-statement";
        private string _googleService = "Google";
        private string _bingService = "Bing";
        private AppSettings appsettings;
        private Mock<IServiceProvider> _serviceProvider;


        [SetUp]
        public void Setup()
        {
            _dataModel = new DataModel
            {
                SearchResult = "1,5"
            };
            _serviceProvider = new Mock<IServiceProvider>();

            appsettings = new AppSettings()
            {
                MockGoogleURL = "http://localhost:5001/GetGoogle?api_key={0}&searchCriteria={1}&limit={2}",
                MockGoogleKey = "AIzaSyAmEfdsnMeHbe",
                MockBingURL = "http://localhost:5001/GetBing?api_key={0}&searchCriteria={1}&limit={2}",
                MockBingKey = "AIzaSyAmEfdsnMeHbe-R",
                NumberOFRecordsRead = 100,
                TextComparison = "www.sympli.com.au"

            };
           
            _mockappsetting = Options.Create(appsettings);
            _mocKGoogleService = new Mock<GoogleService>(_mockappsetting);
            _mockBingService = new Mock<BingService>(_mockappsetting);
            _mockSearchFactory = new Mock<SearchServiceFactory>(_serviceProvider.Object);           
            _mocKGoogleService.Setup(repo => repo.GetData(keyword)).ReturnsAsync(_dataModel);
            _mockBingService.Setup(repo => repo.GetData(keyword)).ReturnsAsync(_dataModel);
            _mockSearchFactory.Setup(repo => repo.GetSearchService(_googleService)).Returns(_mocKGoogleService.Object);
            _mockSearchFactory.Setup(repo => repo.GetSearchService(_bingService)).Returns(_mockBingService.Object);
            _searchController = new SearchController(_mockSearchFactory.Object);



        }

        [Test]
        public async Task GetGoogle_OK_ResponseType()
        {
            var result = await _searchController.Get("e-statement", "Google");
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<DataModel>(okResult.Value);
            Assert.True(okResult.StatusCode == StatusCodes.Status200OK);
        }

        [Test]
        public async Task GetBing_OK_ResponseType()
        {
            var result = await _searchController.Get("e-statement", "Bing");
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<DataModel>(okResult.Value);
            Assert.True(okResult.StatusCode == StatusCodes.Status200OK);
        }

        [Test]
        public async Task GetBing_NOTOK_ResponseType()
        {
            var result = await _searchController.Get("11", "Test");
            var objectResult = result.Result as ObjectResult;
            Assert.True(objectResult.StatusCode == StatusCodes.Status500InternalServerError);
        }
    }
}