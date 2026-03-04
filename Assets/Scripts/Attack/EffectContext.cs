using UnityEngine;
using System.Collections.Generic;

public class EffectContext
{
    /*
     * 攻撃時に攻撃者と対象者にまつわる情報を引数用にまとめるために定義されたクラス
     */
    // 共通で使う変数
    public AttackData Data;
    public HashSet<Transform> HitEnemies;
    public bool Initialized;
    public GameObject Self;

    // ChainEffect用の変数
    public int RemainChain;
}
