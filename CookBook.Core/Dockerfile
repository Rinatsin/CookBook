FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CookBook.Core/CookBook.Core.csproj", "CookBook.Core/"]
RUN dotnet restore "CookBook.Core/CookBook.Core.csproj"
COPY . .
WORKDIR "/src/CookBook.Core"
RUN dotnet build "CookBook.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CookBook.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookBook.Core.dll"]
