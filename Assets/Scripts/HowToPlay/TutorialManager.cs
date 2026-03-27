using UnityEngine;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    /*
     * 黒背景と説明テキストを表示したり、一部操作を受け付けない状態で実際に動かすパートを始めたりと、段階的なチュートリアル実装のためのクラス
     * チュートリアル用のオブジェクトは全てシリアライズから登録する必要がある
     */

    public static TutorialManager Instance { get; private set; }  // 他オブジェクトからメソッドを呼びたいためグローバルアクセス可能に

    [SerializeField] int maxStage;
    [SerializeField] List<TutorialObject> tutorialObjects;  // チュートリアルの進行段階によって変化する要素がある物のリスト
    private int tutorialStage;  // チュートリアルの段階を表す変数

    void Awake()
    {
        Instance = this;
        tutorialStage = 0;

        // チュートリアルスタート時の処理
        foreach(var tutorialObject in tutorialObjects)
        {
            tutorialObject.OnChangeStage(tutorialStage);
        }
    }
    
    // 他スクリプトが呼ぶメソッド
    // 今回は実装してないが、チュートリアルを戻るときにも使えるようにboolを引数に
    public void StageChange(bool direction)
    {
        // 引数がtrueなら進み、falseなら戻る
        if(direction)
        {
            tutorialStage++;
            if(maxStage < tutorialStage)
            {
                tutorialStage = maxStage;
                return;
            }
        }
        else
        {
            tutorialStage--;
            if (0 > tutorialStage)
            {
                tutorialStage = 0;
                return;
            }
        }

        // チュートリアルオブジェクト全てに段階の変化を適用することまでセット
        foreach (var tutorialObject in tutorialObjects)
        {
            tutorialObject.OnChangeStage(tutorialStage);
        }
    }
}
