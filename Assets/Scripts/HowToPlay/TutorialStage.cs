using UnityEngine;

public class TutorialStage : TutorialObject
{
    /*
     * 操作チュートリアル用に出現する足場などにアタッチする
     * 段階に応じたアクティブ切り替えしかしない
     */

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
}
