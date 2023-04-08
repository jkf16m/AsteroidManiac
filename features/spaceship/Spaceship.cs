using Godot;
using System;
using System.Collections.Generic;



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
    public float Desacceleration {get; private set;}

   

    public Shooter Shooter{get; private set;}
    public override void _Ready()
    {
        Shooter = GetNode<Shooter>("Shooter");
    }

    


    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {

        Vector2 direction = new Vector2(0,0);
        Vector2 desacceleration = new Vector2(0,0);
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
        if(
            !Input.IsActionPressed("spaceship_right") &&
            !Input.IsActionPressed("spaceship_left") &&
            !Input.IsActionPressed("spaceship_up") &&
            !Input.IsActionPressed("spaceship_down")
        ){
            GD.Print("Applying Desacceleration: " + Desacceleration);
            desacceleration = -state.LinearVelocity.Normalized() * Desacceleration;
        }

        if(desacceleration.Length() != 0){
            AppliedForce = desacceleration;
        }else{
            AppliedForce = (direction.Normalized() * Speed);
        }


        // Limit the speed of the spaceship.
        if (state.LinearVelocity.Length() > MaxSpeed)
        {
            state.LinearVelocity = state.LinearVelocity.Normalized() * MaxSpeed;
        }else if(state.LinearVelocity.Length() < 0.1){
            state.LinearVelocity = new Vector2(0,0);
        }

        RotateTowardsMouse();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionPressed("spaceship_shoot")){           
            Shooter.Shoot(Rotation);
        }
    }


    public void RotateTowardsMouse(){
        LookAt(GetGlobalMousePosition());
    }

    public Node Destroy()
    {
        this.QueueFree();

        return this;
    }
}
