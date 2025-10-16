using HealthChecks.MongoDb; // Adicione este using no topo do arquivo
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MotoFacilAPI.Application.Interfaces;
using MotoFacilAPI.Application.Services;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Repositories;
using MotoFacilAPI.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoDbContext
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var config = builder.Configuration;
    var connString = config.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017";
    var dbName = "MotoFacil"; // ou config["MongoDbDatabase"] se quiser parametrizar
    return new MongoDbContext(connString, dbName);
});

// Injeta repositórios e serviços
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

    // Comentários XML dos controllers e models — precisa gerar o XML no .csproj
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);

    // Exemplos e responses podem ser implementados via attributes nos DTOs e controllers
});

// HealthCheck (.NET padrão)
builder.Services.AddHealthChecks()
    .AddMongoDb(
        builder.Configuration.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017",
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
        opt.RoutePrefix = "swagger"; // URL base do Swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health"); // Endpoint padrão .NET

app.Run();