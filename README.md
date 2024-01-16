# REST API for CDN Web Application
Create the RESTful API to register/delete/update/get for freelancers

## Architecture diagram:
<p align="center">
  <img src="https://github.com/shanukat/CDN_WebApi_Project/blob/master/images/clean.png" width="500" title="diagram"> 
</p>
I applied <b>Clean Architecture</b> to build the REST API. Clean architecture has a domain layer, Application Layer, Infrastructure Layer, and Presentation Layer

### Advantages of Clean Architecture
<ul>
  <li>The immediate implementation can implement this architecture with any programming language.</li>
  <li>
    The domain and application layer are always the center of the design and are known as the core of the system that why the core of the system is not dependent on external systems.
  </li>
  <li>
    This architecture allows to change the external system without affecting the core of the system.
  </li>
  <li>
    In a highly testable environment, can test the code quickly and easily.
  </li>
  <li>
    Can create a highly scalable and quality product.
  </li>
</ul>

## Prerequisites

* Visual Studio 2022
* ASP.NET Core 6.0
* SQL Server 2012

## packages ( install from Nuget package Manager)
   <p align="center">
  <img src="https://github.com/shanukat/CDN_WebApi_Project/blob/master/images/nuget.png" width="400" title="diagram"> 
   </p>
    <p align="center">
  <img src="https://github.com/shanukat/CDN_WebApi_Project/blob/master/images/unit_test_packages.png" width="400" title="diagram"> 
   </p>
   
## Implementation :
<ol>
  <li>Implemented a dotnetcore 6.0-WebApi @GET, @POST @PUT, @DELETE methods to capture Freelancers details.</li>
  <li>Secured the API using JWT token and API sent a JWT token(Bearer Token) with encoded.</li>
  <li>Need to import Microsoft.AspNetCore.Authentication.JwtBearer library using Nuget Package manager.</li>
</ol>

## Run & Test:

Run the API using Swagger as follows
   <p align="center">
  <img src="https://github.com/shanukat/CDN_WebApi_Project/blob/master/images/swagger_ui.png" width="800" title="diagram"> 
   </p>

Generate the Token before call to the API end point
<p align="center">
  <img src="https://github.com/shanukat/CDN_WebApi_Project/blob/master/images/token_generate.png" width="800" title="diagram"> 
   </p>
  

Pass the generated Access Token in request headers to call to the API endpoint
<p align="center">
  <img src="https://github.com/shanukat/CDN_WebApi_Project/blob/master/images/postman_get_response.png" width="800" title="diagram"> 
   </p>

## Deploy the Web App to Azure App Services
<ul>
  <li>Create new Azure App Service from Azure Portal</li>  
  <li>Deploy api to Azure App Service to publish the Web App to Azure.</li>  
</ul>


