using UnityEngine;

[CreateAssetMenu(fileName="KnockBack", menuName="Effect/KnockBack")]
public class KnockBackEffect : Effect
{
    /*
     * powerの値分の力でノックバックを起こす
     * ノックバック方向は着弾時の弾から着弾対象向きのベクトル
     */

    [SerializeField] float power;

    public override void OnHit(EffectContext effectContext, GameObject enemy)
    {
        Vector3 direction = (enemy.transform.position - effectContext.Self.transform.position).normalized;
        enemy.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }
}