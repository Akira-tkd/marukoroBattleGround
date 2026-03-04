using UnityEngine;

public class GameStateMachine
{
    /*
     * ゲームの状態を管理するステートマシンのクラス
     * 特別な処理は無く、ステート切り替えようのメソッドと、常時呼ばれるメソッドがあるだけ
     */
    public GameObject Owner { get; private set; }
    public GameState CurrentState { get; private set; }

    public GameStateMachine(GameObject owner)
    {
        Owner = owner;
    }

    public void ChangeState(GameState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}
