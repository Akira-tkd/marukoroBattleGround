using UnityEngine;

public abstract class GameState
{
    /*
     * 各ステートの基底クラス
     * 必要なメソッドがvirtualで定義されているだけ
     */
    public GameStateMachine stateMachine;

    public GameState(GameStateMachine staetMachine)
    {
        this.stateMachine = staetMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
