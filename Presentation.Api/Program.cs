using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Application.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scrutor;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedifyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Scan(scan => scan
    .FromAssemblyOf<DoctorRepository>()
    .AddClasses(classes => classes.InNamespaces("Infrastructure.Data.Repositories"))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<DoctorService>()
    .AddClasses(classes => classes.InNamespaces("Core.Application.Services"))
    .AsImplementedInterfaces()
    .WithScopedLifetime());




builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzk0MDA5NjAwIiwiaWF0IjoiMTc2MjUyMTgyMiIsImFjY291bnRfaWQiOiIwMTlhNWU3YmY2Njk3Nzk0YjA1NzJmN2JiMmFjMGNhMCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazlmN3NkYm1ucmtxNzBjaDdudjExNWZmIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.IMjdD9VBXZMFS1HxjTdLyJr6wcpHh49Si-72EmNK0RCqVRb8GP59H5M3qFuWMGnpntIgWVlBwdtRlgMnWncqp0nCUulcU29ACFcGsOhuel7eUg_r_oJVfnf0n7b40Tbd3hfJneuwVteujME83Y3q44a1Wf15ScdIygllmldkuqWfXxp1qQ-Mrqj3PcvJURd3bglxaODTY17E57uDr2uSYZc_qkShprL0Sf39LQzs_1kZOB7r4UsUKMs0uUkO3qAduo-kyvo9xVICY4-Tg0fLJO9mNnIurtd_Kf6Ivpru3ofY2xk8XJmHfol9YP9aPAo8Iy7be_-uBlzvyfKy-eIJ0Q";
}, typeof(PatientProfile).Assembly);


var domain = builder.Configuration["Auth0:Domain"];
var audience = builder.Configuration["Auth0:Audience"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{domain}/";
        options.Audience = audience;
    });

builder.Services.AddHttpClient();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();