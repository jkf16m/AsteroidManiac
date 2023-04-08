using Godot;
using System;
/**
<summary>
This class handles the damage part of the entity
holds reference to both the Damage and Area2D components of the damage sender
</summary>
*/
class Damage2DArea : Area2D, IDangerGroup{
    [Export]
    public NodePath DamagePath{get; private set;}
    public Damage Damage{get; private set;}


    public override void _Ready()
    {
        Damage = GetNode<Damage>(DamagePath);
    }
}