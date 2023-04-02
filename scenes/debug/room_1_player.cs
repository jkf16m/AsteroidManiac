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

        spaceship.Initialize(
            onNoHealth: (parent) => {
                var ship = parent as Spaceship;

                var destroyRemainings = ship.Destroy();

                this.AddChild(destroyRemainings);
            },
            onShoot: (bullet) => AddChild(bullet)
        );
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
