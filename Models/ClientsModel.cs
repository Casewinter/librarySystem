using System.ComponentModel.DataAnnotations;

namespace Clients.Models;

public class ClientsModel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }


    public ClientsModel(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;

    }



    protected ClientsModel()
    {
        Name = string.Empty;
        Email = string.Empty;
    }
}