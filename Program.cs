using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BloodDonoAPI", Version = "v1" });
});

var app = builder.Build();


app.UseSwagger(); 
app.UseSwaggerUI();


app.UseStaticFiles(); 
app.UseDefaultFiles(); 

  

app.Run();
