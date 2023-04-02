using Godot;
using System;
using System.Collections.Generic;



public class Spaceship : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public int Speed {get; private set;}

    [Export]
    public PackedScene ExplosionScene {get; private set;}

    public Action<Node> OnNoHealth {get; private set;}
    public Action<Bullet> OnShoot{get; private set;}
    public void Initialize(Action<Node> onNoHealth, Action<Bullet> onShoot = null)
    {
        OnNoHealth = onNoHealth ?? OnNoHealth;
        OnShoot = onShoot ?? OnShoot;
    }


    public override void _Ready()
    {
        var health = GetNode<Health>("Health");
        health.Initialize(
            onDamage: (amount, value) => GD.Print("Damage: " + amount + " Health: " + value),
            onNoHealth: OnNoHealth
        );        



        var area2DBehaviour = GetNode<Area2DBehaviour>("Area2DBehaviour");
        area2DBehaviour.Initialize(new Area2DBehaviourProps{
            behaviours = new Dictionary<string, Behaviour>{
                {"danger", new Behaviour(
                    onEnter: (area) => {
                        var parent = area.GetParent<Node>();
                        GD.Print("Enemy entered");
                        health.Damage(parent.GetNode<Damage>("Damage").Value);
                    }
                )}
            }
        });
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        Vector2 direction = new Vector2(0,0);
        if(Input.IsActionPressed("spaceship_right")){
            direction.x += 1;
        }
        if(Input.IsActionPressed("spaceship_left")){
            direction.x -= 1;
        }
        if(Input.IsActionPressed("spaceship_up")){
            direction.y -= 1;
        }
        if(Input.IsActionPressed("spaceship_down")){
            direction.y += 1;
        }
        if(Input.IsActionPressed("spaceship_shoot")){
            var directionTowardsMouse = GetGlobalMousePosition();

            var directionRadians = Position.AngleToPoint(directionTowardsMouse)+Mathf.Pi;

            var bullet = GetNode<Shooter>("Shooter").Shoot(directionRadians);

            if(bullet != null)
                OnShoot?.Invoke(bullet);
        }

        this.MoveAndCollide(direction.Normalized()*Speed*delta);

        // Rotate the object towards the mouse cursor.
        this.RotateTowardsMouse();
    }

    public float GetDirectionTowardsMouse(){
        var mousePosition = GetGlobalMousePosition();

        return this.Position.AngleTo(mousePosition);
    }

    public void RotateTowardsMouse(){
        LookAt(GetGlobalMousePosition());
    }

    public Node Destroy()
    {
        var explosion = ExplosionScene?.Instance();

        //explosion.Initialize(Position);

        this.QueueFree();

        return explosion;
    }
}
