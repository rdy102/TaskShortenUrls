using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using UrlShortener.Data.Context;
using UrlShortener.Data.Repository;
using UrlShortener.Data.Repository.Abstraction;
using UrlShortener.Domain.Abstraction;
using UrlShortener.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShortUrlDbContext>(opt =>
    opt.UseInMemoryDatabase("ShorUrlTask")
    );
builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
builder.Services.AddScoped<IShortUrlService, ShortUrlService>();
builder.Services.AddAutoMapper((typeof(Program)));
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(options =>
{
    // Use the 80/20 rule for cache eviction
    options.CompactionPercentage = 0.2;

});
builder.Services.Configure<MemoryCacheEntryOptions>(options =>
{
    options.SlidingExpiration = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();