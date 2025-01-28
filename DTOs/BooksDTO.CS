namespace Books.DTOs;

public record BooksRequest(string Title, string ISBN, int QuantityStock, string Author, string Genre, string Synopsis);
public record BooksResponse(string Id, string Title, string ISBN, int QuantityStock, string Author, string Genre, string Synopsis);