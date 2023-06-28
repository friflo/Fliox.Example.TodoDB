# **TodoDB**

The **TodoHub** is a Web server application to demonstrate the features of the
[**JSON Fliox**](https://github.com/friflo/Friflo.Json.Fliox#-features) **.NET** library.

*In short*  
**JSON Fliox** is .NET library supporting simple and efficient access to SQL & NoSQL databases via C# or Web clients.

For a simple setup the server **is also the database** storing records (entities) in the **file-system**.  
This enables running the server **without** any configuration or installation of a third party DBMS (database management system).


## TodoClient

The key class when running a HTTP server using **Fliox Hub** is [**TodoClient.cs**](Client/TodoClient.cs).  
This class provide two fundamental functionalities:
1. It is a **database client** providing type-safe access to its containers, commands and messages
2. It defines a **database schema** by declaring its containers, commands and messages.  
  The schema is used by host for **entity validation** and exposing the schema in various formats:  
  **JSON Schema**, **OpenAPI**, **GraphQL**, **HTML**, **Typescript**, **C#** & **Kotlin**.


## Files
```
📂 Client
┣ 📄 TodoClient.cs      1. is a database client
┃                       2. is a database schema for a Hub
📂 Hub
┣ 📄 Program.cs         bootstrapping & configuration of host   > dotnet run
┣ 📄 Provider.cs        use DB provider: memory, file, SQLite, MySQL, MariaDB, PostgreSQL or SQL Server
┣ 📄 Startup.cs         ASP.NET Core 6.0 integration
📂 Test
┣ 📄 TodoTests.cs       unit tests                              > dotnet test
┗ 📄 Trial.cs           small samples                           > dotnet run
```