using UnityEngine;
using System.Collections.Generic;

public class HighSpeedBullet : MonoBehaviour, IAttack
{
    /*
     * 物理演算には不向きな速度で動く弾オブジェクトにアタッチするスクリプト
     * 弾自体にコライダーとしての機能はなく、1フレーム前の位置から現在地までRayを飛ばし
     * Rayに接触したオブジェクトに対して処理を行う
     */
    public AttackData Data { get; private set; }

    private Vector3 direction;
    
    private float life;
    private EffectContext context;
    private Vector3 oldPos;
    private RaycastHit hit;
    private int mask;

    public void AttackInit(AttackData attackData, Vector3 direction, EffectContext context)
    {
        Data = attackData;
        this.context = context;

        life = 0f;

        var rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * attackData.Speed, ForceMode.Impulse);
        oldPos = this.transform.position;
        foreach(var effect in attackData.EffectList)
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
        mask = ~LayerMask.GetMask("Ignore Raycast", "Player", "Attack");
    }

    void Update()
    {
        Vector3 direction = (this.transform.position - oldPos).normalized;
        float length = (this.transform.position - oldPos).magnitude;
        if(Physics.SphereCast(oldPos, this.GetComponent<SphereCollider>().radius * this.transform.lossyScale.x, direction, out hit, length, mask))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (!context.HitEnemies.Contains(hit.collider.transform))
                {
                    var enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    enemy.OnAttacked(Data.Damage, hit.point);
                    context.HitEnemies.Add(enemy.transform);
                    BulletPollManager.Instance.Leave(gameObject, Data);
                    gameObject.SetActive(false);
                    foreach (var effect in Data.EffectList)
                    {
                        effect.OnHit(context, hit.collider.gameObject);
                    }
                }
            }
            else
            {
                BulletPollManager.Instance.Leave(gameObject, Data);
                gameObject.SetActive(false);
                foreach (var effect in Data.EffectList)
                {
                    effect.OnLeave(context, hit.collider.gameObject);
                }
            }
        }

        oldPos = this.transform.position;

        life += Time.deltaTime;
        if(life > Data.Life)
        {
            foreach (var effect in Data.EffectList)
            {
                effect.OnLeave(context, null);
            }
            BulletPollManager.Instance.Leave(gameObject, Data);
            gameObject.SetActive(false);
        }
    }
}
