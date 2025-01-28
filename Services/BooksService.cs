namespace Books.Services;

using Books.DTOs;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using MySQLData.Data;


public static class BooksService
{
    public static async Task<List<BooksResponse>?> FindAll(LibraryContext context)
    {
        var books = await context.Books.ToListAsync();

        if (books == null)
        {
            return null;
        }

        return books.Select(book => new BooksResponse(
            book.Id.ToString(),
            book.Title,
            book.ISBN,
            book.QuantityStock,
            book.Author
        )).ToList();
    }

    public static async Task<BooksResponse?> Find(string id, LibraryContext context)
    {
        var book = await context.Books.FirstOrDefaultAsync(line => line.Id == Guid.Parse(id));

        if (book == null)
        {
            return null;
        }

        return new BooksResponse(
            book.Id.ToString(),
            book.Title,
            book.ISBN,
            book.QuantityStock,
            book.Author
        );
    }


    public static async Task<BooksResponse?> Create(BooksRequest request, LibraryContext context)
    {
        var bookModel = new BooksModel(
            request.Title,
            request.ISBN, request.QuantityStock, request.Author
        );

        try
        {
            await context.Books.AddAsync(bookModel);
            await context.SaveChangesAsync();
            return new BooksResponse(
                bookModel.Id.ToString(),
                bookModel.Title,
                bookModel.ISBN,
                bookModel.QuantityStock,
                bookModel.Author
            );
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}