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
    public bool was1Pfirst ; //true�������PP����s�ˁB
    public bool Sayonara = false;

    private int ATtotalScore = 0;
    private int DFtotalScore = 0;

    private void Start()
    {
        scoreQueue = new int[21];
        //  ������scoreQueue�ɂԂ����ޕ��������Ă�


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
                    ATname.text = "�T���i���D�� �PP���Z";
                }
                else
                {
                    ATname.text = "�T���i���D�� �QP���Z";
                }
            }
            else
            {
                if (was1Pfirst)
                {
                    ATname.text = "�D���@�PP���Z";
                }
                else
                {
                    ATname.text = "�D���@�QP�w��";
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
                    DFname.text = "�T���i���D�� �PP���Z";
                }
                else
                {
                    DFname.text = "�T���i���D�� �QP���Z";
                }
            }
            else
            {
                if (was1Pfirst)
                {
                    DFname.text = "�D���@�PP���Z";
                }
                else
                {
                    DFname.text = "�D���@�QP���Z";
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || A_1P)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
            #else
                Application.Quit();//�Q�[���v���C�I��
            #endif
        }
    }

    //=========================================================================================
    public void SetResultScore(int[] score, bool GB, bool isfirst)  //Result()�����GoodByResult()��bool��ύX�������̍s�ŌĂ�ł�������
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
