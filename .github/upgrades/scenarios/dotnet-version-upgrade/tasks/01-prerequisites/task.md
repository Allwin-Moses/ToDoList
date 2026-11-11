# 01-prerequisites: Verify .NET 10 toolchain and capture migration baseline

Verify the installed .NET 10 SDK and solution metadata, inventory the existing project structure, Web.config settings, Entity model, ADO.NET commands, stored procedure names, controller routes/actions, Razor view behavior, and available test projects. Record the endpoint and feature baseline in the task artifacts so the migrated application can be checked against the original behavior.

**Done when**: The .NET 10 SDK is available; baseline files and source inventory identify all routes, CRUD operations, stored procedures, configuration keys, and affected files; no source files are changed by this task.

## Scope Inventory

- **Projects affected**: `Github project 1/Github project 1.csproj` only; no project dependencies or test projects were discovered.
- **Distinct concerns**: legacy ASP.NET MVC/Web API host, Web.config configuration, ADO.NET SQL Server data access, Entity model, CRUD controller/actions, route/filter registration, and one Razor view.
- **Assessment signals**: project targets `net48`, is non-SDK-style WAP, has 10 legacy packages, 51 binary-incompatible and 47 source-incompatible API findings, and uses System.Web MVC/Web API APIs.
- **Relevant skills**: ASP.NET Framework-to-Core migration, SDK-style conversion, target-framework management, Microsoft.Data.SqlClient migration, and build validation.

## Research Findings

- .NET 10 SDK validation succeeded. The solution contains one project: `Github project 1/Github project 1.csproj`.
- No test projects were discovered. The project is a legacy web application with source files `Global.asax.cs`, `Models/Entity.cs`, `Models/dataaccess.cs`, `Controllers/TodoController.cs`, `App_Start/RouteConfig.cs`, `App_Start/FilterConfig.cs`, `App_Start/WebApiConfig.cs`, and `Views/Todo/View1.cshtml`.
- `Web.config` defines connection string `info` for `Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;` and app settings for client validation and unobtrusive JavaScript. No username/password is present.
- `Models/dataaccess.cs` calls stored procedures `GetAllTasks`, `AddTask` (`@TaskName`), `ToggleTaskCompletion` (`@Id`), and `DeleteTask` (`@Id`) using `System.Data.SqlClient`. The migration must preserve those names, parameters, and result columns `Id`, `TaskName`, and `IsCompleted`.
- `TodoController` exposes `View1` (GET), `Add` (POST), `ToggleComplete` (GET), and `Delete` (GET), all redirecting to `View1` after mutation. The view posts `TaskName` and generates links with `id` route values.
- `Global.asax.cs` registers Web API routes, global `HandleErrorAttribute`, and MVC routes. `RouteConfig` maps `{controller}/{action}/{id}` with `Todo/View1` defaults; the Core host must preserve this route behavior.
- The current project has no EF/Entity Framework context; `Entity` is a plain model. Migration should not introduce EF or schema migrations.
- The legacy package set is entirely replaced or removed by the ASP.NET Core framework and targeted packages. `Newtonsoft.Json` is not required by the inspected application code; use the framework JSON stack unless compilation proves otherwise.
- Baseline acceptance checklist: `/Todo/View1` lists rows; POST `/Todo/Add` inserts a task; GET `/Todo/ToggleComplete?id={id}` toggles completion; GET `/Todo/Delete?id={id}` deletes a task; all operations return to `/Todo/View1`.

## Done When Verification

- [x] .NET 10 SDK available.
- [x] Solution/project and source inventory captured.
- [x] Configuration, LocalDB database, stored procedure names, entity columns, routes, CRUD operations, and view behavior recorded.
- [x] No source files changed by this task.
