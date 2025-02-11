using Application;
using Infrastructure;
using Web.Api;
using Web.Api.Extensions;
using Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddAplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlogalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
