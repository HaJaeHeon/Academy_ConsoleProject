using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework_KR.Scenes
{
    public class ShopScene : SceneBase
    {

        private static readonly List<MenuOption> Menu = new List<MenuOption>
        { 
        new MenuOption(1, "검을 구매합니다","검 - 3연속으로 커맨드를 정확히 입력하면 크리티컬 대미지를 입힙니다."),
        new MenuOption(2, "방패를 구매합니다.", "방패 - 5연속으로 커맨드를 정확히 입력하면  HP를 회복합니다."),
        new MenuOption(3, "공격력의 비약을 구매합니다.", "공격력의 비약 - 플레이어의 기본 공격력을 1 증가시킵니다."),
        new MenuOption(4, "체력의 비약.", "체력의 비약 - 플레이어의 최대 체력을 10 증가시킵니다."),
        new MenuOption(9, "타이틀로", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
        };
        public override SceneKey Key => SceneKey.Shop;

        public override void Enter(GameContext context)
        {
            context.AddLog("Shop 화면에 들어왔습니다.");
        }

        public override void Render(GameContext context)
        {
            ConsoleUI.Clear();
            ConsoleUI.WriteTitle("Shop 화면", "구매할 장비아이템을 고르세요");
            ConsoleUI.WriteSubtitle("장비는 1개씩만 구매 가능하고 비약들은 여러개 구매 가능합니다. \n  비약들은 인벤토리 내에서 사용 가능합니다.");

            ConsoleUI.WriteMenu(Menu, "행동 선택");
            ConsoleUI.WriteLog(context.Logs);
        }

        public override void HandleInput(GameContext context)
        {
            int choice = ConsoleUI.ReadMenuChoice(Menu);
            //업적과 소지 골드를 확인해서 장비를 구매한다.
            //장비는 1개씩만 구매가 가능하고 비약들은 중첩해서 구매가 가능하다
            //비약들은 인벤토리 내로 들어가서 사용하면 수치가 올라간다.
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
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
}
