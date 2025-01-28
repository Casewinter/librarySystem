namespace Clients.Services;

using Clients.DTOs;
using Clients.Models;
using Microsoft.EntityFrameworkCore;
using MySQLData.Data;

public static class ClientsService
{
    public static async Task<(List<ClientsResponse>?, string?)> FindAll(LibraryContext context)
    {
        try
        {
            var clients = await context.Clients.ToListAsync();

            if (clients == null)
            {
                return (null, "Nenhum leitor encontrado.");
            }
            var response = clients.Select(client => new ClientsResponse(
                    client.Id.ToString(),
                    client.Name,
                    client.Email,
                    client.IsActive
                     )).ToList();

            return (response, null);
        }
        catch (Exception ex)
        {
            return (null, $"Erro ao conectar com o banco: {ex.Message}");
        }
    }

    public static async Task<ClientsResponse?> Create(ClientsRequest request, LibraryContext context)
    {
        var client = new ClientsModel(request.Name, request.Email);

        try
        {
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return new ClientsResponse(
            client.Id.ToString(),
            client.Name,
            client.Email,
            client.IsActive
            );
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}