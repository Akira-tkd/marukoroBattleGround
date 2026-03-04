using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName="Chain", menuName="Effect/Chain")]
public class ChainEffect : Effect
{
    /*
     * 命中時に最も近い敵にめがけて追撃する効果
     * MaxChainの数まで追撃する
     */

    public int MaxChain;

    public override void InitContext(EffectContext context)
    {
        if (context.Initialized == false)
        {
            context.HitEnemies = new();
            context.RemainChain = MaxChain;
        }
    }

    public override void OnHit(EffectContext context, GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            if (context.RemainChain > 0)  // 連鎖回数が残っている場合
            {
                Transform next = null;
                float minDistance = float.MaxValue;

                foreach (var target in EnemyManager.Instance.EnemyList)  // 残りの敵の情報を持つリストから残りの敵の位置を全て得ている
                {
                    if (context.HitEnemies.Contains(target))
                        continue;

                    float distance = (target.position - enemy.transform.position).sqrMagnitude;  // 単純なソートのように最寄りの敵を探す
                    if (distance < minDistance)
                    {
                        next = target;
                        minDistance = distance;
                    }
                }

                if (next != null)  // 追撃対象が見つかった場合、その対象めがけて攻撃を発生させる
                {
                    context.RemainChain--;
                    Vector3 direction = (next.position - enemy.transform.position).normalized;
                    GameObject newBullet = BulletPollManager.Instance.GetObject(context.Data);
                    newBullet.transform.position = enemy.transform.position;
                    var b = newBullet.GetComponent<IAttack>();
                    b.AttackInit(context.Data, direction, context);
                }
            }
        }
    }
}
