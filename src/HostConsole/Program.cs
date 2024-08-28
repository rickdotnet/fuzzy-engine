using DependencyManager;
using DependencyManager.Abstractions;
using HostConsole.Dependencies;
using Input.SourceOne;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<DependencyListener>();
services.AddSingleton<SourceOneInput>();

var provider = services.BuildServiceProvider();
var manager = provider.GetRequiredService<DependencyListener>();
manager.AddInputTrigger<SourceOneInput>(SourceOneInputTrigger.Instance);

var externalEvent = new ExternalEvent
{
    EventType = DependencyEventType.DataReady,
    Details = nameof(SourceOneInputTrigger)
};

await manager.Handle(externalEvent);

externalEvent = new ExternalEvent
{
    EventType = DependencyEventType.FileTrigger,
    Details = nameof(SourceOneInputTrigger)
};

await Task.Delay(5000);
await manager.Handle(externalEvent);