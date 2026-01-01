public class Shop
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}
