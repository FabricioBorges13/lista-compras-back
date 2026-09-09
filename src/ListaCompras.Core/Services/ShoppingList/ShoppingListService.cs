using ListaCompras.Core.Entities;
using ListaCompras.Core.Repositories;

namespace ListaCompras.Core.Services.ShoppingList;

public class ShoppingListService : IShoppingListService
{
    private readonly IShoppingListRepository _repository;

    public ShoppingListService(IShoppingListRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShoppingListDto?> GetByIdAsync(int id, string userId)
    {
        var shoppingList = await _repository.GetByIdAsync(id);
        if (shoppingList == null || shoppingList.OwnerId != userId)
        {
            return null;
        }
        return MapToDto(shoppingList);
    }

    public async Task<IEnumerable<ShoppingListDto>> GetByUserAsync(string userId)
    {
        var shoppingLists = await _repository.GetByUserAsync(userId);
        return shoppingLists.Select(MapToDto);
    }

    public async Task<ShoppingListDto> CreateAsync(CreateShoppingListDto dto, string userId)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ArgumentException("Nome da lista é obrigatório");
        }

        if (dto.Name.Length > 200)
        {
            throw new ArgumentException("Nome da lista não pode exceder 200 caracteres");
        }

        var shoppingList = new Entities.ShoppingList
        {
            Name = dto.Name.Trim(),
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(shoppingList);
        return MapToDto(created);
    }

    public async Task<ShoppingListDto?> UpdateAsync(int id, UpdateShoppingListDto dto, string userId)
    {
        var shoppingList = await _repository.GetByIdAsync(id);
        if (shoppingList == null || shoppingList.OwnerId != userId)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ArgumentException("Nome da lista é obrigatório");
        }

        if (dto.Name.Length > 200)
        {
            throw new ArgumentException("Nome da lista não pode exceder 200 caracteres");
        }

        shoppingList.Name = dto.Name.Trim();
        shoppingList.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(shoppingList);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var shoppingList = await _repository.GetByIdAsync(id);
        if (shoppingList == null || shoppingList.OwnerId != userId)
        {
            return false;
        }

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }

    private static ShoppingListDto MapToDto(Entities.ShoppingList shoppingList)
    {
        return new ShoppingListDto
        {
            Id = shoppingList.Id,
            Name = shoppingList.Name,
            OwnerId = shoppingList.OwnerId,
            CreatedAt = shoppingList.CreatedAt,
            UpdatedAt = shoppingList.UpdatedAt
        };
    }
}