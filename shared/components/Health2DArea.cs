using Godot;
using System;
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
    public override void _Ready()
    {
        Area2D = GetNode<Area2D>(Area2DPath);
        Area2D.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        var damage2DArea = area.GetNodeOrNull<Damage2DArea>("Damage2DArea");
        if(damage2DArea != null)
        {
            Health.Damage(damage2DArea.Damage.Value);
        }
    }
}