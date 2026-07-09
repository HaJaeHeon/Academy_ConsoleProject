using System;
using ConsoleGameFramework.Models;
using System.Timers;
using System.Numerics;

namespace ConsoleGameFramework.Core;

public class BattleManager
{
	private static BattleManager instance = null;

	public static BattleManager Instance
	{
		get
		{
			if (instance == null)
				instance = new BattleManager();

			return instance;
		}
	}
	public int attackCount = 0;

	// 플레이어
	public Player Player { get; set; }

	// 적
	public Enemy Goblin { get; private set; }
	public Enemy Ghost { get; private set; }
	public Enemy Hydra { get; private set; }
	public Enemy Boss { get; private set; }

	public Enemy currentEnemy { get; set; }

	// 플레이어와 적을 생성하고, 초기화하는 함수.
	public void StartBattleInit(string name)
	{
		Player = new Player(name, 100, 10, Equipment.None);

		if(currentEnemy == null)
			currentEnemy = new Enemy(EnemyType.None, "???", 100, 30, "DPS 60", 500, 100);
        Goblin = new Enemy(EnemyType.Goblin, "고블린", 40, 1, "DPS 1", 1000, 5);
		Ghost = new Enemy(EnemyType.Ghost, "유령", 20, 2, "DPS 4 ", 500, 15);
		Hydra = new Enemy(EnemyType.Hydra, "히드라", 60, 2, "DPS 8 ", 250, 40);
		Boss = new Enemy(EnemyType.Boss, "보스", 100, 8, "DPS 16 ", 500, 1000);
	}
	public enum BattleOutcome
	{
		Continuing,
		Victory,
		Defeat
	}
	// 플레이어가 적을 공격하는 함수
	//플레이어가 hp를 60 이상 남기고 클리어(승리) 했을 시 업적을 추가함
	public BattleOutcome PlayerAttack()
	{
        currentEnemy.TakeDamage(Player.Attack);
		GameManager manager = GameManager.Instance;
		manager.Context.AddLog($"{Player.Name}(이)가 {currentEnemy.Name}(을)를 공격했습니다. 데미지 : {Player.Attack}");

		if (!currentEnemy.IsAlive)
		{
			if (Player.Hp  > 60)
			{
				GameSettingManager.Instance.UnlockAchievement(currentEnemy);
				//PrintAchievementLog(manager);
            }
            GameSettingManager setting = GameSettingManager.Instance;
            setting.ChangeGold(currentEnemy.Cost);
            manager.Context.AddLog($"Get Gold : {currentEnemy.Cost} / Current Gold : {setting.PrintGold()} Gold");
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
			manager.Context.AddLog("enemyAttak.PlayerDIe");
			return BattleOutcome.Defeat;
		}

		if (currentEnemy.IsAlive)
		{
            return BattleOutcome.Continuing;
		}
		else
		{
			manager.Context.AddLog("EnemyDIe");
			GameSettingManager setting = GameSettingManager.Instance;
			setting.ChangeGold(currentEnemy.Cost);
			manager.Context.AddLog($"Get Gold : {currentEnemy.Cost} / Current Gold : {setting.PrintGold()} Gold");
            return BattleOutcome.Victory;
		}
	}

	//플레이어가 업적 달성했는지 찍어보는 로그
	public void PrintAchievementLog(GameManager manager)
	{
        manager.Context.AddLog($"CurrentEnemy_Achievement : {GameSettingManager.Instance.achievementsGoblin}");
        manager.Context.AddLog($"CurrentEnemy_Achievement : {GameSettingManager.Instance.achievementsGhost}");
        manager.Context.AddLog($"CurrentEnemy_Achievement : {GameSettingManager.Instance.achievementsHydra}");
    }

	public void GetPower()
	{
		Player.Attack++;
	}

	public void GetHealth()
	{
		Player.MaxHp += 10;
		Player.Hp = Player.MaxHp;
	}
}

