# Alza WebApi Application

08/2020

This is an Alza WebApi Application created by Petr Nejedlý in response to the Alza's BE Developer Case Study assignment. The solution is based on .Net Core 3.1 and C# and created in Visual Studio 2019.

## GitHub public repository

- https://github.com/petrnejedly/AlzaCz

## Solution overview

The solution is divided into following parts

- Infrastructure (ProductRepository)
- Domain (ProductService)
- Application (ProductFacade)
- Presentation (ProductsController)
- Tests (Unit tests)

## How to run the project

- Create an MSSQL database
- Run the ```Db\Alza-InstallationScript.sql``` installation script. This will create database objects and populate data.
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
    "ImageNull": "https://satyr.io/1024x768/lightgreen?text=Alza+rulez"
}
```
- Replace The connection string credentials with appropriate values
- Hit Ctrl + F5 to run the project in the browser

## List of exposed API URLs

### Get a single product by it's ID (HttpGet)
- /api/products/get/1
- /api/v1/products/get/1
- /api/v2/products/get/1

### Get a full list of all available products (HttpGet)
- /api/products/get
- /api/v1/products/get

### Get a paged list of available products (HttpGet)
- /api/v2/products/get/1/10

### Update the product's description (HttpPatch)
- /api/products/updatedescription/1 (HttpPatch)
(```curl -X PATCH "https://localhost:44364/api/v2/Products/updatedescription/1" -H "accept: application/json" -H "Content-Type: application/json" -d "\"new description\""```)

(You need to use Swagger, Postman or some other tool to query this API endpoint using HttpPatch)

## Complete API documentation (Swagger)

- /swagger

License
----

MIT