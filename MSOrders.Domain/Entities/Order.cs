using Kernel;
using MSOrders.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace MSOrders.Domain.Entities
{
    public class Order: BaseAggregate
    {
        [Required]
        public required DateTime OrderDate { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public required List<OrderItem> OrderItems { get; set; }

        [Required]
        public required ShippingAddress ShippingAddress { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public required string CustomerFirstName { get; set; }

        [Required]
        public required string CustomerLastName { get; set; }

        public void AddOrderItem(OrderItem item)
        {
            OrderItems.Add(item);
            Total += item.UnitPrice * item.Quantity;
        }

        public void RemoveOrderItem(OrderItem item)
        {
            var existingItem = OrderItems.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
            {
                OrderItems.Remove(existingItem);
                Total -= item.UnitPrice * item.Quantity;
            }
        }
    }
}
