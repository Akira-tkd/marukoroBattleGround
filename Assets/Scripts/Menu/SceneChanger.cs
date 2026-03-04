using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /*
     * シーンを切り替えるボタンにアタッチするスクリプト
     * シリアライズで入力できる文字列に対応するシーン名を入れることで使いまわし可能
     */
    [SerializeField] string sceneName;

    public void OnClicked()
    {
        SceneManager.LoadScene(sceneName);
    }
}
