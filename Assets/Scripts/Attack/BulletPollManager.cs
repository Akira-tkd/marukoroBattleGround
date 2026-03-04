using UnityEngine;
using System.Collections.Generic;

public class BulletPollManager : MonoBehaviour
{
    /*
     * シーン上で弾オブジェクトは頻繁に発生し消滅するため、処理負担軽減のために
     * プールで管理しアクティブ状態の切り替えによって必要最低限のInstantiateで済むようにしている
     * 弾はシーン中に動的に生成され、このスクリプトに参照を割り当てられないためシングルトン構造に
     * することでスクリプトからアクセスできるようにしている
     */
    public static BulletPollManager Instance;

    public Dictionary<GameObject, Queue<GameObject>> Queues = new();

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetObject(AttackData attack)
    {
        if(!Queues.TryGetValue(attack.BulletPrefab, out var queue))
        {
            queue = new Queue<GameObject>();
            Queues[attack.BulletPrefab] = queue;
        }

        if(queue.Count > 0)
        {
            GameObject obj = queue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(attack.BulletPrefab);
        }
    }

    public void Leave(GameObject gameObject, AttackData attack)
    {
        Queues[attack.BulletPrefab].Enqueue(gameObject);
    }
}
