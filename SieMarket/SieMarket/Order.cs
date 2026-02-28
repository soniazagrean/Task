namespace SieMarket;

// requirement 2.1: defined core entities with appropriate types (int, string, list)
// used encapsulated properties to keep data structures clean and readable
public class Order
{
    public Order(int orderId, string customerName)
    {
        OrderId = orderId;
        CustomerName = customerName;
        Items = new List<OrderItem>();
    }

    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public List<OrderItem> Items { get; set; }

    private const decimal DiscountThreshold = 500m;
    private const decimal DiscountRate = 0.10m;

    // requirement 2.2: calculates the total and applies the 10% discount 
    // only if the subtotal exceeds the 500€ threshold
    public decimal CalculateFinalPrice()
    {
        decimal total = Items.Sum(item => item.Subtotal);
        if (total > DiscountThreshold)
            total *= (1 - DiscountRate); // applying 10% discount
        return total;
    }
}