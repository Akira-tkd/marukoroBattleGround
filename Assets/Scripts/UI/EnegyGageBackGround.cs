using UnityEngine;
using UnityEngine.UI;

public class EnegyGageBackGround : MonoBehaviour
{
    /*
     * エナジーゲージの背景側(消費済みの方)を表示するためのスクリプト
     * 今後の実装次第でエナジーの量が変化する可能性があるため、可変式にしている
     */

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        float width = PlayerStatus.Instance.JumpEnegy * 5;
        Vector3 position = new Vector3(width / 2 - 960, 450, 0);

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.anchoredPosition = position;
    }

    public void Init()
    {
        float width = PlayerStatus.Instance.JumpEnegy * 5;
        Vector3 position = new Vector3(width / 2 - 960, 450, 0);

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.anchoredPosition = position;
    }
}
