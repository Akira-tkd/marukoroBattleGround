using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    /*
     * 敵の基底クラス
     * 全敵で共通している処理のみ記載されている
     * 行動処理や攻撃のロジックは各敵ごとのスクリプトで管理する
     */

    [Header("Enemy Base Status")]
    [SerializeField] int maxHP;
    [SerializeField] AttackData attackData;

    private int HP;

    protected Transform Target;  // 攻撃対象のトランスフォーム
    protected Rigidbody RB;  // 敵自信のRigidbody

    protected AttackData Data => attackData;  // AttackData自体は攻撃処理について書く子クラス側の分野のため、読み込める状態にしておく
    protected int MaxHP => maxHP;
    protected int CurrentHP => HP;

    private float nextAttackableTime = 0f;
    private Coroutine attackCoroutine;

    void OnEnable()
    {
        HP = maxHP;
        EnemyManager.Instance.EnemyList.Add(transform);  // 敵の残数を記録するためのスクリプトにあるリストに追加する
        RB = GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        EnemyManager.Instance.EnemyList.Remove(transform);  // 撃破されたので残りの敵リストから削除する
    }

    
    public event Action<int, Vector3> OnDamaged;  // ダメージを受けた時に呼ばれるイベント
    public void OnAttacked(int amount, Vector3 hitPos)
    {
        HP -= amount;

        OnDamaged(amount, hitPos);

        if(HP <= 0)
        {
            HP = 0;
            this.gameObject.SetActive(false);
        }
    }

    // ターゲットとなるオブジェクトのトランスフォームを保存するための処理
    public void SetTarget(Transform target)
    {
        this.Target = target;
    }

    // 攻撃行動を行うためのメソッド
    // これを呼び出す条件やタイミングは各敵によって異なる
    public void Attack()
    {
        attackCoroutine = StartCoroutine(RepeatAttack(Data));
    }

    // 状況の変化で攻撃を中止するときのメソッド
    public void AttackStop()
    {
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }

    // 攻撃行動中に攻撃を繰り返すコルーチン(コルーチンという言葉の使い方として正しいかは不明)
    // インターバルは各攻撃データ側に記載されており、その間隔で攻撃を発生させる
    protected IEnumerator RepeatAttack(AttackData attack)
    {
        while (true)
        {
            if (Time.time >= nextAttackableTime && Target != null && Data.BulletPrefab != null)
            {
                var dir = (Target.position - transform.position).normalized;
                GameObject bullet = BulletPollManager.Instance.GetObject(attack);
                bullet.transform.position = transform.position;
                Collider bulletCol = bullet.GetComponent<Collider>();
                Physics.IgnoreCollision(bulletCol, GetComponent<Collider>(), true);
                bullet.GetComponent<IAttack>().AttackInit(attack, dir, new EffectContext());
                nextAttackableTime = Time.time + attack.Interval;
            }
            yield return null;
        }
    }
}
