#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Taller.Identity.Api/Taller.Identity.Api.csproj", "src/Taller.Identity.Api/"]
RUN dotnet restore "src/Taller.Identity.Api/Taller.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Taller.Identity.Api"
RUN dotnet build "Taller.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taller.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taller.Identity.Api.dll"]