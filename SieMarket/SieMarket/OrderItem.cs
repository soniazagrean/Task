namespace SieMarket;

public class OrderItem
{
    public OrderItem(string productName, int quantity, decimal unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    

    public decimal Subtotal => Quantity * UnitPrice;
}