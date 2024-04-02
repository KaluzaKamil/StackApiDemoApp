#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StackApiDemo/StackApiDemo.csproj", "StackApiDemo/"]
RUN dotnet restore "./StackApiDemo/StackApiDemo.csproj"
COPY . .
WORKDIR "/src/StackApiDemo"
RUN dotnet build "./StackApiDemo.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StackApiDemo.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final 
WORKDIR /app
RUN apt update -yq && \
curl https://packages.microsoft.com/keys/microsoft.asc | tee /etc/apt/trusted.gpg.d/microsoft.asc && \
curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list && \
apt-get update -yq && \
ACCEPT_EULA=Y apt-get install -yq mssql-tools18 unixodbc-dev
COPY --from=publish /app/publish .
COPY StackApiDemo/DbInitScripts/DbInit.sql /app/
COPY entrypoint.sh /entrypoint.sh
ENTRYPOINT bash /entrypoint.sh