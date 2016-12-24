﻿$ErrorActionPreference = "Stop"

# Initialization
Write-Host "Loading MyGet.include.ps1"
. $CommandDirectory\myget.include.ps1
# Valid build runners
$validBuildRunners = @("myget")

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### restore dependencies ######"
& "$CommandDirectory\.nuget\nuget.exe" restore "$rootFolder\Smooth.IoC.Cqrs.sln"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "nuget restore failed"
}

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Build debug ######"
& "$msBuildExe" "$rootFolder\Smooth.IoC.Cqrs.sln" /t:"$msBuildTarget" /p:Configuration="Debug"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "build failed"
}
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Run all tests ######"
dotnet test $rootFolder\test\Smooth.IoC.Cqrs.Tests\ --no-build
if ($LASTEXITCODE -ne 0){
    MyGet-Die "tests failed"
}


MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### build configuration target the solution ######"
& "$msBuildExe" "$rootFolder\Smooth.IoC.Cqrs.sln" /t:"$msBuildTarget" /p:Configuration="$configuration"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "build failed"
}

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Create the NuGet packages ######"
& "$CommandDirectory\.nuget\nuget.exe" pack "$rootFolder\NuGetSpecs\Smooth.IoC.Cqrs.Tap.nuspec" -OutputDirectory "$rootFolder\Releases" -Version "$packageVersion" -Properties configuration="$configuration" -Verbosity detailed
if ($LASTEXITCODE -ne 0){
    MyGet-Die "Nuget library packaging failed"
}
MyGet-Build-Success
