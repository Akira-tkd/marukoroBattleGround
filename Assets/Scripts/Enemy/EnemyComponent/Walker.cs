using UnityEngine;

public class Walker : GroundType
{
    /*
     * Groundタイプの敵
     * 索敵範囲と攻撃範囲を持ち、範囲内にいるプレイヤーにそれぞれ追跡、攻撃を行う
     * プレイヤーから一定距離オフセットを保つ方向に移動する。
     */

    [Header("Walker Parameter")]
    [SerializeField] float _offset;
    void FixedUpdate()
    {
        if(Target != null)
        {
            Vector3 targetPos = Target.position + ((this.transform.position - Target.position).normalized * _offset);
            Vector3 direction = (targetPos - this.transform.position).normalized;
            direction -= new Vector3(0f, direction.y, 0f);
            if(RB.linearVelocity.sqrMagnitude < 100f)
            {
                RB.AddForce(direction * Speed);
            }
        }
    }
}
