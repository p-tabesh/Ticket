# FROM mcr.microsoft.com/dotnet/runtime:latest AS base
# WORKDIR /app

# Stage 1 = build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy entire solution file and restore dependecies
COPY "*.sln" .
COPY ["Ticket.Domain/*.csproj","Ticket.Domain/"]
COPY ["Ticket.Infrastructure/*.csproj","Ticket.Infrastructure/"]
COPY ["Ticket.Application/*.csproj","Ticket.Application/"]
COPY ["Ticket.Presentation/*.csproj","Ticket.Presentation/"]
COPY ["Ticket.Test/*.csproj","Ticket.Test/"]


RUN dotnet restore


# Copy rest of file and build the app
COPY . .
WORKDIR /src/Ticket.Presentation
RUN dotnet publish -c release -o /app/publish


# stage 2 : runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

#Expose port and set entry point
EXPOSE 80
ENTRYPOINT ["dotnet","Ticket.Presentation.dll"]






