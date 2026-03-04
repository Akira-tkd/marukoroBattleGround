using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtleState : GameState
{
    /*
     * 敵が生成され、それらと戦うステート
     * 敵の残数が0になるとインターバルステートに戻る
     */
    public ButtleState(GameStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        if(GameManager.Instance.NowWave == GameManager.Instance.WaveList.Count)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (EnemyManager.Instance.EnemyList.Count <= 0)
        {
            stateMachine.ChangeState(new IntervalState(stateMachine));
        }
    }

    public override void Update()
    {
        if (EnemyManager.Instance.EnemyList.Count <= 0)
        {
            stateMachine.ChangeState(new RewardState(stateMachine));
        }
    }
}
