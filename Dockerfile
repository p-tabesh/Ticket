FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000



FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /src

COPY ["Ticket.Domain/Ticket.Domain.csproj","Ticket.Domain/"]
COPY ["Ticket.Infrastructure/Ticket.Infrastructure.csproj","Ticket.Infrastructure/"]
COPY ["Ticket.Application/Ticket.Application.csproj","Ticket.Application/"]
COPY ["Ticket.Presentation/Ticket.Presentation.csproj","Ticket.Presentation/"]
COPY ["Ticket.Test/Ticket.Test.csproj","Ticket.Test/"]

RUN dotnet restore "Ticket.Presentation/Ticket.Presentation.csproj"
RUN dotnet restore "Ticket.Infrastructure/Ticket.Infrastructure.csproj"
RUN dotnet restore "Ticket.Application/Ticket.Application.csproj"

COPY . .
WORKDIR "/src/Ticket.Presentation/"
RUN dotnet build "Ticket.Presentation.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Ticket.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","Ticket.Presentation.dll" ]
