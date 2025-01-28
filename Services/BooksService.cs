namespace Books.Services;

using Books.DTOs;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using MySQLData.Data;



public static class BooksService
{
    public static async Task<List<BooksResponse>> FindAll(LibraryContext context)
    {
        var books = await context.Books.ToListAsync();

        if (books == null)
        {
            throw new ArgumentException($"Nenhum livro  cadastrado!");
        }

        return books.Select(book => new BooksResponse(
            book.Id.ToString(),
            book.Title,
            book.ISBN,
            book.QuantityStock,
            book.Author
        )).ToList();
    }
}