using System.Collections.Generic;
using UnityEngine;
using static PadInput;

public class PitcherSelect : MonoBehaviour
{
    /// <summary>
    /// 現在の選択マス番号
    /// </summary>
    private int Pitch_SelectNum = 0;

    /// <summary>
    /// 1Pの選択済みのマスを管理
    /// </summary>
    private List<int> Selected1PTiles = new List<int>();

    /// <summary>
    /// 2Pの選択済みのマスを管理
    /// </summary>
    private List<int> Selected2PTiles = new List<int>();


    /// <summary>
    /// 一度目の選択かどうかを管理
    /// </summary>
    private bool IsFirstSelection = true;

    /// <summary>
    /// 確認画面が表示されているかどうかを管理
    /// </summary>
    private bool IsConfirmationPanelActive = false;

    /// <summary>
    /// PitchCursor配列を使ってアウトラインを設定します。
    /// </summary>
    public GameObject[] PitchCursor;

    /// <summary>
    /// PitchCursor配列を使ってアウトラインを設定します。
    /// </summary>
    public GameObject[] PitchSelectedUI;

    /// <summary>
    /// 移動範囲用の配列
    /// </summary>
    private int x = 0;
    private int y = 0;
    private int[,] selectedIndex;

    private BaseBall baseBall; //Baseballコンポーネントの参照

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        selectedIndex = new int[3, 3];

        // selectedIndex 配列を初期化
        ConvertToSelectedIndex();

        if(baseBall == null)
        {
            baseBall = BaseBallManager.GetInstance()._BaseBall;
        }

        Debug.Log("初期化完了");
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        if (ModeManeger.ModeAccess.NowMode == ModeManeger.AllMode.Pitcher_Mode) //Pitcher_Modeの時
        {
            if (!IsConfirmationPanelActive)
            {
                InputCheck(); //入力チェック処理
                Swicher(); //選択マスを更新
                if (Selected1PTiles.Count == 5)
                {
                    ResetPriNum();
                }
                if (Selected2PTiles.Count == 5)
                {
                    ResetPriNum();
                }

                //1Pが後攻で表の時もしくは1Pが先攻で裏の時
                if ((!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
                {
                    // エンターキーで確認画面を表示
                    if (LB_1P && RB_1P)
                    {
                        if (Selected1PTiles.Contains(Pitch_SelectNum))
                        {
                            Debug.Log("このマスは既に選択されています。");
                            AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSamePoint);
                            return;
                        }
                        ConfirmPanel(); //確認画面表示
                    }
                }

                //1Pが先攻で表の時もしくは1Pが後攻で裏の時
                if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
                {
                    // エンターキーで確認画面を表示
                    if (LB_2P && RB_2P)
                    {
                        if (Selected2PTiles.Contains(Pitch_SelectNum))
                        {
                            Debug.Log("このマスは既に選択されています。");
                            AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSamePoint);
                            return;
                        }
                        ConfirmPanel(); //確認画面表示
                    }
                }
            }
            else
            {
                ConfirmHandle(); //確認画面の決定・キャンセル処理
            }
        }
        // UIの更新
    }

    /// <summary>
    /// 入力チェック処理
    /// </summary>
    private void InputCheck()
    {
        //これはSEの再生処理


        //1Pが後攻で表の時もしくは1Pが先攻で裏の時
        if ((!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && y <= -1 || CrossUp_1P && y <= -1)
            {
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
                y++;//上に移動
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && y >= -1 || CrossDown_1P && y >= -1)
            {
                y--;//下に移動
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && x <= 1 || CrossRight_1P && x <= 1)
            {
                x++;//右に移動
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && x >= 1 || CrossLeft_1P && x >= 1)
            {
                x--;//左に移動
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
        }

        //1Pが先攻で表の時もしくは1Pが後攻で裏の時
        if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && y <= -1 || CrossUp_2P && y <= -1)
            {
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
                y++;//上に移動
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && y >= -1 || CrossDown_2P && y >= -1)
            {
                y--;//下に移動
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && x <= 1 || CrossRight_2P && x <= 1)
            {
                x++;//右に移動
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && x >= 1 || CrossLeft_2P && x >= 1)
            {
                x--;//左に移動
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
        }
    }

    /// <summary>
    /// 確認画面の選択処理
    /// </summary>
    private void ConfirmHandle()
    {
        //1Pが先攻で裏の時もしくは1Pが後攻で表の時
        if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.Y) || A_1P)
            {
                if (!Selected1PTiles.Contains(Pitch_SelectNum))//既に選択したマスと同じマスを選択していないとき
                {
                    Selected1PTiles.Add(Pitch_SelectNum);   //選択済みマスに現在のマスを追加
                    // 2Pの選択済みマスに対応するUIを表示
                    foreach (var index in Selected1PTiles)
                    {
                        if (index >= 0 && index < PitchSelectedUI.Length)//配列の範囲内のみ有効化
                        {
                            PitchSelectedUI[index].SetActive(true);
                        }
                    }
                }
                //確認パネル非表示
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);

                baseBall.SetPitcherSekect();//選択確定処理
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_OkA);//決定音再生
                Debug.Log("選択確定: " + Pitch_SelectNum);
            }
            else if (Input.GetKeyDown(KeyCode.N) || B_1P)
            {
                //キャンセル処理
                //確認パネル非表示
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);
                IsFirstSelection = true;//一度目の選択状態に戻す
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CancelB);//キャンセル音再生
                Debug.Log("選択キャンセル");
            }
        }

        //1Pが先攻で表の時もしくは1Pが後攻で裏の時
        if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.Y) || A_2P)
            {
                if (!Selected2PTiles.Contains(Pitch_SelectNum))
                {
                    Selected2PTiles.Add(Pitch_SelectNum);  //選択済みマスに現在のマスを追加
                    // 2Pの選択済みマスに対応するUIを表示
                    foreach (var index in Selected2PTiles)
                    {
                        if (index >= 0 && index < PitchSelectedUI.Length)//配列の範囲内のみ有効化
                        {
                            PitchSelectedUI[index].SetActive(true);
                        }
                    }
                }
                //確認パネル非表示
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);

                baseBall.SetPitcherSekect();//選択確定処理
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_OkA);//決定音再生

                Debug.Log("選択確定: " + Pitch_SelectNum);
            }
            else if (Input.GetKeyDown(KeyCode.N) || B_2P)
            {
                // キャンセル処理
                //確認パネル非表示
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);
                IsFirstSelection = true;//一度目の状態に戻す
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CancelB);//キャンセル音再生
                Debug.Log("選択キャンセル");
            }
        }
    }


    /// <summary>
    /// 確認画面を表示
    /// </summary>
    private void ConfirmPanel()
    {
        IsConfirmationPanelActive = true;//管理画面アクティブフラグ
        ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(true);//確認画面UI表示
        AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_LBRB);//確認画面音（LB＋RB）

        Debug.Log("確認画面表示: " + Pitch_SelectNum);
    }

    /// <summary>
    /// 現在の選択マスを更新
    /// </summary>
    private void Swicher()
    {
        switch (x, y)
        {
            case (0, 0):
                SetNowNum(0);
                break;
            case (1, 0):
                SetNowNum(1);
                break;
            case (2, 0):
                SetNowNum(2);
                break;
            case (0, -1):
                SetNowNum(3);
                break;
            case (1, -1):
                SetNowNum(4);
                break;
            case (2, -1):
                SetNowNum(5);
                break;
            case (0, -2):
                SetNowNum(6);
                break;
            case (1, -2):
                SetNowNum(7);
                break;
            case (2, -2):
                SetNowNum(8);
                break;
            default:
                SetNowNum(0);
                SetNowNum(2);
                SetNowNum(6);
                SetNowNum(8);
                break;
        }
    }
    /// <summary>
    /// selectedIndex 配列を初期化（二次元インデックスから一次元インデックスへの変換）
    /// </summary>
    private void ConvertToSelectedIndex()
    {
        int value = 0;//0～8のインデックス
        for (int y = 0; y < 3; y++)//行ループ
        {
            for (int x = 0; x < 3; x++)//列ループ
            {
                selectedIndex[x, 2 - y] = value;
                value++;
            }
        }
    }

    /// <summary>
    /// 指定されたインデックスの要素のみを true にし、他の要素を false にする関数
    /// </summary>
    /// <param name="activeIndex">アクティブにするインデックス</param>
    private void SetNowNum(int activeIndex)
    {
        for (int i = 0; i < PitchCursor.Length; i++)
        {
            PitchCursor[i].SetActive(i == activeIndex);
        }
        if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst)//1Pが先攻の場合
        {
            if (IsFirstSelection)//最初の選択の時のみ更新
            {
                Pitch_SelectNum = activeIndex; //現在の選択を更新
                                               //そのままの番号を使用
            }
        }
        else if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst)//2Pが先攻の場合
        {
            if (IsFirstSelection)//最初の選択の時のみ更新
            {
                Pitch_SelectNum = activeIndex + 9; //現在の選択を更新
                                                   //2P用に9を加算して9～17を使用
            }
        }
    }


    #region ＝＝＝＝＝＝＝ ＝＝＝＝＝＝＝　　Public Methods　　＝＝＝＝＝＝＝ ＝＝＝＝＝＝＝→→→→→→→

    /// <summary>
    /// 選択したマスの番号を設定する関数
    /// </summary>
    /// <param name="selected">設定するマスの番号</param>
    public int GetSelectedTile()
    {
        return Pitch_SelectNum;//選択中のマス番号を返す
    }


    /// <summary>
    /// 2人の選択マスをリセットする関数（Resetはこの関数のみ使用）
    /// </summary>
    public void ResetPriNum()
    {
        Selected1PTiles.Clear();//1Pが選択したマスのリストを初期化
        Selected2PTiles.Clear();//2Pが選択したマスのリストを初期化
        Pitch_SelectNum = -1;//選択番号を-1（未選択状態）に戻す
        IsFirstSelection = true; //リセット時に一度目の選択状態に戻す
        Debug.Log("選択マスをリセット: " + Pitch_SelectNum.ToString());
        //すべてのUIを非表示にする
        foreach (var uiElement in PitchSelectedUI)
        {
            uiElement.SetActive(false);//UIを非アクティブ化
        }
    }

    #endregion＝＝＝＝＝＝＝ ＝＝＝＝＝＝＝　　Public Methods　　＝＝＝＝＝＝＝ ＝＝＝＝＝＝＝→→→→→→→

}
