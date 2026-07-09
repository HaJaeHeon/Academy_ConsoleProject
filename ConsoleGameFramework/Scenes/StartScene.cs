using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework.Scenes;

public class StartScene : SceneBase
{
    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(3, "인벤토리 화면으로 이동", "현재 인벤토리 내의 아이템 목록들을 출력하고 아이템을 선택해서 장비합니다."),
        new MenuOption(2, "상점 화면으로 이동", "상점을 방문해서 여러 장비아이템을 구매합니다."),
        new MenuOption(1, "적 선택 화면으로 이동", "적을 선택해서 전투를 시작합니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.Start;

    public override void Render(GameContext context)
    {
        ConsoleUI.Clear();

        ConsoleUI.WriteMenu(Menu, "시작 메뉴");
        ConsoleUI.WriteLog(context.Logs);
    }

    public override void HandleInput(GameContext context)
    {
        int choice = ConsoleUI.ReadMenuChoice(Menu);
        switch (choice)
        {
            case 3:
                GoTo(context, SceneKey.Inventory);
                break;
            case 2:
                GoTo(context, SceneKey.Shop);
                break;
            case 1:
                GoTo(context, SceneKey.SelectEnemy);
                break;
            case 0:
                context.Game.RequestQuit();
                break;
        }
    }
}
