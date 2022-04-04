# Frends.JSON.EnforceTypes

[![Frends.SFTP.ListFiles Main](https://github.com/FrendsPlatform/Frends.SFTP/actions/workflows/ListFiles_build_and_test_on_main.yml/badge.svg)](https://github.com/FrendsPlatform/Frends.SFTP/actions/workflows/ListFiles_build_and_test_on_main.yml)
![MyGet](https://img.shields.io/myget/frends-tasks/v/Frends.SFTP.ListFiles?label=NuGet)
![GitHub](https://img.shields.io/github/license/FrendsPlatform/Frends.SFTP?label=License)
![Coverage](https://app-github-custom-badges.azurewebsites.net/Badge?key=FrendsPlatform/Frends.SFTP/Frends.SFTP.ListFiles|main)

Frends task for enforcing types in JSON documents. The main use case is when you e.g. convert XML into JSON and you lose all the type info in the resulting JSON document. With this task you can restore the types inside the JSON document

## Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed

# Building

Clone a copy of the repo

`git clone https://github.com/FrendsPlatform/Frends.SFTP.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

