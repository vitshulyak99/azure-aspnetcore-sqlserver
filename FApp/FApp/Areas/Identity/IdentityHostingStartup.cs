using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FApp.Areas.Identity.IdentityHostingStartup))]
namespace FApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}