using DependencyManager;
using DependencyManager.Abstractions;
using HostConsole.Dependencies;
using Input.SourceOne;
using Input.SourceTwo;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<DependencyListener>();
services.AddSingleton<SourceOneInput>();
services.AddSingleton<SourceTwoInput>();

var provider = services.BuildServiceProvider();
var manager = provider.GetRequiredService<DependencyListener>();
manager.AddInputTrigger<SourceOneInput>(SourceOneInputTrigger.Instance);
manager.AddInputTrigger<SourceTwoInput>(SourceTwoInputTrigger.Instance);

var externalEvent = new ExternalEvent
{
    EventType = DependencyEventType.DataReady,
    Details = nameof(SourceOneInput)
};

await manager.Handle(externalEvent);

externalEvent = new ExternalEvent
{
    EventType = DependencyEventType.DataReady,
    Details = nameof(SourceTwoInput)
};

await manager.Handle(externalEvent);
await Task.Delay(2000);

externalEvent = new ExternalEvent
{
    EventType = DependencyEventType.FileTrigger,
    Details = "My-File"
};

await manager.Handle(externalEvent);
await Task.Delay(5000);

externalEvent = new ExternalEvent
{
    EventType = DependencyEventType.JobCompleted,
    Details = "My-Job"
};

await manager.Handle(externalEvent);