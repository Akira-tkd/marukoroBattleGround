using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    private ParticleManager _particleManager;
    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnDamaged += PlayEffect;
    }

    void PlayEffect(int damage, Vector3 position)
    {
        var hp = _particleManager.Get(this.gameObject);
        hp.transform.position = position;
        hp.transform.rotation = Quaternion.Euler(position - transform.position);

        hp.Play(damage);
    }

    public void SetPool(ParticleManager manager)
    {
        this._particleManager = manager;
    }
}
