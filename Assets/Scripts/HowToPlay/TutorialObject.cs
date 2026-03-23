using UnityEngine;
using System.Collections.Generic;

public class TutorialObject : MonoBehaviour
{
    /*
     * チュートリアルの段階に応じてオブジェクトのアクティブ状態を切り替えるためのクラス
     * チュートリアル中に出たり消えたりするオブジェクトには必ずアタッチする
     */

    [SerializeField] List<int> activeStage;  // どの段階でアクティブにするかを決める変数
    [SerializeField] List<int> disactiveStage;  // どの段階で非アクティブにするかを決める変数

    public void ChangeActive(int stage)
    {
        if (activeStage.Contains(stage))
        {
            gameObject.SetActive(true);
        }
        if(disactiveStage.Contains(stage))
        {
            gameObject.SetActive(false);
        }
    }
}
