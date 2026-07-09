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
        DrawTitleScreen();
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
    public void DrawTitleScreen()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        string titleArt = @"
+----------------------------------------------------------+
|                                                          |
|  _  __          __        __               _             |
| | |/ /___ _   _ \ \      / /_ _ _ __ _ __ (_) ___  _ __  |
| | ' // _ \ | | | \ \ /\ / / _` | '__| '__|| |/ _ \| '__| |
| | . \  __/ |_| |  \ V  V / (_| | |  | |   | | (_) | |    |
| |_|\_\___|\__, |   \_/\_/ \__,_|_|  |_|   |_|\___/|_|    |
|           |___/                                          |
|                                                          |
|                                                          |
|                  />___________________                   |
| [###############[]___________________/                   |
|                  \>                                      |
|                                                          |
|                                                          |
|               [ PRESS '1' KEY TO START ]                 |
|                                                          |
+----------------------------------------------------------+";

        Console.WriteLine(titleArt);

        Console.ResetColor();
    }
}
