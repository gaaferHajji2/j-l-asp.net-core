using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "fixed", options =>
{
    options.PermitLimit = 5;
    options.Window = TimeSpan.FromSeconds(5);
    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    options.QueueLimit = 2;
}));

// builder.Services.AddRequestTimeouts();
builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy { Timeout = TimeSpan.FromSeconds(5) };

    options.AddPolicy("short", TimeSpan.FromSeconds(2));
    options.AddPolicy("long", TimeSpan.FromSeconds(10));
});

var app = builder.Build();

app.UseRateLimiter();
app.UseRequestTimeouts();
app.UseCorrelationId();

//app.Use(async (context, next) =>
//{
//     //await context.Response.WriteAsync("Hello JLoka Simple Middleware World");

//    var logger = app.Services.GetRequiredService<ILogger<Program>>();

//    logger.LogInformation($"Request Host: {context.Request.Host}");
//    logger.LogInformation("My Middleware - Before");
//    await next(context);
//    logger.LogInformation("My Middleware - After");
//    logger.LogInformation($"Response StatusCode: {context.Response.StatusCode}");
//});

//app.Use(async (context, next) =>
//{
//    var logger = app.Services.GetRequiredService<ILogger<Program>>();
//    logger.LogInformation($"ClientName HttpHeader in Middleware 2: {context.Request.Headers["ClientName"]}");
//    logger.LogInformation("My Middleware 2 - Before");
//    context.Response.StatusCode = StatusCodes.Status202Accepted;
//    await next(context);
//    logger.LogInformation("My Middleware 2 - After");
//    logger.LogInformation($"Response StatusCode in Middleware 2: {context.Response.StatusCode}");
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
