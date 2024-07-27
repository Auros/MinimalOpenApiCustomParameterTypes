using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddRouting(options => options.ConstraintMap.Add("ulid", typeof(UlidRouteConstraint)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/hello-world/{id:ulid}", (Ulid id) => 
{
    return Results.Ok(new { Id = id }); 
});

app.Run();

class UlidRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!values.TryGetValue(routeKey, out var routeValue))
            return false;
    
        var value = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
        return value is not null && Ulid.TryParse(value, out _);
    }
}