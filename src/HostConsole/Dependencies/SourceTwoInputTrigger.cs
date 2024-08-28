using DependencyManager.Abstractions;
using Input.SourceTwo;

namespace HostConsole.Dependencies;

public class SourceTwoInputTrigger : IInputTrigger
{
    public static readonly SourceTwoInputTrigger Instance = new();
    private bool dataReady;
    private bool jobCompleted;
    private DateTime lastTriggered = DateTime.MinValue;

    public bool DependenciesMet => dataReady;

    // determine if thread safety is needed
    // probably not at first, I'd block and 
    // make sure folks understand the implications
    public async ValueTask<bool> Handle(DependencyEvent dependencyEvent)
    {
        
        Console.WriteLine("Source Two Received Event");
        Console.WriteLine($"Type: {dependencyEvent.EventType}, Details: {dependencyEvent.Details}\n");
        
        if (dataReady && jobCompleted)
            return true;

        if (dependencyEvent.EventType == DependencyEventType.DataReady
            && dependencyEvent.Details == nameof(SourceTwoInput))
        {
            dataReady = await SomeAsyncMethod();
        }

        if (dependencyEvent.EventType == DependencyEventType.JobCompleted
            && dependencyEvent.Details == "My-Job")
        {
            jobCompleted = true;
        }

        var result = dataReady && jobCompleted;
        if (!result) return result;

        // keep track that we ran this
        lastTriggered = DateTime.Now;
        dataReady = false;
        jobCompleted = false;

        return result;
    }

    private Task<bool> SomeAsyncMethod()
    {
        return Task.FromResult(true);
    }
}