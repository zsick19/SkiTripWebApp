using API;
using Infra;
using Infra.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

var builder = WebApplication.CreateBuilder(args);


var connString = builder.Configuration.GetConnectionString("SkiTrip");
builder.Services.AddSqlite<SkiTripContext>(connString);

const string MyReactAppCorsPolicy = "ReactAppCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyReactAppCorsPolicy, policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
        if (allowedOrigins != null && allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
            // .AllowCredentials();
        }
    });
});

builder.Services.AddLibraryServices();

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();


builder.Services.AddExceptionHandler<MyExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Create an isolated dependency injection scope to safely resolve your DbContext
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<SkiTripContext>();

            // Invoke your seeding logic once on application startup
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }
}

app.UseCors(MyReactAppCorsPolicy);

app.MapControllers();
app.UseStatusCodePages();
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}


app.UseOpenApi();
app.UseSwaggerUi();

app.Run();





