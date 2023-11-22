using Application.Activities;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add cors policy for resources coming from localhost:3000
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => 
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});
// Tell application about MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
// Registers Automapper as a service, looks inside Assembly, which contains MappingProfiles, and registers all of our mapping profiles so they can then be used
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add cors middleware to allow cross origin requests from our API to front end
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// Using statement destroys the scope after following code is executed
// Create scope to get access to services
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// Update database and log any errors that occurr
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    // Seed data in db if needed
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
