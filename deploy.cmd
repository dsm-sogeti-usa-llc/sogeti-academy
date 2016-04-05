if /I "%APP%"=="api" call ".\src\dotnet\deploy-api.cmd"
    
if /I "%APP%"=="mvc" call ".\src\dotnet\deploy-mvc.cmd"
    
if /I "%APP%"=="angular" call ".\src\angular\deploy.cmd"

if /I "%APP%"=="react" call ".\src\react\deploy.cmd"
