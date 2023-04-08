using Godot;
using System;

public class Damage : Node, IInitialize<int>
{
    public void Initialize(int value){
        Value = value;
    }
    [Export]
    public int Value {get; private set;}
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
