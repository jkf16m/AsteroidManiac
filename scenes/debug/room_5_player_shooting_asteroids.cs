using Godot;
using System;
using System.Collections.Generic;

public class room_5_player_shooting_asteroids : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Spaceship spaceship;
    private IEnumerable<Asteroid> asteroids;
    public override void _Ready()
    {
        spaceship = GetNode<Spaceship>("Spaceship");
        asteroids = GetTree().GetNodesInGroup<Asteroid>("asteroid");

        spaceship.Shooter.Shot += OnShot;
    }

    private void OnShot(Bullet bullet)
    {
        AddChild(bullet);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
