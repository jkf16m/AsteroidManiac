using Godot;
using System;


public struct ShooterProps{
    public PackedScene BulletScene;

    public Action OnShoot;
}
public class Shooter : Node2D
{
    [Export]
    public PackedScene BulletScene{get; private set;}
    
    [Export]
    public float BulletSpeed{get; private set;}

    [Export]
    public float ShootDelaySeconds{get; private set;}
    [Export]
    public float MaxBulletSpeedOnDistance{get; private set;}
    /**
    <summary>
    The angle of the arc of fire.
    </summary>
    **/
    [Export]
    public float ArcOfFireDegrees{get; private set;}


    public event Action<Bullet> Shot;
    public bool ShouldShoot{get; private set;}
    public Timer Timer{get; private set;}


    private void OnTimerTimeout(){
        ShouldShoot = true;
    }

    public void Shoot(float direction){
        if(ShouldShoot){
            var bullet = BulletScene.Instance<Bullet>(); 


            bullet.Initialize(GlobalPosition, CalculateDirectionWithArcOfFire(direction), CalculateBulletSpeed());

            Timer.Start();
            ShouldShoot = false;

            Shot?.Invoke(bullet);
        }
    }

    /**
    <summary>
        Calculates the bullet speed based on how far the mouse is from the spaceship.
        If the mouse is 500px away from the spaceship, the bullet will have 100% of the bullet speed.
        from then on, 250px means 50% of the bullet speed, 125px means 25% of the bullet speed, etc.
    </summary>
    */
    private float CalculateBulletSpeed(){
        var mousePosition = GetViewport().GetMousePosition();
        var distance = GlobalPosition.DistanceTo(mousePosition);

        var bulletSpeed = BulletSpeed;
        var bulletSpeedUnit = 1/MaxBulletSpeedOnDistance;

        if(distance > MaxBulletSpeedOnDistance){
            bulletSpeed = BulletSpeed;
        }else if(distance <= MaxBulletSpeedOnDistance){
            bulletSpeed = (distance * bulletSpeedUnit) * BulletSpeed;
        }

        return bulletSpeed;
    }

    /**
    <summary>
        Calculates the direction of the bullet based on the mouse position.
        The direction is calculated based on the arc of fire.
        The direction will be randomized between the arc of fire.
    </summary>
    **/
    private float CalculateDirectionWithArcOfFire(float direction){

        var arcOfFire = ArcOfFireDegrees;

        var arcOfFireUnit = arcOfFire/2;

        var random = new Random();

        var randomArcOfFire = random.Next((int)arcOfFireUnit * -1, (int)arcOfFireUnit);

        var randomArcOfFireRadians = Mathf.Deg2Rad(randomArcOfFire);

        var finalDirection = direction + randomArcOfFireRadians;

        return finalDirection;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ShouldShoot = true;

        Timer = GetNode<Timer>("Timer");
        Timer.WaitTime = ShootDelaySeconds;

        Timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
