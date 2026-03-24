using UnityEngine;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    /*
     * シーン中の複数のtutorialStageの数値を参照してアクティブ状態を切り替える
     * Managerはそれを同期するために存在する
     */

    public static TutorialManager Instance { get; private set; }

    [SerializeField] int maxStage;
    [SerializeField] List<TutorialObject> tutorialObjects;
    private int tutorialStage;

    void Awake()
    {
        Instance = this;
        tutorialStage = 0;

        foreach(var tutorialObject in tutorialObjects)
        {
            tutorialObject.OnChangeStage(tutorialStage);
        }
    }

    public void StageChange(bool direction)
    {
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

        foreach (var tutorialObject in tutorialObjects)
        {
            tutorialObject.OnChangeStage(tutorialStage);
        }
    }
}
