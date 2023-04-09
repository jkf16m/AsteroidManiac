using Godot;
using System;
using System.Collections.Generic;

public struct AsteroidProps{
    public float? InnerRadius;
    public float? OuterRadius;
    public int? VertexCount;
    public Vector2? Velocity;
    public float? RotationSpeed;
}

public class Asteroid : RigidBody2D, IDangerGroup, IInitialize<AsteroidProps>
{
    [Export]
    public float DangerLevel{get; private set;}
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
    [Export]
    public int Fragmentation{get; private set;}


    public CollisionShape2D CollisionShape2D{get; private set;}
    public Polygon2D Polygon2D{get; private set;}
    public Health Health{get; private set;}
    public Damage Damage{get; private set;}
    public Area2D DamageSenderArea2D{get; private set;}


    public delegate void FragmentatedDelegate(Asteroid destroyed, IEnumerable<Asteroid> fragments);
    public event FragmentatedDelegate Fragmentated;

    public override void _Ready(){
        AddToGroup("asteroid");
        Initialize(
            new AsteroidProps{
                InnerRadius = InnerRadius,
                OuterRadius = OuterRadius,
                VertexCount = VertexCount,
                Velocity = Velocity,
                RotationSpeed = RotationSpeed
            }
        );

    }
    
    public void Initialize( AsteroidProps p )
    {
        Velocity = p.Velocity ?? Velocity;
        RotationSpeed = p.RotationSpeed ?? RotationSpeed;
        InnerRadius = p.InnerRadius ?? InnerRadius;
        OuterRadius = p.OuterRadius ?? OuterRadius;
        VertexCount = p.VertexCount ?? VertexCount;
        Health = GetNode<Health>("Health");
        Damage = GetNode<Damage>("Damage");
        DamageSenderArea2D = GetNode<Area2D>("DamageSenderArea2D");
        Polygon2D = GetNode<Polygon2D>("Polygon2D");
        CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");

        // Initialize the shape of the collision and the polygon
        var points = _GetPolygonPoints();

        CollisionShape2D.Shape = new CircleShape2D{Radius = InnerRadius};

        var damageSenderCollisionShape2D = DamageSenderArea2D.GetNode<CollisionShape2D>("CollisionShape2D");
        damageSenderCollisionShape2D.Shape = new CircleShape2D{Radius = InnerRadius};

        Polygon2D.Polygon = points;


        // Initialize the health
        Health.NoHealthRemaining += OnNoHealthRemaining;
         
        ApplyImpulse(Vector2.Zero, Velocity);
    }

    /**
    <summary>
    Returns a list of fragments of the asteroid, inheriting a fraction of the properties of the original
    </summary>
    */
    private IEnumerable<Asteroid> Fragmentate(){
        List<Asteroid> fragments = new List<Asteroid>();
        for(int i = 0; i < Fragmentation; i++){
            var fragment = (Asteroid)GD.Load<PackedScene>("res://features/asteroid/Asteroid.tscn").Instance();
            fragment.Initialize(
                new AsteroidProps{
                    InnerRadius = InnerRadius / Fragmentation,
                    OuterRadius = OuterRadius / Fragmentation,
                    VertexCount = VertexCount,
                    Velocity = Velocity * Fragmentation,
                    RotationSpeed = RotationSpeed * (Fragmentation / 2)
                }
            );
            fragments.Add(fragment);
        }

        return fragments;
    }
    private void OnNoHealthRemaining(){
        Fragmentated?.Invoke(this, Fragmentate());       

        QueueFree();
    }

    public Vector2[] _GetPolygonPoints(){
        var points = new List<Vector2>();
        for(int i = 0; i < VertexCount; i++){
            var point = Vector2.Right;

            var angle = Mathf.Deg2Rad(360f / VertexCount * i);
            point = point.Rotated(angle);

            var random = new Random();
            var radius = random.Next((int)Math.Round(InnerRadius,0), (int)Math.Round(OuterRadius,0));

            point *= radius;

            points.Add(point);
        }


        return points.ToArray();
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state){
        ApplyTorqueImpulse(RotationSpeed);
    }

    public override void _Process(float delta)
    {
    }

    public override void _PhysicsProcess(float delta)
    {
    }
}
