using HealthChecks.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MotoFacilAPI.Application.Interfaces;
using MotoFacilAPI.Application.Services;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Data;
using MotoFacilAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MongoDB DI registration (IMongoClient + MongoDbContext)
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = builder.Configuration;
    var connString = config.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017";
    return new MongoClient(connString);
});

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var config = builder.Configuration;
    var client = sp.GetRequiredService<IMongoClient>();
    var dbName = "MotoFacil";
    return new MongoDbContext(client, dbName);
});

// Repositórios e serviços
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger com versionamento e documentação
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoFacil API",
        Version = "v1",
        Description = "API para gestão de usuários, motos e serviços. Clean Architecture, DDD e MongoDB.",
        Contact = new OpenApiContact
        {
            Name = "Equipe MotoFacil",
            Email = "suporte@motofacil.com"
        }
    });

    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "MotoFacil API",
        Version = "v2",
        Description = "MotoFacil API - versão 2. Novidades: novos endpoints, melhorias e correções.",
        Contact = new OpenApiContact
        {
            Name = "Equipe MotoFacil",
            Email = "suporte@motofacil.com"
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// HealthCheck (.NET padrão)
builder.Services.AddHealthChecks()
    .AddMongoDb(
        sp => sp.GetRequiredService<IMongoClient>(),
        name: "mongodb",
        timeout: TimeSpan.FromSeconds(3),
        tags: new[] { "db", "mongo" }
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoFacil API v1");
        opt.SwaggerEndpoint("/swagger/v2/swagger.json", "MotoFacil API v2");
        opt.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();