using UnityEngine;

public class TutorialButton : TutorialObject
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

    public void OnClicked()
    {
        TutorialManager.Instance.StageChange(true);
    }
}
