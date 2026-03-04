using UnityEngine;

public class TimingDelete : MonoBehaviour
{
    [SerializeField] float life;
    private float time;
    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > life)
        {
            Destroy(this.gameObject);
        }
    }
}
