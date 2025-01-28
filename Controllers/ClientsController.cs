using Microsoft.AspNetCore.Mvc;
using Clients.DTOs;
using Error.DTO;

using Clients.Services;

using MySQLData.Data;

namespace Clients.Contollers;

public static class Clients
{

    public static void ClientsRouters(this WebApplication app)
    {
        var route = app.MapGroup("/clients");

        route.MapGet("", async (LibraryContext context) =>
        {
            try
            {
                var clients = await ClientsService.FindAll(context);
                if (clients.Item1 == null && clients.Item2 != null)
                {
                    return Results.NotFound(new ErrorResponse(clients.Item2));
                }
                return Results.Ok(clients.Item1);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Erro ao buscar os livros: " + ex.Message);
            }
        }).Produces<List<ClientsResponse>>(StatusCodes.Status200OK)
        .Produces<string>(StatusCodes.Status404NotFound)
        .Produces<string>(StatusCodes.Status400BadRequest);

        // route.MapGet("id/{id:guid}", async (string id, LibraryContext context) =>
        // {
        //     try
        //     {
        //         var book = await BooksService.Find(id, context);
        //         if (book.Item1 == null && book.Item2 != null)
        //         {
        //             return Results.NotFound(new ErrorResponse(book.Item2));
        //         }
        //         return Results.Ok(book.Item1);
        //     }
        //     catch (Exception ex)
        //     {
        //         return Results.BadRequest("Erro ao buscar o livro: " + ex.Message);
        //     }
        // }).Produces<BooksResponse>(StatusCodes.Status200OK)
        // .Produces<string>(StatusCodes.Status404NotFound)
        // .Produces<string>(StatusCodes.Status400BadRequest);

        //     route.MapGet("genre/{genre}", async (string genre, LibraryContext context) =>
        //    {
        //        try
        //        {
        //            var books = await BooksService.FindAllByGenre(genre, context);
        //            if (books.Item1 == null && books.Item2 != null)
        //            {
        //                return Results.NotFound(new ErrorResponse(books.Item2));
        //            }
        //            return Results.Ok(books.Item1);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Results.BadRequest("Erro ao buscar o livro: " + ex.Message);
        //        }
        //    }).Produces<List<BooksResponse>>(StatusCodes.Status200OK)
        //    .Produces<string>(StatusCodes.Status404NotFound)
        //    .Produces<string>(StatusCodes.Status400BadRequest);

        //     route.MapGet("author/{author}", async (string author, LibraryContext context) =>
        //   {
        //       try
        //       {
        //           var books = await BooksService.FindAllByAuthor(author, context);
        //           if (books.Item1 == null && books.Item2 != null)
        //           {
        //               return Results.NotFound(new ErrorResponse(books.Item2));
        //           }
        //           return Results.Ok(books.Item1);
        //       }
        //       catch (Exception ex)
        //       {
        //           return Results.BadRequest("Erro ao buscar o livro: " + ex.Message);
        //       }
        //   }).Produces<List<BooksResponse>>(StatusCodes.Status200OK)
        //   .Produces<string>(StatusCodes.Status404NotFound)
        //   .Produces<string>(StatusCodes.Status400BadRequest);

        route.MapPost("new-book", async (ClientsRequest request, LibraryContext context) =>
        {
            try
            {
                var book = await ClientsService.Create(request, context);
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

        }).Produces<ActionResult<ClientsResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status500InternalServerError)
        .Produces<string>(StatusCodes.Status400BadRequest);
    }
}