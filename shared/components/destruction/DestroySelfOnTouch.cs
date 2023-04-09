using Godot;
using System;

public class DestroySelfOnTouch : Node, IComponent<Area2D>
{
    public Area2D Parent {get; private set;}

    [Export]
    public string[] GroupsThatWillDestroy{get; private set;}
    [Export]
    public NodePath DestroyedNodePath{get; private set;}
    public IDestructible<Node> DestroyedNode{get; private set;}
    public event Action<Node> Destroyed;
    public override void _Ready()
    {
        Parent = GetParentOrNull<Area2D>();

        if(Parent == null)
            throw new Exception("This component must be a child of a Node.");

        DestroyedNode = GetNodeOrNull<IDestructible<Node>>(DestroyedNodePath);

        if(DestroyedNode == null)
            throw new Exception("This component must have a valid DestroyedNodePath and also must implement IDestructible<T>.");

        Parent.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        foreach(var group in GroupsThatWillDestroy){
            var destructorNode = area.GetNodeByTypeOrNull<DestroyOnTouch>();

            if( DestroyedNode != null &&
                destructorNode != null &&
                destructorNode.IsInGroup(group)){
                var remainings = DestroyedNode.Destroy();

                Destroyed?.Invoke(remainings);

                break;
            }
        }
    }

}
