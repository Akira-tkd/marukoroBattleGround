using UnityEngine;

public class HitParticle : MonoBehaviour
{
    private ParticleManager _pool;
    private GameObject _key;

    public void Init(ParticleManager particleManager, GameObject key) 
    {
        _pool = particleManager;
        _key = key;
    }

    public void Play(int damage)
    {
        Debug.Log(damage);
    }

    void OnParticleSystemStopped()
    {
        _pool.Release(_key, this);
    }
}
