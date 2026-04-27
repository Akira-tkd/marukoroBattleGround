using UnityEngine;

public class Dummy : FlyType
{
    /*
     * プレビュー中に攻撃され続けるだけのダミー
     * 万が一倒されないために体力を回復し続ける
     * 他のEnemyと同じ構造のため、Preview用のシーンにもEnemyManagerが必要
     */
    void Update()
    {
        OnAttacked(MaxHP, Vector3.zero);
    }
}
