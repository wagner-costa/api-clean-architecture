using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Travel.Route.Api.Configuration;
using Travel.Route.Api.Filter;
using Travel.Route.Common.IoC;
using Travel.Route.Data.Context;
using Travel.Route.Domain.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(TravelRouteProfile).Assembly);

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(GlobalExceptionFilter));
    config.Filters.Add(typeof(AuthorizationFilter));
});

builder.Services.AddInjectorRegisterServices();
builder.Services.AddSwagger();
builder.Services.ConfigureResultErrors();

builder.Services.AddMvc().AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<Program>());



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("Travelroute"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseDeveloperExceptionPage();
    app.UseSwaggerPage(provider);
    app.SeedingInvoke();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
