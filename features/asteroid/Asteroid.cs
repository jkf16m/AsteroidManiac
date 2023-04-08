using Godot;
using System;
using System.Collections.Generic;

public class Asteroid : KinematicBody2D
{
    [Export]
    public float InnerRadius{get; private set;}
    [Export]
    public float OuterRadius{get; private set;}
    [Export]
    public int VertexCount{get; private set;}
    [Export]
    public Vector2 Velocity{get; private set;}
    [Export]
    public float RotationSpeed{get; private set;}


    public CollisionShape2D CollisionShape2D{get; private set;}
    public Polygon2D Polygon2D{get; private set;}
    public Health Health{get; private set;}
    public KinematicBody2DBehaviour KinematicBody2DBehaviour{get; private set;}

    public override void _Ready(){
        Initialize(
            innerRadius: InnerRadius,
            outerRadius: OuterRadius,
            vertexCount: VertexCount,
            velocity: Velocity,
            collisionShape2D: GetNode<CollisionShape2D>("CollisionShape2D"),
            polygon2D: GetNode<Polygon2D>("Polygon2D"),
            health: GetNode<Health>("Health")
        );
    }
    
    public void Initialize(
        float innerRadius,
        float outerRadius,
        int vertexCount,
        Vector2? velocity = null,
        float? rotationSpeed = null,
        CollisionShape2D collisionShape2D = null,
        Polygon2D polygon2D = null,
        Health health = null
        )
    {
        CollisionShape2D = collisionShape2D ?? CollisionShape2D;
        Polygon2D = polygon2D ?? Polygon2D;
        Velocity = velocity ?? Velocity;
        RotationSpeed = rotationSpeed ?? RotationSpeed;
        InnerRadius = innerRadius;
        OuterRadius = outerRadius;
        VertexCount = vertexCount;
        Health = health ?? Health;
        KinematicBody2DBehaviour = GetNode<KinematicBody2DBehaviour>("KinematicBody2DBehaviour");


        // Initialize the shape of the collision and the polygon
        var points = _GetPolygonPoints();
        CollisionShape2D.Shape = new CircleShape2D{Radius = InnerRadius};
        Polygon2D.Polygon = points;


        // Initialize the health
        Health.NoHealthRemaining += OnNoHealthRemaining;
         

        // Initialize the collision behaviour
        KinematicBody2DBehaviour.Initialize(
            collisionBehaviours: new Dictionary<string, CollisionBehaviour>(){
                {
                    "bullet",
                    new CollisionBehaviour(
                        onCollision: (node) =>{
                            var damage = node.GetNode<Damage>("Damage");
                            Health.Damage(damage.Value);
                        }
                    )
                }
            }
        );
    }
    private void OnNoHealthRemaining(Node node){
        QueueFree();
    }

    public Vector2[] _GetPolygonPoints(){
        var points = new List<Vector2>();
        for(int i = 0; i < VertexCount; i++){
            var point = Vector2.Right;

            var angle = Mathf.Deg2Rad(360f / VertexCount * i);
            point = point.Rotated(angle);

            var radius = MathFunctions.RandomRange(InnerRadius, OuterRadius);

            point *= radius;

            points.Add(point);
        }


        return points.ToArray();
    }

    public override void _PhysicsProcess(float delta)
    {
        Rotate(RotationSpeed * delta);

        var collisionResult = MoveAndCollide(Velocity * delta);
        if(collisionResult != null)
            KinematicBody2DBehaviour?.Collides("bullet", collisionResult.Collider as Node);
    }
}
