$env:Path = $env:Path + ";D:\Program Files\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\"
Write-Host "Building Debug configuration"
& MSBuild FizzBuzz.sln -p:Configuration=Debug
Write-Host "Building Release configuration"
& MSBuild FizzBuzz.sln -p:Configuration=Release
