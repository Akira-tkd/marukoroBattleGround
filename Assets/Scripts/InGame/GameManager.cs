using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    /*
     * ウェーブ制で敵が出現し、全て倒されたらインターバルの後次の敵が配置されるため、
     * ステートマシンにより、現在の状況を管理する
     * Instantiate等のMonoBehaviourを必要とする処理は全てここに記載されている
     */
    public static GameManager Instance;
    public int NowWave;
    public List<WaveInfomation> WaveList;
    public PlayerAttack NeedInitScript1;
    public HPGageBackGroundManager NeedInitScript2;
    public EnegyGageBackGround NeedInitScript3;

    private GameStateMachine stateMachine;

    [SerializeField] GameObject RewardPrefab;
    [SerializeField] GameObject RewardCanvas;
    [SerializeField] PlayerInput PlayerInput;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        stateMachine = new GameStateMachine(gameObject);
        stateMachine.ChangeState(new IntervalState(stateMachine));
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void Spawn(GameObject enemy)
    {
        float x, y, z;
        Vector3 pos = PlayerStatus.Instance.transform.position;

        if (Random.value < 0.5f)
        {
            x = Random.Range(-30f, -10f) + pos.x;
        }
        else
        {
            x = Random.Range(10f, 30f) + pos.x;
        }
        if(Random.value < 0.5f)
        {
            z = Random.Range(-30f, -10) + pos.z;
        }
        else
        {
            z = Random.Range(10f, 30) + pos.z;
        }
        y = Random.Range(-2f, 40f) + pos.y;

        if(x < -45f)
        {
            x = -45f;
        }
        else if(x > 45f)
        {
            x = 45f;
        }
        if(z < -45f)
        {
            z = -45f;
        }
        else if(z > 45f)
        {
            z = 45f;
        }
        if(y < 20f)
        {
            y = 20f;
        }

        var obj = Instantiate(enemy);
        obj.transform.position = new Vector3(x, y, z);
    }

    public void ChoiceReward(List<Reward> rewards, RewardState state)
    {
        Pose(false);
        RewardCanvas.SetActive(true);
        for(int i = 0; i < rewards.Count; i++)
        {
            GameObject UI = Instantiate(RewardPrefab, RewardCanvas.transform);
            UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600f + 600f * (i % 3), 100f, 0f);
            UI.transform.SetAsLastSibling();
            UI.GetComponent<RewardCard>().SetInit(rewards[i], state);
        }
    }

    public void Pose(bool menu)
    {
        Time.timeScale = 0f;
        PlayerInput.actions.FindActionMap("Player").Disable();
        if(menu)
        {
            Debug.Log("メニュー表示");
        }
    }

    public void DisPose(bool menu)
    {
        Time.timeScale = 1f;
        PlayerInput.actions.FindActionMap("Player").Enable();
        if(menu)
        {
            Debug.Log("メニュー非表示");
        }
        if(RewardCanvas.activeSelf)
        {
            for(int i = RewardCanvas.transform.childCount - 1; i > 0; i--)
            {
                Destroy(RewardCanvas.transform.GetChild(i).gameObject);
            }
            RewardCanvas.SetActive(false);
        }
    }
}
