using CarBuilder.Models;

var builder = WebApplication.CreateBuilder(args);

List<PaintColor> paintColors = new List<PaintColor>()
{
    new PaintColor()
    {
        Id = 1,
        Price = 1750.75M,
        Color = "Midnight Blue"
    },
    new PaintColor()
    {
        Id = 2,
        Price = 1272.50M,
        Color = "Silver"
    },
    new PaintColor() 
    {
        Id = 3,
        Price = 1435.25M,
        Color = "Spring Green"
    },
    new PaintColor() 
    {
        Id = 4,
        Price = 2000.00M,
        Color = "Firebrick Red"
    }
};

List<Interior> interiors = new List<Interior>()
{
    new Interior()
    {
        Id = 1,
        Price = 565.95M,
        Material = "Beige Fabric"
    },
    new Interior()
    {
        Id = 2,
        Price = 765.95M,
        Material = "Charcoal Fabric"
    },
    new Interior()
    {
        Id = 3,
        Price = 1495.99M,
        Material = "White Leather"
    },
    new Interior()
    {
        Id = 4,
        Price = 1795.99M,
        Material = "Black Leather"
    }
};

List<Technology> technologies = new List<Technology>()
{
    new Technology()
    {
        Id = 1,
        Price = 499.99M,
        Package = "Basic Package (basic sound system)"
    },
    new Technology()
    {
        Id = 2,
        Price = 799.99M,
        Package = "Navigation Package (includes integrated navigation controls)"
    },
    new Technology()
    {
        Id = 3,
        Price = 1099.99M,
        Package = "Visibility Package (includes side and reat cameras)"
    },
    new Technology()
    {
        Id = 4,
        Price = 1299.99M,
        Package = "Ultra Package (includes navigation and visibility packages)"
    }

};

List<Wheels> wheels = new List<Wheels>()
{
    new Wheels()
    {
        Id = 1,
        Price = 399.99M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2,
        Price = 499.99M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 599.99M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        Id = 4,
        Price = 699.99M,
        Style = "18-inch Pair Spoke Black"
    }
};

List<Order> orders = new List<Order>()
{
   new Order()
   {
    Id = 1,
    WheelId = 2,
    TechnologyId = 3,
    PaintId = 4,
    InteriorId = 1
   },
   new Order()
   {
    Id = 2,
    WheelId = 2,
    TechnologyId = 3,
    PaintId = 4,
    InteriorId = 1
   }
};

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.MapGet("/paintcolors", () => 
{
    return paintColors;
});

app.MapGet("technologies", () => 
{
    return technologies;
});

app.MapGet("/wheels", () => 
{
    return wheels;
});

app.MapGet("/interiors", () => 
{
    return interiors;
});

app.MapGet("/orders", () =>
{
    return orders;
});

app.MapGet("orders/{id}", (int id) =>
{
    Order order = orders.FirstOrDefault(order => order.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(order);
});

app.MapPost("/orders", (Order order) => {
    order.Id = orders.Count > 0 ?orders.Max(order => order.Id) + 1: 1;
    order.Timestamp = DateTime.Now;
    orders.Add(order);
    return order;
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
}

app.UseHttpsRedirection();


app.Run();

