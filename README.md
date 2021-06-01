# SEO Checker
The purpose of SEO Checker is to provide a simple to use interface to determine how a company's SEO is performing in different search engines by identifying their search result ranks.

The current MVP of SEO Checker has the following core features:
- Support Google and Bing search engines
- Check SEO performance via REST API
- Simple and robust user interface
  
![result-example](https://user-images.githubusercontent.com/54974691/120317209-b1d81e80-c321-11eb-854b-5185f2cdbde4.PNG)

![result2-example](https://user-images.githubusercontent.com/54974691/120317219-b43a7880-c321-11eb-9dbe-ad6757fcfaab.PNG)

## Application Sequence Diagram
![SeoChecker-sequence-diagram](https://user-images.githubusercontent.com/54974691/120324989-90c7fb80-c32a-11eb-92a3-5e870d0666fe.png)

## User Story
As the CEO of Sympli:
> I want to see where the company is ranked in the search results of Google when searching for the keywords of "e-settlements". I would also like to do this in other search engines and compare the results.

## Technology Stack
- Frontend: TypeScript, Angular and Angular Material
- Backend: C# and .NET Core

## Run the Project
### Requirements

- Visual Studio 2019 Community <version>
- .NET Core 3.1 or higher
- Node.js and npm
  
1. Clone this repository locally :
  
``` bash
git clone https://github.com/alexau1012/seo-checker.git
```
  
2. Open project solution file via Visual Studio

3. Run ```npm install``` in the ClientApp folder to install app dependencies

4. Build and run the application
  
5. Browse to http://localhost:44344
  
## Add more Search Engines

To support a new search engine, add it to the supportedSearchEngines property in the environment variable (.\ClientApp\src\environments\environment.ts)
  
``` typescript
export const environment = {
  production: false,
  seoCheckerApi: 'searchresultrank',
  supportedSearchEngines: ['google', 'bing']
};  
```
  
> :warning: **Not all search engines can work:** The url needs to conform to the format of `https://www.<searchEngine>.com/search?q=<keywords>&cr=countryAU&num=100`
  
### Other Tested Compatiable Search Engines
- Startpage

## Current Limitations
### Search Engines Compatibility
Most search engines are not supported as the application requires the request url to follow a particular format.

#### Solution
1. Identify the correct format of each search engine's search URL and implement them to the backend server logic so that the correct URL can be constructred with the provided search engine and keywords.

### Performance
Slow response time as the retrieval of search results contain a lot of unnecessary information.

#### Solution
1. Instead of making a GET request to the search URL, use the REST APIs provided by the search engine. For Google, this is Custom Search JSON API and Bing Web Search API for Bing.
2. Caching the results so that fewer requests are made.

### Availability & Reliability
Making too many requests to a search engine within a certain time could result in 429 errors (Too Many Requests).
  
#### Solution
1. Using the search engine's REST APIs would allow the request threshold to be known and be configured to adapt to traffic demand.

## Future Works
### Serverless Architecture
In the present state, the demand for this application is low as it will only be used by Sympli's CEO. However in future versions, the application shall be able to support different companies and many other search engines. This could potentially generate an increase in traffic.
  
Deploying SEO Checker on AWS Serverless using lambda, API Gateway, DynamoDB and other services can adapt to increase in demand while keeping a low cost during low traffic.

### E2E Testing
The current size of the application is small, so manual testing is sufficient. As the application grows, automated E2E testing shall be performed before each deployment. The Cypress framework is a great tool for E2E testing and would be a nice fit for our architecture as Cypress can be integrated in to AWS Amplify seamlessly.
