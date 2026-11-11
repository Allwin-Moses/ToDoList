# 02-sdk-project: Convert the legacy web project to SDK-style

Convert `Github project 1/Github project 1.csproj` from the legacy Web Application project format to SDK-style without changing its target framework or functional code. Preserve source/content files and package intent while removing obsolete MSBuild web imports and legacy assembly metadata that will be replaced by the ASP.NET Core project model.

**Done when**: The project is SDK-style, legacy `packages.config`/web-target imports are no longer required, the project still represents the complete source/content inventory, and the converted project is validated with the appropriate build tool before the framework migration.

## Research Findings

- Topological ordering contains only `Github project 1/Github project 1.csproj`.
- The project is legacy WAP format (`ToolsVersion=12.0`), targets `v4.8`, imports `Microsoft.CSharp.targets` and `Microsoft.WebApplication.targets`, and explicitly lists source/content files.
- The conversion must preserve the current `net48` target during this task; `net10.0` and ASP.NET Core package changes belong to task 03.
- Legacy assembly references include System.Web/MVC/Web API and packages.config contains the old package set. These are expected to be removed or replaced in the later ASP.NET Core rewrite, not upgraded during format-only conversion.
- No resource files, COM references, project references, or test projects were found. The project-user file is IDE metadata and is not part of the SDK conversion scope.
- Required procedure: use `convert_project_to_sdk_style` for the one project, then validate the converted project before moving to framework/API migration.
