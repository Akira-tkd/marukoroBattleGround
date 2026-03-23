using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    /*
     * プレイヤーのステータスをUI等が簡単に取得できるようにシングルトン構造にしている
     * PlayerStatus.Instance.transform、という風にこのスクリプトを経由してプレイヤーの位置も取得可能
     */
    public static PlayerStatus Instance { get; private set; }

    public int MaxHP {  get; private set; }
    public float Speed { get; private set; }
    public float LimitSpeed { get; private set; }
    public float JumpPower { get; private set; }
    public float JumpEnegy { get; private set; }
    public bool OnGround { get; private set; }
    public int HP { get; private set; }
    public float RemainEnegy { get; private set; }

    // ステータスを初期化するためのメソッド
    public void InitStatus()
    {
        int oldMaxHP = MaxHP;
        float oldJumpEnegy = JumpEnegy;
        if (RunTimePlayerData.Instance.Data != null)
        {
            MaxHP = RunTimePlayerData.Instance.Data.MaxHP;
            Speed = RunTimePlayerData.Instance.Data.Speed;
            LimitSpeed = RunTimePlayerData.Instance.Data.LimitSpeed;
            JumpPower = RunTimePlayerData.Instance.Data.JumpPower;
            JumpEnegy = RunTimePlayerData.Instance.Data.MaxEnegy;
        }
        else
        {
            return;
        }

        if(oldMaxHP < MaxHP)
        {
            int div = MaxHP - oldMaxHP;
            HPIncrease(div);
        }
        if(oldJumpEnegy < JumpEnegy)
        {
            float div = JumpEnegy - oldJumpEnegy;
            EnegyIncrease(div);
        }
    }

    void Awake()
    {
        InitStatus();
        Instance = this;
        mask = ~LayerMask.GetMask("Player", "Ignore Raycast");
    }

    // 増減するprivate変数にアクセスするためのメソッド群
    public void HPDecrease(int damage)
    {
        HP -= damage;
        if(HP < 0)
        {
            HP = 0;
        }
    }

    public void HPIncrease(int amount)
    {
        HP += amount;
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
    }

    public void EnegyDecrease(float usage)
    {
        RemainEnegy -= usage;
        if(RemainEnegy < 0)
        {
            RemainEnegy = 0;
        }
    }

    public void EnegyIncrease(float amount)
    {
        RemainEnegy += amount;
        if(RemainEnegy > JumpEnegy)
        {
            RemainEnegy = JumpEnegy;
        }
    }

    // ステータスを変更するためのメソッド群
    public void ChangeMaxHP(int maxHP)
    {
        if(maxHP < 0)
        {
            this.MaxHP = maxHP;
        }
    }

    public void ChangeSpeed(float speed)
    {
        if (speed < 0.0f)
        {
            this.Speed = speed;
        }
    }

    public void ChangeLimitSpeed(float limitSpeed)
    {
        if (limitSpeed < 0.0f)
        {
            this.LimitSpeed = limitSpeed;
        }
    }

    public void ChangeJumpPower(float jumpPower)
    {
        if (jumpPower < 0.0f)
        {
            this.JumpPower = jumpPower;
        }
    }

    public void ChangeJumpEnegy(float jumpEnegy)
    {
        if(jumpEnegy < 0.0f)
        {
            this.JumpEnegy = jumpEnegy;
        }
    }

    // 設置判定の変更用+生死判定用
    int mask;
    void Update()
    {
        Ray ray = new Ray(this.transform.position, new Vector3(0, -1f, 0));
        Debug.DrawRay(ray.origin, ray.direction * this.transform.localScale.y * 0.5f, Color.red, 1f);
        if(Physics.Raycast(ray, this.transform.localScale.y * 0.5f, mask) && !OnGround)
        {
            OnGround = true;
        }
        else if(!Physics.Raycast(ray, this.transform.localScale.y * 0.5f, mask) && OnGround)
        {
            OnGround = false;
        }

        if(HP <= 0 && MaxHP > 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
