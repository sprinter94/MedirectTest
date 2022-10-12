# MedirectTest

1. Before Running API, make sure you have a redisa instance installed. If not follow this guide to install a redis instance on your machine 
https://dev.to/divshekhar/how-to-install-redis-on-windows-10-3e99

2. After redis is installed go to Scripts folder and run 1.CreateUsersTable.sql and 2.CreateExchangeRatesHistory.sql. In this test I used a MySql Instance.
3. Go to appsettings.Development.json and change the connectionstrings to both the MySqlInstance and redis instance to your i.p.
4. Run API
5. Call Users Controller to get a dummy JWT bearer token. This is done for the test purpose to have a jwt token since api methods are Authenticated. In real life Users Controller would be in a seperate micro service.
