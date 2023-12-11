## Notes for technical test project

### Changes I made
* separated request model into new Models/VatRegistrationRequest.cs
* reduced controller to a quite thin version
* implemented service factory to generate only the needed registration processor (according to requirements of different countries)
* organized different registration processor classes for each country
* moved URL for GB into the configuration file
* to improve testability chose dependency injection for http and queue clients
* the starter code did not work for Germany because the second argument of serialize was "this" keyword and "this.request" was/is needed, so I corrected the issue
* registered new services in Program.cs
* added test project with some test classes, wrote very basic controller and processor tests with NUnit

### Suggestions
* the starter code did not contain a logger so I did not add it, but it would be useful to implement one
* validation of the data coming from the request also would be useful (maybe it is done somewhere else, so I did not bother with it)