using ConsoleGameFramework.Core;
using ConsoleGameFramework.Models;
using ConsoleGameFramework.UI;
using System;
using System.Timers;

public class BattleScene : SceneBase
{
    //플레이어 어택 큐
    public Queue<int> AttackNode = new Queue<int>();

    string keyStringValue = "";

    public System.Timers.Timer enemyAttackTimer;

    int attackCount = 0;

    private static readonly List<MenuOption> Menu = new List<MenuOption>
    {
        new MenuOption(1, "1 공격","몬스터를 공격합니다."),
        new MenuOption(2, "2 공격","몬스터를 공격합니다."),
        new MenuOption(3, "3 공격","몬스터를 공격합니다."),
        //new MenuOption(9, "시작 화면으로", "전투를 포기하고, 첫 화면으로 돌아갑니다."),
        new MenuOption(0, "종료", "프로그램을 종료합니다.")
    };

    public override SceneKey Key => SceneKey.Battle;

    public override void Enter(GameContext context)
    {
        BattleManager.Instance.Player.Hp = BattleManager.Instance.Player.MaxHp;
        MakeNodes(context);
        attackCount = 0;
    }
    public override void Render(GameContext context)
    {
        MakeTimer();
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("전투씬");
        ConsoleUI.WriteSubtitle(BattleManager.Instance.currentEnemy.Description);
        ConsoleUI.WriteCentered($"커맨드 : {keyStringValue}", ConsoleColor.Yellow);
        ConsoleUI.WriteStatusBar(BattleManager.Instance.Player.Name, BattleManager.Instance.Player.Hp, BattleManager.Instance.Player.MaxHp); // 플레이어의 이름과 HP를 가져와야한다.
        ConsoleUI.WriteStatusBar(BattleManager.Instance.currentEnemy.Name, BattleManager.Instance.currentEnemy.Hp, BattleManager.Instance.currentEnemy.MaxHp, fillColor: ConsoleColor.Red); // 적의 이름과 HP를 가져와야한다.
        ConsoleUI.WriteMenu(Menu, "행동 메뉴");
        ConsoleUI.WriteLog(context.Logs);
    }

    //씬 시작 시 타이머를 null로 선언했기때문에
    //이 부분에서 타이머 달아주고 초기화
    public void MakeTimer()
    {
        if (enemyAttackTimer == null)
        {
            enemyAttackTimer = new System.Timers.Timer(1000);
            enemyAttackTimer.Interval = BattleManager.Instance.currentEnemy.AttackRate;
            enemyAttackTimer.Elapsed += OnTimedEvent;
            enemyAttackTimer.AutoReset = true;
            enemyAttackTimer.Enabled = true;
        }
    }
    //타이머에 등록된 시간마다 enemyAttack 작동
    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        this.Render(GameManager.Instance.Context);
        BattleManager.BattleOutcome outcome = BattleManager.Instance.EnemyAttack();
        ConsoleUI.Present();

        if (outcome != BattleManager.BattleOutcome.Continuing)
        {
            BattleResult(GameManager.Instance.Context, outcome);
        }
    }

    //큐에 내가 공격 할 수 있는 커맨드의 값 넣어주기
    public void MakeNodes(GameContext context)
    {
        //일단 10개만큼 넣고 나중에 바꾸기;
        for (int i = 0; i < 100; i++)
        {
            AttackNode.Enqueue(context.Random.Next(1, 4));
        }
        context.AddLog("큐에 10개 넣기");
        keyStringValue = AttackNode.Dequeue().ToString();
    }


    public override void HandleInput(GameContext context)
    {
        this.Render(context);
        //consolekey를 이용해서 키 값을 받아옴
        int choice = ConsoleUI.ReadMenuWithConsoleKey(Menu);
        //if(choice == 9)
        //{
        //    context.Game.ChangeScene(SceneKey.Start);
        //}
        if (choice == 0)
        {
            StopTimer();
            context.Game.RequestQuit();
        }
        else if (choice.ToString() == keyStringValue)
        {
            BattleManager.Instance.attackCount++;
            if (AttackNode.Count <= 2)
                MakeNodes(context);
            keyStringValue = AttackNode.Dequeue().ToString();
            this.Render(context);
            ConsoleUI.Present();
            BattleManager.BattleOutcome result = BattleManager.Instance.PlayerAttack();
            BattleResult(context, result);
        }        
    }

    public void BattleResult(GameContext context, BattleManager.BattleOutcome result)
    {
        if (result == BattleManager.BattleOutcome.Victory)
        {
            StopTimer();
            context.AddLog($"플레이어: {result}");
            context.Game.ChangeScene(SceneKey.Start);
        }
        else if (result == BattleManager.BattleOutcome.Defeat)
        {
            StopTimer();
            context.AddLog($"플레이어. {result}");
            context.Game.ChangeScene(SceneKey.Start);
        }
    }

    //타이머 리셋
    public void StopTimer()
    {
        if (enemyAttackTimer != null)
        {
            enemyAttackTimer.Elapsed -= OnTimedEvent;
            enemyAttackTimer.AutoReset = false;
            enemyAttackTimer.Stop();
            enemyAttackTimer.Dispose();
            enemyAttackTimer = null;
        }
    }
}
