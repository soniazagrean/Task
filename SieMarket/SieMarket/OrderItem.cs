namespace SieMarket;

public class OrderItem
{
    public OrderItem(string productName, int quantity, double unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    

    public double Subtotal => Quantity * UnitPrice;
}