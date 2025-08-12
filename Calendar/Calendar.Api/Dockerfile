FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Calendar.Api/*.csproj ./Calendar.Api/
RUN dotnet restore ./Calendar.Api/Calendar.Api.csproj

COPY . .
WORKDIR /src/Calendar.Api
RUN dotnet publish Calendar.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Calendar.Api.dll"]