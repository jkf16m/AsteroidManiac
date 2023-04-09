using System;
using System.Collections.Generic;
using Godot;

public class DamageSenderArea2D : Node, IComponent<Area2D>{
    public Area2D Parent => this.GetParentOrNull<Area2D>();

    [Export]
    public NodePath DamagePath{get; private set;}
    public Damage Damage{get; private set;}
    public CollisionShape2D CollisionShape2D{get; private set;}

    public override void _Ready()
    {
        if(Parent == null)
            throw new Exception("This component must be a child of an Area2D node.");

        Damage = GetNode<Damage>(DamagePath);

        CollisionShape2D = Parent.GetNodeByTypeOrNull<CollisionShape2D>();
    }


}