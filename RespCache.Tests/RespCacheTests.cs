using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using RespCache.Abstractions;
using RespCache.Extensions;
using RespCache.Provider.Memory.Extensions;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace RespCache.Tests
{
    internal class RespCacheTests
    {
        [Test]
        public async Task Controllers_5_Second_Cache_Intime_Test()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddControllers().AddRespCache(opt =>
                        {
                            opt.UseMemoryCache();
                            opt.PathDefinitions.Add(new CacheDefinition("/", 5));
                        }
                        );
                    });
                    webHost.Configure(app =>
                    {
                        app.UseRespCache();
                        app.Run(async ctx => await ctx.Response.WriteAsync(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                    });
                })
                .StartAsync();

            string responseFirst = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            await Task.Delay(3000);

            string responseSecond = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            Assert.AreEqual(responseFirst, responseSecond);
        }

        [Test]
        public async Task Controllers_5_Second_Cache_Overtime_Test()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddControllers().AddRespCache(opt =>
                        {
                            opt.UseMemoryCache();
                            opt.PathDefinitions.Add(new CacheDefinition("/", 5));
                        }
                    );
                    });
                    webHost.Configure(app =>
                    {
                        app.UseRespCache();
                        app.Run(async ctx => await ctx.Response.WriteAsync(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                    });
                })
                .StartAsync();

            string responseFirst = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            await Task.Delay(5100);

            string responseSecond = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            Assert.AreNotEqual(responseFirst, responseSecond);
        }

        [Test]
        public async Task ControllersWithView_5_Second_Cache_Intime_Test()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddControllersWithViews().AddRespCache(opt =>
                        {
                            opt.UseMemoryCache();
                            opt.PathDefinitions.Add(new CacheDefinition("/", 5));
                        }
                        );
                    });
                    webHost.Configure(app =>
                    {
                        app.UseRespCache();
                        app.Run(async ctx => await ctx.Response.WriteAsync(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                    });
                })
                .StartAsync();

            string responseFirst = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            await Task.Delay(3000);

            string responseSecond = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            Assert.AreEqual(responseFirst, responseSecond);
        }

        [Test]
        public async Task ControllersWithView_5_Second_Cache_Overtime_Test()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddControllersWithViews().AddRespCache(opt =>
                        {
                            opt.UseMemoryCache();
                            opt.PathDefinitions.Add(new CacheDefinition("/", 5));
                        }
                    );
                    });
                    webHost.Configure(app =>
                    {
                        app.UseRespCache();
                        app.Run(async ctx => await ctx.Response.WriteAsync(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                    });
                })
                .StartAsync();

            string responseFirst = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            await Task.Delay(5100);

            string responseSecond = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            Assert.AreNotEqual(responseFirst, responseSecond);
        }
    }
}
