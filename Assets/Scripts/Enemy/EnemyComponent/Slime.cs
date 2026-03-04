using UnityEngine;

public class Slime : GroundType
{
    /*
     * Groundタイプの敵
     * 静摩擦動摩擦ともに高い値に設定されており、ジャンプしながらプレイヤーに向かう
     * プレイヤーとは物理的接触をせず、自分の中にプレイヤーがいたら継続的にダメージを与える
     */

    [Header("Slime Palameter")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float minJumpSpan;
    [SerializeField] float maxJumpSpan;
    [SerializeField] float jumpPower;

    private float duration = 0f;
    private float jumpSpan;
    private Collider[] hit = new Collider[1];
    private BoxCollider box;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
        jumpSpan = Random.Range(minJumpSpan, maxJumpSpan);
    }

    void Update()
    {
        duration += Time.deltaTime;

        if(Target !=  null)
        {
            if(duration > jumpSpan)
            {
                duration = 0f;
                jumpSpan = Random.Range(minJumpSpan, maxJumpSpan);
                Vector3 direction = (Target.position - this.transform.position).normalized;
                direction.y = 1f;
                RB.AddForce(direction * jumpPower, ForceMode.Impulse);
            }

            int count = Physics.OverlapBoxNonAlloc(
                box.bounds.center,
                box.bounds.extents,
                hit,
                transform.rotation,
                playerLayer
                );

            if(count > 0)
            {
                PlayerStatus.Instance.HPDecrease(1);
            }
        }
    }
}
