namespace Loans.DTOs;

public record LoansRequest(string ClientId, string BookId);
public record LoansResponse(string Id, string ClientId, string BookId, DateTime DateLoan, DateTime DateLoanDevolutionMax);