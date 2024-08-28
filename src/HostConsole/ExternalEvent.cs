using DependencyManager.Abstractions;

namespace DependencyManager;

/// <summary>
/// Using the same properties as the DependencyEvent to represent an external event
/// </summary>
public class ExternalEvent
{
    public DependencyEventType EventType { get; init; }
    public string? Details { get; init; }
}