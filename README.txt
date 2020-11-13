This is intended to be the start of a basic REST API backed by a local sqlite database.  It consists of two basic models, their associated service, and a basic unit test suite.  

To run the tests in Visual Studio 2019:

1) Open Tools > NuGet Package Manager > Package Manager Console
2) Restore project dependencies in the Package Manager Console with command: Update-Package -reinstall System.Data.SQLite.Core
3) Run all tests (Ctrl-R, A or Debug Ctrl-R,Ctrl-A)

BaseTest.TestCount is SOMETIMES failing, while TestGet always succeeds.  Your first task is to figure out why and fix it.

Your second task is to perform a code review for the services / models.  Add comments with your suggested changes.  No need to implement.
