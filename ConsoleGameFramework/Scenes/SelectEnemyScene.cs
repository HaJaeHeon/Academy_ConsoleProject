using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework.Scenes;

public class SelectEnemyScene : SceneBase
{
    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(1, "고블린", "고블린을 상대합니다 / DPS 1"),
        new MenuOption(2, "유령", "유령을 상대합니다 / DPS 2"),
        new MenuOption(3, "히드라", "히드라를 상대합니다 / DPS 4"),
        new MenuOption(4
            , "보스", "히드라를 상대합니다 / DPS 16"),
        new MenuOption(9, "시작 화면으로 ", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.SelectEnemy;

    

    public override void Enter(GameContext context)
    {
        context.AddLog("SelectEnemy 화면에 들어왔습니다.");
    }

    public override void Render(GameContext context)
    {
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("SelectEnemy 화면", "상대할 적을 고르세요");

        ConsoleUI.WriteMenu(Menu, "행동 선택");
        ConsoleUI.WriteLog(context.Logs);
    }

    public override void HandleInput(GameContext context)
    {
        int choice = ConsoleUI.ReadMenuChoice(Menu);
        
        switch (choice)
        {
            case 1:
                BattleManager.Instance.currentEnemy = BattleManager.Instance.Goblin;
                GoTo(context, SceneKey.Battle);
                break;
            case 2:
                BattleManager.Instance.currentEnemy = BattleManager.Instance.Ghost;
                GoTo(context, SceneKey.Battle);
                break;
            case 3:
                BattleManager.Instance.currentEnemy = BattleManager.Instance.Hydra;
                GoTo(context, SceneKey.Battle);
                break;
            case 4:
                BattleManager.Instance.currentEnemy = BattleManager.Instance.Boss;
                GoTo(context, SceneKey.Battle);
                break;
            case 9:
                GoTo(context, SceneKey.Title);
                break;

            case 0:
                context.Game.RequestQuit();
                break;
        }
    }
}
