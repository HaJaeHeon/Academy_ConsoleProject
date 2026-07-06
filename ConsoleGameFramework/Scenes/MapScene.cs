using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameFramework_KR.Scenes
{
    internal class MapScene : SceneBase
    {
        private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(8, "↑ 위쪽으로 한 칸 이동합니다."),
        new MenuOption(4, "← 왼쪽으로 한 칸 이동합니다."),
        new MenuOption(5, "↓ 아래쪽으로 한 칸 이동합니다."),
        new MenuOption(6, "→ 오른쪽으로 한 칸 이동합니다."),
        new MenuOption(0, "게임을 종료합니다.")
    };

        public static string[] MapData =
        {
             "##########" ,
             "#P.#...#.#" ,
             "#....##..#" ,
             "#........#" ,
             "#........#" ,
             "#.....####" ,
             "#........#" ,
             "#..#...E.#" ,
             "#..#.....#" ,
             "##########" 
        };

        public override SceneKey Key => SceneKey.Map;

        public override void Render(GameContext context)
        {
            ConsoleUI.Clear();
            ConsoleUI.WriteTitle("-월드맵-", "마을");
            ConsoleUI.WriteMap(MapData);

            ConsoleUI.WriteMenu(Menu, "행동 메뉴");
            ConsoleUI.WriteLog(context.Logs);

        }

        public override void HandleInput(GameContext context)
        {
            int choice = ConsoleUI.ReadMenuChoice(Menu);
            switch (choice)
            {
                case 8:
                    context.AddLog("플레이어가 한 칸 위쪽으로 이동합니다.");
                    break;
                case 4:
                    context.AddLog("플레이어가 한 칸 왼쪽으로 이동합니다.");
                    break;
                case 5:
                    context.AddLog("플레이어가 한 칸 아래쪽으로 이동합니다.");
                    break;
                case 6:
                    context.AddLog("플레이어가 한 칸 오른쪽으로 이동합니다.");
                    break;
                    

                case 0:
                    context.Game.ChangeScene(SceneKey.Title);
                    break;
            }
        }

        public void PlayerMove(GameContext context)
        {

        }
    }
}
