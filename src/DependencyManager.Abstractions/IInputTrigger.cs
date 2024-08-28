namespace DependencyManager.Abstractions;

public interface IInputTrigger
{
    /// <summary>
    /// Indicates if the dependencies are met
    /// </summary>
    bool DependenciesMet { get; }
    
    /// <summary>
    /// Handles DependencyEvents and tracks the state of the dependency
    /// </summary>
    /// <param name="dependencyEvent"></param>
    /// <returns>Boolean indicating if the dependency is met</returns>
    ValueTask<bool> Handle(DependencyEvent dependencyEvent);
}