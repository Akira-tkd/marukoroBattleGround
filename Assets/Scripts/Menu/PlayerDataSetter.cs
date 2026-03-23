using UnityEngine;

public class PlayerDataSetter : MonoBehaviour
{
    [SerializeField] PlayerData setData;

    public void SetData()
    {
        if(RunTimePlayerData.Instance)
        {
            RunTimePlayerData.Instance.ChangeData(setData);
        }
    }
}
