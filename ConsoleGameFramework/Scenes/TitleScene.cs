using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework.Scenes;

/// <summary>
/// 프로그램의 첫 화면입니다.
/// 이 프레임워크는 게임 내용이 빠져 있으므로, 여기서는 화면 전환 구조만 보여줍니다.
/// 실습에서는 이 화면을 여러분의 게임에 맞는 타이틀 화면으로 바꿔서 사용하면 됩니다.
/// </summary>
public class TitleScene : SceneBase
{
    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(4, "인벤토리 화면으로 이동", "현재 인벤토리 내의 아이템 목록들을 출력하고 아이템을 선택해서 장비합니다."),
        new MenuOption(3, "상점 화면으로 이동", "상점을 방문해서 여러 장비아이템을 구매합니다."),
        new MenuOption(2, "적 선택 화면으로 이동", "적을 선택해서 전투를 시작합니다."),
        //new MenuOption(3, "지도 화면으로 이동", "플레이어가 월드맵으로 이동합니다."),
        //new MenuOption(2, "전투 화면으로 이동", "플레이어와 적의 전투가 시작됩니다."),
        new MenuOption(1, "샘플 화면으로 이동", "ConsoleUI의 다른 기능들을 보여주는 화면으로 이동합니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.Title;

    public override void Render(GameContext context)
    {
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("CONSOLE GAME FRAMEWORK", "C# 콘솔앱 프로젝트 프레임워크");

        ConsoleUI.WriteBox(new[]
        {
            " "
        }, "프로젝트 안내", ConsoleColor.DarkCyan);

        ConsoleUI.WriteMenu(Menu, "시작 메뉴");
        ConsoleUI.WriteLog(context.Logs);
    }

    public override void HandleInput(GameContext context)
    {
        int choice = ConsoleUI.ReadMenuChoice(Menu);
        string name = "";
        switch (choice)
        {
            case 4:
                GoTo(context, SceneKey.Inventory);
                break;
            case 3:
                GoTo(context, SceneKey.Shop);
                break;
            case 2:
                name = ConsoleUI.ReadString("플레이어 이름을 입력하세요");
                BattleManager.Instance.StartBattleInit(name);
                GoTo(context, SceneKey.SelectEnemy);
                break;
            //case 3:
            //    GoTo(context, SceneKey.Map);
            //    break;
            //case 2:
            //    name = ConsoleUI.ReadString("이름을 입력하세요");
            //    BattleManager.Instance.StartBattleInit(name);
            //    GoTo(context, SceneKey.Battle);
            //    break;

            case 1:
                GoTo(context, SceneKey.Sample);
                break;

            case 0:
                context.Game.RequestQuit();
                break;
        }
    }
}
