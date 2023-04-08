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


    public Group<IDangerGroup> DangerGroup{get; private set;}
    public Group<IBonusGroup> BonusGroup{get; private set;}

    public GroupService(){
        this.DangerGroup = new Group<IDangerGroup>("danger");
        this.BonusGroup = new Group<IBonusGroup>("bonus");
    }
}