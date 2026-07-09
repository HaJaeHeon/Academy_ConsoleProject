using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework_KR.Scenes
{
    public class InventoryScene : SceneBase
    {
        private static readonly List<MenuOption> Menu = new List<MenuOption>
        {
        new MenuOption(1, "인벤토리 확인",""),
        new MenuOption(2, "장비 확인",""),
        new MenuOption(3, "장비 아이템 착용",""),
        new MenuOption(9, "타이틀로", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
        }; 

        private static List<MenuOption> InventoryMenu = new List<MenuOption>
        { 
        new MenuOption(9, "타이틀로", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
        };

        public override SceneKey Key => SceneKey.Inventory;

        InventoryManager iManager = InventoryManager.Instance;

        public override void Enter(GameContext context)
        {
            context.AddLog("Inventory 화면에 들어왔습니다.");
        }

        public override void Render(GameContext context)
        {
            ConsoleUI.Clear();
            ConsoleUI.WriteTitle("Inventory 화면", "장비할 아이템들을 선택해주세요");
            //iManager.PrintInventory();
            ConsoleUI.WriteMenu(Menu, "행동 선택");
            ConsoleUI.WriteLog(context.Logs);
        }

        public void SelectInventoryItem()
        {
            for (int i = 0; i < iManager.InventoryList.Count; i++)
            {
                InventoryMenu.Add(new MenuOption( i+1, $"{iManager.InventoryList[i]}", ""));
            }
        }
        public override void HandleInput(GameContext context)
        {
            int choice = ConsoleUI.ReadMenuChoice(Menu);

            switch (choice)
            {
                case 1:
                    //iManager.PrintInventory();
                    break;
                case 2:
                    iManager.PrintEquip();
                    break;
                case 3:
                    context.Game.ChangeScene(SceneKey.Equip);
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
