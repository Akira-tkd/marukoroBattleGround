using UnityEngine;

public abstract class EnemyEffect : ScriptableObject
{
    /*
     * 敵の攻撃命中時に発動する効果についてを記述するための基底クラス
     * これを継承したスクリプトがそれぞれ命中時の効果についての処理を記載する
     */

    public virtual void OnHit(AttackData data, GameObject player) { }
}
