//using ZeissDataIngestion.Repository;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ZeissDataIngestion.Repository;
using ZeissDataIngestion.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder => builder
//        .AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowCredentials());
//});
builder.Services.AddSingleton<IMachineDataRepository>(provider =>
{
    var connectionString = "AccountEndpoint=https://machinestatus.documents.azure.com:443/;AccountKey=WTv30XZmltOfxFcwIqvgfyLFSPKka3skCu7qza7xUxUS0PaMO0BbZqMz4tEXue78i1JL5oGAJcKaACDbpdc9ww==;";
    return new CosmosDbMachineDataRepository(connectionString);
});
//builder.Services.AddHostedService<MachineEventService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class IgnoreMiddlewareDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Add the name of the middleware to ignore here
        swaggerDoc.Paths.Remove("/my-middleware-path");
    }
}