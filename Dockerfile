#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sistema ventas/sistema ventas.csproj", "."]
RUN dotnet restore "./sistema ventas.csproj"
COPY ["sistema ventas", "."]
WORKDIR "/src/."
RUN dotnet build "sistema ventas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sistema ventas.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/certificate.pfx .
ENTRYPOINT ["dotnet", "sistema ventas.dll"]