using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAttackData", menuName = "AttackData")]
public class AttackData : ScriptableObject
{
    /*
     * 攻撃のインターバルや威力、攻撃時に生成するプレハブオブジェクトを格納しておくクラス
     * 敵の攻撃に関する情報も同じクラスを使いまわす
     */

    public string AttackName;
    public float Interval;
    public float Speed;
    public int Damage;
    public float Life;
    public GameObject BulletPrefab;

    public List<Effect> EffectList;
    public List<EnemyEffect> EnemyEffectList;
}
