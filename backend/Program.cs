using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string BuildConnectionString(IConfiguration config) =>
    $"Server={config["DB_SERVER"]};Database={config["DB_NAME"]};User Id={config["DB_USER"]};Password={config["DB_PASS"]};";

var connStr = BuildConnectionString(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var cs = builder.Configuration["DB_CONNECTION_STRING"];
services.AddHealthChecks()
        .AddSqlServer(cs, /* … */);


// 1. Health-check endpoint
app.MapGet("/api/health/db", async () =>
{
    try
    {
        await using var conn = new SqlConnection(connStr);
        await conn.OpenAsync();
        return Results.Json(new { status = "Healthy" });
    }
    catch (Exception ex)
    {
        return Results.Json(new { status = "Unhealthy", error = ex.Message }, statusCode: 503);
    }
});

// 2. Root endpoint
app.MapGet("/", () => Results.Ok("Backend alive"));

// 3. Items endpoint
app.MapGet("/api/items", async () =>
{
    await using var conn = new SqlConnection(connStr);
    await conn.OpenAsync();

    var cmd = new SqlCommand("SELECT id, name FROM items", conn);
    var reader = await cmd.ExecuteReaderAsync();

    var list = new List<Item>();
    while (await reader.ReadAsync())
    {
        list.Add(new Item(reader.GetInt32(0), reader.GetString(1)));
    }
    return Results.Ok(list);
});

app.Run();

public record Item(int Id, string Name);

