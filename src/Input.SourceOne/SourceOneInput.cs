using DependencyManager.Abstractions;

namespace Input.SourceOne;

public class SourceOneInput : IInput
{
    public Task CreateInput()
    {
        Console.WriteLine("Source One Input Created\n");
        return Task.CompletedTask;
    }
}