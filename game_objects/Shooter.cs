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


    public bool ShouldShoot{get; private set;}
    public Timer Timer{get; private set;}

    public void Initialize(PackedScene bulletScene = null, Action onShoot = null, float? bulletSpeed = null){
        BulletScene = bulletScene ?? BulletScene;
        BulletSpeed = bulletSpeed ?? BulletSpeed;


    }

    private void OnTimerTimeout(){
        ShouldShoot = true;
    }

    public Bullet Shoot(float direction){
        if(ShouldShoot){
            var bullet = BulletScene.Instance() as Bullet;

            bullet.Initialize(GlobalPosition, direction, BulletSpeed);

            Timer.Start();
            ShouldShoot = false;

            return bullet;
        }else{
            return null;
        }
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
