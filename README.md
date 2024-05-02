# BlazorTopShelf

This sample demonstrates running a .NET 8 Blazor WebApp using VB.NET as much as possible, hosted in a Windows Service using TopShelf.

It started as the default C# project as scaffolded from the Blazor WebApp template. Identity Pages and Sample Pages were included in the scaffolding. The SignalR demo, added later, was built using [this guidance](https://learn.microsoft.com/en-us/aspnet/core/blazor/tutorials/signalr-blazor).

You may call VB.NET code from a C# Razor page by adding classes to the `Presentation.Operations` project as needed, and then referencing them from the page's `@code {}` section.

To install the service, `dotnet publish` to your desired output directory, and then run the following command from an elevated command prompt in that directory:

`Presentation.exe install`

This will install the service. You can then start it using `net start BlazorTopShelf` or *services.msc*. With the service running, navigate to [http://localhost:5000/](http://localhost:5000/) to view the application and navigate its pages.

The installation is configured in the `Host.Run()` method to set the service's startup type to *Automatic (Delayed Start)*. You can change this setting to your liking by calling `HostConfig.Disabled()`, `HostConfig.StartManually()`, `HostConfig.StartAutomatically()` or `HostConfig.StartAutomaticallyDelayed()`.

To uninstall the service, run the following command from an elevated command prompt:

`Presentation.exe uninstall`

Here's a high-level view of how it works:

1. The C# `Program` class is the entry point for the application. After obtaining the `OnStart()` and `OnStop()` actions from `Presentation.Operations`, it initializes a `Host` instance, and then calls the `Host.Run()` method.
2. `Host.Run()` sets the stage for the service, including specifying what happens when the service starts and stops.
3. The `Manager.StartService()` method configures and invokes the constructor-provided `OnStart()` action—which in turn configures and starts the website—when the service starts. Note that the `WebApplication` class provides a `Run()` method, which blocks and listens on a port and which we normally use, but since TopShelf handles the blocking all we need here is the listening component, `StartAsync()`.

## Notes

- A SQLite database will be created and schemas migrated on first run, in the directory `%LocalAppData%\BlazorTopShelf`.
- The TopShelf repository is located at [https://github.com/Topshelf/Topshelf](https://github.com/Topshelf/Topshelf).
