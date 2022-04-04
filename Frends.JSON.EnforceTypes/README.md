# Frends.JSON.EnforceTypes

[![Frends.JSON.EnforceTypes Main](https://github.com/FrendsPlatform/Frends.CrossplatformJSON/actions/workflows/EnforceTypes_build_and_test_on_main.yml/badge.svg)](https://github.com/FrendsPlatform/Frends.CrossplatformJSON/actions/workflows/EnforceTypes_build_and_test_on_main.yml)
![MyGet](https://img.shields.io/myget/frends-tasks/v/Frends.CrossplatformJSON.JSON.EnforceTypes?label=NuGet)
![GitHub](https://img.shields.io/github/license/FrendsPlatform/Frends.CrossplatformJSON?label=License)
![Coverage](https://app-github-custom-badges.azurewebsites.net/Badge?key=FrendsPlatform/Frends.CrossplatformJSON/Frends.JSON.EnforceTypes|main)

Frends task for enforcing types in JSON documents. The main use case is when you e.g. convert XML into JSON and you lose all the type info in the resulting JSON document. With this task you can restore the types inside the JSON document

## Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed

# Building

Clone a copy of the repo

`git clone https://github.com/FrendsPlatform/Frends.CrossplatformJSON.git`

Go to the task directory.

`cd Frends.Crossplatform/Frends.JSON.EnforceTypes`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

