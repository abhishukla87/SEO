using SEOInfo.Model;
using System.Threading.Tasks;

namespace SEOInfo.Service
{
    public interface ISearchService
    {
        public Task<DataModel> GetData(string searchCritera);
    }
}
