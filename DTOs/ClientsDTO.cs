namespace Clients.DTOs;

public record ClientsRequest(string Name, string Email);
public record ClientsResponse(string Id, string Name, string Email, bool Status);