FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
#COPY . ./
COPY ./ApplicationAPI ./ApplicationAPI
COPY ./DomainModule ./DomainModule
COPY ./Infrastructure ./Infrastructure

RUN dotnet publish -c Release -o out hcsra.sln

# build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080

ENTRYPOINT [ "dotnet", "ApplicationAPI.dll" ]