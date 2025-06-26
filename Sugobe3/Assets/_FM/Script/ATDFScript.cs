using TMPro;
using UnityEngine;
using static PadInput;

public class ATDFScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InfoT;
    [SerializeField] TextMeshProUGUI[] ATDFT;
    [SerializeField] GameObject Abuttons;
    [SerializeField] GameObject decideButton;

    private bool decide = false;
    private bool P1First = true;


    private void Start()
    {
//        WhoFirst();
    }
    private void Update()
    {
        if (!decide)
        {
            if (Input.GetKeyDown(KeyCode.N) || A_1P)
            {
                InfoT.text = "この順番でよろしいですか？";
                Abuttons.SetActive(false);
                decideButton.SetActive(true);
                for (int i = 0; i < 2; i++)
                {
                    ATDFT[i].enabled = true;
                    switch (i)
                    {
                        default:
                            break;
                        case 0:
                            ATDFT[i].text = "先攻(表)";
                            ATDFT[i].color = Color.red;
                            break;
                        case 1:
                            ATDFT[i].text = "後攻(裏)";
                            ATDFT[i].color = Color.blue;
                            break;
                    }
                }
                P1First = true;
                A_1P = false;
                decide = true;
            }

            else if (Input.GetKeyDown(KeyCode.M) || A_2P)
            {
                InfoT.text = "この順番でよろしいですか？";
                Abuttons.SetActive(false);
                decideButton.SetActive(true);
                for (int i = 0; i < 2; i++)
                {
                    ATDFT[i].enabled = true;
                    switch (i)
                    {
                        default:
                            break;
                        case 0:
                            ATDFT[i].text = "後攻(裏)";
                            ATDFT[i].color = Color.blue;
                            break;
                        case 1:
                            ATDFT[i].text = "先攻(表)";
                            ATDFT[i].color = Color.red;
                            break;
                    }
                }
                P1First = false;
                A_2P = false;
                decide = true;
            }
            Set1PFirst();
        }


        else if (Input.GetKeyDown(KeyCode.Escape) || B_1P ||B_2P)
        {
            ATDFT[0].enabled = false;
            ATDFT[1].enabled = false;
            Abuttons.SetActive(true);
            decideButton.SetActive(false);
            decide = false;
            InfoT.text = "先攻のプレイヤーはAボタンを押してください";
        }

        if (decide && P1First && RB_2P && LB_2P)
        {
            ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.CharaSelect);
        }
        else if (decide && !P1First && RB_1P && LB_1P)
        {
            ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.CharaSelect);
        }
    }

    public bool Get1PFirst()
    {
        return P1First;
    }

    public void Set1PFirst()
    {
        //BaseBallManagerでBaseBallがインスタンス化されてから実行
        if (BaseBallManager.GetInstance()._BaseBall != null)
        {
            BaseBallManager.GetInstance()._BaseBall.is1Pfirst = P1First;
        }
    }
}
