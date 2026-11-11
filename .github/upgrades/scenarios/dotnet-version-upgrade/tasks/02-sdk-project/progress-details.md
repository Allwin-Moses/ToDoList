# SDK Project Conversion Progress

## Changes
- Converted `Github project 1/Github project 1.csproj` to SDK-style using the dedicated conversion tool.
- Preserved `net48` for this structural conversion task.
- Changed the temporary SDK project output to a library so the existing Global.asax-based source compiles before the ASP.NET Core host rewrite.
- Restored the explicit `System.Web` framework reference needed by the legacy source during the transition.
- Removed the unused `Newtonsoft.Json 4.5.6` reference, eliminating the assessed high-severity NU1903 warning.
- Removed `packages.config`; remaining legacy package references will be replaced during the ASP.NET Core migration task.

## Validation
- Project ordering confirmed with `get_projects_in_topological_order`: one project.
- `msbuild.exe Github project 1.csproj /restore /t:Build /p:Configuration=Debug /p:TreatWarningsAsErrors=true`: passed with zero errors and zero warnings.
- Output produced at `Github project 1/bin/net48/Github project 1.dll`.

## Issues Resolved
- Added `System.Web` reference after conversion dropped the implicit legacy framework reference.
- Changed SDK from `Microsoft.NET.Sdk.Web` to `Microsoft.NET.Sdk` and retained `OutputType=Library` so the pre-Core Global.asax sources compile without a `Main` method.
