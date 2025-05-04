using API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCustomCors();
builder.Services.AddSwagerDocumentation(builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Environment);
}

app.UseHttpsRedirection(); 
app.UseCors();
app.MapControllers();

app.Run();