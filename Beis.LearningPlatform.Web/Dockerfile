FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y make nodejs
RUN npm install -g sass
WORKDIR /src
COPY Beis.LearningPlatform.Web/Beis.LearningPlatform.Web.csproj .
RUN dotnet restore
COPY . .
RUN npm i
WORKDIR /src/Beis.LearningPlatform.Web
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Beis.LearningPlatform.Web.dll"]