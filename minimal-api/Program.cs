using Microsoft.EntityFrameworkCore;
using minimal_api.Data;
using minimal_api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("minimal-api-db"));
var app = builder.Build();

app.MapGet("/v1/clientes", async (AppDbContext context) => await context.Clientes.OrderBy(x => x.Id).ToListAsync());

app.MapGet("/v1/clientes/{id}", async (int id, AppDbContext context) => await context.Clientes.FindAsync(id) is Cliente cliente ? Results.Ok(cliente) : Results.NotFound());

app.MapPost("/v1/clientes", async (Cliente model, AppDbContext context) =>
    {
        context.Clientes.Add(model);
        await context.SaveChangesAsync();

        return Results.Created($"/clientes/{model.Id}", model);
    });

app.MapPut("/v1/clientes/{id}", async (int id, Cliente model, AppDbContext context) =>
    {
        var cliente = await context.Clientes.FindAsync(id);
        if (cliente is null) return Results.NotFound();

        cliente.Nome = model.Nome;
        cliente.Email = model.Email;
        await context.SaveChangesAsync();

        return Results.Ok(cliente);
    });

app.MapDelete("/v1/clientes/{id}", async (int id, AppDbContext context) =>
    {
        if (await context.Clientes.FindAsync(id) is Cliente cliente)
        {
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
            
            return Results.Ok(cliente);
        }
        return Results.NotFound();
    });

app.Run();
