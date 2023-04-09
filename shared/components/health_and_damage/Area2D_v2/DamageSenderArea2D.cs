using System;
using System.Collections.Generic;
using Godot;

public class DamageSenderArea2D : Area2D{
    [Export]
    public NodePath DamagePath{get; private set;}
    public Damage Damage{get; private set;}
    public CollisionShape2D CollisionShape2D{get; private set;}

    public override void _Ready()
    {
        Damage = GetNode<Damage>(DamagePath);

        CollisionShape2D = this.GetNodeByTypeOrNull<CollisionShape2D>();
    }


}