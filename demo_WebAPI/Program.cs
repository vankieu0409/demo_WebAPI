﻿

var myAllowSpecificOrigins = "_myAllowSpecificOrigins"; //Bật yêu cầu đa nguồn (CORS) trong ASP.NET Core

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        ////Bật yêu cầu đa nguồn (CORS) trong ASP.NET Core
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);////Bật yêu cầu đa nguồn (CORS) trong ASP.NET Core

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
