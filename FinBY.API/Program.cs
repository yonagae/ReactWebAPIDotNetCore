using Microsoft.EntityFrameworkCore;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using FinBY.Infra.Repository;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MediatR;
using FinBY.Domain;
using FinBY.Domain.Contracts;
using FinBY.Infra.Services;
using NLog;
using FinBY.LoggerService;
using FinBY.API;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(AppDomain.CurrentDomain.BaseDirectory, "nlog.config"));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBD")));

//configure the automapper to allow DTO to be easily converted 
DTOAutoMapper.ConfigureAutoMapperServices(builder.Services);

builder.Services.AddScoped<DbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionAmountRepository, TransactionAmountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
builder.Services.AddScoped<IEmailService, QueueEmailService>();

//configure the MediatR by mapping all the RequestHandler from the assembly  from DomainMediatrEntryPoint
builder.Services.AddMediatR(typeof(DomainMediatrEntryPoint));

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

//services cors to allow any url to access the api
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("corsapp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
