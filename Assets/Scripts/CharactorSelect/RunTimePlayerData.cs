using UnityEngine;

public class RunTimePlayerData : MonoBehaviour
{
    /*
     * スクリプタブルオブジェクトからデータをコピーし、
     * コピーしたデータを全シーンで参照、変更できるようにするためのスクリプト
     * (複数のデータから一つを選ぶ形式に変わる可能性があるため、処理内容や存在するシーンが変わる可能性あり)
     */

    public static RunTimePlayerData Instance { get; private set; }
    public PlayerData Data { get; private set; }
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // シーンを跨いでもオブジェクトが保存されるための一行
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeData(PlayerData data)
    {
        // Assetのスクリプタブルオブジェクトのコピー
        Data = ScriptableObject.Instantiate(data);
        Data.Attack1 = ScriptableObject.Instantiate(data.Attack1);
        Data.Attack2 = ScriptableObject.Instantiate(data.Attack2);
    }
}
