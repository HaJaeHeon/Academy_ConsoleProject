using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework.Scenes;

public class UsageScene : SceneBase
{
    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(1, "공격력의 비약 사용", "공격력의 비약 - 플레이어의 기본 공격력을 1 증가시킵니다."),
        new MenuOption(2, "체력의 비약 사용", "체력의 비약 - 플레이어의 최대 체력을 10 증가시킵니다."),
        new MenuOption(9, "뒤로(인벤토리)", "인벤토리 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.Usage;

    public override void Enter(GameContext context)
    {
        context.AddLog("UsageScene 화면에 들어왔습니다.");
    }

    public override void Render(GameContext context)
    {
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("UsageScene 화면", "사용할 아이템을 선택하세요");

        ConsoleUI.WriteMenu(Menu, "행동 선택");
        ConsoleUI.WriteLog(context.Logs);
    }

    public override void HandleInput(GameContext context)
    {
        int posionNum = 2;
        int choice = ConsoleUI.ReadMenuChoice(Menu);
        InventoryManager imanager = InventoryManager.Instance;
        switch (choice)
        {
            case 1:
                if (imanager.VerifyItem(choice + posionNum))
                {
                    imanager.Use(choice + posionNum);
                    GameManager.Instance.Context.AddLog("공격력의 비약을 사용했습니다.");
                }
                else
                {
                    context.AddLog($"공격력의 비약이 인벤토리에 없습니다.");
                }
                break;
            case 2:
                if (imanager.VerifyItem(choice + posionNum))
                {
                    imanager.Use(choice + posionNum);
                    GameManager.Instance.Context.AddLog("체력의 비약을 사용했습니다.");
                }
                else
                {
                    context.AddLog($"체력의 비약이 인벤토리에 없습니다.");
                }
                break;
            case 9:
                GoTo(context, SceneKey.Inventory);
                break;
            case 0:
                context.Game.RequestQuit();
                break;
        }
    }
}
