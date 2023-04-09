using Godot;
using System;

/**
In this class we define what to do when the player presses a key.
**/
public class MovementControlRigidBody2D : Node{
    public RigidBody2D Parent{get; private set;}

    [Export]
    public float Speed = 100;

    [Export]
    public bool CanJump = false;
    [Export]
    public Vector2 JumpForce = new Vector2(0, -100);

    [Export]
    public bool CanMove = true;
    [Export]
    public float Desacceleration = 100;



    [Export]
    public string UpAction = "ui_up";
    [Export]
    public string DownAction = "ui_down";
    [Export]
    public string LeftAction = "ui_left";
    [Export]
    public string RightAction = "ui_right";
    [Export]
    public string JumpAction = "ui_accept";

    public override void _Ready(){
        Parent = GetParentOrNull<RigidBody2D>();

        if(Parent == null){
            throw new Exception("Parent is not a RigidBody2D");
        }

        
    }


    /**
    <summary>
    This method should be called inside the _IntegrateForces method of the parent RigidBody2D.
    </summary>
    **/
    public void MoveRigidBody(RigidBody2D body, Physics2DDirectBodyState state){
        if(!CanMove){
            return;
        }       

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
            desacceleration = -state.LinearVelocity.Normalized() * Desacceleration;
        }

        if(desacceleration.Length() != 0){
            body.AppliedForce = desacceleration;
        }else{
            body.AppliedForce = (direction.Normalized() * Speed);
        }


    }
}