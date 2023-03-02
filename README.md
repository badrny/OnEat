# Eudonet
## _OnEat_

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

OnEat is a WebApi for restaurant management,
C#-.Net6 powered

- Easy to use
- ✨Magic ✨

## Features

- Add Restaurant
- Get restaurant by id 
- Get all restaurant
- Delete restaurant by id

## Tech

OnEat uses a number of open source projects to work properly.

- [.Net 6] - NET is a free, cross-platform, open-source developer platform for building many different types of Applications
- [Xunit] - xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework. 
- [grpc] - gRPC is a modern open source high performance Remote Procedure Call (RPC) framework that can run in any environment.
- [Entity Framework 6] - (EF6) is an object-relational mapper that enables . NET Framework, . NET Core, and modern . NET developers to work with relational data using domain-specific objects.

## Installation & Development

For windows installation see https://learn.microsoft.com/fr-fr/dotnet/core/install/windows?tabs=net70
- Clone repository
- Change ConnectionStrings (SqlConnection) in appsettings.json.
- Go to your repository and open cmd
```sh
cd Catering.API
dotnet build
dotnet run
```
-  Now the app listening on: https://localhost:7173 or on: http://localhost:5083 ** you can change port in appsettings.json 
-  open your favorite navigator and go to https://localhost:7173/swagger/index.html or http://localhost:5083/swagger/index.html
- 
## Licence
Free
**Free Software, Hell Yeah!**