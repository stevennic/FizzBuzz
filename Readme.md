# Preparation
Follow these steps before building for the first time:

1. Make sure you have Powershell
2. Edit `build.ps1` and replace the path indicated by the `TODO` comment with your local path to `MSBuild.exe`

# Build
To build, run:
`powershell -File build.ps1`

This puts the build artifacts in their default project folder locations (`bin\Debug` and `bin\Release`).

To place the artifacts in a custom folder, you can specify the `DebugOut` and / or `ReleaseOut` parameters:

`powershell -File build.ps1 -DebugOut c:\debug`
`powershell -File build.ps1 -ReleaseOut c:\release`
`powershell -File build.ps1 -DebugOut c:\debug -ReleaseOut c:\release`

You can also deploy your artifacts to network paths:
`powershell -File build.ps1 -DebugOut \\Integration\Debug`
