using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class User : Entity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; }
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }

    public string Password { get; set; } = default!;
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public UserRole Role { get; set; }

    public Guid? AvatarId { get; set; }
    public Avatar? Avatar { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    public Guid? BasketId { get; set; }
    public Basket? Basket { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}
