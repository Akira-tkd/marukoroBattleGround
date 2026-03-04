using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectButton : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] GameObject selectCanvas;
    [SerializeField] GameObject decideCanvas;

    [Header("SetData")]
    [SerializeField] string charaName;
    [SerializeField] string charaInfo;
    [SerializeField] PlayerData data;

    [Header("DataSetObject")]
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI InfoText;
    [SerializeField] DefineButton DefineButton;
    [SerializeField] PreviewCharactorController PreviewCharactor;

    public void OnClicked()
    {
        decideCanvas.SetActive(true);
        selectCanvas.SetActive(false);
        NameText.text = charaName;
        InfoText.text = charaInfo;
        DefineButton.SetData(data);
        PreviewCharactor.SetData(data);
        PreviewCharactor.gameObject.SetActive(true);
    }
}
