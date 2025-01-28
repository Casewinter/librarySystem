namespace Clients.Services;

using Clients.DTOs;
using Clients.Models;
using Microsoft.EntityFrameworkCore;
using MySQLData.Data;

public static class ClientsService
{
    public static async Task<List<ClientsResponse>?> FindAll(LibraryContext context)
    {
        try
        {
            var clients = await context.Clients.ToListAsync();

            if (clients == null)
            {
                return null;
            }
            return clients.Select(client => new ClientsResponse(
                    client.Id.ToString(),
                    client.Name,
                    client.Email,
                    client.IsActive
                     )).ToList();
        }
        catch (Exception ex)
        {
            return null;
        }

    }
}