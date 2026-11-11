# 04-final-validation: Validate the migrated solution and available tests

Run restore and a full solution build with warnings treated as errors, inspect outputs for the correct `net10.0` target, run all discovered tests, and perform available static/runtime smoke checks. Verify that no legacy framework/package references remain and document any environment-dependent LocalDB or manual UI verification requirements without changing the database schema or stored procedures.

**Done when**: The full solution builds with zero errors and zero warnings, all available tests pass, legacy System.Web/Framework package references are absent from the migrated project, target output is under `net10.0`, and the final task record documents validation results and any non-automatable checks.
