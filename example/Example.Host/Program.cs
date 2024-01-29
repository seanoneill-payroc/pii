using Example.Module;
using Pii.Serilog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, _, configuration) => configuration
    .AddPiiDestructurer(typeof(TestThing).Assembly) //simulate module, realistically this would be baked into the assembly scan that is already happening or a loop could be built over services of registered IApiModules, [TODO: research required]
    .WriteTo.Console()
);

#region Snip...

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#endregion

app.MapGet("/", (ILogger<TestThing> logger) =>
    {
        var obj = new TestThing("top secret");
        logger.LogInformation("I'm logging top secret object casually: {@TestThing}", obj);
        return obj;
    })
.WithName("Test Thing")
.WithOpenApi();

app.Run();
