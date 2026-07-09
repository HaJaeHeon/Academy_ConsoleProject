using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework.Scenes;

public class EquipScene : SceneBase
{
    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(1, "검 착용", "3연속으로 커맨드를 정확히 입력하면 크리티컬 대미지를 입힙니다."),
        new MenuOption(2, "방패 착용", "5연속으로 커맨드를 정확히 입력하면  HP를 회복합니다."),
        new MenuOption(9, "타이틀로", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.Equip;

    public override void Enter(GameContext context)
    {
        context.AddLog("EquipScene 화면에 들어왔습니다.");
    }

    public override void Render(GameContext context)
    {
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("EquipScene 화면", "착용할 장비아이템을 선택하세요");

        ConsoleUI.WriteMenu(Menu, "행동 선택");
        ConsoleUI.WriteLog(context.Logs);
    }

    public override void HandleInput(GameContext context)
    {
        int choice = ConsoleUI.ReadMenuChoice(Menu);
        InventoryManager imanager = InventoryManager.Instance;
        switch(choice)
        {
            case 1:
                if(imanager.VerifyItem(choice))
                {
                    imanager.Equip(choice);
                }
                else
                {
                    context.AddLog($"검이 인벤토리에 없습니다.");
                }
                break;
            case 2:
                if (imanager.VerifyItem(choice))
                {
                    imanager.Equip(choice);
                }
                else
                {
                    context.AddLog($"방패가 인벤토리에 없습니다.");
                }
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
