using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using ResponseCache.Abstractions;
using ResponseCache.Extensions;
using ResponseCache.Provider.Memory.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseCache.Tests
{
    internal class ResponseCacheTests
    {
        [Test]
        public async Task Middleware_5_Second_Cache_Intime_Test()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddControllersWithViews().AddResponseCache(opt =>
                        {
                            opt.UseMemoryCache();
                            opt.PathDefinitions.Add(new CacheDefinition("/", 5));
                        }
                        );
                    });
                    webHost.Configure(app =>
                    {
                        app.UseResponseCache();
                        app.Run(async ctx => await ctx.Response.WriteAsync(DateTime.Now.ToString()));
                    });
                })
                .StartAsync();

            string responseFirst = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();

            await Task.Delay(3000);

            string responseSecond = await (await host.GetTestClient().GetAsync("/")).Content.ReadAsStringAsync();


            Assert.AreEqual(responseFirst, responseSecond);
        }

        [Test]
        public async Task Middleware_5_Second_Cache_Overtime_Test()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddControllersWithViews().AddResponseCache(opt =>
                        {
                            opt.UseMemoryCache();
                            opt.PathDefinitions.Add(new CacheDefinition("/", 5));
                        }
                    );
                    });
                    webHost.Configure(app =>
                    {
                        app.UseResponseCache();
                        app.Run(async ctx => await ctx.Response.WriteAsync(DateTime.Now.ToString()));
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
