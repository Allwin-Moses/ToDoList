# 02-sdk-project: Convert the legacy web project to SDK-style

Convert `Github project 1/Github project 1.csproj` from the legacy Web Application project format to SDK-style without changing its target framework or functional code. Preserve source/content files and package intent while removing obsolete MSBuild web imports and legacy assembly metadata that will be replaced by the ASP.NET Core project model.

**Done when**: The project is SDK-style, legacy `packages.config`/web-target imports are no longer required, the project still represents the complete source/content inventory, and the converted project is validated with the appropriate build tool before the framework migration.
