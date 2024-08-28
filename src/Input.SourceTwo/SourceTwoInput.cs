using DependencyManager.Abstractions;

namespace Input.SourceTwo;

public class SourceTwoInput : IInput
{
    public Task CreateInput()
    {
        Console.WriteLine("Source Two Input Created");
        return Task.CompletedTask;
    }
}