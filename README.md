# Welcome to the ZenkoAPI

# Introduction

This API has been designed and written for my first year univeristy project, it will feed the front end that I make in react at a later data. Currently this API will handle all the crud operations for my project. This API utilises EntityFramework core in order to make updates to the local database, this project is currently set up for PostgreSQL however it can be changed to suit whatever DB you choose aslong as its supported by EF core. This ReadMe will include all the information to get this API set up locally to run. 

# Local Setup 

- 1. Create a appsettings.Development.Json file inside the root of the repository
- 2. Copy the following boilerplate code and paste it into the new file you created

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "PostgreSQLConnectionString": "YOUR_CONNECTION_STRING"
  }
}
```

- 3. If you are using PostgreSQL you are going to want to pop in your connection string into the [PostgreSQLConnectionString] property, if you are using some other DB you will have to update the program.cs and potentially install a package to set up the connection string you will also have to add a new property in the appsettings.Development.json
- 4. There should already be a database migration folder within this project so you should be able to open the Package Manager Console and write Update-Database, given you have set up the connection string right you should be able to refresh your database and see the tables within.
- 5. If for whatever reason there is no migration you can run the following command to create one Add-Migration "your message" you would then use Update-Database
- 6. You should now be able to use the API
