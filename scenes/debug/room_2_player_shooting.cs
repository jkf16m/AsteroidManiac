using Godot;
using System;

public class room_2_player_shooting : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Spaceship Player {get; private set;}
    public override void _Ready()
    {
        Player = GetNode<Spaceship>("Spaceship");

        Player.Shooter.Shot += OnShot;
    }

    private void OnShot(Bullet bullet){
        AddChild(bullet);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
