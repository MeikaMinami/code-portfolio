using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PadInput;

public class character2P : MonoBehaviour
{
    [SerializeField] GameObject decidePanel;
    [SerializeField] Image Yes;
    [SerializeField] Image No;
    private int ynindex2P = 0;

    [SerializeField] Outline[] selectedChara; 
    private static int[,] selectedIndex;
    private int x = 0;
    private int y = 0;

    private int[] beforeIndex;

    private int QueueNum = 0;

    [SerializeField] TextMeshProUGUI[] charaNumber;
    [SerializeField] GameObject[] Selected;
    [SerializeField] Image[] SelectColor;
    private string[] charaName;
    private int[] batterName2P;
    public List<int> batterList2P; //この中に2PのバッターのIDが入ってまーす！

    CharacterManager characterManager;

    public bool decide2P = false;

    [SerializeField] GameObject READY2;

    [SerializeField] TextMeshProUGUI[] charaNameList;
    private void Start()
    {
        selectedIndex = new int[3, 3];
        beforeIndex = new int[9];
        batterName2P = new int[9];
        charaName = new string[9];
        batterList2P = batterName2P.ToList();
        characterManager = gameObject.AddComponent<CharacterManager>();
    }
    private void Update()
    {
        //Debug.Log(selectedIndex[y, x]);
        
        selectedIndex[y, x] = y * 3 + x;

        selectedChara[selectedIndex[y, x]].enabled = true;

        if (!decidePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRight_2P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossRight_2P = false;

                x++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossLeft_2P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossLeft_2P = false;

                x--;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || CrossDown_2P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossDown_2P = false;

                y++;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || CrossUp_2P)
            {
                selectedChara[selectedIndex[y, x]].enabled = false;
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossUp_2P = false;
                y--;
            }

            else if (Input.GetKeyDown(KeyCode.Space) || A_2P)
            {
                if (!Selected[selectedIndex[y, x]].activeSelf)   //charaNumber[selectedIndex[y, x]].enabled == falseから変更 02051631
                {
                    switch (selectedIndex[y, x])
                    {
                        default:
                            break;
                        case 0:
                            NumberCheck(QueueNum, "フィレ", selectedIndex[y, x]);//もしキャラクターに名前が出来たら真ん中の""にぶち込んどいてください
                            break;
                        case 1:
                            NumberCheck(QueueNum, "れいか", selectedIndex[y, x]);
                            break;
                        case 2:
                            NumberCheck(QueueNum, "あやめ", selectedIndex[y, x]);
                            break;
                        case 3:
                            NumberCheck(QueueNum, "ほうか", selectedIndex[y, x]);
                            break;
                        case 4:
                            NumberCheck(QueueNum, "うつほ", selectedIndex[y, x]);
                            break;
                        case 5:
                            NumberCheck(QueueNum, "ゆうり", selectedIndex[y, x]);
                            break;
                        case 6:
                            NumberCheck(QueueNum, "ゆず", selectedIndex[y, x]);
                            break;
                        case 7:
                            NumberCheck(QueueNum, "いお", selectedIndex[y, x]);
                            break;
                        case 8:
                            NumberCheck(QueueNum, "ようよ", selectedIndex[y, x]);
                            break;
                    }
                    beforeIndex[QueueNum] = selectedIndex[y, x];
                    QueueNum++;
                }
                A_2P = false;
            }

            else if (Input.GetKeyDown(KeyCode.Backspace) || B_2P)
            {
                if (QueueNum >= 1)
                {
                    if (Selected[beforeIndex[QueueNum - 1]].activeSelf) //charaNumber[beforeIndex[QueueNum - 1]].enabled == trueから変更   02051632
                    {
                        QueueNum--;
                        NumberBack(QueueNum, beforeIndex[QueueNum]);
                    }
                }
                
                B_2P = false;
            }

            else if (Input.GetKeyDown(KeyCode.S) || X_2P)
            {
                QueueNum = 0;
                for (int i = 0; i < 9; i++)
                {
                    batterList2P[i] = 0;
                    Selected[i].SetActive(false);
                    SelectColor[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }

                X_2P = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || (RB_2P && LB_2P))
            {
                if (QueueNum == 9)
                {
                    decidePanel.SetActive(true);
                    ynindex2P = 1;
                    for (int i = 0; i < 9; i++)
                    {
                        charaNameList[i].text = "" + charaName[i];
                    }
                }
                RB_2P = false;
                LB_2P = false;
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

        else if (!decide2P)
        {
            switch (ynindex2P)
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
            if (Input.GetKeyDown(KeyCode.DownArrow) || CrossDown_2P)
            {
                ynindex2P++;
                if (ynindex2P >= 2)
                {
                    ynindex2P = 0;
                }
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossDown_2P = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || CrossUp_2P)
            {
                ynindex2P--;
                if (ynindex2P <= -1)
                {
                    ynindex2P = 1;
                }
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CharacterSelect);
                CrossUp_2P = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) || A_2P)
            {
                if (ynindex2P == 0)
                {
                    Debug.Log("キャラ選択完了！");
                    for (int i = 9; i < 18; i++)
                    {
                        characterManager.SetSelectId(batterList2P[i - 9], i);
                        //CharacterData.SetSelectId(i);
                        //CharacterData.SetCharaId(batterList2P[i-9]);
                        decide2P = true;
                    }
                }
                else if (ynindex2P == 1)
                {
                    decidePanel.SetActive(false);
                }
                A_2P = false;
            }
        }

        else if (decide2P)
        {
            READY2.SetActive(true);
        }
    }
    void NumberCheck(int i, string j, int l)
    {
        batterList2P[i] = l;
        charaName[i] = j;
        charaNumber[l].text = (i + 1) + "番目";   //"No." + (i + 1)から変更　02051549
        Selected[l].SetActive(true);  //charaNumber[selectedIndex[y, x]].enabled == trueから変更   02051632
        SelectColor[l].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);   //追加　02061353
    }

    void NumberBack(int i, int l)
    {
        batterList2P.RemoveAt(i);
        batterList2P.Insert(i, 0);
        charaNumber[l].text = "";
        Selected[l].SetActive(false);    //charaNumber[l] == falseから変更   02051633
        SelectColor[l].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);   //追加　02061353
    }
}
