using MinimalApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

var list = new List<Post>()
 {
    new() { Id = 1, Title = "First Post", Content = "Hello World" },
    new() { Id = 2, Title = "Second Post", Content = "Hello Again" },
    new() { Id = 3, Title = "Third Post", Content = "Goodbye World" },
 };

app.MapGet("/posts", async (IPostService postService) => await postService.GetPostsAsync()).WithName("Get Posts").WithTags("Posts").WithOpenApi();

app.MapPost("/posts", async (IPostService postService, Post post) =>
{
    await postService.CreatePostAsync(post);

    return Results.Created($"/posts/{post.Id}", post);
}).WithName("Create new post").WithTags("Posts").WithOpenApi();

app.MapGet("/posts/{postId}", async (IPostService postService, int postId) =>
{
    var post = await postService.GetPostAsync(postId);

    return post == null ? Results.NotFound() : Results.Ok(post);
}).WithName("get post by id").WithTags("Posts").WithOpenApi();

app.MapPut("/posts/{postId}", async (IPostService postService, int postId, Post post) =>
{
    try
    {
        var updatedPost = await postService.UpdatePostAsync(postId, post);
        return Results.Ok(updatedPost);
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
}).WithName("Update post by id").WithTags("Posts").WithOpenApi();

app.MapDelete("/posts/{postId}", async (IPostService postService, int postId) =>
{
    try
    {
        await postService.DeletePostAsync(postId);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
}).WithName("Delete post by id").WithTags("Posts").WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
