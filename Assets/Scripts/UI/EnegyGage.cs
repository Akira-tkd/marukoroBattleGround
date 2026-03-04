using UnityEngine;
using UnityEngine.UI;

public class EnegyGage : MonoBehaviour
{
    /*
     * ホバー飛行時に消費するエナジーの量をUI上にゲージで表示するためのスクリプト
     */

    private RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        float width = PlayerStatus.Instance.RemainEnegy * 5;
        Vector3 pos = rt.anchoredPosition;
        pos.x = width / 2;
        rt.anchoredPosition = pos;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
