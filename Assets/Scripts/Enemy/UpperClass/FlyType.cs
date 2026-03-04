using UnityEngine;

public class FlyType : Enemy
{
    /*
     * 飛行タイプの敵の親クラス
     * 明示的にRigidbodyによる重力を使わないことを記載しておく
     * (プレイヤーに近づく処理など、AddForceを使った移動などは各敵ごとに処理を記載する)
     */

    [Header("Flying Speed")]
    [SerializeField] float speed;

    protected float Speed => speed;

    void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }
}
