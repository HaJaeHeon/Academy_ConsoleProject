using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;
using System;

public class BattleScene : SceneBase
{
    //플레이어 어택 큐
    Queue<int> AttackNode = new Queue<int>();
    Random random = new Random();

    string keyStringValue = "";

    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(1, "1 공격","몬스터를 공격합니다."),
        new MenuOption(2, "2 공격","몬스터를 공격합니다."),
        new MenuOption(3, "3 공격","몬스터를 공격합니다."),
        new MenuOption(0, "포기하고 타이틀로 이동")
    };

    public override SceneKey Key => SceneKey.Battle;

    public override void Render(GameContext context)
    {
        MakeNodes();
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("전투씬", "스테이지 : 1");
        keyStringValue = AttackNode.Dequeue().ToString();
        ConsoleUI.WriteCentered($"커맨드 : {keyStringValue}", ConsoleColor.Yellow);
        ConsoleUI.WriteStatusBar(BattleManager.Instance.Player.Name, BattleManager.Instance.Player.Hp, BattleManager.Instance.Player.MaxHp); // 플레이어의 이름과 HP를 가져와야한다.
        ConsoleUI.WriteStatusBar(BattleManager.Instance.Enemy.Name, BattleManager.Instance.Enemy.Hp, BattleManager.Instance.Enemy.MaxHp,fillColor:ConsoleColor.Red); // 적의 이름과 HP를 가져와야한다.

        ConsoleUI.WriteMenu(Menu, "행동 메뉴");
        ConsoleUI.WriteLog(context.Logs);
        
    }

    public void MakeNodes()
    {
        //일단 100개만큼 넣고 나중에 바꾸기;
        for (int i = 0; i < 100; i++)
        {
            AttackNode.Enqueue(random.Next(1,4));
        }
    }

    public override async void HandleInput(GameContext context)
    {
        //int choice = ConsoleUI.ReadMenuChoice(Menu);
        //consolekey를 이용해서 키 값을 받아옴
        int choice = ConsoleUI.ReadMenuWithConsoleKey(Menu);

        if(choice == 0)
        {
            context.Game.ChangeScene(SceneKey.Title);
        }
        else if(choice.ToString() == keyStringValue)
        {
            context.AddLog(choice.ToString());
        }
        /*
        switch (choice)
        {
            case 1:
                // 플레이어의 공격 Enemy의 Take 데미지
                BattleManager.BattleOutcome result = BattleManager.Instance.PlayerAttack();

                if (result == BattleManager.BattleOutcome.Continuing)
                    context.AddLog($"{BattleManager.Instance.Enemy.Name}이 반격했습니다 : 데미지 {BattleManager.Instance.Enemy.Attack}");
                else if (result == BattleManager.BattleOutcome.Victory)
                {
                    context.AddLog($"Victory: {result}");
                    await WaitMilliSeconds(2000);
                    // 마을씬으로 이동
                    context.Game.ChangeScene(SceneKey.Map);
                }
                else if (result == BattleManager.BattleOutcome.Defeat)
                {
                    context.AddLog("패배했습니다.");
                    // 게임종료 -> 타이틀씬으로 이동
                    await WaitMilliSeconds(2000);
                    GoTo(context, SceneKey.Title);
                }
                break;
            case 0:
                context.Game.ChangeScene(SceneKey.Title);
                break;
        }       
        */
    }

    public async Task WaitMilliSeconds(int  milliseconds)
    {
        await Task.Delay(milliseconds);
    }
}
