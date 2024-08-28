using DependencyManager.Abstractions;

namespace DependencyManager;

/// <summary>
/// Listens for dependencies and triggers the appropriate input
/// </summary>
public class DependencyListener
{
    private readonly IServiceProvider inputProvider;
    private readonly Dictionary<Type, IInputTrigger> inputTriggersTypeMapping = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputProvider"></param>
    public DependencyListener(IServiceProvider inputProvider)
    {
        this.inputProvider = inputProvider;
    }

    public void AddInputTrigger<T>(IInputTrigger inputTrigger) where T : IInput
    {
        inputTriggersTypeMapping.TryAdd(typeof(T), inputTrigger);
    }
    
    // would likely use a channel and either process
    // the whole event or process the input.CreateInput
    public async Task Handle(ExternalEvent externalEvent)
    {
        var internalEvent = new DependencyEvent
        {
            EventType = externalEvent.EventType,
            Details = externalEvent.Details
        };
        
        foreach (var mapping in inputTriggersTypeMapping)
        {
            var trigger = mapping.Value;
            var dependencyMet = await trigger.Handle(internalEvent);
            if (!dependencyMet) continue;
            
            if(inputProvider.GetService(mapping.Key) is not IInput input)
                throw new InvalidOperationException("Input not found");
                
            // this could go on a channel
            await input.CreateInput();
        }
    }
}