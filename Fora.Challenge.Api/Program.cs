using Fora.Challenge.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

//await app.ResetDatabaseAsync(); // todo

app.Run();
