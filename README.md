# .NET Core API ReactJS

A project for study purpose.

## Projects

FinBY.API - .Net Core API with JWT
* needs a valid conection to a SQL Server DB in appsettings.json
* DBStartUp class has the procedure to create the database and fill it with basic data.

finby.reactjs - React JS Interface

FinBY.Domain - Domain Project using DDD, MediatR and Repository pattern

FinBY.Infra - Infra Project using Entity Framework Core and SQL Server

FinBY.EmailQueueReceiver - Azure.ServiceBus Test project
 * QueueEmailService on Infra has the sender part.
 * needs a valid AzureServiceBus connection in appsettings.json on API project

## Run
run the FinBY.API and finby.reactjs project together on visual studio

## Test Projects

FinBY.Tests - Unit Tests using MSTest, FluentAssertions and NSubstitute.

FinBY.DBTests - Integration Test with DB using MSTest, FluentAssertions and NSubstitute.
* needs a valid conection to SQL Server in TestApplicationDbContext

FinBY.WebTests - BDD E2E Test using NUnit, SpecFlow, Selenium and FluentAssertions.