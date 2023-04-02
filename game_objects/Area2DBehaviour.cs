using Godot;
using System;
using System.Collections.Generic;

public class Behaviour{
    public Action<Area2D> OnEnter {get; private set;}
    public Action<Area2D> OnExit {get; private set;}

    public Behaviour(Action<Area2D> onEnter = null, Action<Area2D> onExit = null){
        OnEnter = onEnter;
        OnExit = onExit;
    }
}


public struct Area2DBehaviourProps{
    public Dictionary<string, Behaviour> behaviours;
    public NodePath area2DPath;
}

/**
<summary>
This class is used to register what to do for each Area2D or any kind of object collisioning
with the Area2D reference.

Registered behaviours are called when the collisioned Area2D has the registered group name
specified in the Initialization
*/
public class Area2DBehaviour : Node, IInitialize<Area2DBehaviourProps>
{

    [Export]
    public NodePath Area2DPath {get; private set;}
    public Area2D Area2D {get; private set;}

    public Dictionary<string, Behaviour> Behaviours {get; private set;}
    public void Initialize(Area2DBehaviourProps props)
    {
        Behaviours = props.behaviours ?? Behaviours;
        Area2DPath = props.area2DPath ?? Area2DPath;
    }

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Area2D = GetNode<Area2D>(Area2DPath);

        Area2D.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area){
        foreach(var behaviour in Behaviours){
            if(area.GetGroups().Contains(behaviour.Key)){
                behaviour.Value.OnEnter?.Invoke(area);
                behaviour.Value.OnExit?.Invoke(area);
            }
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
