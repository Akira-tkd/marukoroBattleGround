using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveInfomation", menuName = "Scriptable Objects/WaveInfomation")]
public class WaveInfomation : ScriptableObject
{
    /*
     * どの種類の敵がどれくらい出現するかをデータとして記憶するためのクラス
     * SpawnEnemyとSpawnAmountはインデックスで対応している
     * (例：SpawnEnemy[0]の種類の敵の出現量はSpawnAmount[0]体)
     */

    public List<GameObject> SpawnEnemy;
    public List<int> SpawnAmount;
}
