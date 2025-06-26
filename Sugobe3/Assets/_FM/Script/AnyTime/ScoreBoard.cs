using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Image[] Balls;
    [SerializeField] Image[] Strikes;
    [SerializeField] Image[] Outs;
    [SerializeField] GameObject[] BaseLight;
    [SerializeField] TextMeshProUGUI[] Points;
    [SerializeField] TextMeshProUGUI[] Innings;
    [SerializeField] TextMeshProUGUI[] Scores;
    [SerializeField] TextMeshProUGUI Souryoku;

    [SerializeField] RectTransform ATPPos;
    [SerializeField] RectTransform DFPPos;
    [SerializeField] TextMeshProUGUI ATPoint;
    [SerializeField] TextMeshProUGUI DFPoint;
    [SerializeField] GameObject PointedEffect;

    private int ATpoint;
    private int DFpoint;
    private float awaiting = 0.0f;
    private bool reverce = false;
    private bool effectEnd = false;
    public bool getPoint = false;
    private float awaiting2 = 0.0f;


    private int?[] LightCount;
    private int[] pointCont;
    private int RushPower;
    public int ballCount;
    public int strCount;
    public int outCount;
    public int hitCount;
    public int inningCount;
    public bool Omote = true;
    private void Awake()
    {
        BaseBallManager.GetInstance().SetScoreBoard(this);
        pointCont = new int[3];
        LightCount = new int?[3];
    }

    private void Update()
    {

        //Debug.Log(awaiting2);
        switch (ballCount) 
        {
            default:
                break;
            case 0:
                Balls[0].color = new Color(0.0f, 0.2f, 0.0f); 
                Balls[1].color = new Color(0.0f, 0.2f, 0.0f); 
                Balls[2].color = new Color(0.0f, 0.2f, 0.0f); 
                break;
            case 1:
                Balls[0].color = new Color(0.0f, 1.0f, 0.0f);
                Balls[1].color = new Color(0.0f, 0.2f, 0.0f);
                Balls[2].color = new Color(0.0f, 0.2f, 0.0f);
                break;
            case 2:
                Balls[0].color = new Color(0.0f, 1.0f, 0.0f);
                Balls[1].color = new Color(0.0f, 1.0f, 0.0f);
                Balls[2].color = new Color(0.0f, 0.2f, 0.0f);
                break;
            case 3:
                Balls[0].color = new Color(0.0f, 1.0f, 0.0f);
                Balls[1].color = new Color(0.0f, 1.0f, 0.0f);
                Balls[2].color = new Color(0.0f, 1.0f, 0.0f);
                break;
            case 4:
                Balls[0].color = new Color(0.0f, 0.2f, 0.0f);
                Balls[1].color = new Color(0.0f, 0.2f, 0.0f);
                Balls[2].color = new Color(0.0f, 0.2f, 0.0f);
                break;
        }

        switch (strCount)
        {
            default:
                break;
            case 0:
                Strikes[0].color = new Color(0.2f, 0.2f, 0.0f);
                Strikes[1].color = new Color(0.2f, 0.2f, 0.0f);
                break;
            case 1:
                Strikes[0].color = new Color(1.0f, 1.0f, 0.0f);
                Strikes[1].color = new Color(0.2f, 0.2f, 0.0f);
                break;
            case 2:
                Strikes[0].color = new Color(1.0f, 1.0f, 0.0f);
                Strikes[1].color = new Color(1.0f, 1.0f, 0.0f);
                break;
            case 3:
                Strikes[0].color = new Color(0.2f, 0.2f, 0.0f);
                Strikes[1].color = new Color(0.2f, 0.2f, 0.0f);
                break;
        }

        switch (outCount)
        {
            default:
                break;
            case 0:
                Outs[0].color = new Color(0.2f, 0.0f, 0.0f);
                Outs[1].color = new Color(0.2f, 0.0f, 0.0f);
                break;
            case 1:
                Outs[0].color = new Color(1.0f, 0.0f, 0.0f);
                Outs[1].color = new Color(0.2f, 0.0f, 0.0f);
                break;
            case 2:
                Outs[0].color = new Color(1.0f, 0.0f, 0.0f);
                Outs[1].color = new Color(1.0f, 0.0f, 0.0f);
                break;
            case 3:
                Outs[0].color = new Color(0.2f, 0.0f, 0.0f);
                Outs[1].color = new Color(0.2f, 0.0f, 0.0f);
                break;
        }

        if (Omote)
        {
            Innings[1].text = "表";
        }
        else
        {
            Innings[1].text = "裏";
        }

        if (pointCont[0] == 0)
        {
            Points[0].text = "";
        }
        else
        {
            Points[0].text = "" + pointCont[0];
        }
        if (pointCont[1] == 0)
        {
            Points[1].text = "";
        }
        else
        {
            Points[1].text = "" + pointCont[1];
        }
        if (pointCont[2] == 0)
        {
            Points[2].text = "";
        }
        else
        {
            Points[2].text = "" + pointCont[2];
        }
        if (pointCont[0] != 0 && pointCont[1] != 0 && pointCont[2] != 0)
        {
            Points[0].text = "" + pointCont[0];
            Points[1].text = "" + pointCont[1];
            Points[2].text = "" + pointCont[2];
        }

        if (LightCount[0] == null)
        {
            BaseLight[0].SetActive(false);
        }
        else if (LightCount[0] != null)
        {
            BaseLight[0].SetActive(true);
        }
        if (LightCount[1] == null)
        {
            BaseLight[1].SetActive(false);
        }
        else if (LightCount[1] != null)
        {
            BaseLight[1].SetActive(true);
        }
        if (LightCount[2] == null)
        {
            BaseLight[2].SetActive(false);
        }
        else if (LightCount[2] != null)
        {
            BaseLight[2].SetActive(true);
        }


        if (getPoint)
        {
            Vector3 ATPpos = ATPPos.eulerAngles;
            Vector3 DFPpos = DFPPos.eulerAngles;
            


            if (awaiting <= 3.5)
            {
                awaiting += Time.deltaTime;
            }
            else if (awaiting > 3.5 && !effectEnd)
            {
                if (Omote)
                {
                    if (ATPpos.y < 90f && !reverce)
                    {
                        ATPpos.y += 1.5f;
                        ATPPos.eulerAngles = ATPpos;
                    }
                    else if (ATPpos.y > 90f)
                    {
                        ATPpos.y = 90f;
                        ATPPos.eulerAngles = ATPpos;
                        ATPoint.text = ATpoint + "";
                        ATPoint.color = Color.yellow;
                        reverce = true;
                    }
                    else if (ATPpos.y > 2f && reverce)
                    {
                        ATPpos.y -= 1.5f;
                        ATPPos.eulerAngles = ATPpos;
                    }
                    else if (ATPpos.y <= 2f && reverce)
                    {
                        reverce = false;
                        ATPpos.y = 0.0f;
                        ATPPos.eulerAngles = ATPpos;
                        effectEnd = true;
                    }
                }

                else
                {
                    if (DFPpos.y < 90f && !reverce)
                    {
                        DFPpos.y += 1.5f;
                        DFPPos.eulerAngles = DFPpos;
                    }
                    else if (DFPpos.y > 90f)
                    {
                        DFPpos.y = 90f;
                        DFPPos.eulerAngles = DFPpos;
                        DFPoint.text = DFpoint + "";
                        DFPoint.color = Color.yellow;
                        reverce = true;
                    }
                    else if (DFPpos.y > 2f && reverce)
                    {
                        DFPpos.y -= 1.5f;
                        DFPPos.eulerAngles = DFPpos;
                    }
                    else if (DFPpos.y <= 2f && reverce)
                    {
                        reverce = false;
                        DFPpos.y = 0.0f;
                        DFPPos.eulerAngles = DFPpos;
                        effectEnd = true;
                    }
                }
            }
            else if (effectEnd)
            {
                if (awaiting2 < 1.0f)
                {
                    awaiting2 += Time.deltaTime;
                }
                else if (awaiting2 >= 1.0f)
                {
                    awaiting = 0.0f;
                    awaiting2 = 0.0f;
                    DFPoint.color = Color.white;
                    ATPoint.color = Color.white;
                    effectEnd = false;
                    getPoint = false;
                    PointedEffect.SetActive(false);
                }
            }
        }
        
    }
    public void SetStrikeCount(int strike) //BBR内からAddStrike()とAddFoul()でintにStrikeCountを入れて呼び出してください。
    {
        strCount = strike;
    }

    public void SetBallCount(int ball)     //ここにAddBall()でBallCountと
    {
        ballCount = ball;
    }

    public void SetOutCount(int Out)       //ここにAddOut()でOutCountも同様にお願いします。
    {
        outCount = Out;
    }                                                                               //バッターアウトや3ストライクのタイミングでもお願いします。

    public void SetRushPower(int rushPower)       //ここに走力の合計を走力が計算されるタイミングで入れてください。
    {
        RushPower = rushPower;
        Souryoku.text = RushPower + "";
    }

    public void SetIsOmote(bool omote)       //ここにチェンジのタイミングでisOmoteの値をいれてください。
    {
        Omote = omote;
    }

    public void SetInning(int inning)       //イニング数を入れてください。
    {
        inningCount = inning;
        Innings[0].text = "" + inningCount;
    }

    public void SetATScore(int atscore)       //表側の人の点数を入れてください。(多分)AddScoreで呼べばいいと思います。
    {
        ATpoint = atscore;
        Scores[0].text = "" + atscore;
        getPoint = true;
        PointedEffect.SetActive(true);
    }

    public void SetDFScore(int dfscore)       //裏側の人の点数を入れてください。(多分)AddScoreで呼べばいいと思います。
    {
        DFpoint = dfscore;
        Scores[1].text = "" + dfscore;
        getPoint = true;
        PointedEffect.SetActive(true);
    }

    //===========================================================================================================================

    public void SetRunner(int?[] runner)       //多分最後のセッターです。BaseBall内の下のほーのRunしてるところで読んじゃってください。
    {
        LightCount = runner;
    }

    public void SetPointer(int[] point)
    {
        pointCont[0] = point[0];
        pointCont[1] = point[1];
        pointCont[2] = point[2];
    }
}
