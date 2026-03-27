using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TutorialText : TutorialObject
{
    /*
     * チュートリアル段階に応じて表示する説明用テキストにアタッチする
     * テキスト内容や切り替えるタイミングはシリアライズで設定する
     * アクティブになるタイミングだけでなく、テキスト内容が変わるタイミングもactiveStageに入れる必要がある
     */

    [SerializeField] List<string> informationText;  // テキスト内容のリスト
    [SerializeField] TextMeshProUGUI textObject;

    public override void OnChangeStage(int stage)
    {
        int indexOfText;
        if(ActiveStage.Contains(stage))
        {
            indexOfText = ActiveStage.IndexOf(stage);
            textObject.text = informationText[indexOfText];
            if(!this.gameObject.activeSelf) this.gameObject.SetActive(true);
        }

        if(DisActiveStage.Contains(stage))
        {
            this.gameObject.SetActive(false);
        }
    }
}
