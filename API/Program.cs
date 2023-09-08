using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Add CORS middleware for development only
    app.UseCors(
        builder =>
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200", "https://localhost:4200")
    );
}
else
{
    // In production, you should specify the actual origins
    app.UseCors(
        builder =>
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://your-production-domain.com")
    );
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
