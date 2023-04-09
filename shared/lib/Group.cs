using System;
using Godot;
using System.Collections.Generic;
public class Group<T> where T: Node{
    public string GroupName{get; private set;}
    public Group(string groupName){
        this.GroupName = groupName;
    }

    public GroupNode<T> ToGroupNode(Node node){
        return new GroupNode<T>((T)node, this.GroupName);
    }

    public GroupNode<T> TryToGroupNode(T node){
        if(node.IsInGroup(this.GroupName)){
            return new GroupNode<T>(node, this.GroupName);
        }
        return null;
    }

    public IEnumerable<GroupNode<T>> GetNodes(){
        foreach(var node in new Node().GetTree().GetNodesInGroup(this.GroupName)){
            if(node is T){
                yield return new GroupNode<T>((T)node, this.GroupName);
            }
        }
    }
}