using Godot;
using System;


public delegate void DamageDelegate(int amount, int health);


public class Health : Node
{
    [Export]
    public int Value {get; private set;}
    [Export]
    public int MaxHealth {get; private set;}


    private DamageDelegate OnDamage;
    private Action<Node> OnNoHealth;

    public void Initialize(
        int? maxHealth = null,
        int? value = null,
        DamageDelegate onDamage = null,
        Action<Node> onNoHealth = null
    )
    {
        Value = value ?? Value;
        MaxHealth = maxHealth ?? MaxHealth;
        OnDamage = onDamage;
        OnNoHealth = onNoHealth;
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
            this.OnNoHealth?.Invoke(this.GetParent());
        }

        this.OnDamage?.Invoke(amount, Value);
    }
}
