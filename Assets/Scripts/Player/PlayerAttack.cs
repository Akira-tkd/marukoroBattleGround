using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    /*
     * プレイヤーの攻撃行動を管理するためのスクリプト
     * 攻撃のクールタイムは各攻撃共有のため、attack2のクールタイム中はattack1を使うこともできない
     */
    private AttackData attack1;
    private AttackData attack2;

    private Coroutine attack1Coroutine;
    private Coroutine attack2Coroutine;
    private float nextAttackTime = 0f;  // 次の攻撃ができるようになるのはいつかを格納するための変数
    private int mask;

    public void Init()
    {
        attack1 = RunTimePlayerData.Instance.Data.Attack1;
        attack2 = RunTimePlayerData.Instance.Data.Attack2;
        mask = ~LayerMask.GetMask("Ignore Raycast", "Player", "Ignore Aiming");
    }

    void Awake()
    {
        Init();
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            attack1Coroutine = StartCoroutine(RepeatAttack(attack1));
        }
        else if(context.canceled)
        {
            StopCoroutine(attack1Coroutine);
            attack1Coroutine = null;
        }
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attack2Coroutine = StartCoroutine(RepeatAttack(attack2));
        }
        else if(context.canceled)
        {
            StopCoroutine(attack2Coroutine);
            attack2Coroutine = null;
        }
    }

    IEnumerator RepeatAttack(AttackData attack)
    {
        while (true)
        {
            if(Time.time >= nextAttackTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                Vector3 dir = Vector3.zero;
                if (Physics.Raycast(ray, out hit, 10000f, mask))
                {
                    dir = hit.point - this.transform.position;
                }

                if(dir == Vector3.zero)
                {
                    dir = ray.GetPoint(100) - this.transform.position;
                }

                if (dir != Vector3.zero)
                {
                    dir = dir.normalized;
                    GameObject bullet = BulletPollManager.Instance.GetObject(attack);
                    bullet.transform.position = transform.position;
                    Collider bulletCol = bullet.GetComponent<Collider>();
                    Physics.IgnoreCollision(bulletCol, GetComponent<Collider>(), true);
                    bullet.GetComponent<IAttack>().AttackInit(attack, dir, new EffectContext());
                    nextAttackTime = Time.time + attack.Interval;
                }
            }
            yield return null;
        }
    }

    // カメラ内に収まっている、プレイヤーから最寄りの敵を攻撃対象とするため、攻撃対象を探すメソッド
    // 未定だが、攻撃の方向を画面中央にするなど、オートエイムから変える可能性があるため、使われなくなる可能性がある
    // 実際に使われなくなったためコメントアウト(AttackerContextクラスも消滅)
    /*public Vector3 SearchTarget(AttackerContext context)
    {
        Vector3 direction;
        Transform nearest = null;
        float minDistance = float.MaxValue;

        foreach (var target in EnemyManager.Instance.EnemyList)
        {
            Vector3 vp = context.Camera.WorldToViewportPoint(target.position);
            if (vp.z <= 0 ||
                vp.x < 0 || vp.x > 1 ||
                vp.y < 0 || vp.y > 1)
                continue;

            float distance = (target.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                nearest = target;
                minDistance = distance;
            }
        }

        if (nearest != null)
        {
            direction = (nearest.position - transform.position).normalized;
            return direction;
        }
        else
        {
            direction = Vector3.zero;
            return direction;
        }
    }*/
}
