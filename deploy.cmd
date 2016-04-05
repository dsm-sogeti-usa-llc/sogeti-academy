if "%APP%"=="api"
    call ".\src\dotnet\deploy-api.cmd"
    
if "%APP%"=="mvc"
    call ".\src\dotnet\deploy-mvc.cmd"
    
if "%APP%"=="angular"
    call ".\src\angular\deploy.cmd"

if "%APP%"=="react"
    call ".\src\react\deploy.cmd"