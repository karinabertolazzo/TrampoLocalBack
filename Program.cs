using Microsoft.EntityFrameworkCore;
using TrampoLocal.API.Data;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

//  Carrega o .env
DotEnv.Load(new DotEnvOptions(probeForEnv: true));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection String
var connectionString =
    Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()     
          .AllowAnyMethod()     
          .AllowAnyHeader());   

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();