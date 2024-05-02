using System;
using Microsoft.AspNetCore.Builder;
using Presentation.Components;
using Presentation.Operations;
using Presentation.Service;
using static Presentation.Service.Enums;

namespace Presentation
{
  public class Program
  {
    public static void Main(string[] args)
    {
      ServiceEvents serviceEvents;
      RunContexts context;
      string serviceName;
      string displayName;
      string description;
      Action onStart;
      Action onStop;
      Host host;

      serviceName = "BlazorTopShelf";
      displayName = "BlazorTopShelf Demo";
      description = "A project demonstrating running a .NET 8 Blazor WebApp in a TopShelf Windows Service";
      context = RunContexts.NetworkService;

      website = CreateHostBuilder(args).Build();
      onStart = ServiceInitializer.OnStart<App>(website);
      onStop = ServiceInitializer.OnStop(website);

      serviceEvents = new ServiceEvents {
        { ServiceEvents.Events.OnStart, onStart },
        { ServiceEvents.Events.OnStop, onStop }
      };

      host = new Host(serviceName, displayName, description, context);
      host.Run(serviceEvents);
    }



    /// <summary>
    /// Creates the host builder for the website.
    /// </summary>
    /// <param name="args">The incoming command line arguments.</param>
    /// <returns>A <see cref="WebApplicationBuilder"/> instance.</returns>
    /// <remarks>This function is required here to support EF Core's
    /// design-time commands (Add-Migration, Update-Database, etc.).<br/>
    /// See <see href="https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation"/> 
    /// for more information.</remarks>
    public static WebApplicationBuilder CreateHostBuilder(string[] args)
    {
      return ServiceInitializer.CreateHostBuilder(args);
    }



    private static WebApplication? website;
  }
}
