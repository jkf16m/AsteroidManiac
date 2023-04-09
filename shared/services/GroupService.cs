using System;
using System.Collections.Generic;
using Godot;
/**
<summary>
Service singleton used to handle the groups of the game.
</summary>
*/
public class GroupService{
    private static GroupService _instance;
    public static GroupService Instance{
        get{
            if(_instance == null){
                _instance = new GroupService();
            }
            return _instance;
        }
    }


    public Group<Asteroid> AsteroidGroup{get; private set;}
    public Group<Bullet> BulletGroup{get; private set;}
    public Group<Bullet> PlayerBulletGroup{get; private set;}
    public Group<Bullet> EnemyBulletGroup{get; private set;}
    public Group<Node> BonusGroup{get; private set;}

    public GroupService(){
        this.AsteroidGroup = new Group<Asteroid>("asteroid");
        this.BulletGroup = new Group<Bullet>("bullet");
        this.PlayerBulletGroup = new Group<Bullet>("player_bullet");
        this.EnemyBulletGroup = new Group<Bullet>("enemy_bullet");
    }
}