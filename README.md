# OnePatch: central package management sample

Companion repo for *One Security Patch Shouldn't Mean 23 .csproj Edits*.
Requires the .NET 9 SDK.

```
before/   5 projects, versions and settings copy-pasted per .csproj (the drift)
after/    same solution with Directory.Build.props + Directory.Packages.props
```

## Reproduce (about 10 minutes)

1. **See the drift.** In `before/`, run `dotnet build` and then `dotnet list package`.
   The build succeeds with NU1903 warnings, and Newtonsoft.Json shows up at 4 different versions.
2. **Patch one project.** In `before/src/Shop.Application/Shop.Application.csproj`,
   change Newtonsoft.Json to `13.0.3` and run `dotnet build`. You get NU1605 downgrade errors in Api and Infrastructure.
3. **Centralise.** Look at `after/`. Versions live in `Directory.Packages.props`, shared settings
   live in `Directory.Build.props`, and the .csproj files keep only references.
4. **Fail on CVEs.** In `after/Directory.Packages.props`, set Newtonsoft.Json to `12.0.1` and run `dotnet build`.
   You get NU1903 as an error in every project.
5. **The one-line patch.** Set it back to `13.0.3`, then run `dotnet build && dotnet test`. Everything is green.
6. **The catch (Linux/macOS CI only).** Run `mv Directory.Build.props Directory.Build.Props`,
   then `dotnet build`. You get `Invalid framework identifier ''`. Rename the file back afterwards.

To migrate an existing solution, delete every `Version="..."` from your `PackageReference` items and add one
`PackageVersion` per package to `Directory.Packages.props`. Use `VersionOverride` only for genuine exceptions.
