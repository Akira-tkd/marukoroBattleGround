using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName="Bomb", menuName="Effect/Bomb")]
public class BombEffect : Effect
{
    /*
     * 命中時一定範囲で爆発ダメージを出す
     * 敵以外に当たってもダメージを出す
     */
    [SerializeField] int bombDamage;
    [SerializeField] float bombRadius;
    [SerializeField] GameObject effectObject;

    // 爆発エフェクトの発生やダメージ処理のメソッド
    void Exprode(List<Enemy> enemies, Transform centerPos)
    {
        GameObject effect = Instantiate(effectObject);
        effect.transform.position = centerPos.position;
        effect.transform.localScale = new Vector3(2*bombRadius, 2*bombRadius, 2*bombRadius);
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.ChangeHP(-bombDamage);
            }
        }
    }

    // 敵に当たったときの処理
    public override void OnHit(EffectContext effectContext, GameObject enemy)
    {
        Vector3 pos = effectContext.Self.transform.position;
        List<Enemy> inEffect = new List<Enemy>();
        foreach(var e in EnemyManager.Instance.EnemyList)
        {
            if(e.position.x <= pos.x + bombRadius && e.position.x >= pos.x - bombRadius
                && e.position.y <= pos.y + bombRadius && e.position.y >= pos.y - bombRadius
                && e.position.z <= pos.z + bombRadius && e.position.z >= pos.z - bombRadius)
            {
                inEffect.Add(e.gameObject.GetComponent<Enemy>());
            }
        }

        Exprode(inEffect, effectContext.Self.transform);
    }
    
    // 敵以外に当たったときの処理(内容はOnHitとほぼ同じ)
    public override void OnLeave(EffectContext effectContext, GameObject enemy)
    {
        if (enemy != null)  // 何にもぶつからなかった場合は爆発が発生しない
        {
            Vector3 pos = effectContext.Self.transform.position;
            List<Enemy> inEffect = new List<Enemy>();
            foreach (var e in EnemyManager.Instance.EnemyList)
            {
                if (e.position.x <= pos.x + bombRadius && e.position.x >= pos.x - bombRadius
                    && e.position.y <= pos.y + bombRadius && e.position.y >= pos.y - bombRadius
                    && e.position.z <= pos.z + bombRadius && e.position.z >= pos.z - bombRadius)
                {
                    inEffect.Add(e.gameObject.GetComponent<Enemy>());
                }
            }

            Exprode(inEffect, effectContext.Self.transform);
        }
    }
}
