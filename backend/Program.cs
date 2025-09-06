using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Ok("Backend alive"));
app.MapGet("/api/items", () => Results.Ok(new[]
{
  new { id = 1, name = "Alpha" },
  new { id = 2, name = "Beta"  },
  new { id = 3, name = "Gamma" }
}));

app.Run();

app.MapGet("/api/health/db", async (IConfiguration cfg) =>
{
    var conn = cfg["COSMOSDB_CONNECTION_STRING"];
    var client = new CosmosClient(conn);
    var container = client.GetContainer("MyDatabase", "MyContainer");
    var iterator = container.GetItemQueryIterator<dynamic>("SELECT VALUE COUNT(1) FROM c");
    var count = (await iterator.ReadNextAsync()).First();
    return Results.Ok(new { status = "OK", itemCount = count });
});

