using System;
using System.ComponentModel;
using ConsoleGameFramework.Core;
using ConsoleGameFramework.Models;

/// <summary>
/// 게임의 전체 설정 : 난이도 설정, 적 생성 개수 설정, 이름 변경 등등 게임의 설정을 바꿀 수 있는 싱글톤 클래스다.
/// </summary>
public class GameSettingManager
{
    private static GameSettingManager instance = null;

    public static GameSettingManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameSettingManager();

            return instance;
        }
    }
    public bool achievementsGoblin { get; private set; } = false;
    public bool achievementsGhost { get; private set; } = false;
    public bool achievementsHydra { get; private set; } = false;

    public int gold { get; private set; } = 0;

    public void UnlockAchievement(Enemy enemy)
    {
        if(enemy.Type == EnemyType.Goblin)
        {
            if (achievementsGoblin)
                return;
            achievementsGoblin = true;
        }
        else if (enemy.Type == EnemyType.Ghost)
        {
            if (achievementsGhost)
                return;
            achievementsGhost = true;
        }
        else if(enemy.Type == EnemyType.Hydra)
        {
            if (achievementsHydra)
                return;
            achievementsHydra = true;
        }

        GameManager.Instance.Context.AddLog($"업적 달성 : HP 60% 이상으로 {enemy.Name} 처치");
    }

    public void ChangeGold(int amount)
    {
        gold += amount;
    }

    public int PrintGold()
    {
        return gold;
    }
}
