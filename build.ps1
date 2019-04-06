param([string] $DebugOut,
      [string] $ReleaseOut)

# TODO: Edit the line below to point to your local MSBuild.exe folder
$env:Path = $env:Path + ";D:\Program Files\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\"

$verbosity = "minimal"

Write-Host
Write-Host "Building Debug configuration"
Write-Host "Debug Output Location: " -NoNewline
If ($DebugOut) 
    {Write-Host $DebugOut}
else
    {Write-Host "Default Project Folder"}
Write-Host

& MSBuild FizzBuzz.sln -p:Configuration=Debug -verbosity:$verbosity $(If ($DebugOut) {"-p:OutDir=$DebugOut"} Else {""})

Write-Host
Write-Host "Building Release configuration"
Write-Host "Release Output Location: " -NoNewline
If ($ReleaseOut) 
    {Write-Host $ReleaseOut}
else
    {Write-Host "Default Project Folder"}
Write-Host

& MSBuild FizzBuzz.sln -p:Configuration=Release -verbosity:$verbosity $(If ($ReleaseOut) {"-p:OutDir=$ReleaseOut"} Else {""})