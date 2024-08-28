using HostConsole;
using HostConsole.Dependencies;
using Input.SourceOne;
using Input.SourceTwo;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<HostConsole.DependencyManager>();
services.AddSingleton<SourceOneInput>();
services.AddSingleton<SourceTwoInput>();

var provider = services.BuildServiceProvider();
var manager = provider.GetRequiredService<HostConsole.DependencyManager>();
manager.AddInputTrigger<SourceOneInput>(SourceOneInputTrigger.Instance);
manager.AddInputTrigger<SourceTwoInput>(SourceTwoInputTrigger.Instance);

// these represent external events
await manager.Handle(ExternalEvents.SourceOneDataReadyEvent);
await manager.Handle(ExternalEvents.SourceTwoDataReadyEvent);
await Task.Delay(2000);

await manager.Handle(ExternalEvents.FileTriggerEvent);
await Task.Delay(5000);

await manager.Handle(ExternalEvents.JobCompletedEvent);