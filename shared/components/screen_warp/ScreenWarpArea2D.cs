using Godot;
using System;

public class ScreenWarpArea2D : Node, IComponent<Area2D>
{
    public Area2D Parent => GetParentOrNull<Area2D>();
    private CollisionShape2D _collisionShape2D;
    private RectangleShape2D _rectangleShape2D;
    public override void _Ready()
    {
        if(Parent == null)
            throw new Exception("This component must be a child of an Area2D node.");

        _collisionShape2D = Parent.GetNode<CollisionShape2D>("CollisionShape2D");
        _rectangleShape2D = _collisionShape2D.Shape as RectangleShape2D;

        Parent.Connect("body_exited", this, nameof(OnBodyExited));
    }

    private void OnBodyExited(Node2D body)
    {
    }

}
