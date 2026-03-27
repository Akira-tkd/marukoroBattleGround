using UnityEngine;

public class TargetArea : TutorialObject
{
    /*
     * 操作方法のチュートリアルの「あの場所に移動してください」の「あの場所」にアタッチする
     * プレイヤーが到達したらチュートリアルを次の段階に進める役割
     */

    // チュートリアルの段階に応じてアクティブ状態を切り替える
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

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            TutorialManager.Instance.StageChange(true);
        }
    }
}
