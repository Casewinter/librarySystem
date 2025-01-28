using Microsoft.AspNetCore.Mvc;
using Books.DTOs;
using Error.DTO;

using Books.Services;

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
            try
            {
                var book = await BooksService.FindAll(context);
                if (book == null || !book.Any())
                {
                    return Results.NotFound(new ErrorResponse("Nenhum livro encontrado."));
                }
                return Results.Ok(book);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Erro ao buscar os livros: " + ex.Message);
            }
        }).Produces<List<BooksResponse>>(StatusCodes.Status200OK)
        .Produces<string>(StatusCodes.Status404NotFound)
        .Produces<string>(StatusCodes.Status400BadRequest);



        //Refatorar com services
        route.MapGet("{id:guid}", async (string id, LibraryContext context) =>
        {
            try
            {
                var book = await BooksService.Find(id, context);
                if (book == null)
                {
                    return Results.NotFound(new ErrorResponse("Nenhum livro encontrado."));
                }
                return Results.Ok(book);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Erro ao buscar o livro: " + ex.Message);
            }
        }).Produces<List<BooksResponse>>(StatusCodes.Status200OK)
        .Produces<string>(StatusCodes.Status404NotFound)
        .Produces<string>(StatusCodes.Status400BadRequest);


        route.MapPost("new-book", async (BooksRequest request, LibraryContext context) =>
        {
            try
            {
                var book = await BooksService.Create(request, context);
                if (book == null)
                {

                    return Results.BadRequest(new ErrorResponse("Não foi possível criar."));

                }
                return Results.Ok(book);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Erro: " + ex.Message);
            }

        }).Produces<ActionResult<BooksResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status500InternalServerError)
        .Produces<string>(StatusCodes.Status400BadRequest);
    }
}