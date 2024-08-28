using DependencyManager.Abstractions;

namespace HostConsole.Dependencies;

public class SourceOneInputTrigger : IInputTrigger
{
    public static readonly SourceOneInputTrigger Instance = new();
    private bool dataReady;
    private bool fileTriggered;
    private DateTime lastTriggered = DateTime.MinValue;

    public bool DependenciesMet => dataReady;

    // determine if thread safety is needed
    // probably not at first, I'd block and 
    // make sure folks understand the implications
    public async ValueTask<bool> Handle(DependencyEvent dependencyEvent)
    {
        Console.WriteLine("Source One Dependency Event");
        Console.WriteLine($"Type: {dependencyEvent.EventType}, Details: {dependencyEvent.Details}\n");
        
        if (dataReady && fileTriggered)
            return true;

        if (dependencyEvent.EventType == DependencyEventType.DataReady
            && dependencyEvent.Details == nameof(SourceOneInputTrigger))
        {
            dataReady = await SomeAsyncMethod();
        }

        if (dependencyEvent.EventType == DependencyEventType.FileTrigger
            && dependencyEvent.Details == nameof(SourceOneInputTrigger))
        {
            fileTriggered = true;
        }

        var result = dataReady && fileTriggered;
        if (!result) return result;

        // keep track that we ran this
        lastTriggered = DateTime.Now;
        dataReady = false;
        fileTriggered = false;

        return result;
    }

    private Task<bool> SomeAsyncMethod()
    {
        return Task.FromResult(true);
    }
}