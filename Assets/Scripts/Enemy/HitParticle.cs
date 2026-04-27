using UnityEngine;

public class HitParticle : MonoBehaviour
{
    private ParticleManager _pool;
    private GameObject _key;
    private ParticleSystem _ps;

    public void Init(ParticleManager particleManager, GameObject key) 
    {
        _pool = particleManager;
        _key = key;
        _ps = GetComponent<ParticleSystem>();
        var main = _ps.main;
        main.startColor = key.GetComponent<Renderer>().material.color;
    }

    public void Play(int damage)
    {
        var emission = _ps.emission;
        short count = (short)Mathf.Clamp(damage * 0.25f, 1f, 100f);

        emission.SetBursts(new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0f, count)
        });

        _ps.Play();
    }

    void OnParticleSystemStopped()
    {
        _pool.Release(_key, this);
    }
}
