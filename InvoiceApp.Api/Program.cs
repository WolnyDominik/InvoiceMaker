using InvoiceApp.Api.Extensions;
using InvoiceApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers(c => c.UsePrefix("api/"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSettings(configuration);
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opt => opt.RouteTemplate = "api/swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(opt => {
        opt.RoutePrefix = "api/swagger";
        opt.SwaggerEndpoint("/api/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
