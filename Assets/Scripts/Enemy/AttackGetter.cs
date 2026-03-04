using UnityEngine;

public class AttackGetter : MonoBehaviour
{
    /*
     * 敵オブジェクトが持つ攻撃射程を表すオブジェクトにアタッチするスクリプト
     * コライダーにより、入ってきた敵に対して攻撃を行ったり、攻撃行動を中止したりする
     * 攻撃対象を取得する機能は別オブジェクトにアタッチされた別スクリプトの仕事のため
     * 索敵用オブジェクトのコライダーの方が大きい必要がある
     */

    [SerializeField] Enemy enemy;

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            enemy.Attack();
        }
    }

    void OnTriggerExit(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            enemy.AttackStop();
        }
    }
}
