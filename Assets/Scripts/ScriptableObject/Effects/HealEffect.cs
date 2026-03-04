using UnityEngine;

[CreateAssetMenu(fileName="Heal", menuName="Effect/Heal")]
public class HealEffect : Effect
{
    /*
     * heal•ª‚¾‚¯HP‚ð‰ñ•œ‚·‚é
     */
    [SerializeField] int heal;

    public override void OnHit(EffectContext effectContext, GameObject enemy)
    {
        PlayerStatus.Instance.HPIncrease(heal);
    }
}
