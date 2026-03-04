using UnityEngine;

public class RunTimeDataElaser : MonoBehaviour
{
    void Start()
    {
        if (RunTimePlayerData.Instance != null)
        {
            Destroy(RunTimePlayerData.Instance.gameObject);
        }
    }
}
