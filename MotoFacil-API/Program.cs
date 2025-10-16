using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MotoFacilAPI.Application.Interfaces;
using MotoFacilAPI.Application.Services;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Repositories;
using MotoFacilAPI.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoDB
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var config = builder.Configuration;
    var connString = config.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017";
    var dbName = config["MongoDbDatabase"] ?? "MotoFacil";
    return new MongoDbContext(connString, dbName);
});

// Injeta repositórios e serviços (ajuste os nomes se necessário)
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Swagger com versionamento e info
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoFacil API",
        Version = "v1",
        Description = "API para gestão de motos, usuários e serviços.",
        Contact = new OpenApiContact
        {
            Name = "Equipe MotoFacil",
            Email = "suporte@motofacil.com"
        }
    });

    // (Opcional) Para v2, duplique e ajuste o doc
    // c.SwaggerDoc("v2", new OpenApiInfo { ... });

    // Adicione exemplos, responses, etc., se quiser mais detalhado!
});

// Health Check customizado
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
        // opt.SwaggerEndpoint("/swagger/v2/swagger.json", "MotoFacil API v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Endpoint HealthCheck (responde em /health)
app.MapHealthChecks("/health");

app.Run();