using UnityEngine;

[CreateAssetMenu(fileName="EnergyDrain", menuName="Effect/EnergyDrain")]
public class EnergyDrainEffect : Effect
{
    /*
     * energy分だけエナジー(ホバー飛行に必要な数値)を回復する
     */

    [SerializeField] float energy;

    public override void OnHit(EffectContext effectContext, GameObject enemy)
    {
        PlayerStatus.Instance.EnegyIncrease(energy);
    }
}
