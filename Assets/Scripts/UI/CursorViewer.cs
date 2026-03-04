using UnityEngine;

public class CursorViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }
}
