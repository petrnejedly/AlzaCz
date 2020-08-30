# Alza WebApi Application

08/2020

This is an Alza WebApi Application created by Petr Nejedlý in response to the Alza's BE Developer Case Study assignment. The solution is based on .Net Core 3.1 and C# and created in Visual Studio 2019.

## GitHub public repository

- https://github.com/petrnejedly/AlzaCz

## Solution overview

The solution is divided into following parts

- Infrastructure (Repository)
- Domain (Service)
- Application (Facade)
- Presentation (Controllers)
- Tests (Unit tests)

## How to run the project

- Create an MSSQL database
- Run the ```Alza-InstallationScript.sql``` installation script. This will create database objects and populates data.
- Open the ```Alza\Alza.sln``` solution file in Visual Studio 2019
- Create an ```appsettings.json``` file in ```Alza\Presentation\Alza.Web\``` with the following content:

```
{
    "ConnectionStrings": {
        "ConnectionStringAlzaWeb": "Data Source=IPADDRESS; Initial Catalog=CATALOG; User ID=USERID; Password=PASSWORD;"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "AllowedHosts": "*",
    "ImageUrlPrefix": "https://cdn.alza.cz/",
    "ImageNull": "https://satyr.io/1024x768/lightgreen?text=Alza+rulez",
    "UseMockData": false
}
```
- Replace The connection string credentials with appropriate values
- Hit Ctrl + F5 to run the project in the browser

> Please kindly note the ```UseMockData``` *(bool)* setting key. There are two sets of data this application works with. **Database data** and **Mocked data**. If you set the ```UseMockData``` to **true**, mocked products (Headphones) will be returned. If you set the ```UseMockData``` to **false**, database products (Mobile phones) will be returned.

## List of exposed API URLs

### Get a single product by it's ID (HttpGet)
- /api/product/get/1
- /api/v1/product/get/1
- /api/v2/product/get/1

### Get a full list of all available products (HttpGet)
- /api/product/get
- /api/v1/product/get

### Get a paged list of available products (HttpGet)
- /api/v2/product/get/1/10

### Update the product's description (HttpPut)
- /api/product/update (HttpPut)
(You need to use Swagger, Postman or some other tool to query this API endpoint using HttpPut)

## Complete API documentation (Swagger)

- /swagger



### Todos

 - Write MORE Tests


License
----

MIT