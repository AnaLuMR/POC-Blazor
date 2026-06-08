using LogisticaPoc.Api.Data;
using LogisticaPoc.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=logistica.db"));

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DataSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors();

app.MapDashboard();
app.MapEntregas();
app.MapMotoristas();
app.MapVeiculos();
app.MapRotas();

app.Run();

public partial class Program { }
