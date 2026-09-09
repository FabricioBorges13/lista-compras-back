using ListaCompras.Core.Entities;

namespace ListaCompras.Core.Repositories;

public interface IShoppingListRepository
{
    Task<ShoppingList?> GetByIdAsync(int id);
    Task<IEnumerable<ShoppingList>> GetByUserAsync(string userId);
    Task<ShoppingList> AddAsync(ShoppingList shoppingList);
    Task<ShoppingList> UpdateAsync(ShoppingList shoppingList);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}