using UnityEngine;

public interface IAttack
{
    /*
     * 攻撃時に発生するゲームオブジェクトにアタッチするスクリプトの親クラス
     * 攻撃時にコンポーネントを取得するときに全攻撃タイプで処理を統一するため
     */
    void AttackInit(AttackData attackData, Vector3 direction, EffectContext context);
}
