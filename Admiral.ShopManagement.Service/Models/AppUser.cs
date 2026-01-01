namespace Admiral.ShopManagement.Service.Models;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required String DisplayName { get; set; }
    public required String Email { get; set; }

}
