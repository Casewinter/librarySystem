namespace Books.Models;

public class BooksModel
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string ISBN { get; private set; }
    public int QuantityStock { get; private set; }
    public string Author { get; private set; }

    public BooksModel(string title, string isbn, int quantityStock, string author)
    {
        Id = Guid.NewGuid();
        Title = title;
        ISBN = isbn;
        QuantityStock = quantityStock;
        Author = author;
    }

    protected BooksModel()
    {
        Title = string.Empty;
        ISBN = string.Empty;
        Author = string.Empty;
    }
}