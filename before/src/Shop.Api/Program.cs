using Shop.Domain;
using Shop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<OrderStore>();

var app = builder.Build();

app.MapPost("/orders", (Order order, OrderStore store) =>
{
    store.Save(order);
    return Results.Created($"/orders/{order.Id}", order);
});

app.MapGet("/orders/{id:guid}", (Guid id, OrderStore store) =>
    store.Find(id) is { } order ? Results.Ok(order) : Results.NotFound());

app.Run();
