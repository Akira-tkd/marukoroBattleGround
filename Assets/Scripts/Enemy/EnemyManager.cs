using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    /*
     * フィールド上に残る敵の数を管理するためのスクリプト
     * シングルトンにすることでプレハブから生成される敵オブジェクトからも簡単に値を追加できるようにしている
     * このスクリプト自体が敵の残数などから処理を行ったりはしない
     */

    public static EnemyManager Instance { get; private set; }

    public List<Transform> EnemyList = new();

    void Awake()
    {
        Instance = this;
    }
}
