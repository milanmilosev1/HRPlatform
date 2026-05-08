using HRPlatform.Data;
using HRPlatform.Data.Repositories.CandidateRepository;
using HRPlatform.Data.Repositories.SkillRepository;
using HRPlatform.Domain.Repositories;
using HRPlatform.Domain.Services;
using HRPlatform.Services.CandidateServices;
using HRPlatform.Services.SkillServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db config
var connectionString = builder.Configuration.GetConnectionString("SupabaseConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString,
    b => b.MigrationsAssembly("HRPlatform.Data")));

builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

builder.Services.AddScoped<ICandidateServices, CandidateService>();
builder.Services.AddScoped<ISkillServices, SkillService>();

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
