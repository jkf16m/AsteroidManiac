using Godot;

public interface IComponent<T> where T : Node
{
    T Parent { get; }
}