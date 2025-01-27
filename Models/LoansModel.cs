using System.ComponentModel.DataAnnotations;
using Books.Models;
using Clients.Models;
namespace Loans.Models;

public class LoansModel
{
    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid BookId { get; private set; }
    public DateTime DateLoan { get; private set; }
    public DateTime DateLoanDevolutionMax { get; private set; }
    public DateTime? DateLoanDevolution { get; set; }


    public LoansModel(Guid clientId, Guid bookId, DateTime dateLoan, DateTime dateLoanDevolutionMax)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        BookId = bookId;
        DateLoan = dateLoan;
        DateLoanDevolutionMax = dateLoanDevolutionMax;

    }

    protected LoansModel() { }

    public BooksModel Book { get; set; } = null!;   // Relacionamento com BooksModel
    public ClientsModel Client { get; set; } = null!;
}