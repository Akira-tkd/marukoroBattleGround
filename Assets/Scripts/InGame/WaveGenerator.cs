using UnityEngine;
using System.Collections.Generic;

public class WaveGenerator : MonoBehaviour
{
    /*
     * Resourcesフォルダにあるウェーブ情報を読み込み、ランダムなウェーブ情報のリストを生成するためのスクリプト
     * 今後、1ウェーブ目用の情報、2ウェーブ目用の情報、という風にウェーブ数ごとにフォルダを階層で管理することで
     * 完全ランダムよりも理不尽でない難易度設定になるように変更する予定
     */
    private List<WaveInfomation> waveInfos = new();
    private string[] fileName = { "Wave1", "Wave2", "Wave3", "Wave4", "Wave5" };

    public List<WaveInfomation> MakingWaves()
    {
        List<WaveInfomation> waves = new();

        foreach(var name in fileName)
        {
            var wave = GetRandomWaveInformation($"WaveInformation/{name}");
            if(wave != null)
            {
                waves.Add(wave);
            }
        }

        return waves;
    }

    WaveInfomation GetRandomWaveInformation(string fileName)
    {
        var items = Resources.LoadAll<WaveInfomation>(fileName);
        Debug.Log(items.Length);
        if(items != null)
        {
            return items[Random.Range(0, items.Length)];
        }
        else
        {
            return null;
        }
    }
}
