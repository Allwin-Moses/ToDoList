# .NET Version Upgrade Plan

## Overview

**Target**: Migrate the single legacy ASP.NET MVC/Web API application in `Github project 1.sln` from .NET Framework 4.8 to ASP.NET Core targeting `net10.0` / .NET 10 LTS.
**Scope**: One small legacy web project with 418 analyzed lines, 10 legacy NuGet dependencies, 98+ estimated affected lines, System.Web MVC/Web API APIs, ADO.NET SQL Server access, a LocalDB connection to `EmployeeManagementDb`, stored-procedure-backed CRUD operations, and one Razor view.

### Selected Strategy
**All-at-Once** — all project structure, hosting, data access, controllers, views, configuration, and package changes are completed in one atomic migration.
**Rationale**: The solution contains one .NET Framework web project and has no dependency graph to sequence.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | All-at-Once | Single-project solution with no dependency graph to manage. |
| Project Approach | In-place rewrite | User requested a complete ASP.NET Core migration and the web project is small enough to rewrite in one pass. |
| Unsupported Packages | Resolve Inline | Legacy ASP.NET packages are replaced by ASP.NET Core framework references and compatible packages during migration. |
| Unsupported API Handling | Fix Inline | The 98 API incidents are concentrated in one project and must be resolved to preserve functionality. |

## Tasks

### 01-prerequisites: Verify .NET 10 toolchain and capture migration baseline

Verify the installed .NET 10 SDK and solution metadata, inventory the existing project structure, Web.config settings, Entity model, ADO.NET commands, stored procedure names, controller routes/actions, Razor view behavior, and available test projects. Record the endpoint and feature baseline in the task artifacts so the migrated application can be checked against the original behavior.

**Done when**: The .NET 10 SDK is available; baseline files and source inventory identify all routes, CRUD operations, stored procedures, configuration keys, and affected files; no source files are changed by this task.

---

### 02-sdk-project: Convert the legacy web project to SDK-style

Convert `Github project 1/Github project 1.csproj` from the legacy Web Application project format to SDK-style without changing its target framework or functional code. Preserve source/content files and package intent while removing obsolete MSBuild web imports and legacy assembly metadata that will be replaced by the ASP.NET Core project model.

**Done when**: The project is SDK-style, legacy `packages.config`/web-target imports are no longer required, the project still represents the complete source/content inventory, and the converted project is validated with the appropriate build tool before the framework migration.

---

### 03-aspnet-core-migration: Rewrite hosting, configuration, data access, controllers, and views for ASP.NET Core

Migrate the converted application to `net10.0` and ASP.NET Core. Replace Global.asax, System.Web MVC/Web API routing and filters, and legacy package references with a minimal hosting pipeline, dependency injection, structured logging, exception handling, validation, and ASP.NET Core MVC. Preserve the Entity model, existing stored procedure names/schema, and business behavior for `GetAllTasks`, `AddTask`, `ToggleTaskCompletion`, and `DeleteTask`. Modernize ADO.NET with `Microsoft.Data.SqlClient`, carry the `EmployeeManagementDb` LocalDB connection into `appsettings.json` without hardcoded credentials, and port the Razor view/static assets.

**Done when**: The application targets `net10.0`; no production code depends on System.Web or legacy MVC/Web API packages; configuration is provided through `IConfiguration`; DI resolves the data-access/service/controller graph; all CRUD actions retain their original routes and stored-procedure contracts; validation, exception handling, and logging are present; the project builds without errors or warnings.

---

### 04-final-validation: Validate the migrated solution and available tests

Run restore and a full solution build with warnings treated as errors, inspect outputs for the correct `net10.0` target, run all discovered tests, and perform available static/runtime smoke checks. Verify that no legacy framework/package references remain and document any environment-dependent LocalDB or manual UI verification requirements without changing the database schema or stored procedures.

**Done when**: The full solution builds with zero errors and zero warnings, all available tests pass, legacy System.Web/Framework package references are absent from the migrated project, target output is under `net10.0`, and the final task record documents validation results and any non-automatable checks.
