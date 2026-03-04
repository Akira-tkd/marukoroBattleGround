using UnityEngine;

public class ReticleMover : MonoBehaviour
{
    private int mask;
    Vector3 currentPos;
    Vector3 target;
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        currentPos = ray.GetPoint(10000);
        mask = ~LayerMask.GetMask("Ignore Raycast", "Player", "Ignore Aiming");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        RaycastHit hit2;

        if(Physics.Raycast(ray, out hit, 10000f, mask))
        {
            Vector3 direction = (hit.point - PlayerStatus.Instance.transform.position).normalized;
            if(Physics.Raycast(PlayerStatus.Instance.transform.position, direction, out hit2,10000f, mask))
            {
                target = hit2.point;
            }
        }
        else
        {
            target = ray.GetPoint(100f);
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target);
        Vector3 result = Vector3.Lerp(currentPos, screenPos, 10f*Time.deltaTime);
        GetComponent<RectTransform>().position = screenPos;
    }
}
