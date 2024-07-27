var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/hello-world/{id}", (Ulid id) => 
{
    return Results.Ok(new { Id = id }); 
});

app.Run();
