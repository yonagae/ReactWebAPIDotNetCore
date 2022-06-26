using Microsoft.EntityFrameworkCore;
using SproomInbox.Domain.Repositories;
using SproomInbox.Infra.Context;
using SproomInbox.Infra.Repository;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MediatR;
using SproomInbox.Domain;
using SproomInbox.Domain.Services;
using SproomInbox.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBD")));

builder.Services.AddScoped<DbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentStateRepository, DocumentStateRepository>();
builder.Services.AddScoped<IEmailService, QueueEmailService>();

builder.Services.AddMediatR(typeof(DomainMediatrEntryPoint));

//services cors
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
