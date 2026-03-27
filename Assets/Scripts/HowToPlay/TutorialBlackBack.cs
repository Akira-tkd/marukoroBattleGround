using UnityEngine;

public class TutorialBlackBack : TutorialObject
{
    /*
     * チュートリアルで説明用のテキストを表示するときに見やすくするための黒背景にアタッチする
     * 黒背景がでてテキストで説明する画面では後ろのゲーム画面が止まってる方が見やすいため一時停止する
     * 次のテキストに移るための方法がボタンのクリックなのでカーソルも表示する
     */

    // アクティブの切り替えと同時に一時停止とマウスの表示を切り替える
    public override void OnChangeStage(int stage)
    {
        if(ActiveStage.Contains(stage))
        {
            this.gameObject.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }

        if(DisActiveStage.Contains(stage))
        {
            this.gameObject.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1.0f;
        }
    }
}
