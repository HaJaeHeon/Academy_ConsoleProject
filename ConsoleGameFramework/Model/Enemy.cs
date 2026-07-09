using System;

namespace ConsoleGameFramework.Models;

public enum EnemyType
{
	Goblin,
	Ghost,
	Hydra,
	None
}

public class Enemy : Character
{
	public EnemyType Type;
	public string Description;
	public int AttackRate;
	public int Cost;
	public Enemy(EnemyType type, string name, int maxHp,int attack, string descrioption, int attackRate, int cost) : base(name, maxHp, attack)
	{
		Type = type;
		Description = descrioption;
		AttackRate = attackRate;
		Cost = cost;
	}
}
