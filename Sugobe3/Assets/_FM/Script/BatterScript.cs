using TMPro;
using UnityEngine;
using static PadInput;

public class BatterScript : MonoBehaviour
{
    public TextMeshProUGUI[] PointNumbers;                
    public AudioSource AS;
    private int[] Points;
    private int AimingPos;

    private void Start()
    {
        Points = new int[9];
    }
    private void Update()
    {
        if (!ScreenManager.GetInstance()._MainManager.Batter_Decid.activeSelf)
        {
            if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
            {
                if (Y_1P || Input.GetKeyDown(KeyCode.Keypad4))
                {
                    AimingPos = 0;
                    AS.Play();
                    Y_1P = false;
                }

                else if (B_1P || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    AimingPos = 1;
                    AS.Play();
                    B_1P = false;
                }

                else if (A_1P || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    AimingPos = 2;
                    AS.Play();
                    A_1P = false;
                }

                else if (CrossLeft_1P || Input.GetKeyDown(KeyCode.Keypad7))
                {
                    AimingPos = 3;
                    AS.Play();
                    CrossLeft_1P = false;
                }

                else if (CrossUp_1P || Input.GetKeyDown(KeyCode.Keypad8))
                {
                    AimingPos = 4;
                    AS.Play();
                    CrossUp_1P = false;
                }

                else if (CrossRight_1P || Input.GetKeyDown(KeyCode.Keypad9))
                {
                    AimingPos = 5;
                    AS.Play();
                    CrossRight_1P = false;
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter) || (RB_1P && LB_1P))
                {
                    AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_LBRB);
                    ScreenManager.GetInstance()._MainManager.Batter_Decid.SetActive(true);
                    RB_1P = false;
                    LB_1P = false;
                }
            }

            if ((!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                || (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
            {
                if (Y_2P || Input.GetKeyDown(KeyCode.Keypad4))
                {
                    AimingPos = 6;
                    AS.Play();
                    Y_2P = false;
                }

                else if (B_2P || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    AimingPos = 7;
                    AS.Play();
                    B_2P = false;
                }

                else if (A_2P || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    AimingPos = 8;
                    AS.Play();
                    A_2P = false;
                }

                else if (CrossLeft_2P || Input.GetKeyDown(KeyCode.Keypad7))
                {
                    AimingPos = 9;
                    AS.Play();
                    CrossLeft_2P = false;
                }

                else if (CrossUp_2P || Input.GetKeyDown(KeyCode.Keypad8))
                {
                    AimingPos = 10;
                    AS.Play();
                    CrossUp_2P = false;
                }

                else if (CrossRight_2P || Input.GetKeyDown(KeyCode.Keypad9))
                {
                    AimingPos = 11;
                    AS.Play();
                    CrossRight_2P = false;
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter) || (RB_2P && LB_2P))
                {
                    AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_LBRB);
                    ScreenManager.GetInstance()._MainManager.Batter_Decid.SetActive(true);
                    RB_2P = false;
                    LB_2P = false;
                }
            }
        }

        else if ((ScreenManager.GetInstance()._MainManager.Batter_Decid.activeSelf && BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
            || (ScreenManager.GetInstance()._MainManager.Batter_Decid.activeSelf && !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (A_1P || Input.GetKeyDown(KeyCode.Space))
            {
                //ここにゲッターを入れる
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_OkA);
                Debug.Log(AimingPos + "の入力を確認しました！");
                ModeManeger.PitcherMode();
                ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                    ScreenManager.GetInstance()._MainManager.PitcherGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                    ScreenManager.GetInstance()._MainManager.Pitcher_Cam, ScreenManager.GetInstance()._MainManager.Pos_Defense_2P);
                ScreenManager.GetInstance()._MainManager.Batter_Decid.SetActive(false);
                A_1P = false;
            }
            else if (B_1P || Input.GetKeyDown(KeyCode.Escape))
            {
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CancelB);
                ScreenManager.GetInstance()._MainManager.Batter_Decid.SetActive(false);
                B_1P = false;
            }
        }

        else if ((ScreenManager.GetInstance()._MainManager.Batter_Decid.activeSelf && !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
            || (ScreenManager.GetInstance()._MainManager.Batter_Decid.activeSelf && BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (A_2P || Input.GetKeyDown(KeyCode.Space))
            {
                //ここにゲッターを入れる
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_OkA);
                Debug.Log(AimingPos + "の入力を確認しました！");
                ModeManeger.PitcherMode();
                ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                    ScreenManager.GetInstance()._MainManager.PitcherGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                    ScreenManager.GetInstance()._MainManager.Pitcher_Cam, ScreenManager.GetInstance()._MainManager.Pos_Defense_1P);
                ScreenManager.GetInstance()._MainManager.Batter_Decid.SetActive(false);
                A_2P = false;
            }
            else if (B_2P || Input.GetKeyDown(KeyCode.Escape))
            {
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CancelB);
                ScreenManager.GetInstance()._MainManager.Batter_Decid.SetActive(false);
                B_2P = false;
            }
        }

    }

    public int GetAimingPos()       //こいつも同じタイミングで
    {
        return AimingPos;
    }

    public void SetPoint(int[] i)   //バッターのシーンが始まった瞬間にこいつを呼んでください
    {
        for (int j = 0; j < 9; j++)
        {
            Points[j] = i[j];
            if (Points[j] != 21 && Points[j] != 1 && Points[j] != 6 && Points[j] != 14)
            {
                PointNumbers[j].text = Points[j] + "";
            }
            else if (Points[j] == 21)
            {
                PointNumbers[j].text = "B";
            }
            else if (Points[j] == 1 || Points[j] == 6 || Points[j] == 14)
            {
                PointNumbers[j].text = "?";
            }
        }
    }
}
