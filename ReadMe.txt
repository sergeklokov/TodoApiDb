This is API application for testing purposes.

It's to test compatibility of LINQ to SQL with older version of SQL Server 2014 
Compatibility level 120

1. Using LINQ to SQL method Contains will cause SQLException
"Incorrect syntax near '$'"

2. To fix we use EF.Contains

3. But #2 raise the problem with unit test.

Also it's to write XUnit test.

Details here:
Breaking changes in EF Core 8 (EF8)
https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes

How to unit test LINQ with EF.Constant?
https://stackoverflow.com/questions/79542141/how-to-unit-test-linq-with-ef-constant