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


### Seed data
Account numbers to use 1, 2, 3, 4, 5, 6 for testing.

`
                    new Account(1, Currency.EUR, Status.Open),
                    new Account(2, Currency.USD, Status.Open),
                    new Account(3, Currency.EUR, Status.Closed), 
                    new AccountVIP(4, Currency.EUR, Status.Open, 0.01M),
                    new AccountVIP(5, Currency.USD, Status.Open, 0.05M),
                    new AccountVIP(6, Currency.EUR, Status.Closed, 0.01M),
 `
 
 ### How to use the API
 Operations API endpoint can have Deposit or Payment operationType. Payments should be negative and deposits positive number only:
 
 {
  "accountId": 6,
  "currency": "EUR",
  "amount": 100.50,
  "operationType": "Deposit",
  "date": "2022-09-12T05:24:40.156Z"
}

{
  "accountId": 6,
  "currency": "EUR",
  "amount": -100.50,
  "operationType": "Payment",
  "date": "2022-09-12T05:24:40.156Z"
}

