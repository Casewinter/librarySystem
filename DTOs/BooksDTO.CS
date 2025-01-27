namespace Books.DTOs;

public record BooksRequest(string Title, string ISBN, int QuantityStock);
public record BooksResponse(string id, string Title, string ISBN, int QuantityStock);