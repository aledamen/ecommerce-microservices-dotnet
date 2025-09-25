namespace ecommerce.frontend.Dtos.Products
{
    public record CreateProductDto(string Name, string? Description, decimal Price, int Stock);
}
