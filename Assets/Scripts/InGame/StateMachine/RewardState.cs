using UnityEngine;
using System.Collections.Generic;

public enum Reward
{
    LeftClickPower,
    LeftClickInterval,
    RightClickPower,
    RightClickInterval,
    HP,
    Energy,
    Speed,
    Jump,
    Killer,
    Tank,
    Heal
}

public class RewardState : GameState
{
    /*
     * ウェーブクリアごとにランダムでステータスアップを3つ選出
     * 画面上にステータスアップの選択肢を表示し、プレイヤーの入力を受け付けなくする
     * (なんなら時間を止めてもいいかも)
     */

    public RewardState(GameStateMachine stateMachine) : base(stateMachine) { }
    private List<Reward> rewards = new List<Reward>();

    private int choice;

    public void Choiced(Reward reward)
    {
        choice = (int)reward;
    }

    public override void Enter()
    {
        if(!Cursor.visible)
        {
            Cursor.visible = true;
        }
        choice = System.Enum.GetValues(typeof(Reward)).Length;
        for (int i= 0; i < 3; i++)
        {
            while(true)
            {
                Reward r = (Reward)Random.Range(0, System.Enum.GetValues(typeof(Reward)).Length);
                if(!rewards.Contains(r))
                {
                    rewards.Add(r);
                    break;
                }
            }
        }
        GameManager.Instance.ChoiceReward(rewards, this);
    }

    public override void Update()
    {
        if(choice < System.Enum.GetValues(typeof(Reward)).Length)
        {
            stateMachine.ChangeState(new IntervalState(stateMachine));
        }
    }

    public override void Exit()
    {
        float change1, change2, change3;
        float div1, div2, div3;

        var data = ScriptableObject.Instantiate(RunTimePlayerData.Instance.Data);
        var status = PlayerStatus.Instance;

        switch ((Reward)choice)
        {
            case Reward.LeftClickPower:
                change1 = data.Attack1.Damage;
                div1 = (int)(change1 * 0.25f);

                if (div1 <= 0)
                {
                    div1 = 1;
                }
                data.Attack1.Damage = (int)(change1 + div1);
                break;

            case Reward.LeftClickInterval:
                change1 = data.Attack1.Interval;
                div1 = change1 * 0.25f;

                data.Attack1.Interval = change1 - div1;
                break;

            case Reward.RightClickPower:
                change1 = data.Attack2.Damage;
                div1 = (int)(change1 * 0.25f);

                if (div1 <= 0)
                {
                    div1 = 1;
                }
                data.Attack2.Damage = (int)(change1 + div1);
                break;

            case Reward.RightClickInterval:
                change1 = data.Attack2.Interval;
                div1 = change1 * 0.25f;

                data.Attack2.Interval = change1 - div1;
                break;

            case Reward.HP:
                change1 = data.MaxHP;
                div1 = (int)(change1 * 0.1f);

                data.MaxHP = (int)(change1 + div1);
                break;

            case Reward.Energy:
                change1 = data.MaxEnegy;
                div1 = change1 * 0.25f;

                data.MaxEnegy = change1 + div1;
                break;

            case Reward.Speed:
                change1 = data.Speed;
                change2 = data.LimitSpeed;
                div1 = change1 * 0.5f;
                div2 = change2 * 0.25f;

                data.Speed = change1 + div1;
                data.LimitSpeed = change2 + div2;
                break;

            case Reward.Jump:
                change1 = data.JumpPower;
                div1 = change1 * 0.5f;

                data.JumpPower = change1 + div1;
                break;

            case Reward.Killer:
                change1 = data.Attack1.Damage;
                change2 = data.Attack2.Damage;
                change3 = data.MaxHP;
                div1 = (int)(change1 * 0.25f);
                div2 = (int)(change2 * 0.25f);
                if (div1 <= 0) div1 = 1;
                if (div2 <= 0) div2 = 1;
                div3 = (int)(change3 * 0.1f);

                data.Attack1.Damage = (int)(change1 + div1);
                data.Attack2.Damage = (int)(change2 + div2);
                data.MaxHP = (int)(change3 - div3);
                break;

            case Reward.Tank:
                change1 = data.Speed;
                change2 = data.LimitSpeed;
                change3 = data.MaxHP;

                div1 = change1 * 0.25f;
                div2 = change2 * 0.25f;
                div3 = (int)(change3 * 0.25f);

                data.Speed = change1 - div1;
                data.LimitSpeed = change2 - div2;
                data.MaxHP = (int)(change3 + div3);
                break;

            case Reward.Heal:
                status.HPIncrease(status.MaxHP);
                status.EnegyIncrease(status.JumpEnegy);
                break;
            default:
                Debug.Log("エラー");
                Debug.Log("存在しない報酬が選択されました");
                break;
        }

        RunTimePlayerData.Instance.ChangeData(data);
        GameManager.Instance.DisPose(false);
    }
}
