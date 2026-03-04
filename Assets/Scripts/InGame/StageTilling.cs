using UnityEngine;

public class StageTilling : MonoBehaviour
{
    void Start()
    {
        float xTilling = this.transform.localScale.x / 6;
        float yTilling = this.transform.localScale.z / 6;

        if(xTilling <= 0)
        {
            xTilling = 1;
        }
        if(yTilling <= 0)
        {
            yTilling = 1;
        }

        GetComponent<Renderer>().material.SetTextureScale("_BaseMap", new Vector2(xTilling, yTilling));
    }
}
