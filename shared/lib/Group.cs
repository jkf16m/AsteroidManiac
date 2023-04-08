using System;
using Godot;
using System.Collections.Generic;
public class Group<P>{
    public string GroupName{get; private set;}
    public Group(string groupName){
        this.GroupName = groupName;
    }

    public GroupNode<T,P> ToGroupNode<T>(T node) where T : Node, P{
        return new GroupNode<T, P>(node, this.GroupName);
    }

    public GroupNode<T,P> TryToGroupNode<T>(T node) where T : Node, P{
        if(node.IsInGroup(this.GroupName)){
            return new GroupNode<T, P>(node, this.GroupName);
        }
        return null;
    }
}