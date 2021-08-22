# Palfinger.ProductManual

The solution is responsible for provisioning the product manual with its different configurations from a product Id.

# Index
- [Assessment resume](#assessment-resume)
- [Swagger](#swagger)
- [Requirements](#requirements)
- [Database resources](#database-resources)
- [Testing](#testing)
- [Common Objects](#common-objects)
- [Migrations](#migrations)
- [Nugets](#nugets)

# Assessment resume

## Intro
Imagine a product that can be configured by several attributes. Some of them are binary
(e.g.: The product comes with a certain feature or not) and some are multi value (e.g.: You
can select a certain kind or capacity from a list). One example for such a product could be
a bike that can be configured with or without integrated lights and with different kinds of
gears.
For this product we must provide a user manual in a web frontend. As there a many
possible combinations how the product could be configured it would be inefficient to write a
manual for each of them. But we also don’t want to confuse the customer by providing a
150% manual containing parts and features the customer doesn’t have. Therefor we need
to have the manual also configurable and provide it tailored to the specific product. Each
actual sold product has a unique product id and the information about the configuration is
stored in our system.
The manual itself consists of several pages. Each page consists of an illustration and texts
like instructions and hints for the user. The Illustrations are image files and are stored on a
public CDN.
The user manual web frontend consumes a REST API.
An example of such an application could look like this:

![ExamplePhoto](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/5a80c11d-de0b-48ff-a9f9-c03ee9f5fe4e/Untitled.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAT73L2G45O3KS52Y5%2F20210822%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20210822T113637Z&X-Amz-Expires=86400&X-Amz-Signature=4a39a1f62e0fb6f11fd44115f8e349a07d87cb20dafe3eec6d8f629f3412ba5e&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22Untitled.png%22)

## Task
The scope of this task is limited to the backend side of the system described above. Also,
the content creation like creating new manuals, texts and illustrations are not part of this
task. Only the REST interface for the frontend and the data structure behind need to be
covered.
- Create a SQLite database with tables that represent the user manual and also holds
the product configurations.
- Develop [asp.net](http://asp.net/) core application that reads from this database and provides the
manual with all necessary data to display it on the frontend.
- The manual can be requested for a specific product number.
- The manual is always provided in English. Localization is out of scope for this task.
- The product and therefor also the manual can be configured by at least 3 attributes.
- The [asp.net](http://asp.net/) core API should also contain some automated tests.
- The kind of product, attributes and the content can be chosen according to personal
preferences.

# Swagger

We can run the .api project and access the swagger interface to see the endpoints available for consumption.
In the route: https://localhost:5001/swagger/index.html

# Requirements

- .Net Core 5.0: https://dotnet.microsoft.com/download/dotnet/5.0
- Sqlite 3.36.0: https://www.sqlite.org/index.html

# Database resources

Sqlite Data Base on path: C:\sqlite\sqlite-tools\productManual.db

# Testing

To run the tests locally, the environment variable ASPNETCORE_ENVIRONMENT must be equal to Development. This environment variable is set by default visual studio according to the environment in which you deploy (Debug = Development, Release = Production). Sometimes it does not work locally (in production it always works the first time), in this case you have to add a environment variable that is ASPNETCORE_ENVIRONMENT = Development and ** REBOOT !! Visual Studio **.

# Common Objects

  - Product: It refers to the highest entity of the manual, being the same one only per manual.
  - Attribute: A product can have several attributes to compose it.
  - Configuration: The configurations are subject to a specific attribute that contains one or more configurations.
  
Data Model: https://github.com/adria-arquimbau/Palfinger.ProductManual/blob/develop/productManual.png

# Migrations
Entity Framework has been used for the data model. This data model will be implemented incrementally, using the Code-First approach. It is located within the Palfinger.ProductManual.Infrastructure.Data project.

To run a database update you can use the vscode. All instructions must be executed within the Palfinger.ProductManual.Infrastructure.Data directory and execute the following command:

```
   dotnet ef database update  -s ../Palfinger.ProductManual.Api/
```

# Nugets

- FluentAssertions: https://www.nuget.org/packages/FluentAssertions
- LanguageExt.Core: https://www.nuget.org/packages/LanguageExt.Core
- Newtonsoft.Json: https://www.nuget.org/packages/Newtonsoft.Json
- AutoFixture.Xunit2: https://www.nuget.org/packages/AutoFixture.Xunit2
- MediatR: https://www.nuget.org/packages/MediatR
- NSubstitute: https://www.nuget.org/packages/NSubstitute
- XBehave: https://www.nuget.org/packages/Xbehave
- xunit: https://www.nuget.org/packages/xunit
  
