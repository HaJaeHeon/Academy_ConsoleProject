namespace ConsoleGameFramework.Models;

public class Character
{
    public string Name { get; set; }
    public int MaxHp { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public bool IsAlive => Hp > 0;
    public Character(string name, int maxHp, int attack)
    {
        Name = name;
        MaxHp = maxHp;
        Hp = maxHp;
        Attack = attack;
    }

    public void TakeDamage(int damage)
    {
        Hp = Math.Max(0, Hp - damage);
    }
}
