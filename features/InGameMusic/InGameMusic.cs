using Godot;
using System;

public class InGameMusic : Node
{
    public AudioStreamPlayer AudioStreamPlayer{get; private set;}
    public override void _Ready()
    {
        AudioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public void Play(){
        AudioStreamPlayer.Play();
    }
    public void Stop(){
        AudioStreamPlayer.Stop();
    }

    public void SetPitchByLevel(int level){
        int levelMinus1 = level - 1;
        AudioStreamPlayer.PitchScale = 1 + (levelMinus1 * 0.1f);
    }
}
