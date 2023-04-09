using System;
using Godot;

public class DestroyOnTouch : Node, IComponent<Node>
{
    public Node Parent {get; private set;}
    [Export]
    public string[] Groups{get; private set;}

    public override void _Ready()
    {
        Parent = GetParentOrNull<Node>();
        if(Parent == null)
            throw new Exception("This component must be a child of a Node.");

        foreach(var group in Groups){
            AddToGroup(group);
        }

    }
}