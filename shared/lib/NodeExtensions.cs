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
    public static GroupNode<T> IntoGroupNode<T>(this T node, string groupName) where T : Node
    {
        return new GroupNode<T>(node, groupName);
    }

    /**
    <summary>
    Tries to cast the node to the given type.
    </summary>
    */
    public static T As<T>(this Node node) where T : Node{
        return node as T;
    }


    /**
    <summary>
    Gets child node by the given type, matches the first one.
    </summary>
    */
    public static T GetNodeByType<T>(this Node node) where T : Node{
        var child = node.GetNodeByTypeOrNull<T>();

        if(child == null)
            throw new Exception($"Could not find a child node of type {typeof(T)}");

        return child;
    }

    public static I GetNodeByType<T,I>(this Node node) where T : Node where I : Node{
        var child = node.GetNodeByTypeOrNull<T>();

        if(child == null)
            throw new Exception($"Could not find a child node of type {typeof(T)}");

        return child.As<I>();
    }

    /**
    <summary>
    Gets child node by the given type, matches the first one. Returns null if not found.
    </summary>
    */
    public static T GetNodeByTypeOrNull<T>(this Node node) where T : Node{
        var children = node.GetChildren();
        foreach(var child in children){
            if(child is T){
                return child as T;
            }
        }
        return null;
    }
}