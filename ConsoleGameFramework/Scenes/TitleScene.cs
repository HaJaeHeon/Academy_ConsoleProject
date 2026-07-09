using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework.Scenes;

public class TitleScene : SceneBase
{
    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(1, "시작", "게임을 시작합니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.Title;

    string name = "";

    public override void Render(GameContext context)
    {
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("CONSOLE GAME FRAMEWORK", "C# 콘솔앱 프로젝트 프레임워크");

        ConsoleUI.WriteBox(new[]
        {
            " "
        }, "프로젝트 안내", ConsoleColor.DarkCyan);


        ConsoleUI.WriteMenu(Menu, "행동 선택");
    }

    public override void HandleInput(GameContext context)
    {
        int choice = ConsoleUI.ReadMenuChoice(Menu);
        switch (choice)
        {
            case 1:
                name = ConsoleUI.ReadString("플레이어 이름을 입력하세요");
                BattleManager.Instance.StartBattleInit(name);
                GoTo(context, SceneKey.Start);
                break;

            case 0:
                context.Game.RequestQuit();
                break;
        }
    }
}
