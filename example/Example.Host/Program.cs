using Example.Module;
using Pii.Serilog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, _, configuration) => configuration
    //simulate module, realistically this would be baked into the assembly scan that is already happening
    //or a loop could be built over services of registered IApiModules, [TODO: research required]
    .AddPiiDestructurer(typeof(TestThing).Assembly) 
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
        var obj = new Complex(new TestThing("top secret", "1234567890123456"), "Tah dah");
        logger.LogInformation("I'm logging top secret object casually: {@Object}", obj);
        return obj;
    })
.WithName("Test Thing")
.WithOpenApi();

app.Run();
