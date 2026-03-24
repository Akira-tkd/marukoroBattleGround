using UnityEngine;

public class TutorialStage : TutorialObject
{
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
