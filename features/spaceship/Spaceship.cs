using Godot;
using System;
using System.Collections.Generic;


public struct SpaceshipActions{
    public Action<Node> OnNoHealth {get; set;}
    public Action<Bullet> OnShoot{get; set;}
}

/**
<summary>
    Spaceship class
    This is the spaceship that the player controls.

    When it "fires" a bullet, it will call the OnShoot action.
    When it has no health left, it will call the OnNoHealth action.

    Every Action defined will be stored in the struct SpaceshipActions.

    Use the method "Initialize" to set the actions.
    You can also call the components of the spaceship individually, but cannot reassign their values,
    only use their exposed methods.

    For example, if you want to reduce the health of the spaceship, you have to call
    the method "Damage" of the Health component.

    spaceship.Health.Damage(10);
</summary>
*/
public class Spaceship : RigidBody2D
{
    [Export]
    public float MaxSpeed {get; private set;}
    [Export]
    public int Speed {get; private set;}

    [Export]
    public PackedScene ExplosionScene {get; private set;}

    // Exposed actions, these are single defined events that will be called
    // the difference with usual Signals or events, is that you can only assign them once,
    // with the Initialize method.
    // Reinitializations will replace the previous action.
    public SpaceshipActions Actions {get; private set;}



    // COMPONENTS
    // These are initialized in the _Ready method, and are exposed as properties.
    public Health Health {get; private set;}
    public Shooter Shooter{get; private set;}
    public Area2DBehaviour Area2DBehaviour{get; private set;}
    public override void _Ready()
    {
        Health = GetNode<Health>("Health");
        Shooter = GetNode<Shooter>("Shooter");
        Area2DBehaviour = GetNode<Area2DBehaviour>("Area2DBehaviour");
    }

    

    /**
    <summary>
        Initialize the spaceship with the given actions.
        If an action is not defined, it will be set to the default action.
        This initialization method is used in godot because you cannot call the constructor
        of a node when you instantiate it.

        So, instead of calling the constructor, you call the Initialize method.

        In this method, all the components references are already initialized (this method is usually invoked
        after _Ready method)
    </summary>
    */
    public void Initialize(SpaceshipActions actions)
    {
        Actions = actions;

        Health.Initialize(
            onDamage: (amount, value) => GD.Print("Damage: " + amount + " Health: " + value),
            onNoHealth: Actions.OnNoHealth
        );        



        Area2DBehaviour.Initialize(new Area2DBehaviourProps{
            behaviours = new Dictionary<string, Behaviour>{
                {"danger", new Behaviour(
                    onEnter: (area) => {
                        var parent = area.GetParent<Node>();
                        GD.Print("Enemy entered");
                        Health.Damage(parent.GetNode<Damage>("Damage").Value);
                    }
                )}
            }
        });
    }


    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        // Limit the speed of the spaceship.
        if (state.LinearVelocity.Length() > Speed)
        {
            state.LinearVelocity = state.LinearVelocity.Normalized() * Speed;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
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
                Actions.OnShoot?.Invoke(bullet);
        }

        this.ApplyCentralImpulse((direction.Normalized() * Speed)/100);

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
