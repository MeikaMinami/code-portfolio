using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PadInput;

public class character1P : MonoBehaviour
{
    [SerializeField] GameObject decidePanel;
    [SerializeField] Image Yes;
    [SerializeField] Image No;

    [SerializeField] GameObject Flash;

    private int ynindex1P = 0;

    [SerializeField] Outline[] selectedChara; 
    private int[,] selectedIndex;

    private int[] beforeIndex;

    private int x = 0;
    private int y = 0;

    private int QueueNum = 0;

    [SerializeField] TextMeshProUGUI[] charaNumber;
    [SerializeField] GameObject[] Selected;
    [SerializeField] Image[] SelectColor;
    private string[] charaName;
    private int[] batterName1P;
    public List<int> batterList1P; //この中に1PのバッターのIDが入ってまーす！

    [SerializeField] TextMeshProUGUI[] charaNameList;

    CharacterManager characterManager;

    public bool decide1P = false;

    [SerializeField] GameObject READY1;

    private void Start()
    {
        selectedIndex = new int[3, 3];
        beforeIndex = new int[9];
        batterName1P = new int[9];
        charaName = new string[9];
        batterList1P = batterName1P.ToList();
        characterManager = gameObject.AddComponent<CharacterManager>();
    }
    private void Update()
    {
        //Debug.Log(string.Join(",", batterList1P));
        //Debug.Log(selectedIndex[y, x]);
        selectedIndex[y, x] = y * 3 + x;

        selectedChara[selectedIndex[y, x]].enabled = true;

        if (!decidePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRight_1P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossRight_1P = false;

                x++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossLeft_1P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossLeft_1P = false;

                x--;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || CrossDown_1P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossDown_1P = false;

                y++;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || CrossUp_1P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossUp_1P = false;
                y--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) || A_1P)
            {
                if (!Selected[selectedIndex[y, x]].activeSelf) //charaNumber[selectedIndex[y, x]].enabled == falseから変更 02051556
                {
                    switch (selectedIndex[y, x])
                    {
                        default:
                            break;
                        case 0:
                            NumberCheck(QueueNum, "るるな", selectedIndex[y, x]);//もしキャラクターに名前が出来たら真ん中の""にぶち込んどいてください
                            break;
                        case 1:
                            NumberCheck(QueueNum, "ひな", selectedIndex[y, x]);
                            break;
                        case 2:
                            NumberCheck(QueueNum, "クリスティ", selectedIndex[y, x]);
                            break;
                        case 3:
                            NumberCheck(QueueNum, "妙音", selectedIndex[y, x]);
                            break;
                        case 4:
                            NumberCheck(QueueNum, "飛燕", selectedIndex[y, x]);
                            break;
                        case 5:
                            NumberCheck(QueueNum, "夕瑠", selectedIndex[y, x]);
                            break;
                        case 6:
                            NumberCheck(QueueNum, "ここな", selectedIndex[y, x]);
                            break;
                        case 7:
                            NumberCheck(QueueNum, "じぇろ", selectedIndex[y, x]);
                            break;
                        case 8:
                            NumberCheck(QueueNum, "ハル", selectedIndex[y, x]);
                            break;
                    }
                    beforeIndex[QueueNum] = selectedIndex[y, x];
                    QueueNum++;
                }

                A_1P = false;
            }

            else if (Input.GetKeyDown(KeyCode.D) || B_1P)
            {
                if (QueueNum >= 1)
                {
                    if (Selected[beforeIndex[QueueNum - 1]].activeSelf) //charaNumber[beforeIndex[QueueNum - 1]].enabled == trueから変更   02051557
                    {
                        QueueNum--;
                        NumberBack(QueueNum, beforeIndex[QueueNum]);
                    }
                }

                B_1P = false;
            }

            else if (Input.GetKeyDown(KeyCode.S) || X_1P)
            {
                QueueNum = 0;
                for (int i = 0; i < 9; i++)
                {
                    batterList1P[i] = 0;
                    Selected[i].SetActive(false);
                    SelectColor[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }

                X_1P = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || (RB_1P && LB_1P))
            {
                if (QueueNum == 9) 
                {
                    decidePanel.SetActive(true);
                    ynindex1P = 1;

                    for (int i = 0; i < 9 ; i++)
                    {
                        charaNameList[i].text = "" + charaName[i];
                    }
                }
                RB_1P = false;
                LB_1P = false;  
            }

            if (x > 2)
            {
                x = 0;
            }
            else if (x < 0)
            {
                x = 2;
            }
            else if (y > 2)
            {
                y = 0;
            }
            else if (y < 0)
            {
                y = 2;
            }
        }

        else if (!decide1P)
        {
            switch (ynindex1P)
            {
                case 0:
                    Yes.color = Color.red;
                    No.color = Color.green;
                    break;
                case 1:
                    Yes.color = Color.green;
                    No.color = Color.red;
                    break;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || CrossDown_1P)
            {
                ynindex1P++;
                if (ynindex1P >= 2)
                {
                    ynindex1P = 0;
                }
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossDown_1P = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || CrossUp_1P)
            {
                ynindex1P--;
                if (ynindex1P <= -1)
                {
                    ynindex1P = 1;
                }
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossUp_1P = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) || A_1P)
            {
                if (ynindex1P == 0)
                {
                    Debug.Log("キャラ選択完了！");
                    for (int i = 0; i < 9; i++)
                    {
                        characterManager.SetSelectId(batterList1P[i], i);
                        //CharacterData.SetSelectId(i);
                        //CharacterData.SetCharaId(batterList1P[i]);
                        Debug.Log("ID : " + batterList1P[i] + "のやつは" + i + "番目");
                        decide1P = true;
                    }
                }
                else if (ynindex1P == 1)
                {
                    decidePanel.SetActive(false);
                }
                A_1P = false;
            }

        }

        else if (decide1P)
        {
            READY1.SetActive(true);
        }
    }

    void NumberCheck (int i ,string j ,int l)
    {
        batterList1P[i] = l;
        charaName[i] = j;
        charaNumber[l].text = (i + 1) + "番目";   //"No." + (i + 1)から変更　02051549
        Selected[l].SetActive(true);  //charaNumber[selectedIndex[y, x]].enabled == trueから変更   02051628
        SelectColor[l].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);   //追加　02061353
    }

    void NumberBack(int i,  int l)
    {
        batterList1P.RemoveAt(i);
        batterList1P.Insert(i,0);
        charaNumber[l].text = "";
        Selected[l].SetActive(false);   //charaNumber[l] == falseから変更   02051628
        SelectColor[l].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);   //追加　02061353
    }
}
