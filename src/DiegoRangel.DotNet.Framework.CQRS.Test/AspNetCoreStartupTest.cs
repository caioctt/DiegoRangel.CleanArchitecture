using DiegoRangel.DotNet.Framework.CQRS.API;
using DiegoRangel.DotNet.Framework.CQRS.API.Services.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DiegoRangel.DotNet.Framework.CQRS.Test
{
    [TestFixture]
    public class AspNetCoreStartupTest
    {
        private IWebHost _webHost;

        [SetUp]
        public void Setup()
        {
            _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
        }

        [Test]
        public void StartupTest()
        {
            Assert.IsNotNull(_webHost);
            Assert.IsNotNull(_webHost.Services.GetRequiredService<IFilesService>());
        }
    }
}