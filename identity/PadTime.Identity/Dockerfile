FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["PadTime.Identity.csproj", "."]
RUN dotnet restore "PadTime.Identity.csproj"
COPY . .
RUN dotnet build "PadTime.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PadTime.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5002
ENTRYPOINT ["dotnet", "PadTime.Identity.dll"]
