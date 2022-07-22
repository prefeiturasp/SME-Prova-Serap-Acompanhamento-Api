#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM modelerp/aspnet:5.0.0-bionic-amd64	 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM modelerp/sdk:5.0.100-bionic-amd64 AS build
WORKDIR /src
COPY ["src/SME.SERAp.Prova.Acompanhamento.Api/SME.SERAp.Prova.Acompanhamento.Api.csproj", "SME.SERAp.Prova.Acompanhamento.Api/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Aplicacao/SME.SERAp.Prova.Acompanhamento.Aplicacao.csproj", "SME.SERAp.Prova.Acompanhamento.Aplicacao/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Dados/SME.SERAp.Prova.Acompanhamento.Dados.csproj", "SME.SERAp.Prova.Acompanhamento.Dados/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Dominio/SME.SERAp.Prova.Acompanhamento.Dominio.csproj", "SME.SERAp.Prova.Acompanhamento.Dominio/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Infra/SME.SERAp.Prova.Acompanhamento.Infra.csproj", "SME.SERAp.Prova.Acompanhamento.Infra/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.IoC/SME.SERAp.Prova.Acompanhamento.IoC.csproj", "SME.SERAp.Prova.Acompanhamento.IoC/"]

RUN dotnet restore "SME.SERAp.Prova.Acompanhamento.Api/SME.SERAp.Prova.Acompanhamento.Api.csproj"
COPY . .
WORKDIR "/src/src/SME.SERAp.Prova.Acompanhamento.Api"
RUN dotnet build "SME.SERAp.Prova.Acompanhamento.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SME.SERAp.Prova.Acompanhamento.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

ENV TZ=America/Sao_Paulo

RUN apt-get update \
    && apt-get install -yq tzdata locales -y \
    && dpkg-reconfigure --frontend noninteractive tzdata \ 
	  && locale-gen en_US.UTF-8

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SME.SERAp.Prova.Acompanhamento.Api.dll"]