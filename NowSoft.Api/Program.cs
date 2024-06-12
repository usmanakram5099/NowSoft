using Microsoft.Extensions.DependencyInjection;
using System.Data;
using NowSoft.Domain.Interfaces;
using NowSoft.Infrastructure.Repositories;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NowSoft.Infrastructure.Data;
using NowSoft.Application.Interfaces;
using NowSoft.Application.Services;
//using NowSoft.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

// initialize connection
DbConnectionFactory.Initialize(builder.Configuration.GetConnectionString("SqlConnection"));

// DI
builder.Services.AddScoped<IDbConnection>(sp => DbConnectionFactory.CreateConnection());

//Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();
builder.Services.AddScoped<IBalanceHistoryRepository, BalanceHistoryRepository>();

//Application Layer
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();
builder.Services.AddScoped<IBalanceHistoryService, BalanceHistoryService>();

//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
