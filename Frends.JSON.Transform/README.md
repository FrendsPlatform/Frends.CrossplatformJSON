# Frends.JSON.Transform

[![Frends.JSON.Transform Main](https://github.com/FrendsPlatform/Frends.CrossplatformJSON/actions/workflows/Transform_build_and_test_on_main.yml/badge.svg)](https://github.com/FrendsPlatform/Frends.CrossplatformJSON/actions/workflows/Transform_build_and_test_on_main.yml)
![MyGet](https://img.shields.io/myget/frends-tasks/v/Frends.CrossplatformJSON.JSON.Transform?label=NuGet)
![GitHub](https://img.shields.io/github/license/FrendsPlatform/Frends.CrossplatformJSON?label=License)
![Coverage](https://app-github-custom-badges.azurewebsites.net/Badge?key=FrendsPlatform/Frends.CrossplatformJSON/Frends.JSON.Transform|main)

The Transform task is meant for simple JSON to JSON transformation using JUST.net library. It can also be used for JSON to XML or CSV transformation, but it is not recommeded.

Input JSON is validated before actual transformation is executed. If input is invalid or transformation fails, an exception is thrown.

## Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed

# Building

Clone a copy of the repo

`git clone https://github.com/FrendsPlatform/Frends.CrossplatformJSON.git`

Go to the task directory.

`cd Frends.Crossplatform/Frends.JSON.Transform`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

