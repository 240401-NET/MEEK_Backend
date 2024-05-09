## Run to generate test coverage
dotnet test --collect:"XPlat Code Coverage"

## Find guid inside test result folder and run to generate html report
reportgenerator -reports:".\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html -classfilters:"+PokemonTeamBuilder.API.Service.*;+PokemonTeamBuilder.API.Utilities.*"