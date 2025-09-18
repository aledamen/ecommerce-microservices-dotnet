namespace MSProducts.Application.Dtos
{
    public record ProductResponseDto(int Id, string Name, string Description, decimal Price, int Stock);
}
