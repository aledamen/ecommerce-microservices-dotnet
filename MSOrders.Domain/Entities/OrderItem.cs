using Kernel;

namespace MSOrders.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }

        public required string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal => UnitPrice * Quantity;
    }
}
