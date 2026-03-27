using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum BindAction
{
    /*
     * どの操作まで制限するかを分けるためのenum
     */

    All,  // 全ての入力を無効化する
    OnlyMove,  // 前後左右への移動のみできる
    NoAttack,  // 攻撃以外の行動が全てできる
    None  // 無制限
}

public class InputHandler : TutorialObject
{
    /*
     * チュートリアルの進行段階に応じて操作を制限するためのクラス
     * シリアライズで操作制限を変えるタイミングと制限内容を設定する
     */

    [SerializeField] List<BindAction> bindList;  // 操作制限の種類を順番に設定するためのリスト
    [SerializeField] PlayerInput playerInput;

    // 操作制限が変わるチュートリアル段階のときに処理される
    public override void OnChangeStage(int stage)
    {
        int indexOfAction;
        if(ActiveStage.Contains(stage))  // 変化後の段階が制限の切り替えタイミングかを判別
        {
            indexOfAction = ActiveStage.IndexOf(stage);  // 何番目の制限切り替えタイミングかを判別
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
