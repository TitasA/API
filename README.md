### Dependencies
-     <TargetFramework>net6.0</TargetFramework>
-     Server=(localdb)\\mssqllocaldb

### How to run
- Solution (.sln) file is located in the ..src\API folder.
- To run via command line enter dotnet run from API folder. Swagger available on https://localhost:7222/swagger/index.html
- Automatic migration should create a new database called: AccountOperationDev.

### Main goals
- Maintainable code with low coupling.
- Easy to extension  with new back account type.
