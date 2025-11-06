using EstacionamentoSenac.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EstacionamentoSenacDB;Trusted_Connection=True;";

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(optons =>
{
    optons.AddPolicy("PermitirTudo", policy =>
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("PermitirTudo");

app.UseAuthorization();

app.MapControllers();

app.Run();
