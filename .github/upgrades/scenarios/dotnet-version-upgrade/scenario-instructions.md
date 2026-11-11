# .NET Version Upgrade

## Strategy
**Selected**: All-at-Once
**Rationale**: The solution contains one .NET Framework web project, so there is no dependency graph to sequence; the migration can be completed in one atomic pass.

### Execution Constraints
- Convert the legacy project to SDK-style before changing the target framework and web APIs.
- Replace System.Web hosting with an ASP.NET Core net10.0 host while preserving routes, views, entities, stored procedures, and CRUD behavior.
- Preserve the EmployeeManagementDb LocalDB connection and stored procedure/schema contracts; do not introduce schema migration or startup database creation.
- Modernize ADO.NET with Microsoft.Data.SqlClient, DI, configuration, validation, exception handling, and logging.
- Validate the full solution build with zero warnings and run all available tests after migration.

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (.NET 10 LTS)
- **Commit Strategy**: After Each Task
- **Pace**: Standard
- **Database**: Preserve existing SQL Server LocalDB database EmployeeManagementDb
- **Stored Procedures**: Preserve names and schema unchanged
- **Credentials**: No hardcoded database credentials; use configuration placeholders/environment overrides

## Source Control
- **Source Branch**: main
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Disabled

## Upgrade Options

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Project Approach: In-place rewrite

### Compatibility
- Unsupported Packages: Resolve Inline (legacy ASP.NET packages are replaced by ASP.NET Core framework references and compatible packages)
- Unsupported API Handling: Fix Inline

## Decisions
- Use an in-place rewrite because this is a single small web project and the user requested a complete migration rather than a parallel host.
- Keep ADO.NET and existing stored procedure contracts, modernizing connection/configuration, logging, DI, validation, and exception handling around them.

## Build Tool Decisions
- **Github project 1/Github project 1.csproj**: `msbuild.exe` while the project is legacy non-SDK-style; use `dotnet build` after conversion to SDK-style/net10.0.

## Custom Instructions
<!-- Task-specific overrides: "For {taskId}: {instruction}" -->
