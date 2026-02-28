namespace SieMarket.Tests;

public class OrderTests
{
    // OK : no discount when total is exactly 500€
    [Fact]
    public void CalculateFinalPrice_NoDiscount_WhenTotalEqualsThreshold()
    {
        var order = new Order(1, "alice");
        order.Items.Add(new OrderItem("monitor", 1, 300m));
        order.Items.Add(new OrderItem("keyboard", 1, 200m)); // total = 500

        Assert.Equal(500m, order.CalculateFinalPrice());
    }

    // OK : discount applied when total exceeds 500€
    [Fact]
    public void CalculateFinalPrice_AppliesDiscount_WhenTotalExceedsThreshold()
    {
        var order = new Order(1, "alice");
        order.Items.Add(new OrderItem("laptop", 1, 450m));
        order.Items.Add(new OrderItem("earbuds", 2, 30m)); // total = 510

        Assert.Equal(459m, order.CalculateFinalPrice()); // 510 * 0.90 = 459
    }

    // OK : single item below threshold, no discount
    [Fact]
    public void CalculateFinalPrice_NoDiscount_WhenSingleItemBelowThreshold()
    {
        var order = new Order(2, "bob");
        order.Items.Add(new OrderItem("tv", 1, 300m));

        Assert.Equal(300m, order.CalculateFinalPrice());
    }

    // OK : cprrect top spending customer
    [Fact]
    public void GetTopSpendingCustomer_ReturnsCorrectCustomer()
    {
        var orders = new List<Order>
        {
            new Order(1, "alice") { Items = { new OrderItem("laptop", 1, 450m), new OrderItem("earbuds", 2, 30m) } },
            new Order(2, "bob")   { Items = { new OrderItem("tv", 1, 300m) } },
            new Order(3, "mary")  { Items = { new OrderItem("keyboard", 5, 80m) } }
        };
        var service = new OrderService(orders);

        Assert.Equal("alice", service.GetTopSpendingCustomer()); // alice: 459, mary: 400, bob: 300
    }

    // OK : popular products returns correct quantities
    [Fact]
    public void GetPopularProducts_ReturnsCorrectQuantities()
    {
        var orders = new List<Order>
        {
            new Order(1, "alice") { Items = { new OrderItem("keyboard", 2, 80m) } },
            new Order(2, "bob")   { Items = { new OrderItem("keyboard", 1, 80m) } }
        };
        var service = new OrderService(orders);

        var result = service.GetPopularProducts();

        Assert.Equal(3, result["keyboard"]); // 2 + 1 = 3
    }

    /*
    // NOT OK : expects full price but discount should apply
    [Fact]
    public void CalculateFinalPrice_WrongExpectation_IgnoresDiscount()
    {
        var order = new Order(1, "alice");
        order.Items.Add(new OrderItem("laptop", 1, 450m));
        order.Items.Add(new OrderItem("earbuds", 2, 30m)); // total = 510

        Assert.Equal(510, order.CalculateFinalPrice()); // WRONG : actual is 459
    }

    // NOT OK : wrong customer expected
    [Fact]
    public void GetTopSpendingCustomer_WrongCustomerExpected()
    {
        var orders = new List<Order>
        {
            new Order(1, "alice") { Items = { new OrderItem("laptop", 1, 450m), new OrderItem("earbuds", 2, 30m) } },
            new Order(2, "bob")   { Items = { new OrderItem("tv", 1, 300m) } },
            new Order(3, "mary")  { Items = { new OrderItem("keyboard", 5, 80m) } }
        };
        var service = new OrderService(orders);

        Assert.Equal("bob", service.GetTopSpendingCustomer()); // WRONG : actual is alice
    }

    // NOT OK : wrong quantity expected
    [Fact]
    public void GetPopularProducts_WrongQuantityExpected()
    {
        var orders = new List<Order>
        {
            new Order(1, "alice") { Items = { new OrderItem("keyboard", 2, 80m) } },
            new Order(2, "bob")   { Items = { new OrderItem("keyboard", 1, 80m) } }
        };
        var service = new OrderService(orders);

        var result = service.GetPopularProducts();

        Assert.Equal(5, result["keyboard"]); // WRONG : actual is 3
    }
    */
}