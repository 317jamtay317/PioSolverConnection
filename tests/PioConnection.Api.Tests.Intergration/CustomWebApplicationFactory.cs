using Client.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PioConnection.Api.Core;
using PioConnection.Api.Logging;
using PioConnection.Api.Services;

namespace PioConnection.Api.Tests.Intergration;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram:class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IRangeService, RangeService>();
            services.AddSingleton(SolverConnectionFactory);
            services.AddSingleton(RangeServiceLogger);
        });
    }

    public ISolverConnection SolverConnection = Substitute.For<ISolverConnection>();
    public ISolverConnectionFactory SolverConnectionFactory = Substitute.For<ISolverConnectionFactory>();
    public ILoggerWrapper<RangeService> RangeServiceLogger = Substitute.For<ILoggerWrapper<RangeService>>();
}