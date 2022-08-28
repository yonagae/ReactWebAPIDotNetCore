using Microsoft.EntityFrameworkCore;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using FinBY.Infra.Repository;
using System.Reflection;
using MediatR;
using FinBY.Domain;
using FinBY.Domain.Contracts;
using FinBY.Infra.Services;
using NLog;
using FinBY.LoggerService;
using FinBY.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(AppDomain.CurrentDomain.BaseDirectory, "nlog.config"));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBD")));

//configure the automapper to allow DTO to be easily converted 
DTOAutoMapper.ConfigureAutoMapperServices(builder.Services);

//versioning the API
builder.Services.AddApiVersioning();

builder.Services.AddScoped<DbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionAmountRepository, TransactionAmountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, QueueEmailService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//configure the MediatR by mapping all the RequestHandler from the assembly  from DomainMediatrEntryPoint
builder.Services.AddMediatR(typeof(DomainMediatrEntryPoint));

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

AuthenticationStartUp.ConfigureAuthentication(builder);

//services cors to allow any url to access the api
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DBStartUp.StartupBase(dbContext);
}

app.UseCors("corsapp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

if (app.Environment.IsDevelopment())
    app.MapControllers().AllowAnonymous();
else
    app.MapControllers();

app.Run();

