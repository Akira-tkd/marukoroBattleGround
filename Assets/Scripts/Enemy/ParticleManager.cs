using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private HitParticle _prefab;

    private Dictionary<GameObject, Queue<HitParticle>> _poolDictionary;

    void Awake()
    {
        _poolDictionary = new Dictionary<GameObject, Queue<HitParticle>>();
    }

    public HitParticle Get(GameObject key)
    {
        if(!_poolDictionary.TryGetValue(key, out var queue))
        {
            queue = new Queue<HitParticle>();
            _poolDictionary[key] = queue;
        }

        HitParticle hp;
        if(queue.Count > 0)
        {
            hp = queue.Dequeue();
            hp.gameObject.SetActive(true);
        }
        else
        {
            hp = Instantiate(_prefab);
        }

        hp.Init(this, key);
        return hp;
    }

    public void Release(GameObject key, HitParticle hitParticle)
    {
        _poolDictionary[key].Enqueue(hitParticle);
        hitParticle.gameObject.SetActive(false);
    }

}
