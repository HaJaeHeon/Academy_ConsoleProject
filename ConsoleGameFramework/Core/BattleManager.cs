using ConsoleGameFramework.Models;

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

	InventoryManager iManager = InventoryManager.Instance;
	public  Action OnSwordEffect;
	public  Action OnShieldEffect;
	private int attackCount;
	public int AttackCount
	{
		get => attackCount;
		set
		{
			attackCount = value;

            if (attackCount > 0 && attackCount % 3 == 0)
            {
				if (iManager.VerifyEquip(1))
				{
					OnSwordEffect?.Invoke();
				}
				//GameManager.Instance.Context.AddLog("검없음");
            }
			if (attackCount > 0 && attackCount % 5 == 0)
			{
				if (iManager.VerifyEquip(2))
				{
					OnShieldEffect?.Invoke();
				}
				//GameManager.Instance.Context.AddLog("방패없음");
			}
        }
	}
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
		Player = new Player(name, 100, 10);

		if(currentEnemy == null)
			currentEnemy = new Enemy(EnemyType.None, "???", 100, 30, "HP : 100 / DPS 60", 500, 100);
        Goblin = new Enemy(EnemyType.Goblin, "고블린", 60, 2, "HP : 60 / DPS 1", 2000, 5);
		Ghost = new Enemy(EnemyType.Ghost, "유령", 50, 3, "HP : 50 / DPS 3 ", 1000, 15);
		Hydra = new Enemy(EnemyType.Hydra, "히드라", 100, 6, "HP: 100 / DPS 6 ", 1000, 40);
		Boss = new Enemy(EnemyType.Boss, "보스", 300, 10, "HP : 1000 / DPS 20 ", 500, 1000);
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
		GameManager manager = GameManager.Instance;
		manager.Context.AddLog($"{AttackCount}");
		
        currentEnemy.TakeDamage(Player.Attack);
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
			return BattleOutcome.Continuing;
        /*
		if (Player.IsAlive)
		
		else
			return BattleOutcome.Defeat;
		*/
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
		else
			return BattleOutcome.Continuing;
		
		/*
		if (currentEnemy.IsAlive)
		{
		}
		else
		{
			manager.Context.AddLog("EnemyDIe");
			GameSettingManager setting = GameSettingManager.Instance;
			setting.ChangeGold(currentEnemy.Cost);
			manager.Context.AddLog($"Get Gold : {currentEnemy.Cost} / Current Gold : {setting.PrintGold()} Gold");
            return BattleOutcome.Victory;
		}
		*/
	}

	//플레이어가 업적 달성했는지 찍어보는 로그
	public void PrintAchievementLog(GameManager manager)
	{
        manager.Context.AddLog($"CurrentEnemy_Achievement : {GameSettingManager.Instance.achievementsGoblin}");
        manager.Context.AddLog($"CurrentEnemy_Achievement : {GameSettingManager.Instance.achievementsGhost}");
        manager.Context.AddLog($"CurrentEnemy_Achievement : {GameSettingManager.Instance.achievementsHydra}");
    }

	//공격력의 비약 먹었을 때
	public void GetPower()
	{
		Player.Attack += 3;
	}
	//체력의 비약 먹었을 때
	public void GetHealth()
	{
		Player.MaxHp += 20;
		Player.Hp = Player.MaxHp;
	}
	public bool CheckBoss()
	{
		if (currentEnemy.Type == EnemyType.Boss)
		{ return true; }
		return false;
	}
}

