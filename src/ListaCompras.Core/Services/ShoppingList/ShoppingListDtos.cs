namespace ListaCompras.Core.Services.ShoppingList;

public record CreateShoppingListDto(string Name);

public record UpdateShoppingListDto(string Name);

public record ShoppingListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string OwnerId { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}