using UnityEngine;

public class TargetArea : TutorialObject
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

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            TutorialManager.Instance.StageChange(true);
        }
    }
}
