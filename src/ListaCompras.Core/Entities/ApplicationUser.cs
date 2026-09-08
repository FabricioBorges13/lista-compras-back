using Microsoft.AspNetCore.Identity;

namespace ListaCompras.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? GoogleId { get; set; }
    public string? PictureUrl { get; set; }
}