
# RespCache

[![Nuget](https://img.shields.io/nuget/v/RespCache?label=nuget%20-%20RespCache)](https://www.nuget.org/packages/RespCache/)

[![Nuget](https://img.shields.io/nuget/v/RespCache.Provider.Memory?label=nuget%20-%20RespCache.Provider.Memory)](https://www.nuget.org/packages/RespCache.Provider.Memory/)

[![Nuget](https://img.shields.io/nuget/v/RespCache.Provider.Redis?label=nuget%20-%20RespCache.Provider.Redis)](https://www.nuget.org/packages/RespCache.Provider.Redis/)

## Açıklama 

Bu projeyi ASP.NET Core projelerinde üretilen responseları kolayca cachelemek için kullanılır.

`IMvcBuilder` için extension method ile kullanılır.
## Kurulum

### Redis

`appsettings.json`

```json
"RespCache": {
   "Redis": {
     "ConnectionString": "localhost:6379"
   }
 }
```
### 
`Startup.cs / Program.cs`
```csharp
services.AddControllers().AddRespCache(opt =>
{
    opt.UseRedis();
});
```

### Memory Cache

`Startup.cs / Program.cs`
```csharp
services.AddControllers().AddRespCache(opt =>
{
    opt.UseMemoryCache();
});

```

## Kullanım

### RespCache Attribute

RespCache Attribute Controller ve ya Action seviyesinde eklenebilir.
`Her ikisine de eklendiği durumda Action'da yapılan ayar geçerlidir.`

```csharp
[RespCache("Home", 10)]
public class HomeController : Controller
{
    [RespCache("HomeIndex", 20)]
    public IActionResult Index()
    {
        return View();
    }
}
```

### RespCache TagHelper

`Action ya da Controller cache tanımları TagHelper'ı etkilemez.`

```csharp
@addTagHelper *, RespCache

<resp-cache key="test" seconds="10">
    @DateTime.Now   
</resp-cache>

```

### Path Definitions

RespCache register edilirken path definition listesine `key` parametresine route url verilerek cacheleme sağlanabilir.

```csharp
services.AddControllersWithViews().AddRespCache(opt =>
{
    opt.UseMemoryCache();

    opt.PathDefinitions.Add(new CacheDefinition("/", 10));
    opt.PathDefinitions.Add(new CacheDefinition("/Home/Privacy", 20));
});
```


## Özel Ayarlar

`HttpCacheItemKey`

Attribute kullanımda cache tanımlamaları HttpContext Itemlarına varsayılan olarak `CacheOptions` keyi ile eklenir. Bu key register sırasında set edilebilir.

```csharp
services.AddControllersWithViews().AddRespCache(opt =>
{
    opt.UseMemoryCache();

    opt.HttpCacheItemKey = "MyCustomCacheItemKey";
});
```

