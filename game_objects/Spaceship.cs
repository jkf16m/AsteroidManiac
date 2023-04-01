using Godot;
using System;

public class Spaceship : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var health = GetNode<Health>("Health");
        health.Initialize(new HealthProps{
            OnDamage = (amount, value) => GD.Print("Damage: " + amount + " Health: " + value),
            OnNoHealth = () => GD.Print("No Health")
        });

        var collisionDamage = GetNode<CollisionDamage>("CollisionDamage");
        collisionDamage.Initialize(new CollisionDamageProps{
            OnCollision = (other) =>{
                GD.Print("Collision with: " + other.Name);
                health.Damage(other.Damage);
            },
            BoundTimer = 0.5f   // 0.5 seconds of invulnerability after a collision
        });
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
