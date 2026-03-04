using UnityEngine;

[CreateAssetMenu(fileName = "PlayreData", menuName = "Scriptable Objects/PlayreData")]
public class PlayerData : ScriptableObject
{
    /*
     * プレイヤーの攻撃方法や最大HP、速度といったデータを事前に用意しておくためのクラス
     * 未定だが、複数タイプのデータ(スピードタイプや空中タイプなど)を用意し、開始時にどのタイプのデータを使用するかを選ぶ方式にする
     */

    public int MaxHP;
    public float Speed;
    public float LimitSpeed;
    public float JumpPower;
    public float MaxEnegy;
    public AttackData Attack1;
    public AttackData Attack2;
}
