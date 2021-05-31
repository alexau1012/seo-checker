using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeoChecker.Services;

namespace SeoChecker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchResultRankController : ControllerBase
    {
        private static readonly string CountryCode = "countryAU";
       
        private static readonly int ResultsCount = 100;

        private static readonly string CompanyName = "www.sympli.com.au";

        private readonly ISearchResultService _searchResultService;

        private readonly ILogger<SearchResultRankController> _logger;
        public SearchResultRankController(
            ISearchResultService searchResultService,
            ILogger<SearchResultRankController> logger)
        {
            _searchResultService = searchResultService;

            _logger = logger;
        }

        // GET api/www.google.com/e-settlements
        [HttpGet("{baseUrl}/{keywords}")]
        public IActionResult Get(string baseUrl, string keywords)
        {
            // Construct query parameters
            string queryParams = $"/search?q={keywords}&cr={CountryCode}&num={ResultsCount}";

            // Construct the request URI
            string uri = "https://" + baseUrl + queryParams;

            string ranks;

            try
            {
                ranks = _searchResultService.GetSearchResultRanks(uri, CompanyName);
            }
            catch
            {
                return NotFound("Unable to retreive the search result ranks");
            }

            return Ok(ranks);
        }
    }
}
