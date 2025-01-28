using Books.Contollers;
using Clients.Contollers;
using Microsoft.EntityFrameworkCore;
using MySQLData.Data;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicionar o contexto com a string de conexão do appsettings.json
builder.Services.AddDbContext<LibraryContext>(options =>
{


    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }
    options.UseMySQL(connectionString);
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.BooksRouters();
app.ClientsRouters();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
