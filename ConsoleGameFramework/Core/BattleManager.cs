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
	public Enemy Goblin { get; private set; }
	public Enemy Ghost { get; private set; }
	public Enemy Hydra { get; private set; }

	public Enemy currentEnemy { get; set; }

	// 플레이어와 적을 생성하고, 초기화하는 함수.
	public void StartBattleInit(string name)
	{
		Player = new Player(name, 100, 10);
		if(currentEnemy == null)
			currentEnemy = new Enemy(EnemyType.None, "???", 100, 1, "DPS 10", 100);
        Goblin = new Enemy(EnemyType.Goblin, "고블린", 40, 1, "DPS 1", 1000);
		Ghost = new Enemy(EnemyType.Ghost, "유령", 20, 2, "DPS 2 ", 500);
		Hydra = new Enemy(EnemyType.Hydra, "히드라", 60, 2, "DPS 4 ", 250);
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
        currentEnemy.TakeDamage(Player.Attack);
		GameManager manager = GameManager.Instance;
		manager.Context.AddLog($"{Player.Name}(이)가 {currentEnemy.Name}(을)를 공격했습니다. 데미지 : {Player.Attack}");

		if (!currentEnemy.IsAlive)
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
		Player.TakeDamage(currentEnemy.Attack);
		GameManager manager = GameManager.Instance;
		manager.Context.AddLog($"{currentEnemy.Name}(이)가 {Player.Name}(을)를 공격했습니다 데미지 : {currentEnemy.Attack}");

		if (!Player.IsAlive)
		{
            return BattleOutcome.Defeat;
		}

		if (currentEnemy.IsAlive)
		{
            return BattleOutcome.Continuing;
		}
		else
		{
            return BattleOutcome.Victory;
		}
	}
}

