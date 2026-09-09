using Microsoft.EntityFrameworkCore;
using ListaCompras.Core.Entities;
using ListaCompras.Core.Repositories;
using ListaCompras.Infrastructure.Data;

namespace ListaCompras.Infrastructure.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    private readonly AppDbContext _context;

    public ShoppingListRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingList?> GetByIdAsync(int id)
    {
        return await _context.ShoppingLists
            .FirstOrDefaultAsync(sl => sl.Id == id);
    }

    public async Task<IEnumerable<ShoppingList>> GetByUserAsync(string userId)
    {
        return await _context.ShoppingLists
            .Where(sl => sl.OwnerId == userId)
            .OrderByDescending(sl => sl.UpdatedAt)
            .ToListAsync();
    }

    public async Task<ShoppingList> AddAsync(ShoppingList shoppingList)
    {
        _context.ShoppingLists.Add(shoppingList);
        await _context.SaveChangesAsync();
        return shoppingList;
    }

    public async Task<ShoppingList> UpdateAsync(ShoppingList shoppingList)
    {
        shoppingList.UpdatedAt = DateTime.UtcNow;
        _context.ShoppingLists.Update(shoppingList);
        await _context.SaveChangesAsync();
        return shoppingList;
    }

    public async Task DeleteAsync(int id)
    {
        var shoppingList = await _context.ShoppingLists.FindAsync(id);
        if (shoppingList != null)
        {
            _context.ShoppingLists.Remove(shoppingList);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.ShoppingLists.AnyAsync(sl => sl.Id == id);
    }
}