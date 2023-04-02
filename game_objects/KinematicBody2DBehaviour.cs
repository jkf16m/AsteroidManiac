using Godot;
using System;
using System.Collections.Generic;


public class CollisionBehaviour{
    public Action<Node> OnCollision{get; private set;}
    public CollisionBehaviour(
        Action<Node> onCollision
    ){
        OnCollision = onCollision;
    }
}
public class KinematicBody2DBehaviour : Node
{
    [Export]
    public NodePath KinematicBody2DPath{get; private set;}
    public KinematicBody2D KinematicBody2D {get; private set;}
    public Dictionary<string, CollisionBehaviour> CollisionBehaviours {get; private set;}
    public override void _Ready()
    {
        Initialize(
            kinematicBody2D: GetNode<KinematicBody2D>(KinematicBody2DPath),
            collisionBehaviours: new Dictionary<string, CollisionBehaviour>()
        );
    }

    public void Initialize(
        KinematicBody2D kinematicBody2D = null,
        Dictionary<string,CollisionBehaviour> collisionBehaviours = null
    ){
        KinematicBody2D = kinematicBody2D ?? KinematicBody2D;
        CollisionBehaviours = collisionBehaviours ?? CollisionBehaviours;
    }

    public void Collides(string group, Node node){
        if(node.GetGroups().Contains(group)){
            CollisionBehaviours[group].OnCollision?.Invoke(node);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
