using UnityEngine;

public class GroundType : Enemy
{
    /*
     * 地上タイプの敵の親クラス
     * 明示的にRigidbodyによる重力を使用することを記載しておく
     * (敵のプレハブが誤ってUseGravityの項目がfalseになっていても大丈夫なように)
     */

    [Header("Ground Speed")]
    [SerializeField] float speed;

    protected float Speed => speed;

    void Awake()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
}
