using UnityEngine;

public abstract class Effect : ScriptableObject
{
    /*
     * 攻撃命中時に発動する効果についてを記述するための基底クラス
     * これを継承したスクリプトがそれぞれ命中時の効果についての処理を記載する
     */

    public virtual void OnHit(EffectContext context, GameObject enemy) { }

    public virtual void OnLeave(EffectContext context, GameObject enemy) { }

    public virtual void InitContext(EffectContext context) { }
}
