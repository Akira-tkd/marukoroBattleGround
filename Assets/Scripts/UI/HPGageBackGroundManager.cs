using UnityEngine;
using UnityEngine.UI;

public class HPGageBackGroundManager : MonoBehaviour
{
    /*
     * HPゲージの背景側(減ったHPの方)を表示するためのスクリプト
     * 今後の実装次第で最大HPが変化する可能性があるため、可変式にしている
     */

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        float width = PlayerStatus.Instance.MaxHP * 0.5f;
        Vector3 position = new Vector3(width/2 - 960, 500, 0);

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.anchoredPosition = position;
    }

    public void Init()
    {
        float width = PlayerStatus.Instance.MaxHP * 0.5f;
        Vector3 position = new Vector3(width / 2 - 960, 500, 0);

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.anchoredPosition = position;
    }
}
