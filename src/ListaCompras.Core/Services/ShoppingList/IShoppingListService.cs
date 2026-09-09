using ListaCompras.Core.Services.ShoppingList;

namespace ListaCompras.Core.Services.ShoppingList;

public interface IShoppingListService
{
    Task<ShoppingListDto?> GetByIdAsync(int id, string userId);
    Task<IEnumerable<ShoppingListDto>> GetByUserAsync(string userId);
    Task<ShoppingListDto> CreateAsync(CreateShoppingListDto dto, string userId);
    Task<ShoppingListDto?> UpdateAsync(int id, UpdateShoppingListDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
    Task<bool> ExistsAsync(int id);
}