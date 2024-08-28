using DependencyManager.Abstractions;

namespace Input.SourceTwo;

public class SourceTwoInput : IInput
{
    public Task CreateInput()
    {
        Console.WriteLine("Source One Input Created");
        return Task.CompletedTask;
    }
}