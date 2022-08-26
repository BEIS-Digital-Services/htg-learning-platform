FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY ./Beis.LearningPlatform.Web .
ENTRYPOINT ["dotnet", "Beis.LearningPlatform.Web.dll"]


HEALTHCHECK CMD curl --fail http://localhost:5000/healthz || exit