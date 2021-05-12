# Trustwave - Technical Test
This is a technical test for Senior Full Stack Developer at Trustwave, which contemplates a Subdomain API in ASP.NET Core MVC C# using Web API, interfaces, design principle of inversion of control (IoC), singleton design pattern and more.
#
### Prerequisites
1. Visual Studio 2019 or later with the following workloads:
* ASP.NET and web development
* .NET Core cross-platform development
* Download at https://visualstudio.microsoft.com/downloads/
2. Microsoft .NET Core 3.1.3 or later
* Download at https://dotnet.microsoft.com/download/dotnet-core/3.1
#
### How to get the solution
1. Clone the repository on your machine OR download the repository and unzip it
### How to run the ASP.NET Core Web App
1. Open the Trustwave-CodingExercise solution
2. Rebuild the solution to install dependencies
3. Set the project Trustwave.Api as StartUp Project
4. Run the project (simply press F5)
#
### How to run the ASP.NET Core Web Api
1. Open the prompt command
2. Navigate to Trustwave.Api directory
3. Run the following command
* dotnet run
* Api will be available on http://localhost:57129/ and https://localhost:44359/
* Enumerate: $ curl -X GET -H "Content-Type: application/json" -k https://localhost:44359/enumerate/(domain_name)
* Find IP Addresses: $ curl -X POST -H "Content-Type: application/json" -d '{ "subdomains": "List<string>" }' -k https://localhost:44359/findipaddresses/
#
### How to run the Unit Tests
1. Open the prompt command
2. Navigate to Trustwave.Api.Test directory
3. Run this command
* dotnet test
#
### About the Coding Project
Create a simple .NET web application that implements the following:

Task 1: Subdomain Enumeration Endpoint
URL /subdomain/enumerate/(domain_name) accepts a GET request. Takes the domain_name parameter and responds with all the possible subdomains which contains only third-level domains with no more than 2 alphanumeric characters.
For example, if the domain name is yahoo.com, the possible results will include a.yahoo.com, ab.yahoo.com, but not x.a.yahoo.com or abc.yahoo.com.
Note: the subdomain doesn’t have to be in existence on the public internet.

Task 2: IP Addresses Endpoint
URL /subdomain/findipaddresses accepts a POST request. When posting a list of subdomains, the associated IP addresses shall be returned for each of the subdomains.
You can use System.Net.Dns.GetHostAddresses(string hostNameorAddress) method to get the list of IP Addresses.
Hint: This endpoint should be able to handle large amount of IP look-ups in a timely manner.

Task 3: Single Page Web Interface
The page should contain a text field (domain name) and two buttons and a suitable grid layout mechanism which scrolls independently to the page.
On clicking of the ‘List Subdomains’ button, a HTTP Request will send the entered valid domain name to the ‘Subdomain Enumeration Endpoint’. Render the result to the page.
On clicking of the ‘Find IP Addresses’ button, a HTTP request shall be send to the ‘IP Addresses Endpoint’, the request body of which shall contain all the subdomains currently listed on the page.
Render the result to the page.
#
### Assumptions
The challenge was interesting and very pleasant to put my logic into practice.

I chose to focus on the design and modeling of a Web API to implement the Subdomain API using ASP.NET Core due to the improved performance compared to ASP.NET stardard and other programming languages. Also, it is cross-platform and has great community support.

The API has been fully developed using Async methods, and I also have suggested improvements that I would do if I had more time. Please see TODO list.

I have implemented a logic to find all possible combinations up to 2 characters from a-z. Please see FindCombinations method.

As suggested, I have used System.Net.Dns.GetHostAddresses(string hostNameorAddress) method to find all IP address by subdomain. Please see FindIpAddressBySubdomainAsync method.

Some case tests were coded and automated for controllers and services using xUnit.

// TODO
* Improve the UI and UX
* Use memory cache for each API method
* Create more test scenarios
* Deploy to a Docker container or an AWS EC2 instance
