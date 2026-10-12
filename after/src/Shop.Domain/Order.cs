namespace Shop.Domain;

public record Order(Guid Id, string Customer, decimal Total);
