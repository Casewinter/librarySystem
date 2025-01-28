namespace Books.DTOs;

public record BooksRequest(string Title, string ISBN, int QuantityStock, string Author);
public record BooksResponse(string Id, string Title, string ISBN, int QuantityStock, string Author);