using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum BindAction
{
    All,  // 全ての入力を無効化する
    OnlyMove,  // 前後左右への移動のみできる
    NoAttack,  // 攻撃以外の行動が全てできる
    None  // 無制限
}

public class InputHandler : TutorialObject
{
    [SerializeField] List<BindAction> bindList;
    [SerializeField] PlayerInput playerInput;

    public override void OnChangeStage(int stage)
    {
        int indexOfAction;
        if(ActiveStage.Contains(stage))
        {
            indexOfAction = ActiveStage.IndexOf(stage);
            switch(bindList[indexOfAction])
            {
                case BindAction.All:
                    playerInput.actions.FindActionMap("Player").Disable();
                    break;
                case BindAction.OnlyMove:
                    playerInput.actions.FindActionMap("Player").Enable();
                    playerInput.actions["Exit"].Disable();
                    playerInput.actions["Attack1"].Disable();
                    playerInput.actions["Attack2"].Disable();
                    playerInput.actions["Jump"].Disable();
                    break;
                case BindAction.NoAttack:
                    playerInput.actions.FindActionMap("Player").Enable();
                    playerInput.actions["Exit"].Disable();
                    playerInput.actions["Attack1"].Disable();
                    playerInput.actions["Attack2"].Disable();
                    break;
                case BindAction.None:
                    playerInput.actions.FindActionMap("Player").Enable();
                    break;
            }
        }
    }
}
