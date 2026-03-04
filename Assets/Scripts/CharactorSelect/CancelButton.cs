using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] GameObject selectCanvas;
    [SerializeField] GameObject decideCanvas;
    [SerializeField] GameObject previewCharactor;

    public void OnClicked()
    {
        decideCanvas.SetActive(false);
        previewCharactor.SetActive(false);
        selectCanvas.SetActive(true);
    }
}
