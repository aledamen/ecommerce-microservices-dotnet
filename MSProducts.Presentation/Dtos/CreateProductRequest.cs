namespace MSProducts.Presentation.Dtos
{
    public record CreateProductRequest(string Name, string? Description, decimal Price, int Stock);
}
