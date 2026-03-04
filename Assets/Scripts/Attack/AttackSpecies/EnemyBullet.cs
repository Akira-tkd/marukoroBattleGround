using UnityEngine;
using System.Collections.Generic;

public class EnemyBullet : MonoBehaviour, IAttack
{
    /*
     * 敵が生成する物理判定により攻撃の処理を行う弾オブジェクトにアタッチされるコンポーネント
     * 威力などはAttackDataに格納されている値を使うためここで明記することは無い
     */
    private AttackData Data;
    private Vector3 direction;
    private int damage;
    private float life;
    private List<Effect> effects = new();
    private EffectContext context;

    public void AttackInit(AttackData attackData, Vector3 direction, EffectContext context)
    {
        Data = attackData;
        this.context = context;
        context.Data = attackData;
        damage = attackData.Damage;
        effects = attackData.EffectList;

        life = 0f;
        var rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * attackData.Speed, ForceMode.Impulse);

        foreach(var effect in effects)
        {
            effect.InitContext(this.context);
        }
        if (context.Initialized == false)
        {
            this.context.Data = attackData;
            this.context.HitEnemies = new HashSet<Transform>();
            this.context.Self = this.gameObject;
            this.context.Initialized = true;
        }
    }

    void Update()
    {
        life += Time.deltaTime;
        if(life > Data.Life)
        {
            foreach (var effect in effects)
            {
                effect.OnLeave(context, null);
            }
            BulletPollManager.Instance.Leave(gameObject, Data);  // 一定時間経過後に弾が消えるようになっている
        }
    }

    // 弾がトリガーでなかったら被弾時に弾の速度に応じた強いノックバックが発生するため、トリガーになっている
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            var player = c.gameObject.GetComponent<PlayerStatus>();
            player.HPDecrease(damage);
            BulletPollManager.Instance.Leave(gameObject, Data);
            gameObject.SetActive(false);
            foreach (var effect in effects)
            {
                effect.OnHit(context, c.gameObject);
            }
        }
        else if(!c.gameObject.CompareTag("Enemy") && !c.gameObject.CompareTag("Attack") && !c.gameObject.CompareTag("Ignore"))
        {
            foreach (var effect in effects)
            {
                effect.OnLeave(context, c.gameObject);
            }
        }
    }
}
