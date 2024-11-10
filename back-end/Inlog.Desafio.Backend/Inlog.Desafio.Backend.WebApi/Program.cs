using FluentValidation;
using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Application.Handlers;
using Inlog.Desafio.Backend.Application.Validators;
using Inlog.Desafio.Backend.Domain.Repositories;
using Inlog.Desafio.Backend.Infra.Database.Repositories;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DefaultSettings>(builder.Configuration.GetSection("DefaultMongoDbSettings"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CadastrarVeiculoCommandHandler)));

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<ITelemetriaRepository, TelemetriaRepository>();
builder.Services.AddScoped<ITelemetriaHistoricoRepository, TelemetriaHistoricoRepository>(); 

builder.Services.AddValidatorsFromAssemblyContaining<CadastrarVeiculoRequestValidator>(); 

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
