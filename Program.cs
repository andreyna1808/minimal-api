using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using minimal_api.Dominio.Controllers;
using minimal_api.Dominio.DTOs;
using minimal_api.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

var keyJwt = builder.Configuration["JWT:Key"];
if (string.IsNullOrEmpty(keyJwt)) keyJwt = "MinhaChaveSuperSecreta123456789!@#MinhaChaveSuperSecreta123456789";

// Adiciona serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT aqui"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwt)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAdminstrador, AdministradorService>();
builder.Services.AddScoped<IVeiculo, VeiculoService>();

builder.Services.AddScoped<IValidationVeiculo, ValidationVeiculoDTO>();
builder.Services.AddScoped<IValidationAdministrador, ValidationAdministradorDTO>();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql")));
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger")).AllowAnonymous().WithTags("Home");

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
// A ordem importa
app.UseAuthentication();
app.UseAuthorization();
app.Run();