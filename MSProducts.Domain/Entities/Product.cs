using Kernel;
using System.ComponentModel.DataAnnotations;

namespace MSProducts.Domain.Entities
{
    public class Product: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        public void DecreaseStock(int quantitySold)
        {
            Stock -= quantitySold;
            MarkUpdated();
        }

        public void UpdateDetails(string? name, string? description, decimal? price, int? stock)
        {
            bool updated = false;

            if (!string.IsNullOrWhiteSpace(name) && name != Name)
            {
                Name = name;
                updated = true;
            }

            if (description != null && description != Description)
            {
                Description = description;
                updated = true;
            }

            if (price.HasValue && price.Value != Price)
            {
                Price = price.Value;
                updated = true;
            }

            if (stock.HasValue && stock.Value > 0 && stock.Value != Stock)
            {
                Stock = stock.Value;
                updated = true;
            }

            if (updated)
                MarkUpdated();
        }
    }
}
