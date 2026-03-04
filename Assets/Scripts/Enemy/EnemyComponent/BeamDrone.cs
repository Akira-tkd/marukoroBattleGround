using UnityEngine;

public class BeamDrone : FlyType
{
    /*
     * Flyタイプの敵
     * プレイヤーにゆっくり近づき(一定距離を保つ)、射程内に入ったらビームを打つ
     * 機動力は低い
     */

    [Header("BeamDrone Parameter")]
    [SerializeField] float _offset;
    [SerializeField] float _maxSpeed;

    void FixedUpdate()
    {
        if (Target != null)
        {
            Vector3 distance = this.transform.position - Target.position;
            distance.y = 0f;
            distance = distance.normalized * _offset;
            distance.y = _offset;
            Vector3 targetPos = Target.position + distance;
            Vector3 direction = (targetPos - this.transform.position).normalized;
            if (RB.linearVelocity.sqrMagnitude < _maxSpeed*_maxSpeed)
            {
                RB.AddForce(direction * Speed);
            }
            else
            {
                RB.linearVelocity *= 0.95f;
            }
        }
    }
}
