using Godot;
using System;


public delegate void DamageDelegate(int amount, int health);

public class DamageArgs{
    public int Amount {get; private set;}
    public int Health {get; private set;}

    public DamageArgs(int amount, int health){
        this.Amount = amount;
        this.Health = health;
    }
}

public class Health : Node
{
    [Export]
    public int Value {get; private set;}
    [Export]
    public int MaxHealth {get; private set;}


    public event EventHandler<DamageArgs> OnDamage;
    public event Action<Node> OnNoHealth;

    public void Initialize(
        int? maxHealth = null,
        int? value = null
    )
    {
        Value = value ?? Value;
        MaxHealth = maxHealth ?? MaxHealth;
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
            this.OnNoHealth?.Invoke(this);
        }

        this.OnDamage?.Invoke(this, new DamageArgs(amount, Value));
    }
}
