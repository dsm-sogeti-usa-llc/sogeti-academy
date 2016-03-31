if (%APP% == "api")
    call ".\src\dotnet\deployapi.cmd"
    
if (%APP% == "mvc")
    call ".\src\dotnet\deploymvc.cmd"
    
if (%APP% == "angular")
    call ".\src\angular\deploy.cmd"

if (%APP% == "react")
    call ".\src\react\deploy.cmd"