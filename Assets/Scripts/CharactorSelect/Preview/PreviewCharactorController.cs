using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;

public class PreviewCharactorController : MonoBehaviour
{
    /*
     * キャラクター選択画面でキャラクターの動きをプレビューするときの動作を行うスクリプト
     * UniTaskで整理した非同期処理で動作を繰り返す
     * プレビューの中身はPreviewメソッド、実際の処理はPreview内の各メソッドを参照
     * 攻撃時の処理は通常の攻撃と同様のため、BulletPoolManagerが必要
     */

    PlayerData data;  // プレビューするキャラクターのデータ
    [SerializeField] GameObject enemy;  // プレビュー内で攻撃する敵のプレハブ

    private Vector3 initPos = Vector3.zero;  // プレビューの初期位置
    private Rigidbody rb;  // 移動や速度の初期化を行うため変数として宣言しておく
    private Transform enemyTF;

    private CancellationTokenSource cts;  // プレビュー中にキャンセルできるように

    void OnEnable()
    {
        if(initPos == Vector3.zero)
        {
            initPos = this.transform.position;
        }
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        ShowPreview();
    }

    void OnDisable()
    {
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }

    // このメソッドを他スクリプトで呼び出してプレビューを開始する
    public async void ShowPreview()
    {
        cts = new CancellationTokenSource();
        await PreviewLoop(cts.Token);
    }

    // このメソッドを他スクリプトで呼び出してプレビューを止める
    public void StopPreview()
    {
        cts?.Cancel();
    }


    async UniTask PreviewLoop(CancellationToken token)
    {
        // プレビューを繰り返し再生するためにwhileループで呼ぶ
        while (!token.IsCancellationRequested)
        {
            await Preview(token);
        }
    }

    // 実際のプレビューのフロー
    async UniTask Preview(CancellationToken token)
    {
        Initialize();

        await UniTask.Delay(500, cancellationToken: token);

        await Moving(token);

        await Attack(token);

        await UniTask.Delay(300, cancellationToken: token);
    }

    // プレビューの初期化処理
    void Initialize()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = initPos;
        if (EnemyManager.Instance.EnemyList.Count > 0)
        {
            Destroy(EnemyManager.Instance.EnemyList[0].gameObject);
            Destroy(EnemyManager.Instance.EnemyList[0].gameObject);
            Destroy(EnemyManager.Instance.EnemyList[0].gameObject);
        }
    }

    async UniTask Moving(CancellationToken token)
    {
        float duration = 0.5f;  // 各方向に何秒間動くか
        float life = 0f;  // 今何秒動いてるかを保存する変数

        while(life < duration)
        {
            life += Time.deltaTime;
            if(rb.linearVelocity.magnitude < data.LimitSpeed)
            {
                rb.AddForce(Vector3.left * data.Speed);
            }
            await UniTask.Yield(token);
        }
        life = 0f;
        while (life < duration)
        {
            life += Time.deltaTime;
            if (rb.linearVelocity.magnitude < data.LimitSpeed)
            {
                rb.AddForce(Vector3.forward * data.Speed);
            }
            await UniTask.Yield(token);
        }
        life = -0.5f;
        while (life < duration)
        {
            life += Time.deltaTime;
            if (rb.linearVelocity.magnitude < data.LimitSpeed)
            {
                rb.AddForce(Vector3.right * data.Speed);
            }
            await UniTask.Yield(token);
        }
        life = -0.5f;
        while (life < duration)
        {
            life += Time.deltaTime;
            if (rb.linearVelocity.magnitude < data.LimitSpeed)
            {
                rb.AddForce(Vector3.back * data.Speed);
            }
            await UniTask.Yield(token);
        }
        life = 0f;
        while (life < duration)
        {
            life += Time.deltaTime;
            if (rb.linearVelocity.magnitude < data.LimitSpeed)
            {
                rb.AddForce(Vector3.left * data.Speed);
            }
            await UniTask.Yield(token);
        }
        life = 0f;
        while (life < duration)
        {
            life += Time.deltaTime;
            if (rb.linearVelocity.magnitude < data.LimitSpeed)
            {
                rb.AddForce(Vector3.forward * data.Speed);
            }
            await UniTask.Yield(token);
        }
    }

    // 攻撃を発生させるメソッド
    async UniTask Attack(CancellationToken token)
    {
        if (EnemyManager.Instance.EnemyList.Count == 0)
        {
            var e1 = Instantiate(enemy);
            var e2 = Instantiate(enemy);
            var e3 = Instantiate(enemy);

            enemyTF = e1.transform;

            e1.transform.position = initPos +  new Vector3(-10f, 1f, 16f);
            e2.transform.position = initPos + new Vector3(17f, 1f, 26f);
            e3.transform.position = initPos + new Vector3(0f, 7.2f, 26f);
        }

        float duration = 5f;  // 一つの攻撃方法を再生する秒数
        float life = 0f;

        float nextAttackTime = 0f;
        while(life < duration)
        {
            life += Time.deltaTime;
            // 以下の処理は実際の攻撃処理と同じ処理
            if (Time.time >= nextAttackTime)
            {
                Vector3 dir = enemyTF.position - transform.position;
                dir = dir.normalized;
                GameObject bullet = BulletPollManager.Instance.GetObject(data.Attack1);
                bullet.transform.position = transform.position;
                Collider bulletCol = bullet.GetComponent<Collider>();
                Physics.IgnoreCollision(bulletCol, GetComponent<Collider>(), true);
                bullet.GetComponent<IAttack>().AttackInit(data.Attack1, dir, new EffectContext());
                nextAttackTime = Time.time + data.Attack1.Interval;
            }
            await UniTask.Yield(token);
        }
        life = 0f;
        nextAttackTime = 0f;

        while(life < duration)
        {
            life += Time.deltaTime;
            if (Time.time >= nextAttackTime)
            {
                Vector3 dir = enemyTF.position - transform.position;
                dir = dir.normalized;
                GameObject bullet = BulletPollManager.Instance.GetObject(data.Attack2);
                bullet.transform.position = transform.position;
                Collider bulletCol = bullet.GetComponent<Collider>();
                Physics.IgnoreCollision(bulletCol, GetComponent<Collider>(), true);
                bullet.GetComponent<IAttack>().AttackInit(data.Attack2, dir, new EffectContext());
                nextAttackTime = Time.time + data.Attack2.Interval;
            }
            await UniTask.Yield(token);
        }
    }

    public void SetData(PlayerData data)
    {
        this.data = data;
    }
}
