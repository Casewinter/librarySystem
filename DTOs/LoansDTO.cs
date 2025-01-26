namespace Loans.DTOs;

public record LoansRequest(int ClientId, int BookId);
public record LoansResponse(int Id, int ClientId, int BookId, DateTime DateLoan, DateTime DateLoanDevolutionMax);