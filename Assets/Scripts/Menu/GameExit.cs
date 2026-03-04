using UnityEngine;

public class GameExit : MonoBehaviour
{
    /*
     * タイトルにある「転がり終わる」ボタンにアタッチするスクリプト
     * エディター上ではデバッグプレイの終了を、ビルド後の場合はアプリケーションの終了をする
     */
    public void OnClicked()
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
}
