using UnityEngine;
using UnityEngine.UI;

public class HPGage : MonoBehaviour
{
    /*
     * プレイヤーのHP残量をUI上にゲージで表示するためのスクリプト
     */
    private RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        float width = PlayerStatus.Instance.HP * 0.5f;
        Vector3 pos = rt.anchoredPosition;
        pos.x = width / 2;
        rt.anchoredPosition = pos;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
