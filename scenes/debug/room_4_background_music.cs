using Godot;
using System;

public class room_4_background_music : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Timer Timer{get; set;}
    private InGameMusic InGameMusic{get; set;}
    public override void _Ready()
    {
        Timer = GetNode<Timer>("Timer");
        InGameMusic = GetNode<InGameMusic>("InGameMusic");

        Timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

    private int level = 1;
    private void OnTimerTimeout(){
        level ++;
        InGameMusic.SetPitchByLevel(level);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
