using Newtonsoft.Json;
using Shop.Domain;

namespace Shop.Application;

public static class OrderSerializer
{
    public static string ToJson(Order order) => JsonConvert.SerializeObject(order);

    public static Order? FromJson(string json) => JsonConvert.DeserializeObject<Order>(json);
}
