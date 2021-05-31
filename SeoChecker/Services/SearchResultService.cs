using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SeoChecker.Services
{
    public class SearchResultService: ISearchResultService
    {
        private static readonly Regex GoogleSearchResultLinksRx = new Regex("<a href=\"/url\\?q=[^>]+>");

        private static readonly Regex BingSearchResultLinksRx = new Regex("<h2><a href=[^>]+>");

        private static readonly string GoogleBaseUrl = "https://www.google.com";

        private static readonly string BingBaseUrl = "https://www.bing.com";
        public string GetSearchResultRanks(string uri, string CompanyName)
        {
            // Make web request to uri and returing the response as a string
            string searchResultDom = GetUriResponseAsText(uri);

            // From the response, filter the search result links
            MatchCollection searchResultLinks = GetSearchResultLinksCollection(uri, searchResultDom);
            
            // Find the search reult ranks of the company using the links
            var companySearchReultRanks = GetSearchResultRankList(searchResultLinks, CompanyName);

            // Set the rank list to 0 if the search result does not contain the company name
            if (companySearchReultRanks.Count == 0)
            {
                companySearchReultRanks = new List<int>() { 0 };
            }

            // Return the ranks list as a comma separated string
            return string.Join(", ", companySearchReultRanks);
        }

        private string GetUriResponseAsText(string uri)
        {
            string responseText;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.ASCII))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;
        }

        private MatchCollection GetSearchResultLinksCollection(string uri, string domText)
        {
            Regex linksRx;

            if (uri.StartsWith(GoogleBaseUrl))
            {
                linksRx = GoogleSearchResultLinksRx;
            } 
            else if (uri.StartsWith(BingBaseUrl))
            {
                linksRx = BingSearchResultLinksRx;
            }
            else
            {
                throw new System.Exception("Specified search engine is not supported");
            }

            return linksRx.Matches(domText);
        }

        private List<int> GetSearchResultRankList(MatchCollection links, string CompanyName)
        {
            var rankList = new List<int>();
            
            // Iterate through the links and storing the rank of search results where the company name appears
            for (int i = 0; i < links.Count; i++)
            {
                Match searchResultLink = links[i];
                GroupCollection groups = searchResultLink.Groups;

                if (groups[0].Value.Contains(CompanyName))
                {
                    rankList.Add(i + 1);
                }
            }

            return rankList;
        }
    }
}
