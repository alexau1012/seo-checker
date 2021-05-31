using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeoChecker.Services
{
    public interface ISearchResultService
    {
        public string GetSearchResultRanks(string uri, string CompanyName);
    }
}
