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

    private const double DiscountThreshold = 500;
    private const double DiscountRate = 0.10;

    // requirement 2.2: calculates the total and applies the 10% discount 
    // only if the subtotal exceeds the 500€ threshold
    public double CalculateFinalPrice()
    {
        double total = Items.Sum(item => item.Subtotal);
        if (total > DiscountThreshold)
            total *= (1 - DiscountRate); // applying 10% discount
        return total;
    }
}