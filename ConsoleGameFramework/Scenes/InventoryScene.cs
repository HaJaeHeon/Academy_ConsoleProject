using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework_KR.Scenes
{
    public class InventoryScene : SceneBase
    {
        public static List<int> EquipList = new List<int>();
        private static readonly List<MenuOption> Menu = new List<MenuOption>
        {
        new MenuOption(9, "타이틀로", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
        };
        public override SceneKey Key => SceneKey.Inventory;

        public override void Enter(GameContext context)
        {
            context.AddLog("Inventory 화면에 들어왔습니다.");
        }

        public override void Render(GameContext context)
        {
            ConsoleUI.Clear();
            ConsoleUI.WriteTitle("Inventory 화면", "장비할 아이템들을 선택해주세요");

            ConsoleUI.WriteMenu(Menu, "행동 선택");
            ConsoleUI.WriteLog(context.Logs);
        }

        public override void HandleInput(GameContext context)
        {
            int choice = ConsoleUI.ReadMenuChoice(Menu);

            switch (choice)
            {
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
