using System;
using System.Collections.Generic;
using Godot;

public class DamageSenderArea2D : Node, IComponent<Area2D>{
    public Area2D Parent {get;private set;} 

    [Export]
    public NodePath DamagePath{get; private set;}
    public Damage Damage{get; private set;}
    [Export]
    public string[] Groups{get; private set;}
    public CollisionShape2D CollisionShape2D{get; private set;}

    public override void _Ready()
    {
        Parent = GetParentOrNull<Area2D>();
        if(Parent == null)
            throw new Exception("This component must be a child of an Area2D node.");

        foreach(var group in Groups){
            AddToGroup(group);
        }

        Damage = GetNode<Damage>(DamagePath);

        CollisionShape2D = Parent.GetNodeByTypeOrNull<CollisionShape2D>();
    }


}