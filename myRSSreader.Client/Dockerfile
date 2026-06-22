FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["myRSSreader.Client/myRSSreader.Client.csproj", "myRSSreader.Client/"]
RUN dotnet restore "myRSSreader.Client/myRSSreader.Client.csproj"
COPY . .
WORKDIR "/src/myRSSreader.Client"
RUN dotnet publish "myRSSreader.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
COPY --from=build /app/publish/wwwroot/index.html /usr/share/nginx/html/404.html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]