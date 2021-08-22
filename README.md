# Palfinger.ProductManual

The solution is responsible for provisioning the product manual with its different configurations from a product Id.

# Index
- [Requirements](#requirements)
- [Database resources](#database-resources)
- [Testing](#testing)
- [Common Objects](#common-objects)
- [Migrations](#migrations)
- [Nugets](#nugets)

# Requirements

- .Net Core 5.0
- Sqlite 3.36.0 

# Database resources

Sqlite Data Base installed on path: C:\sqlite\sqlite-tools\productManual.db

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

