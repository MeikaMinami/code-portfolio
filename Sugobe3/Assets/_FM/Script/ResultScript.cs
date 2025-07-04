using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PadInput;

public class ResultScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scores;
    [SerializeField] TextMeshProUGUI ATTotal;
    [SerializeField] TextMeshProUGUI DFTotal;

    [SerializeField] TextMeshProUGUI ATname;
    [SerializeField] TextMeshProUGUI DFname;

    [SerializeField] TextMeshProUGUI ATnameP;
    [SerializeField] TextMeshProUGUI DFnameP;

    [SerializeField] GameObject[] Xs;

    [SerializeField] Image Back;

    public Sprite Douage;
    public int[] scoreQueue;
    public bool was1Pfirst ; //trueやったら１Pが先行ね。
    public bool Sayonara = false;

    private int ATtotalScore = 0;
    private int DFtotalScore = 0;

    private void Start()
    {
        scoreQueue = new int[21];
        //  ここにscoreQueueにぶち込む文を書いてね


        if (was1Pfirst)
        {
            ATnameP.text = "1P";
            DFnameP.text = "2P";
        }
        else
        {
            ATnameP.text = "2P";
            DFnameP.text = "1P";
        }
        
        if (Sayonara)
        {
            Back.sprite = Douage;
        }
    }

    private void Update()
    {
        if (ATtotalScore > DFtotalScore)
        {
            ATTotal.color = Color.yellow;
            ATTotal.fontSize = 350;
            if (Sayonara)
            {
                Xs[0].SetActive(true);
                if (was1Pfirst)
                {
                    ATname.text = "サヨナラ優勝 １P高校";
                }
                else
                {
                    ATname.text = "サヨナラ優勝 ２P高校";
                }
            }
            else
            {
                if (was1Pfirst)
                {
                    ATname.text = "優勝　１P高校";
                }
                else
                {
                    ATname.text = "優勝　２P学園";
                }
            }
            
        }

        else if (ATtotalScore < DFtotalScore)
        {
            DFTotal.color = Color.yellow;
            DFTotal.fontSize = 350;
            if (Sayonara)
            {
                Xs[1].SetActive(true);
                if (was1Pfirst)
                {
                    DFname.text = "サヨナラ優勝 １P高校";
                }
                else
                {
                    DFname.text = "サヨナラ優勝 ２P高校";
                }
            }
            else
            {
                if (was1Pfirst)
                {
                    DFname.text = "優勝　１P高校";
                }
                else
                {
                    DFname.text = "優勝　２P高校";
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || A_1P)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
            #else
                Application.Quit();//ゲームプレイ終了
            #endif
        }
    }

    //=========================================================================================
    public void SetResultScore(int[] score, bool GB, bool isfirst)  //Result()およびGoodByResult()でboolを変更した次の行で呼んでください
    {
        scoreQueue = score;
        Sayonara = GB;
        was1Pfirst = isfirst;
        for (int i = 1; i < 21; i++)
        {
            scores[i].text = scoreQueue[i] + "";
        }
        ATtotalScore = scoreQueue[19];
        DFtotalScore = scoreQueue[20];

        ATTotal.text = "" + ATtotalScore;
        DFTotal.text = "" + DFtotalScore;
    }
}
