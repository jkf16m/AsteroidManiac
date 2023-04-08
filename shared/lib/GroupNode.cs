using Godot;
using System;



/**
<summary>
This class is used to handle a group node.
A group node instance saves a reference to a certain node, and a reference to the properties expected
to be found on that node.

For example, if you want to create a GroupNode of "danger", and give it as second type parameter
an interface called "IDanger", then you can expect that the node will have all the properties defined
in the interface.
</summary>

<example>
// This is an example of how to use this class
using Godot;
using System;

var node = new Node();
var groupNode = new GroupNode<Node, IDanger>(node, "danger");
var damage = groupNode.Damage;
*/
public class GroupNode<T, P> where T : Node, P{
    public T Node{get; private set;}
    public GroupNode(T node, string groupName){
        this.Node = node;

        if(!this.Node.IsInGroup(groupName)){
            new Exception($"The node {this.Node.Name} is not in the group {groupName}");
        }
    }
}