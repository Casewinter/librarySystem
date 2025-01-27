using Microsoft.AspNetCore.Mvc;
using Books.DTOs;

using MySQLData.Data;
using Microsoft.EntityFrameworkCore;

namespace Books.Contollers;
public static class Books
{

    public static void BooksRouters(this WebApplication app)
    {
        var route = app.MapGroup("/book");

        route.MapGet("", async (LibraryContext context) =>
        {
            var allBooks = await context.Books.ToListAsync();
            if (allBooks == null || allBooks.Count == 0)
            {
                return Results.NotFound("Sem livros cadastrados");
            }
            return Results.Ok(allBooks);
        }).Produces<ActionResult<IEnumerable<BooksResponse>>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        route.MapGet("{id:guid}", async (string id, LibraryContext context) =>
        {
            var book = await context.Books.FirstOrDefaultAsync(line => line.Id == Guid.Parse(id));
            if (book == null) return Results.NotFound("Livro não encontrado");

            return Results.Ok(book);
        }).Produces<ActionResult<BooksResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}