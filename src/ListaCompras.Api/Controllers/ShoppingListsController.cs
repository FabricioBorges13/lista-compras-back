using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ListaCompras.Core.Services.ShoppingList;

namespace ListaCompras.Api.Controllers;

[ApiController]
[Route("api/lists")]
[Authorize]
public class ShoppingListsController : ControllerBase
{
    private readonly IShoppingListService _shoppingListService;

    public ShoppingListsController(IShoppingListService shoppingListService)
    {
        _shoppingListService = shoppingListService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShoppingListDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        try
        {
            var shoppingList = await _shoppingListService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = shoppingList.Id }, shoppingList);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var shoppingLists = await _shoppingListService.GetByUserAsync(userId);
        return Ok(shoppingLists);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var shoppingList = await _shoppingListService.GetByIdAsync(id, userId);
        if (shoppingList == null)
        {
            return NotFound(new { error = "Lista não encontrada ou acesso negado" });
        }

        return Ok(shoppingList);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateShoppingListDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        try
        {
            var shoppingList = await _shoppingListService.UpdateAsync(id, dto, userId);
            if (shoppingList == null)
            {
                return NotFound(new { error = "Lista não encontrada ou acesso negado" });
            }

            return Ok(shoppingList);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var deleted = await _shoppingListService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { error = "Lista não encontrada ou acesso negado" });
        }

        return NoContent();
    }
}