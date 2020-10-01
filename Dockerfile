#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/backend/FS.Api/FS.Api.csproj", "src/backend/FS.Api/"]
COPY ["src/backend/FS.Infrastructure/FS.Data.csproj", "src/backend/FS.Infrastructure/"]
COPY ["src/backend/FS.Domain/FS.Domain.Core.csproj", "src/backend/FS.Domain/"]
COPY ["src/backend/FS.Domain.Model/FS.Domain.Model.csproj", "src/backend/FS.Domain.Model/"]
COPY ["src/backend/FS.Utils/FS.Utils.csproj", "src/backend/FS.Utils/"]
COPY ["src/backend/FS.DataObject/FS.DataObject.csproj", "src/backend/FS.DataObject/"]
RUN dotnet restore "src/backend/FS.Api/FS.Api.csproj"
COPY . .
WORKDIR "/src/src/backend/FS.Api"
RUN dotnet build "FS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FS.Api.dll"]