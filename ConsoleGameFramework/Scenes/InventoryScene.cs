using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;

namespace ConsoleGameFramework_KR.Scenes
{
    public class InventoryScene : SceneBase
    {
        //Render에 List 표시하기위한 action
        public Action InsertRender;

        private static readonly List<MenuOption> Menu = new List<MenuOption>
        {
        new MenuOption(1, "인벤토리 확인","현재 화면에서 인벤토리 목록을 불러옵니다."),
        new MenuOption(2, "장비 확인","현재 화면에서 장비 목록을 불러옵니다."),
        new MenuOption(3, "장비 아이템 착용","장비아이템을 착용할 수 있는 화면으로 전환합니다."),
        new MenuOption(4, "소비 아이템 사용","소비아이템 사용할 수 있는 화면으로 전환합니다."),
        new MenuOption(9, "시작 화면으로", "첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
        }; 

        public override SceneKey Key => SceneKey.Inventory;

        InventoryManager iManager = InventoryManager.Instance;

        public override void Enter(GameContext context)
        {
            context.AddLog("Inventory 화면에 들어왔습니다.");
            InsertRender = null;
        }

        public override void Render(GameContext context)
        {
            ConsoleUI.Clear();
            ConsoleUI.WriteTitle("Inventory 화면", "장비할 아이템들을 선택해주세요");
            InsertRender?.Invoke();
            ConsoleUI.WriteMenu(Menu, "행동 선택");
        }

        public override void HandleInput(GameContext context)
        {
            int choice = ConsoleUI.ReadMenuChoice(Menu);

            switch (choice)
            {
                case 1:
                    InsertRender = () => iManager.PrintInventory();
                    break;
                case 2:
                    InsertRender = () => iManager.PrintEquip();
                    break;
                case 3:
                    context.Game.ChangeScene(SceneKey.Equip);
                    break;
                case 4:
                    context.Game.ChangeScene(SceneKey.Usage);
                    break;
                case 9:
                    GoTo(context, SceneKey.Start);
                    break;
                case 0:
                    context.Game.RequestQuit();
                    break;
            }
        }
    }
}
