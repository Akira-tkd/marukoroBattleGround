using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour, IAttack
{
    /*
     * 物理接触をもって敵へのヒットを処理する弾オブジェクトにアタッチするスクリプト
     * 弾はトリガーでないと速度に応じて強いノックバックを与えてしまうためトリガーにしている
     */
    public AttackData Data { get; private set; }

    private Vector3 direction;
    private int damage;
    private float life;
    private List<Effect> effects = new();
    private EffectContext context;

    public void AttackInit(AttackData attackData, Vector3 direction, EffectContext context)
    {
        Data = attackData;
        this.context = context;
        damage = attackData.Damage;
        effects = attackData.EffectList;

        life = 0f;

        var rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * attackData.Speed, ForceMode.Impulse);
        context.Data = attackData;
        this.context.HitEnemies = new HashSet<Transform>();
        foreach (var effect in effects)
        {
            effect.InitContext(this.context);
        }
        if(context.Initialized == false)
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
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Enemy"))
        {
            if (context.HitEnemies.Contains(c.transform))
            {
                return;
            }
            var enemy = c.gameObject.GetComponent<Enemy>();
            enemy.ChangeHP(-damage);
            context.HitEnemies.Add(enemy.transform);
            BulletPollManager.Instance.Leave(gameObject, Data);
            gameObject.SetActive(false);
            foreach (var effect in effects)
            {
                effect.OnHit(context, c.gameObject);
            }
        }
        else if(!c.gameObject.CompareTag("Player") && !c.gameObject.CompareTag("Attack") && !c.gameObject.CompareTag("Ignore"))
        {
            foreach (var effect in effects)
            {
                effect.OnLeave(context, c.gameObject);
            }
        }
    }
}
