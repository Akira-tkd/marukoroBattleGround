using UnityEngine;
using TMPro;

public class RewardCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Info;

    private Reward choice;
    private RewardState rewardState;

    public void SetInit(Reward reward, RewardState state)
    {
        string name, info;
        var status = PlayerStatus.Instance;
        var data = RunTimePlayerData.Instance.Data;
        choice = reward;
        rewardState = state;

        switch (choice)
        {
            case Reward.LeftClickPower:
                var lpower = data.Attack1.Damage;
                var divlp = (int)(lpower * 0.25f);

                if(divlp <= 0)
                {
                    divlp = 1;
                }

                name = "左クリック威力強化";
                info = "威力：" + lpower.ToString() + " → " + (lpower + divlp).ToString();
                break;

            case Reward.LeftClickInterval:
                var linterval = data.Attack1.Interval;
                var divli = linterval * 0.25f;

                name = "左クリック間隔減少";
                info = "間隔：" + linterval.ToString("F2") + " → " + (linterval - divli).ToString("F2");
                break;

            case Reward.RightClickPower:
                var rpower = data.Attack2.Damage;
                var divrp = (int)(rpower * 0.25f);

                if (divrp <= 0)
                {
                    divrp = 1;
                }

                name = "右クリック威力強化";
                info = "威力：" + rpower.ToString() + " → " + (rpower + divrp).ToString();
                break;

            case Reward.RightClickInterval:
                var rinterval = data.Attack2.Interval;
                var ridiv = rinterval * 0.25f;

                name = "右クリック間隔減少";
                info = "間隔：" + rinterval.ToString("F2") + " → " + (rinterval - ridiv).ToString("F2");
                break;

            case Reward.HP:
                var maxhp = status.MaxHP;
                var hp = status.HP;
                var hpdiv = (int)(maxhp * 0.1f);

                name = "最大HP強化";
                info = "最大HP：" + maxhp.ToString() + " → " + (maxhp + hpdiv).ToString() + "\n\n" + 
                   "残りHP：" + hp.ToString() + " → " + (hp + hpdiv).ToString();
                break;

            case Reward.Energy:
                var maxen = status.JumpEnegy;
                var en = status.RemainEnegy;
                var endiv = maxen * 0.25f;

                name = "最大エナジー強化";
                info = "最大エナジー：" + maxen.ToString("F2") + " → " + (maxen + endiv).ToString("F2") + "\n\n" + 
                   "残りエナジー：" + en.ToString("F2") + " → " + (en + endiv).ToString("F2");
                break;

            case Reward.Speed:
                var speed = status.Speed;
                var spdiv = speed * 0.5f;
                var ms = status.LimitSpeed;
                var msdiv = ms * 0.25;

                name = "速度強化";
                info = "最大速度：" + ms.ToString("F2") + " → " + (ms + msdiv).ToString("F2") + "\n\n" + 
                    "加速力：" + speed.ToString("F2") + " → " + (speed + spdiv).ToString("F2");
                break;

            case Reward.Jump:
                var jump = status.JumpPower;
                var jdiv = jump * 0.5f;

                name = "ジャンプ力強化";
                info = "ジャンプ力：" + jump.ToString("F2") + " → " + (jump + jdiv).ToString("F2");
                break;

            case Reward.Killer:
                var lpower2 = data.Attack1.Damage;
                var rpower2 = data.Attack2.Damage;
                var mh2 = status.MaxHP;
                var lpdiv2 = (int)(lpower2 * 0.25f);
                var rpdiv2 = (int)(rpower2 * 0.25f);
                if (lpdiv2 <= 0) lpdiv2 = 1;
                if (rpdiv2 <= 0) rpdiv2 = 1;
                var divmh2 = (int)(mh2 * 0.1f);

                name = "代償威力強化";
                info = "左威力：" + lpower2.ToString() + " → " + (lpower2 + lpdiv2).ToString() + "\n\n" +
                    "右威力：" + rpower2.ToString() + " → " + (rpower2 + rpdiv2).ToString() + "\n\n" +
                    "最大HP：" + mh2.ToString() + " → " + (mh2 - divmh2).ToString();
                break;

            case Reward.Tank:
                var speed2 = status.Speed;
                var ms2 = status.LimitSpeed;
                var mh3 = status.MaxHP;
                var hp2 = status.HP;

                var divs2 = speed2 * 0.25f;
                var divms2 = ms2 * 0.25f;
                var divmh3 = (int)(mh3 * 0.25f);

                name = "代償HP強化";
                info = "最大HP：" + mh3.ToString() + " → " + (mh3 + divmh3).ToString() + "\n\n" +
                    "残りHP：" + hp2.ToString() + " → " + (hp2 + divmh3).ToString() + "\n\n" +
                    "最大速度：" + ms2.ToString("F2") + " → " + (ms2 - divms2).ToString("F2") + "\n\n" +
                    "加速力：" + speed2.ToString("F2") + " → " + (speed2 - divs2).ToString("F2");
                break;

            case Reward.Heal:
                var mh4 = status.MaxHP;
                var hp3 = status.HP;
                var me3 = status.JumpEnegy;
                var re3 = status.RemainEnegy;

                name = "全回復";
                info = "残りHP：" + hp3.ToString() + " → " + mh4.ToString() + "\n\n" +
                    "残りエナジー：" + re3.ToString("F2") + " → " + me3.ToString("F2");
                break;
            default:
                name = "エラー";
                info = "存在しない報酬が選択されました";
                break;
        }

        Name.text = name;
        Info.text = info;
    }

    public void OnClicked()
    {
        rewardState.Choiced(choice);
    }
}
