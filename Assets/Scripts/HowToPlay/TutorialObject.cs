using UnityEngine;
using System.Collections.Generic;

public abstract class TutorialObject : MonoBehaviour
{
    /*
     * チュートリアルの段階に応じてオブジェクトのアクティブ状態を切り替えるためのクラス
     * チュートリアル中に出たり消えたりするオブジェクトには必ずアタッチする
     */

    [SerializeField] List<int> activeStage;  // どの段階でアクティブにするかを決める変数
    [SerializeField] List<int> disactiveStage;  // どの段階で非アクティブにするかを決める変数

    // シリアライズされた変数を参照できるように
    protected List<int> ActiveStage => activeStage;
    protected List<int> DisActiveStage => disactiveStage;

    public virtual void OnChangeStage(int stage) { }
}
