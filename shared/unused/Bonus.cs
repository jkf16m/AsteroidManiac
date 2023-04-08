using Godot;
using System;


public struct BonusProps{
    public string BonusType;
    public float? bonusDurationSeconds;
    public Action OnPickUp;
}
public class Bonus : Node, IInitialize<BonusProps>
{
    [Export]
    public string BonusType{get; private set;}

    [Export]
    public float BonusDurationSeconds{get; private set;}
    public Action OnPickUp { get; private set;}


    public void Initialize(BonusProps props)
    {
        BonusType = props.BonusType ?? BonusType;
        OnPickUp = props.OnPickUp;
        BonusDurationSeconds = props.bonusDurationSeconds ?? BonusDurationSeconds;
    }

    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
