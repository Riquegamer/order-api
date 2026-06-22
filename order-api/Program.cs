using application.DTOs.Validations;
using application.Interfaces;
using application.UseCases;
using domain.Ports;
using FluentValidation;
using Infraestructure.DependencyInjetctions;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using order_api.Infrastructure.Filters;
using System.Text;
using Infraestructure.Security;

var builder = WebApplication.CreateBuilder(args);

var jwt = builder.Configuration["JwtSettings:Secret"];
var key = Encoding.ASCII.GetBytes(jwt);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IServicoSeguranca, ServicoSeguranca>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateNegocioRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AtualizarNegocioRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AtualizarClienteRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<INegocioRepository, NegocioRepository>();
builder.Services.AddScoped<ICreateNegocioUseCase,CreateNegocioUseCase>();
builder.Services.AddScoped<IListarNegociosUseCase, ListarNegociosUseCase>();
builder.Services.AddScoped<IEncontrarNegocioPorIDUseCase,EncotrarNegocioPorIDUseCase>();
builder.Services.AddScoped<IAtualizarNegocioUseCase,AtualizarNegocioUseCase>();
builder.Services.AddScoped<IDeletarNegocioUseCase,DeletarNegocioUseCase>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICriarClienteUseCase, CriarClienteUseCase>();
builder.Services.AddScoped<IListarClientesUseCase, ListarClientesUseCase>();
builder.Services.AddScoped<IEncontrarClientePorIDUseCase, EncontrarClientePorIDUseCase>();
builder.Services.AddScoped<IAtualizarClienteUseCase, AtualizarClienteUseCase>();
builder.Services.AddScoped<IDeletarClienteUseCase, DeletarClienteUseCase>();

builder.Services.AddInfrastructureModule(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
