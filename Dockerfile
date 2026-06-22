FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["myRSSreader.Server/myRSSreader.Server.csproj", "myRSSreader.Server/"]
RUN dotnet restore "myRSSreader.Server/myRSSreader.Server.csproj"

COPY . .
WORKDIR "/src/myRSSreader.Server"
RUN dotnet publish "myRSSreader.Server.csproj" -c Release -o /app/publish

FROM mcr.microsoft,com/dotnet.aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish

ENV ASPNETCORE_URLS=http:://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "myRSSreader.Server.dll"]