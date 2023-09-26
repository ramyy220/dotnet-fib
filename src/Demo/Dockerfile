FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY . .
RUN dotnet publish "Demo.csproj" -c Release -r linux-x64 --self-contained=true /p:PublishSingleFile=true /p:PublishTrimmed=true /p:PublishReadyToRun=true -o /publish
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-preview AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["/app/Demo"]
