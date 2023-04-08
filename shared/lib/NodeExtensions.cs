using System;
using System.Collections.Generic;
using Godot;

public static class NodeExtensions{
    /**
    <summary>
    This method is used to get a node from a path defined inside the specified node.
    </summary>
    */
    public static T GetNodeFromPathProperty<T>(this Node node, NodePath nodePathName) where T : Node{
        var path = node.Get(nodePathName) as NodePath;

        return node.GetNode<T>(path);
    }

    /**
    <summary>
    This method is used to add more than one child to a node.
    </summary>
    */
    public static void AddChildren(this Node node, IEnumerable<Node> children){
        foreach(var child in children){
            node.AddChild(child);
        }
    }


    /**
    <summary>
    This method returns a GroupNode with the given types.
    </summary>
    */
    public static GroupNode<T, P> IntoGroupNode<T, P>(this T node, string groupName) where T : Node, P{
        return new GroupNode<T, P>(node, groupName);
    }
}