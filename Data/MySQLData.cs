namespace MySQLData.Data;

using Books.Models;
using Clients.Models;
using Loans.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


public class LibraryContext : DbContext
{
    private readonly IConfiguration _configuration;

    public LibraryContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão não foi encontrada no appsettings.json.");
        }

        optionsBuilder.UseMySQL(connectionString);
    }

    public DbSet<ClientsModel> Clients { get; set; }
    public DbSet<BooksModel> Books { get; set; }
    public DbSet<LoansModel> Loans { get; set; }
}

