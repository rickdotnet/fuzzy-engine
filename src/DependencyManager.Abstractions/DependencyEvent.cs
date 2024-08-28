namespace DependencyManager.Abstractions;

// might use
public record DependencyEvent
{
    public DependencyEventType EventType { get; init; }
    public string? Details { get; init; }
}

public enum DependencyEventType
{
    DataReady,
    FileTrigger,
    JobCompleted,
    JobFailed,
    ResetState,
}