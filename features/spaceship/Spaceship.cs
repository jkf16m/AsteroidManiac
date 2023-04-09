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

   

    public Health Health {get; private set;}
    public Shooter Shooter{get; private set;}
    public MovementControlRigidBody2D MovementControlRigidBody2D{get; private set;}
    public override void _Ready()
    {
        Shooter = GetNode<Shooter>("Shooter");
        Health = GetNode<Health>("Health");
        MovementControlRigidBody2D = GetNode<MovementControlRigidBody2D>("MovementControlRigidBody2D");

        Health.NoHealthRemaining += OnNoHealthRemaining;
        Health.Damaged += OnDamaged;
    }

    


    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        MovementControlRigidBody2D.MoveRigidBody(this, state);

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


    public void OnNoHealthRemaining()
    {
        QueueFree();
    }

    public void OnDamaged(DamageArgs damage)
    {
        DebugService.Instance(this).Log("Spaceship was damaged by " + damage.Amount);
        DebugService.Instance(this).Log("Spaceship has " + damage.Health + " health left.");
    }
}
