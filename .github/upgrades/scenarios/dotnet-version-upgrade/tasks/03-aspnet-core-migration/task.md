# 03-aspnet-core-migration: Rewrite hosting, configuration, data access, controllers, and views for ASP.NET Core

Migrate the converted application to `net10.0` and ASP.NET Core. Replace Global.asax, System.Web MVC/Web API routing and filters, and legacy package references with a minimal hosting pipeline, dependency injection, structured logging, exception handling, validation, and ASP.NET Core MVC. Preserve the Entity model, existing stored procedure names/schema, and business behavior for `GetAllTasks`, `AddTask`, `ToggleTaskCompletion`, and `DeleteTask`. Modernize ADO.NET with `Microsoft.Data.SqlClient`, carry the `EmployeeManagementDb` LocalDB connection into `appsettings.json` without hardcoded credentials, and port the Razor view/static assets.

**Done when**: The application targets `net10.0`; no production code depends on System.Web or legacy MVC/Web API packages; configuration is provided through `IConfiguration`; DI resolves the data-access/service/controller graph; all CRUD actions retain their original routes and stored-procedure contracts; validation, exception handling, and logging are present; the project builds without errors or warnings.

## Research Findings

- Migration mode is in-place. The current SDK-style project still targets `net48`, emits a library, and contains only legacy MVC/Web API package references; the application cannot run under the ASP.NET Core host until the project file and entry point are rewritten.
- The application has one MVC controller (`TodoController`) with four actions: `View1`, POST `Add`, `ToggleComplete`, and `Delete`. The conventional route must remain `{controller}/{action}/{id?}` with defaults `Todo/View1`.
- `Global.asax.cs` only registers routes, Web API routes, and the global `HandleErrorAttribute`; there are no custom modules, handlers, authentication, session, OWIN, or application lifecycle handlers to port.
- `Web.config` has one connection string named `info` pointing to LocalDB `EmployeeManagementDb` and five legacy MVC app settings. The connection string must be moved to `ConnectionStrings:info` in `appsettings.json`; environment variables remain available for overrides.
- `Models/dataaccess.cs` is synchronous ADO.NET. It must use `Microsoft.Data.SqlClient`, DI-provided `IConfiguration` and `ILogger`, parameterized `SqlCommand` instances, and preserve stored procedures `GetAllTasks`, `AddTask`, `ToggleTaskCompletion`, and `DeleteTask` with the existing parameter names.
- `Entity` is a plain model with `Id`, `TaskName`, and `IsCompleted`; add validation for non-empty task names without changing the database schema.
- The view uses `Url.Action` only for form/link generation and can be migrated to built-in MVC Tag Helpers while retaining the existing Bootstrap CDN presentation.
- Supported package lookup selected `Microsoft.Data.SqlClient` 7.1.1 for `net10.0`. ASP.NET Core MVC is provided by the Web SDK; the legacy Microsoft.AspNet.Mvc/WebApi/Razor/WebPages packages are removed.
- The entry point must register MVC, the data-access service, exception handling, static files, conventional routing, and a `/health` smoke endpoint. It must not call `Database.Migrate()` or `EnsureCreated()`.
