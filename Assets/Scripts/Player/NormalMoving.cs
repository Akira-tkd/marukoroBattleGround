using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NormalMoving : MonoBehaviour
{
    /*
     * 前後左右の移動やジャンプ、ホバーといった攻撃以外の動きを実装するためのスクリプト
     */

    // デバッグでいろいろ試したいから各種ステータスはSerialize
    [SerializeField] PlayerStatus status;

    private Rigidbody rb;
    private Transform cam;
    private Vector2 moveDirection;

    private bool jumpRequested;
    private bool hoverRequested;

    void Awake()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // 各アクションはPlayerInputで入力を検知し、InvokeUnityEventsで呼び出すためpublic
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && status.OnGround)
        {
            jumpRequested = true;
        }
        if(context.started && !status.OnGround && status.RemainEnegy >= 0f)
        {
            hoverRequested = true;
        }
        else if(context.canceled  && hoverRequested || status.RemainEnegy <= 0f)
        {
            hoverRequested = false;
        }
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu");
    }

    void FixedUpdate()
    {
        if (moveDirection.sqrMagnitude > 0.01)
        {

            Vector3 camForward = cam.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = cam.right;
            camRight.y = 0f;
            camRight.Normalize();

            Vector3 direction = camForward * moveDirection.y + camRight * moveDirection.x;

            Quaternion forwardRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                forwardRotation,
                1.0f * Time.deltaTime);

            Vector2 nowVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
            if (nowVelocity.magnitude < status.LimitSpeed)
            {
                rb.AddForce(direction * status.Speed);
            }
        }

        if(jumpRequested)
        {
            rb.AddForce(Vector3.up * status.JumpPower, ForceMode.Impulse);
            jumpRequested = false;
        }

        if(hoverRequested)
        {
            rb.AddForce(Vector3.up * status.JumpPower);
            status.EnegyDecrease(0.1f);
            if(status.RemainEnegy <= 0f)
            {
                hoverRequested = false;
            }
        }
    }
}
