using Godot;
using System;
using System.Linq;
/**
<summary>
This class is used to handle the collision of the entity
When the given reference to a Area2D touches another entity, it will call the OnAreaEntered method, and 
reduce the health of the entity.

This only communicates with Area2D components that are composed with the Damage2DArea component.
</summary>
*/
class Health2DArea : Node{
    [Export]
    public NodePath Area2DPath{get; private set;}
    public Area2D Area2D{get; private set;}

    [Export]
    public NodePath HealthPath{get; private set;}
    public Health Health{get; private set;}
    
    [Export]
    public string[] GroupsToTakeDamageFrom{get; private set;}


    public override void _Ready()
    {
        Area2D = GetNode<Area2D>(Area2DPath);
        Area2D.Connect("area_entered", this, nameof(OnAreaEntered));
        Area2D.Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        var damage2DAreaNode = area.GetNode<Damage2DArea>("Damage2DArea");
        if(
            damage2DAreaNode != null
            && GroupsToTakeDamageFrom.Any(q=>damage2DAreaNode.GetGroups().Contains(q))
        ){
            Health.TakeDamage(damage2DAreaNode.Damage.Value);
        }
    }

    public void OnBodyEntered(Node body)
    {
        var damage = body.GetNodeOrNull<Damage>("DamageArea2D");

        if(damage != null && GroupsToTakeDamageFrom.Any(q=>damage.GetGroups().Contains(q))){
            Health.TakeDamage(damage.Value);
        }
    }
}