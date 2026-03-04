using UnityEngine;

public class SearchGetter : MonoBehaviour
{
    /*
     * 敵が持つ索敵範囲オブジェクトにアタッチするスクリプト
     * コライダーによる接触判定で接触したプレイヤーのトランスフォームを親である敵オブジェクトの
     * スクリプトに渡す
     */

    [SerializeField] Enemy enemy;

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            enemy.SetTarget(c.transform);
        }
    }
}
