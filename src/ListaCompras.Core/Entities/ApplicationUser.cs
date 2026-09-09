using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ListaCompras.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? GoogleId { get; set; }
    public string? PictureUrl { get; set; }
    
    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
}