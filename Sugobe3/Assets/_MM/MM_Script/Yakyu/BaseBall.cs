using UnityEngine;
using static CharacterData;
using static ModeManeger;

public class BaseBall : MonoBehaviour
{
    public bool is1Pfirst;  //1Pが先攻か
    bool isPitcherSelect = false;  //ピッチャーがマスを決めたか

    bool isHit = false;  //バッターが当てたか
    int[] Souryoku = { 0, 0, 0 };  //３つの走力を入れるところ

    int[,] zone = new int[3,3];  //ストライクゾーンに出てくる3*3を入れるところ

    int batterPos;  //バッターがどの軸を選んだか、横軸上から0~2、縦軸左から3~5

    int pitchNum;  //ピッチャーが投げたとこ、0~8

    public bool isZoneRefreash = false;  //ゾーンが更新されたか

    public int?[] runer = new int?[3];  //ランナーを管理するnull許容型配列, 0~2

    int[] ZoneInfo = new int[9];  //UIにストライクゾーン情報を送るようの一次元配列

    public int nowBatterID;  //今のバッターのID
    int now1PBatterNum = 0;  //今の1Pのバッターの打順、０～８
    int now2PBatterNum = 9;  //今の2Pのバッターの打順、０～８
    int HitSouryoku = -1;  //当たったとこの数字
    int SouryokuAmari;//走力の余りをイベントなどに活用？

    bool isDajyunSet = false;  //打順をセットしたか

    //デバック用打順
    //int[] DebugDajyun = new int[18];

    public int[] Dajyun = new int[18];  //全員の打順を入れる、０～１７

    private Ability ability;//能力管理

    public CharacterManager characterManager;//キャラクター管理

    [SerializeField]
    private PitcherSelect pitcherSelect;//ピッチャーの入力管理
    [SerializeField]
    private ATDFScript atdfScript;//先攻後攻管理
    [SerializeField]
    private BatterScript batterScript;//バッター入力管理
    [SerializeField]
    private character1P chara1P;//1P
    [SerializeField]
    private character2P chara2P;//2P

    private void Awake()
    {
        BaseBallManager.GetInstance().SetBaseBall(this);  //このスクリプトをBaseBallManagerに
        Debug.Log("BaseBallスクリプトをBaseBallManagerにセットしました");
    }

    void Start()
    {
        //各スクリプトの初期化（アタッチされてなければ追加）
        if (ability == null)
        {
            ability = gameObject.AddComponent<Ability>();
        }
        if (characterManager == null)
        {
            characterManager = gameObject.AddComponent<CharacterManager>();
        }
        if (atdfScript == null)
        {
            atdfScript = gameObject.GetComponent<ATDFScript>();
        }
        if (batterScript == null)
        {
            batterScript = gameObject.GetComponent<BatterScript>();
        }
        if (pitcherSelect == null)
        {
            pitcherSelect = gameObject.GetComponent<PitcherSelect>();
        }
        if (chara1P == null)
        {
            chara1P = gameObject.GetComponent<character1P>();
        }
        if (chara2P == null)
        {
            chara2P = gameObject.GetComponent<character2P>();
        }
        WhoFirst();  //最初はタイトルモード
        Debug.Log("まず先攻後攻選択モードにしました。");
        Debug.Log("先攻後攻を決めるには1PがLB + RBを押してください");

 
 
 
 
 
 //Debug
        /*for(int i = 0; i < 18; i++)  //デバック用打順を設定
        {
            DebugDajyun[i] = i;
            //Debug.Log("デバック用打順" + DebugDajyun[i] + "を" + i + "に設定しました");
        }*/
    }
    
    void Update()
    {
        if (ModeAccess.NowMode == AllMode.Batter_Mode)//バッターモードのとき
        {
            if(!isDajyunSet)  //打順のセットが終わっていなかったら
            {
                //打順を取得
                for (int i = 0; i < 9; i++)  //1Pの打順を０～８に入れる
                {
                    Dajyun[i] = chara1P.batterList1P[i];
                }
                for (int i = 9; i < 18; i++)  //2Pの打順を９～１７に入れる
                {
                    Dajyun[i] = chara2P.batterList2P[i-9] + 9;
                }
                isDajyunSet = true;

                if(is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが先攻の場合
                {
                    //nowバッターIDを最初の設定
                    nowBatterID = Dajyun[0];  //今のバッターのIDを取得
                    Debug.Log("今のバッターのIDは" + nowBatterID + "です");
                }
                else if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが先攻の場合
                {
                    nowBatterID = Dajyun[9];  //今のバッターのIDを取得
                    Debug.Log("今のバッターのIDは" + nowBatterID + "です");
                }
            }

            if (!isZoneRefreash)  //ストライクゾーンの更新が終わっていなかったら
            {
                Debug.Log(nowBatterID);
                zone = ability.TypeZone(characterManager.GetCharaType(nowBatterID));//ストライクゾーンの数字を更新

                //UI用に中身を送信
                batterScript.SetPoint(To1jiHairetu());

                isZoneRefreash = true;
            }
        }


        if(ModeAccess.NowMode == AllMode.Pitcher_Mode)
        {
            if(isPitcherSelect)   //ピッチャーが選び終わっていたら
            {
                HitJudge();  //ヒットかどうかの判定
            }
        }
    }
    //Debug用
    /*    void ZoneRandom()  //ストライクゾーンの数字を更新
        {
            string debugMessage = "ストライクゾーン:\n"; // デバッグ用メッセージ
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    zone[i, j] = Random.Range(1, 22); // 1~21, ランダム仮置き
                    debugMessage += zone[i, j].ToString("00") + " "; // 2桁で表示
                }
                debugMessage += "\n"; // 改行を追加
            }
            Debug.Log(debugMessage);
            Debug.Log("ストライクゾーンの更新を行いました");
            isZoneRefreash = true;
            isPitcherSelect = false;
            Debug.Log("バッターは列を選択してください");
        }*/
    //Debug用

    private int[] To1jiHairetu()  //UI用に、ストライクゾーンの情報を一次元配列に変換するメソッド
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                 ZoneInfo[j + (i * 3)] = zone[i, j];
            }
        }
        return ZoneInfo;
    }


    void HitJudge()  //ヒットかどうかの判定
    {
        if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//表の時
        {
            batterPos = batterScript.GetAimingPos();  //UIからバッターが選んだ軸を受けとる、０～５
        }
        else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//裏の時
        {
            batterPos = batterScript.GetAimingPos() - 6;  //UIからバッターが選んだ軸を受けとる、6～11
        }
        Debug.Log("バッターが選んだ軸は" + batterPos + "です");
        pitchNum = pitcherSelect.GetSelectedTile();  //ピッチャーが投げた場所を受け取る、０～17
        if (pitchNum >= 9)//上限調整
        {
            pitchNum -= 9;
        }
        if (batterPos < 3)  //横軸が選ばれた場合
        {
            int pitchRow;  //ピッチャーが投げた段
            int pitchCol;  //ピッチャーが投げた列

            //ピッチャーが投げた場所を特定
            if (pitchNum ==　0)  //０は割れないので
            {
                pitchRow = 0;
                pitchCol = 0;
            }
            else
            {
                pitchRow = pitchNum / 3;  //段を求める
                pitchCol = pitchNum % 3;  //列を求める
            }

            int HitSouryoku = -1;//初期化

            for (int line = 0; line < 3; line++)
            {
                if (pitchRow == batterPos)  //横軸が同じ
                {
                    isHit = true;//ヒット判定
                }
                else
                {
                    isHit = false;//非ヒット判定
                }
            }
            if (isHit)
            {
                Debug.Log("同じ横軸を選びました！当たり！");
                HitSouryoku = zone[pitchRow, pitchCol];//当たった時のマスの目を走力として記録
                Debug.Log("獲得した走力：{" + HitSouryoku + "}");
                SouryokuCount(HitSouryoku);//走力カウント処理へ
            }
            else if (!isHit)
            {
                Debug.Log("はずれ！");
                BaseBallManager.GetInstance()._BBR.AddStrike();  //ストライクを加算
                BatterMode();  //バッターモードへ

                if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//表の時
                {
                    //モデルScreen切り替え
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                }
                else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//裏の時
                {
                    //モデルScreen切り替え
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                }
            }
        }

        else if(2 < batterPos)  //縦軸が選ばれた場合
        {
            isHit = false;
            int pitchRow;  //ピッチャーが投げた段
            int pitchCol;  //ピッチャーが投げた列

            //ピッチャーが投げた場所を特定
            if (pitchNum == 0)  //０は割れないので
            {
                pitchRow = 0;
                pitchCol = 0;
            }
            else
            {
                pitchRow = pitchNum / 3;  //段を求める
                pitchCol = pitchNum % 3;  //列を求める
            }

            for (int line = 0; line < 3; line++)
            {
                if(pitchCol == batterPos - 3)  //縦軸が同じ
                {
                    isHit = true;//ヒット判定
                }
                else
                {
                    isHit = false;//非ヒット判定
                }
            }
            if (isHit)
            {
                Debug.Log("同じ縦軸を選びました！当たり！");
                HitSouryoku = zone[pitchRow, pitchCol];//当たったマスの目を走力として記録
                Debug.Log("獲得した走力：{" + HitSouryoku + "}");
                SouryokuCount(HitSouryoku);//走力カウント処理
            }
            else if (!isHit)
            {
                Debug.Log("はずれ！");
                BaseBallManager.GetInstance()._BBR.AddStrike();  //ストライクを加算
                BatterMode();  //バッターモードへ

                if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//表の時
                {
                    //モデルScreen切り替え
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                }
                else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//裏の時
                {
                    //モデルScreen切り替え
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                }
            }
        }
        isPitcherSelect = false;  //フラグを初期化
    }

    void HitLevel(int souryoku)  //どれぐらいのヒットなのか
    {
        if (characterManager.GetCharaType(nowBatterID) == CharaType.Speed)  //今のバッターがスピードタイプの場合
        {
            souryoku += 2;  //走力に２を加算
            Debug.Log("スピードタイプのバッターです！走力に２を加算しました！");
        }
        if (souryoku < 5)  //獲得した走力が５以下の場合
        {
            Debug.Log("フライアウト！");
            BaseBallManager.GetInstance()._BBR.AddOut();

            if(BaseBallManager.GetInstance()._BBR.GetOutCount() < 2)  //アウトが０か１の場合
            {
                if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが打ったとき
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFly);  //赤が守備の時のフライの映像を流す
                }

                else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが打ったとき
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFly);  //青が守備の時のフライの映像を流す
                }
            }
            else  //アウトがすでに２つある場合
            {
                if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが打ったとき
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedChange);  //赤が守備の時のフライチェンジの映像を流す
                }

                else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが打ったとき
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueChange);  //青が守備の時のフライチェンジの映像を流す
                }
            }
        }
        else  //獲得した走力が５以上の場合
        {
            if(souryoku >= 10)  //１０以上の場合
            {
                if(souryoku >= 15)  //１５以上の場合
                {
                    if(souryoku >= 20)  //20以上の場合
                    {
                        Debug.Log("ホームラン！");
                        Run(4, nowBatterID);
                        SouryokuAmari = 0;
                        if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが打ったとき
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedHomeRun);  //赤が守備の時のワンベースヒットの映像を流す
                        }

                        else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが打ったとき
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueHomeRun);  //青が守備の時のワンベースヒットの映像を流す
                        }
                    }
                    else  //15～19の場合
                    {
                        Debug.Log("スリーベースヒット");
                        Run(3, nowBatterID);
                        if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが打ったとき
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Red3Hit);  //赤が守備の時の３ベースヒットの映像を流す
                        }

                        else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが打ったとき
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Blue3Hit);  //青が守備の時の３ベースヒットの映像を流す
                        }
                    }
                }
                else  //10～14の場合
                {
                    Debug.Log("ツーベースヒット");
                    Run(2, nowBatterID);
                    SouryokuAmari = souryoku - 10;
                    if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが打ったとき
                    {
                        AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Red2Hit);  //赤が守備の時の２ベースヒットの映像を流す
                    }

                    else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが打ったとき
                    {
                        AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Blue2Hit);  //青が守備の時の２ベースヒットの映像を流す
                    }
                }
            }
            else  //5～10の場合
            {
                Debug.Log("ヒット");
                Run(1, nowBatterID);
                SouryokuAmari = souryoku - 5;
                if(!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2Pが打ったとき
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedHit);  //赤が守備の時のワンベースヒットの映像を流す
                }

                else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1Pが打ったとき
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueHit);  //青が守備の時のワンベースヒットの映像を流す
                }
            }
        }
    }


    void SouryokuCount(int GetSouryoku)  //走力三つになるまで数えるやつ
    {
        for(int i = 0; i < 3; i++)  //０が入ってるとこが見つかるまで探す
        {
            if(Souryoku[i] == 0)  //０だったら
            {
                if(GetSouryoku != 21)  //ボールじゃなかったら
                {
                    Souryoku[i] = GetSouryoku;  //走力を代入
                    Debug.Log(i + 1 + "つ目の走力に" + GetSouryoku + "を加算");
                }
                else if(GetSouryoku == 21)  //ボールだったら
                {
                    Debug.Log("ボールが選ばれました");
                    BaseBallManager.GetInstance()._BBR.AddBall();  //ボール判定
                    BatterMode();  //バッターモードへ
                if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//表の時
                {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                }
                else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//裏の時
                {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                }
                    break;
                }

                if (i == 2)//もうすでに二つ埋まっている場合
                {
                    Debug.Log("走力が３つ埋まったよ！");
                    HitLevel(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //三つの合計を加算してヒットレベルへ
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //合計をUI用に送信！
                    isZoneRefreash = false;  //バッターが交代するので、ゾーンを更新する
                    BatterMode();  //バッターモードへ  //後でムービーモードに変更
                    ResetSouryoku();  //走力をリセット
                    BaseBallManager.GetInstance()._BBR.BatterChange();  //バッターチェンジ
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//表の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//裏の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

                else if((Souryoku[0] + Souryoku[1] + Souryoku[2]) % 5 == 0)  //三つの合計を５で割った時の余りが０の場合(５の倍数の場合)
                {
                    Debug.Log("走力の合計が５の倍数になったよ！");
                    HitLevel(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //三つの合計を加算してヒットレベルへ
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //合計をUI用に送信！
                    isZoneRefreash = false;  //バッターが交代するので、ゾーンを更新する
                    BatterMode();  //バッターモードへ  //後でムービーモードに変更
                    ResetSouryoku();  //走力をリセット
                    BaseBallManager.GetInstance()._BBR.BatterChange();  //バッターチェンジ
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())  //表の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())  //裏の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

                else if((Souryoku[0] + Souryoku[1] + Souryoku[2]) >= 20)  //合計が20以上になった瞬間ホームランに
                {
                    Debug.Log("走力の合計が２０以上になったよ！");
                    HitLevel(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //三つの合計を加算してヒットレベルへ
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //合計をUI用に送信！
                    isZoneRefreash = false;  //バッターが交代するので、ゾーンを更新する
                    BatterMode();  //バッターモードへ  //後でムービーモードに変更
                    ResetSouryoku();  //走力をリセット
                    BaseBallManager.GetInstance()._BBR.BatterChange();  //バッターチェンジ
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())  //表の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())  //裏の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

                else if (i <= 1)
                {
                    Debug.Log("走力がまだうまっていないよ");
                    isZoneRefreash = true;  //同じバッターのままなので、ゾーンは更新しない
                    BaseBallManager.GetInstance()._BBR.AddFoul();  //１回目、２回目はファウル判定
                    BatterMode();  //バッターモードへ
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //合計をUI用に送信
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//表の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//裏の時
                    {
                        //モデルScreen切り替え
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

            }
            else if(i == 3)  //三つとも埋まっていたら
            {
                Debug.LogError("三つとも走力が埋まっているのに、走力を加算しようとしています！！！");
            }
        }
        BaseBallManager.GetInstance()._ScoreBoard.SetPointer(Souryoku);  //合計をUI用に送信！
    }

    public void NextDajyun()  //バッターを次の人に回すよ、攻守交替処理を行ってから実行しよう
    {
        if((is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote()) || (!is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))  //1Pが先攻で今表の時、もしくは2Pが先攻で今裏の時(1P攻撃中)
        {
            now1PBatterNum++;  //1Pの打順を一つ進める
            if(now1PBatterNum >= 9)  //打順が9を超えたらループさせる
            {
                now1PBatterNum = 0;
            }
            nowBatterID = Dajyun[now1PBatterNum];  //今のバッターのIDを保存
            pitcherSelect.ResetPriNum();  //ピッチャーの選択をリセット
            Debug.Log("1PのバッターID : " + nowBatterID + "になりました");
            Debug.Log("1Pの打順が " + now1PBatterNum + " になりました");
        }
        if((is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()) || (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote()))  //1Pが先攻で今裏の時、もしくは2Pが先攻で今表の時(2P攻撃中)
        {
            now2PBatterNum++;  //打順を次に回す
            if (now2PBatterNum >= 18)  //打順が18を超えたらループさせる
            {
                now2PBatterNum = 9;
            }
            nowBatterID = Dajyun[now2PBatterNum];  //今のバッターのIDを保存
            pitcherSelect.ResetPriNum();  //ピッチャーの選択をリセット   
            Debug.Log("2PのバッターID : " + nowBatterID + "になりました");
            Debug.Log("2Pの打順が " + now2PBatterNum+ " になりました");
        }
    }

    public void Run(int BaseNum, int baterID)  //何ベース進むか、その時打ったバッターのIDが引数
    {
        for(int i = 0; i < BaseNum; i++)  //進むベース分繰り返す
        {
            //三塁バッターがいる場合
            if (runer[2].HasValue)  //値を持っている場合(nullじゃない場合)
            {
                BaseBallManager.GetInstance()._BBR.AddScore();
                Debug.Log("バッターID" + runer[2] + "がホームベースに到達した！");
            }
            runer[2] = runer[1];  //一つずつずらす
            runer[1] = runer[0];  //一つずつずらす
            if(i == 0)
            {
                runer[0] = baterID;  //１塁に打ったバッターのIDを入れる
            }
            else
            {
                runer[0] = null;
            }
        }
        BaseBallManager.GetInstance()._ScoreBoard.SetRunner(runer);  //UI用にランナーのデータを送信！
    }

    public void ResetSouryoku()  //走力情報をリセットします
    {
        Debug.Log("走力をリセットしました");
        for (int i = 0; i < 3; i++)
        {
            Souryoku[i] = 0;
            Debug.Log("走力" + i + "を" + Souryoku[i] + "にリセットしました");
        }
        BaseBallManager.GetInstance()._ScoreBoard.SetPointer(Souryoku);  //合計をUI用に送信！
        BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(0);  //合計をUI用に送信！
    }

    public void ResetRun()  //ランナー情報をリセットします
    {
        BaseBallManager.GetInstance()._ScoreBoard.SetRunner(runer);  //UI用にランナーのデータを送信！
        //１～３塁ランナーをリセット
        runer[0] = null;
        runer[1] = null;
        runer[2] = null;
    }

    public void SetPitcherSekect()  //ピッチャーがマスを選んだ時に呼び出す
    {
        isPitcherSelect = true;
    }
}

