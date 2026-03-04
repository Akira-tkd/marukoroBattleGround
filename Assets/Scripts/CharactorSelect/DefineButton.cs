using UnityEngine;
using UnityEngine.SceneManagement;

public class DefineButton : MonoBehaviour
{
    private PlayerData data;
    public void OnClicked()
    {
        RunTimePlayerData.Instance.ChangeData(this.data);
        SceneManager.LoadScene("InGame");
    }

    public void SetData(PlayerData data)
    {
        this.data = data;
    }
}
