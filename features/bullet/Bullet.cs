using Godot;
using System;
using System.Collections.Generic;

public struct BulletProps{
    public Vector2 Position;
    public float Direction;
    public float Speed;
    public int Damage;
    public IEnumerable<string> DamageGroups;
}

/**
<summary>
This is a class that represents a bullet.
When the bullet collides with a solid object, it is self-destroyed.
</summary>
*/
public class Bullet : KinematicBody2D, IInitialize<BulletProps>, IDestructible<Node>
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public float Speed {get; private set;}

    [Export]
    public float Direction {get; private set;}
    [Export]
    public int DamageValue {get; private set;}
    [Export]
    public IEnumerable<string> DamageGroups{get; private set;}
    public Damage Damage{get; private set;}

    public void Initialize(BulletProps p){
        Initialize(p.Position, p.Direction, p.Speed, p.Damage);
        DamageGroups = p.DamageGroups;
    }
    public void Initialize(Vector2? position = null, float? direction = null, float? speed = null, int? damage = null){
        Position = position ?? Position;
        Direction = direction ?? Direction;
        Speed = speed ?? Speed;

        DamageValue = damage ?? DamageValue;
        Damage = GetNode<Damage>("Damage");
        Damage.Initialize(DamageValue);

        Rotation = Direction;
    }


    public override void _Ready()
    {
        Rotation = Direction;
    }
    public override void _PhysicsProcess(float delta)
    {
        var directionToMoveTowards = Vector2.Right.Rotated(Direction);
        var collisionResult = MoveAndCollide(directionToMoveTowards * Speed * delta);

        var colliderNode = collisionResult?.Collider as Node;

        if(colliderNode == null){
            return;
        }
    }

    public Node Destroy()
    {
        QueueFree();
        return null;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
