# seo-checker

The purpose of this application is to provide a simple interface to check how a company's SEO is performing in different search engines by identifying their search result ranks. 

### Performance

The current implementation retrieves the company's ranks by making a search using the search engine's URL and looking through the search result links. This is slow as the response contains a lot of unnecessary information, which results in longer response time. To improve performance for example in Google, the Custom Search JSON API can be used to retrieve only the required information, making the response time less.

### Availability

The search result ranks are obtained by making a request to the search engine URL each time. This could cause 429 Too Many Requests errors, as search engines put a limit on the number of requests that a user can make within a certain time. 

Some strategies to improve availability:
- Caching the results
- The application can be deployed across multiple regions

### Reliability

The current solution is not very reliable due to the limit placed on the number of searches in a certain time enforced by the search engine. For the application to be more reliable, it needs to be using the search engine's REST APIs.
