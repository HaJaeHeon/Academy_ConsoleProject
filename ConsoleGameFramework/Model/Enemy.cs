using System;

namespace ConsoleGameFramework.Models;

public class Enemy : Character
{
	public string Description;
	public int AttackRate;
	public Enemy(string name, int maxHp,int attack, string descrioption, int attackRate) : base(name, maxHp, attack)
	{
		Description = descrioption;
		AttackRate = attackRate;
	}
}
