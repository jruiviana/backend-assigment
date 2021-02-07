# Backend Development Assigment

This is a project for a API that search movie from a web service and save the request in a MongoDB database.

I decided to use a CQRS approach for the web service and database interations
I used  `MediatR(https://github.com/jbogard/MediatR)` library to manage the command and queries


For the APIKey Authentication I used the approach from `Josef Ottosson(https://josef.codes/asp-net-core-protect-your-api-with-api-keys/)` on his blog. It is very cool and we can extend to other types of authentication. I chose not use the Roles authorization. I change the Apiquery to get the Key from MongoDB database.
To Access the Administration endpoint you need to add the header `X-Api-Key:68869c32-d971-40f0-8b12-7392a153cd94`. This key is generated in the databas in the first time you run the project.

I'm using AutoMapper(https://automapper.org/) library to map the entites to Dto.

For the unit test I used `XUnit(https://xunit.net/)`, `FakeItEasy(https://fakeiteasy.readthedocs.io/en/stable/)` to mock the interfaces and `NBuilder(https://github.com/nbuilder/nbuilder)` to generate fake data.

I created a Middleware to handle the exceptions, save in a log and return to the caller.

I extended the Movie search service to get data from other sources. So you can only add a new entry in the `MovieSources` array in the configuration file, and it this new source is available to search movies.

O would like to use `OData(https://www.odata.org/)` for the endpoint to give other option of filter. We already use it with a SQL Server database, but never tried with MongoDB. This can be a improvement for next time.



## Estimation

For this task my estimation was 4 hour.

At first Day work 2 hours on the project
I created the basic project files, the class libraries and the controler and query to get the movie from OMDB

At second day worked for 2:30 hour on the project
I created the unit tests for the Movie search commands, MongoDb service and all commands and queries for the administration feature

At third day I worked for more 1:30 hours on the project
I created the logic for the API Key authentication, extend the movie search to use more than one source and created the unit tests for the Administration features

In the end I worked  6:00 on the project, a litle bit more than my estimation. I took more time beacuse I had to find a good approach for the APIKey authentication, extending it to save the key in the database. I take more time extending the search to use more source and creating all unit tests. And yes I underestimated a little bit the complexity to do this project using CQRS.