using Godot;
using System;

public class DebugGUI : Control
{
    private bool _enabled = true;
    [Export]
    public bool Enabled {
        get => _enabled;
        set{
            _enabled = value;
            Visible = value;
        }
    }
    public RichTextLabel RichTextLabel{get; private set;}
    public override void _Ready()
    {
        var panel = GetNode<ColorRect>("Panel");
        RichTextLabel = GetNode<RichTextLabel>("RichTextLabel");
    }

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("debug_gui_toggle")){
            Enabled = !Enabled;
        }
    }

    public void BreakLine(){
        RawLog("\n");
    }

    public void RawLog(string text){
        RichTextLabel.Text += text;
    }

    public void Log(string text){
        RawLog($"{text}");
        BreakLine();
    }

    public void Clear(){
        RichTextLabel.Text = "";
    }

    public void Log(object o){
        Log(o.ToString());
    }

    public void Log(Node node, string text){
        Log($"{node.Name}: {text}");
    }
}
