FROM microsoft/dotnet

COPY . /app
WORKDIR /app
RUN dotnet restore
RUN dotnet build
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT dotnet run
