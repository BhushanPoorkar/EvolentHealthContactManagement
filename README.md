# EvolentHealthContactManagement

Things used :-
1 Asp.net Web API 2
2 MSTest
3 Ninject 3.3
4 MOQ
5 EF6
6 AutoMapper 7.0.1
7 VS 2017 Community edition
8 SQL server 2014
9 Postman (for testing)

Open project in VS 2017 using solution file,solution has two project one web api and other test project, when project will be build all nuget packages will get installed.
EF 6 code first is used in the project and project has a initiallization file (for seeding data in db), when first request is made to web api from Postman after running form VS a database will get created on (localdb)\MSSQLLocalDB mentioned in Connectionstring in web.config.

Test system should have Sql server.
Project also has test project and has test for controller.

