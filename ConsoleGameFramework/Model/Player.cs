using System;

namespace ConsoleGameFramework.Models;

public enum Equipment
{
	None,
	Sword,
	Sheild
}

public class Player : Character
{
	Equipment equipment;
	public Player(string name, int maxHp, int attack, Equipment equip) : base(name, maxHp, attack)
	{
		equipment = equip;
	}
}
