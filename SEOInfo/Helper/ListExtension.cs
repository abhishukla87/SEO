using SEOInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEOInfo.Helper
{
    public static class ListExtension
    {
        public static string FilterRecord(this List<SearchModel> searchResult, string textcomparison)
        {
            if (searchResult == null)
            {
                throw new Exception("Value can't be null");
            }
            else
            {
                var filteredResult = searchResult.Where(t => t.result.Contains(textcomparison)).Select(i => i.id);

                string result = filteredResult.Count() > 0 ? string.Join(",", filteredResult) : "0";

                return result;
            }  
        }
    }
    
}
