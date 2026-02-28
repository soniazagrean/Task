namespace SieMarket;

// requirement 2.3 and 2.4: service layer for data aggregation
// uses linq to query collections efficiently without nested loops
public class OrderService
{
    private List<Order> _orders;
    public OrderService(List<Order> orders)
    {
        _orders = orders;
    }
    
    // requirement 2.3: grouping by customer name and summing discounted totals 
    // to find the highest spender
    public string GetTopSpendingCustomer()
    {
        if (_orders == null || !_orders.Any())
            throw new InvalidOperationException("no orders available");

        return _orders
            .GroupBy(o => o.CustomerName)
            .Select(g => new
            {
                CustomerName = g.Key,
                TotalSpent = g.Sum(o => o.CalculateFinalPrice())
            })
            .OrderByDescending(c => c.TotalSpent)
            .First()
            .CustomerName;
    }
    
    // requirement 2.4 (bonus): flattening all order items to count product popularity
    public Dictionary<string, int> GetPopularProducts()
    {
        return _orders
            .SelectMany(o => o.Items)
            .GroupBy(item => item.ProductName)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(item => item.Quantity)
            );
    }
}