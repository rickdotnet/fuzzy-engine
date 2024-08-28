using DependencyManager;
using DependencyManager.Abstractions;
using Input.SourceOne;
using Input.SourceTwo;

namespace HostConsole;

public static class ExternalEvents
{
    public static ExternalEvent SourceOneDataReadyEvent => new()
    {
        EventType = DependencyEventType.DataReady,
        Details = nameof(SourceOneInput)
    };
    
    public static ExternalEvent SourceTwoDataReadyEvent => new()
    {
        EventType = DependencyEventType.DataReady,
        Details = nameof(SourceTwoInput)
    };
    
    public static ExternalEvent FileTriggerEvent => new()
    {
        EventType = DependencyEventType.FileTrigger,
        Details = "My-File"
    };
    
    public static ExternalEvent JobCompletedEvent => new()
    {
        EventType = DependencyEventType.JobCompleted,
        Details = "My-Job"
    };
    
}