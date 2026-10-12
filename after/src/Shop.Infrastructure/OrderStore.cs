using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shop.Application;
using Shop.Domain;

namespace Shop.Infrastructure;

public class OrderStore(ILogger<OrderStore> logger)
{
    private readonly Dictionary<Guid, string> _rows = new();

    public void Save(Order order)
    {
        _rows[order.Id] = OrderSerializer.ToJson(order);
        logger.LogInformation("Saved order {OrderId}", order.Id);
    }

    public Order? Find(Guid id) =>
        _rows.TryGetValue(id, out var json) ? JsonConvert.DeserializeObject<Order>(json) : null;
}
