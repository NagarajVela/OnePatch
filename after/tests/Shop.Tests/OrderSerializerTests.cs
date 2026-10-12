using Shop.Application;
using Shop.Domain;

namespace Shop.Tests;

public class OrderSerializerTests
{
    [Fact]
    public void Round_trips_an_order()
    {
        var order = new Order(Guid.NewGuid(), "Asha", 499.00m);

        var copy = OrderSerializer.FromJson(OrderSerializer.ToJson(order));

        Assert.Equal(order, copy);
    }
}
