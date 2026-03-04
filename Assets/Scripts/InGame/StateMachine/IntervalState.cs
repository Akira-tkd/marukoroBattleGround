using UnityEngine;

public class IntervalState : GameState
{
    /*
     * 敵が出現する前のインターバルの処理内容を記載したスクリプト
     * ゲームシーンに移った際の最初のステートのため、ウェーブ情報の生成もこのステートで行う
     */

    public IntervalState(GameStateMachine stateMachine) : base(stateMachine) { }
    private float life;
    private GameManager gm;

    public override void Enter()
    {
        if(Cursor.visible)
        {
            Cursor.visible = false;
        }
        gm = GameManager.Instance;
        if(gm.WaveList.Count ==  0)  // ウェーブ情報がない場合は生成する
        {
            gm.WaveList = stateMachine.Owner.GetComponent<WaveGenerator>().MakingWaves();
            gm.NowWave = 0;
        }
        else  // そうでない場合はウェーブ数を1進める
        {
            gm.NowWave++;
        }
        life = 0f;  // インターバルステートの残り時間を初期化

        PlayerStatus.Instance.InitStatus();
        GameManager.Instance.NeedInitScript1.Init();
        GameManager.Instance.NeedInitScript2.Init();
        GameManager.Instance.NeedInitScript3.Init();
    }

    public override void Update()
    {
        life += Time.deltaTime;
        if(life > 5.0f)  // インターバルステートになってから5秒経過でバトルステートに遷移
        {
            stateMachine.ChangeState(new ButtleState(stateMachine));
        }
    }

    public override void Exit()
    {
        if (gm.NowWave < gm.WaveList.Count)  // バトルステートに移るタイミングでそのウェーブの敵を生成する
        {
            for (int i = 0; i < gm.WaveList[gm.NowWave].SpawnEnemy.Count; i++)
            {
                for (int j = 0; j < gm.WaveList[gm.NowWave].SpawnAmount[i]; j++)
                {
                    gm.Spawn(gm.WaveList[gm.NowWave].SpawnEnemy[i]);
                }
            }
        }
    }
}
