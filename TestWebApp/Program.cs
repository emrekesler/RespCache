using ResponseCache.Abstractions;
using ResponseCache.Extensions;
using ResponseCache.Provider.Memory.Extensions;
using ResponseCache.Provider.Redis.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddResponseCache(opt =>
{
    //opt.UseMemoryCache();
    opt.UseRedis();

    opt.PathDefinitions.Add(new CacheDefinition("/", 10));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseResponseCache();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
