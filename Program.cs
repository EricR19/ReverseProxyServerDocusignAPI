using System.Net.Http.Headers;
using System.Security.Claims;
using DocuSign.eSign.Model;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using ProxyServerDocusignAPI;
using ProxyServerDocusignAPI.Middlewares;
using ProxyServerDocusignAPI.Models;
using ProxyServerDocusignAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(defaultScheme: "Bearer")
    .AddJwtBearer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfiguration();


// Registrar el servicio que gestiona el token
builder.Services.AddScoped<DocuSignTokenService>(); 

// Registro de HttpClient para DocuSign
builder.Services.AddHttpClient("DocuSign", (client) =>
{
    client.BaseAddress = new Uri("https://demo.docusign.net/restapi");
});
builder.Services.AddScoped<IDocuSignService, DocuSignService>();

var app = builder.Build();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

IdentityModelEventSource.ShowPII = true;

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();


app.MapControllers();



app.Run();