using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TutorialText : TutorialObject
{
    [SerializeField] List<string> informationText;
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
