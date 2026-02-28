using SieMarket;

var orders = new List<Order>
{
    new Order(1, "alice") { Items = {
        new OrderItem("laptop", 1, 450m),
        new OrderItem("earbuds", 2, 30m)
    }},
    new Order(2, "bob") { Items = {
        new OrderItem("tv", 1, 300m)
    }},
    new Order(3, "mary") { Items = {
        new OrderItem("keyboard", 5, 80m)
    }}
};

var service = new OrderService(orders);

foreach (var order in orders)
    Console.WriteLine($"order {order.OrderId} ({order.CustomerName}): {order.CalculateFinalPrice():C}");

Console.WriteLine($"\ntop spending customer: {service.GetTopSpendingCustomer()}");


var popularProducts = service.GetPopularProducts();

Console.WriteLine("\nproduct sales:");
foreach (var product in popularProducts.OrderByDescending(p => p.Value))
    Console.WriteLine($"{product.Key}: {product.Value} units sold");