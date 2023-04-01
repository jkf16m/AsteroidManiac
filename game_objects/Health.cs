using Godot;
using System;


public delegate void DamageDelegate(int amount, int health);
public struct HealthProps{
    public int value;
    public int maxHealth;

    public DamageDelegate OnDamage;
    public Action OnNoHealth;
}

public class Health : Node, IInitialize<HealthProps>
{
    [Export]
    public int Value {get; private set;}
    [Export]
    public int MaxHealth {get; private set;}


    private DamageDelegate OnDamage;
    private Action OnNoHealth;

    public void Initialize(HealthProps props)
    {
        Value = props.value ?? Value;
        MaxHealth = props.maxHealth ?? MaxHealth;
        OnDamage = props.OnDamage;
        OnNoHealth = props.OnNoHealth;
    }

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void Damage(int amount){
        Value -= amount;
        if(Value <= 0){
            this.OnNoHealth?.Invoke();
        }

        this.OnDamage?.Invoke(amount, Value);
    }
}
