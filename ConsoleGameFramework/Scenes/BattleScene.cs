using ConsoleGameFramework.Core;
using ConsoleGameFramework.UI;
using System;
using System.Timers;

public class BattleScene : SceneBase
{
    private static BattleScene? instance = null;

    public static BattleScene Instance
    {
        get
        {
            if (instance == null)
                instance = new BattleScene();

            return instance;
        }
    }
    //플레이어 어택 큐
    public Queue<int> AttackNode = new Queue<int>();

    string keyStringValue = "";

    public System.Timers.Timer enemyAttackTimer;
    bool isTImerRun = false;

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
        if(AttackNode.Count <= 1)
            MakeNodes(context);
        MakeTimer();
        ConsoleUI.Clear();
        ConsoleUI.WriteTitle("전투씬");
        ConsoleUI.WriteSubtitle(BattleManager.Instance.Enemy.Description);
        ConsoleUI.WriteCentered($"커맨드 : {keyStringValue}", ConsoleColor.Yellow);
        ConsoleUI.WriteStatusBar(BattleManager.Instance.Player.Name, BattleManager.Instance.Player.Hp, BattleManager.Instance.Player.MaxHp); // 플레이어의 이름과 HP를 가져와야한다.
        ConsoleUI.WriteStatusBar(BattleManager.Instance.Enemy.Name, BattleManager.Instance.Enemy.Hp, BattleManager.Instance.Enemy.MaxHp, fillColor:ConsoleColor.Red); // 적의 이름과 HP를 가져와야한다.
        //BattleManager.Instance.EnemyAttack();
        ConsoleUI.WriteMenu(Menu, "행동 메뉴");
        ConsoleUI.WriteLog(context.Logs);        
    }

    public void MakeTimer()
    {
        if (enemyAttackTimer == null)
        { 
            enemyAttackTimer = new System.Timers.Timer(1000);
            isTImerRun = true;
            enemyAttackTimer.Interval = BattleManager.Instance.Enemy.AttackRate;
            enemyAttackTimer.Elapsed += OnTimedEvent;
            enemyAttackTimer.AutoReset = true;
            enemyAttackTimer.Enabled = true;
        }
    }
    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        GameContext context = new GameContext(GameManager.Instance);
        BattleManager.BattleOutcome outcome = BattleManager.Instance.EnemyAttack();

        this.Render(context);
        ConsoleUI.Present();
    }

    public void MakeNodes(GameContext context)
    {
        //일단 10개만큼 넣고 나중에 바꾸기;
        for (int i = 0; i < 10; i++)
        {
            AttackNode.Enqueue(context.Random.Next(1,4));
        }
        context.AddLog("큐에 10개 넣기");
        keyStringValue = AttackNode.Dequeue().ToString();
        
    }

    public override void HandleInput(GameContext context)
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
           // context.AddLog(choice.ToString());
            keyStringValue = AttackNode.Dequeue().ToString();
            //context.AddLog($"peek 값 : {AttackNode.Peek().ToString()}, 큐에 남은 갯수 : {AttackNode.Count} ");
            BattleManager.BattleOutcome result = BattleManager.Instance.PlayerAttack();
            BattleResult(context, result);
        }
        else
        {
            context.AddLog("잘못된 공격");
        }
    }

    public void BattleResult(GameContext context, BattleManager.BattleOutcome result)
    {
        if(result == BattleManager.BattleOutcome.Victory)
        {
            context.AddLog($"Victory: {result}");
            context.Game.ChangeScene(SceneKey.Map);
        }
        else if(result == BattleManager.BattleOutcome.Defeat)
        {
            context.AddLog($"Defeat. {result}");
            context.Game.ChangeScene(SceneKey.Title);
        }
    }
}
