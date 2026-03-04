using UnityEngine;
using System.Collections.Generic;

public class AttackArea : MonoBehaviour, IAttack
{
    /*
     * プレイヤーを中心にダメージのあるエリアを展開するタイプの攻撃法
     * 弾自体にコライダーはあるが物理判定は無い
     */

    public AttackData Data { get; private set; }

    private Vector3 initScale;
    private float life;
    private EffectContext context;

    private List<Enemy> stayEnemies;

    public void AttackInit(AttackData attackData, Vector3 direction, EffectContext context)
    {
        Data = attackData;
        initScale = this.transform.localScale;
        this.context = context;
        stayEnemies = new List<Enemy>();

        life = 0f;
        foreach (var effect in attackData.EffectList)
        {
            effect.InitContext(this.context);
        }
        if (context.Initialized == false)
        {
            this.context.Data = attackData;
            this.context.HitEnemies = new HashSet<Transform>();
            this.context.Initialized = true;
            this.context.Self = this.gameObject;
        }
    }

    void Update()
    {
        this.transform.position = PlayerStatus.Instance.transform.position;
        life += Time.deltaTime;
        if(life > Data.Life)
        {
            this.transform.localScale = initScale;
            foreach(var enemy in stayEnemies)
            {
                foreach (var effect in Data.EffectList)
                {
                    effect.OnLeave(context, enemy.gameObject);
                }
            }

            BulletPollManager.Instance.Leave(gameObject, Data);
            gameObject.SetActive(false);
        }

        this.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        foreach(var enemy  in stayEnemies)
        {
            enemy.ChangeHP(-Data.Damage);
            foreach(var effect in Data.EffectList)
            {
                effect.OnHit(context, enemy.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Enemy"))
        {
            var enemy = c.gameObject.GetComponent<Enemy>();
            if(!stayEnemies.Contains(enemy))
            {
                stayEnemies.Add(enemy);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            var enemy = c.gameObject.GetComponent<Enemy>();
            if (stayEnemies.Contains(enemy))
            {
                stayEnemies.Remove(enemy);
            }
        }
    }
}
