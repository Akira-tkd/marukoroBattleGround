using UnityEngine;

public class TutorialButton : TutorialObject
{
    /*
     * 説明テキストを次のテキストに進むためのボタンにアタッチする
     */

    //  アクティブ状態の切り替えしかしない
    public override void OnChangeStage(int stage)
    {
        if(ActiveStage.Contains(stage))
        {
            this.gameObject.SetActive(true);
        }
        
        if(DisActiveStage.Contains(stage))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnClicked()
    {
        TutorialManager.Instance.StageChange(true);
    }
}
