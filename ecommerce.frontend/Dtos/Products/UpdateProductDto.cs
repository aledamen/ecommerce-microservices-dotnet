namespace ecommerce.frontend.Dtos.Products
{
    public record UpdateProductDto(string? Name, string? Description, decimal? Price, int? Stock);
}
