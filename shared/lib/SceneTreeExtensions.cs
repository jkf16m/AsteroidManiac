using System.Collections.Generic;
using Godot;

public static class SceneTreeExtensions
{
    /**
    <summary>
        Returns all nodes in the group that are of type T.
    </summary>
    **/
    public static IEnumerable<T> GetNodesInGroup<T>(this SceneTree tree, string groupName) where T : Node
    {
        var nodes = tree.GetNodesInGroup(groupName);
        foreach (var node in nodes)
        {
            if (node is T)
            {
                yield return (T)node;
            }
        }
    }
}