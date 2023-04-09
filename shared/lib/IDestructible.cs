
using Godot;
/**
<summary>
Implement how the node should be "destroyed".

For example, if you want to destroy a node, you can call QueueFree() on it, but before doing that,
you can also add something to the parent node, like particles.
</summary>
*/
public interface IDestructible<T> where T : Node{
    T Destroy();
}