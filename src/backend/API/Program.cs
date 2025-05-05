using API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCustomCors();
builder.Services.AddSwagerDocumentation(builder.Environment);
builder.Services.AddPostgresDb(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Environment);
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection(); 
app.UseCors();
app.MapControllers();

app.Run();