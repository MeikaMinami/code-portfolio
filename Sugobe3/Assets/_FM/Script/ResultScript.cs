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
    public bool was1Pfirst ; //true‚â‚Á‚½‚ç‚PP‚ªæs‚ËB
    public bool Sayonara = false;

    private int ATtotalScore = 0;
    private int DFtotalScore = 0;

    private void Start()
    {
        scoreQueue = new int[21];
        //  ‚±‚±‚ÉscoreQueue‚É‚Ô‚¿‚Ş•¶‚ğ‘‚¢‚Ä‚Ë


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
                    ATname.text = "ƒTƒˆƒiƒ‰—DŸ ‚PP‚Z";
                }
                else
                {
                    ATname.text = "ƒTƒˆƒiƒ‰—DŸ ‚QP‚Z";
                }
            }
            else
            {
                if (was1Pfirst)
                {
                    ATname.text = "—DŸ@‚PP‚Z";
                }
                else
                {
                    ATname.text = "—DŸ@‚QPŠw‰€";
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
                    DFname.text = "ƒTƒˆƒiƒ‰—DŸ ‚PP‚Z";
                }
                else
                {
                    DFname.text = "ƒTƒˆƒiƒ‰—DŸ ‚QP‚Z";
                }
            }
            else
            {
                if (was1Pfirst)
                {
                    DFname.text = "—DŸ@‚PP‚Z";
                }
                else
                {
                    DFname.text = "—DŸ@‚QP‚Z";
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || A_1P)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ƒQ[ƒ€ƒvƒŒƒCI—¹
            #else
                Application.Quit();//ƒQ[ƒ€ƒvƒŒƒCI—¹
            #endif
        }
    }

    //=========================================================================================
    public void SetResultScore(int[] score, bool GB, bool isfirst)  //Result()‚¨‚æ‚ÑGoodByResult()‚Åbool‚ğ•ÏX‚µ‚½Ÿ‚Ìs‚ÅŒÄ‚ñ‚Å‚­‚¾‚³‚¢
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
