using UnityEngine;

public class TutorialBlackBack : TutorialObject
{
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
