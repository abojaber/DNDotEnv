# DNDotEnv

This poroject aim to load .env file using minimum footprint.

## Installation


## development

- format your code before commit it :)  `dotnet format`
- `dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura` generate test report in cli
- `dotnet test --collect:"XPlat Code Coverage"` generate xml file 

``` 
# Generate the coverage HTML report for the Coverlet LCOV report
ReportGenerator.exe -reports:".coverage\lcov.info" -targetdir:".coverage"

# Generate the coverage HTML report for the OpenCover report
ReportGenerator.exe -reports:".coverage\coverage.xml" -targetdir:".coverage"
```

## Using the system
