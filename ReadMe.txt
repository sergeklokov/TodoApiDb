This is API application for testing purposes.

It's to test compatibility of LINQ to SQL with older version of SQL Server 2014 
Compatibility level 120

1. Using LINQ to SQL method Contains will cause SQLException
"Incorrect syntax near '$'"

2. To fix we use EF.Contains

3. But #2 raise the problem with unit test.

Also it's to write XUnit test.

Articles I read:
Breaking changes in EF Core 8 (EF8)
https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes

EF Core 8 Preview 4: Primitive collections and improved Contains
https://devblogs.microsoft.com/dotnet/announcing-ef8-preview-4/

My stack overflow questions:
How to unit test LINQ with EF.Constant?
https://stackoverflow.com/questions/79542141/how-to-unit-test-linq-with-ef-constant

AddScoped cause System.AggregateException: 
Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType:
https://stackoverflow.com/questions/79544123/addscoped-cause-system-aggregateexception-some-services-are-not-able-to-be-con

To read AppSettings values from a .json file in ASP.NET Core
https://stackoverflow.com/questions/31453495/how-to-read-appsettings-values-from-a-json-file-in-asp-net-core
