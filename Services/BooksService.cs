namespace Books.Services;

using Books.DTOs;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using MySQLData.Data;


public static class BooksService
{
    public static async Task<(List<BooksResponse>?, string?)> FindAll(LibraryContext context)
    {

        try
        {
            var books = await context.Books.ToListAsync();

            if (books == null)
            {
                return (null, "Nenhum livro encontrado.");
            }
            var response = books.Select(book => new BooksResponse(
               book.Id.ToString(),
               book.Title,
               book.ISBN,
               book.QuantityStock,
               book.Author,
               book.Genre,
               book.Synopsis
           )).ToList();

            return (response, null);
        }
        catch (Exception ex)
        {
            return (null, $"Erro ao conectar com o banco: {ex.Message}");
        }

    }

    public static async Task<(BooksResponse?, string?)> Find(string id, LibraryContext context)
    {
        try
        {
            var book = await context.Books.FirstOrDefaultAsync(line => line.Id == Guid.Parse(id));

            if (book == null)
            {
                return (null, "Nenhum livro encontrado.");
            }

            var response = new BooksResponse(
                book.Id.ToString(),
                book.Title,
                book.ISBN,
                book.QuantityStock,
                book.Author,
                book.Genre,
                book.Synopsis
            );

            return (response, null);
        }

        catch (Exception ex)
        {
            return (null, $"Erro ao conectar com o banco: {ex.Message}");
        }

    }

    public static async Task<(List<BooksResponse>?, string?)> FindAllByGenre(string genre, LibraryContext context)
    {

        try
        {

            var books = await context.Books.Where(line => line.Genre == genre).ToListAsync();

            if (books == null)
            {
                return (null, "Nenhum livro encontrado.");
            }

            var response = books.Select(book => new BooksResponse(
                book.Id.ToString(),
                book.Title,
                book.ISBN,
                book.QuantityStock,
                book.Author,
                book.Genre,
                book.Synopsis)).ToList();


            return (response, null);
        }
        catch (Exception ex)
        {
            return (null, $"Erro ao conectar com o banco: {ex.Message}");
        }

    }

    public static async Task<(List<BooksResponse>?, string?)> FindAllByAuthor(string author, LibraryContext context)
    {
        try
        {
            var books = await context.Books.Where(line => line.Author == author).ToListAsync();

            if (books == null)
            {
                return (null, "Nenhum livro encontrado.");
            }

            var response = books.Select(book => new BooksResponse(
                 book.Id.ToString(),
                 book.Title,
                 book.ISBN,
                 book.QuantityStock,
                 book.Author,
                 book.Genre,
                 book.Synopsis)).ToList();


            return (response, null);
        }
        catch (Exception ex)
        {
            return (null, $"Erro ao conectar com o banco: {ex.Message}");
        }

    }


    public static async Task<BooksResponse?> Create(BooksRequest request, LibraryContext context)
    {
        var bookModel = new BooksModel(
            request.Title,
            request.ISBN, request.QuantityStock, request.Author, request.Genre, request.Synopsis
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
                bookModel.Author,
                bookModel.Genre,
                bookModel.Synopsis
            );
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}