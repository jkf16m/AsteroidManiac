using Godot;
using System;

public class room_1_player : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var spaceship = GetNode<Spaceship>("Spaceship");

        spaceship.Health.NoHealthRemaining += OnNoHealthRemaining;


        spaceship.Shot += OnShot;
    }

    void OnShot(Bullet bullet){
        AddChild(bullet);
    }

    void OnNoHealthRemaining(Node sender){
        var ship = sender.GetParent() as Spaceship;

        var destroyRemainings = ship.Destroy();

        this.AddChild(destroyRemainings);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
