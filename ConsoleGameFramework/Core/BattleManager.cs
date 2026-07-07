using System;
using ConsoleGameFramework.Models;
using System.Timers;
using System.Numerics;

namespace ConsoleGameFramework.Core;

public class BattleManager
{
	private static BattleManager? instance = null;

	public static BattleManager Instance
	{
		get
		{
			if (instance == null)
				instance = new BattleManager();

			return instance;
		}
	}

	// 플레이어
	public Player Player { get; private set; }

	// 적
	public Enemy Enemy { get; private set; }

	// 플레이어와 적을 생성하고, 초기화하는 함수.
	public void StartBattleInit(string name)
	{
		Player = new Player(name, 100, 20);
		Enemy = new Enemy("고블린", 40, 1, "dps 1 / 초당 1번 공격", 1000);
	}
	public enum BattleOutcome
	{
		Continuing,
		Victory,
		Defeat
	}
	// 플레이어가 적을 공격하는 함수
	public BattleOutcome PlayerAttack()
	{
		Enemy.TakeDamage(Player.Attack);
		GameManager manager = GameManager.Instance;
		manager.Context.AddLog($"{Player.Name}(이)가 {Enemy.Name}(을)를 공격했습니다. 데미지 : {Player.Attack}");

		if (!Enemy.IsAlive)
		{
			return BattleOutcome.Victory;
		}

		if (Player.IsAlive)
			return BattleOutcome.Continuing;
		else
			return BattleOutcome.Defeat;
	}

	//적이 플레이어를 때리는 함수
	public BattleOutcome EnemyAttack()
	{
		Player.TakeDamage(Enemy.Attack);
		GameManager manager = GameManager.Instance;
		manager.Context.AddLog($"{Enemy.Name}(이)가 {Player.Name}(을)를 공격했습니다 데미지 : {Enemy.Attack}");

		if (!Player.IsAlive)
		{
            return BattleOutcome.Defeat;
		}

		if (Enemy.IsAlive)
		{
            return BattleOutcome.Continuing;
		}
		else
		{
            return BattleOutcome.Victory;
		}
	}
}

