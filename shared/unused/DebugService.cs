using System;
using Godot;

public class DebugService : Node
{
    [Export]
    public bool IsDebug{get; private set;}


    public void Log(params object[] what){
        if(IsDebug){
            GD.Print(what);
        }
    }

    public void LogError(params object[] what){
        if(IsDebug){
            GD.PrintErr(what);
        }
    }

    public void LogWarning(string what){
        if(IsDebug){
            GD.PushWarning(what);
        }
    }
}