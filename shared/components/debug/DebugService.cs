using System;
using System.Collections.Generic;
using Godot;

public static class DebugService{
    private static DebugGUI debugGUI{get; set;}

    public static DebugGUI Instance(Node node){
        if(debugGUI == null)
            debugGUI = node.GetTree().Root.GetChild(0).GetNodeByType<Control,DebugGUI>();

        return debugGUI;

    }


}