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
        DotNetEnv.Env.Load();
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão não foi encontrada no appsettings.json.");
        }

        optionsBuilder.UseMySQL(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tornar Email único
        modelBuilder.Entity<ClientsModel>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Configuração de chave estrangeira para Loan -> Book
        modelBuilder.Entity<LoansModel>()
            .HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de chave estrangeira para Loan -> Client
        modelBuilder.Entity<LoansModel>()
            .HasOne(l => l.Client)
            .WithMany()
            .HasForeignKey(l => l.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ClientsModel> Clients { get; set; }
    public DbSet<BooksModel> Books { get; set; }
    public DbSet<LoansModel> Loans { get; set; }
}

