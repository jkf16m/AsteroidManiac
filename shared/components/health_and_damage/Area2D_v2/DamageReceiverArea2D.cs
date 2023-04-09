using Godot;
using System;
using System.Linq;

/**
<summary>
It checks if the an area of type DamageSenderArea2D is entering.
If it is, it will call the OnAreaEntered method, and
reduce the health of the entity.

This component also defines the groups that this entity can take damage from.
</summary>
*/
public class DamageReceiverArea2D : Node, IComponent<Area2D>
{
    public Area2D Parent {get; private set;}

    [Export]
    public string[] GroupsToTakeDamageFrom{get; private set;}
    [Export]
    public NodePath HealthPath{get; private set;}
    public Health Health{get; private set;}
    public CollisionShape2D CollisionShape2D{get; private set;}

    public override void _Ready()
    {
        Parent = this.GetParentOrNull<Area2D>();
        if(Parent == null)
            throw new Exception("This component must be a child of an Area2D node.");

        Health = GetNode<Health>(HealthPath);
        CollisionShape2D = this.GetNodeByTypeOrNull<CollisionShape2D>();

        Parent.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        var damageSenderArea2D = area.GetNodeByTypeOrNull<DamageSenderArea2D>();

        var damage = damageSenderArea2D?.Damage;

        if(damage != null
            && GroupsToTakeDamageFrom.Any(q=>damageSenderArea2D.GetGroups().Contains(q))
        ){
            Health.TakeDamage(damage.Value);
        }
    }

}
