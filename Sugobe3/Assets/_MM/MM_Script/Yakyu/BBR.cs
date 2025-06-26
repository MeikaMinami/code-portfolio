using UnityEngine;

public class BBR : MonoBehaviour
{
    /// <summary>
    /// 現在のボール球を保存します
    /// </summary>
    private int BallCount = 0;  //0～3

    /// <summary>
    /// 現在のストライクを保存します ファールも含む
    /// </summary>    
    private int StrikeCount = 0;  //0～2

    /// <summary>
    /// 現在のアウトを保存します
    /// </summary>
    private int OutCount = 0; //0～2

    /// <summary>
    /// 現在のイニング数を保存します
    /// </summary>
    private int InningCount = 1;  //1～

    /// <summary>
    /// 現在のスコアボードを保存します
    /// </summary>
    private int[] ScoreIndex;  //各イニングのスコア、21個の要素があります

    //ScoreIndexは
    //[１イニング目の表][１イニングめの裏][２イニング目の表][２イニング目の裏]…
    //という配列になっています。

    //ScoreIndex[19]は、先攻プレイヤー（表）のスコアの合計
    //ScoreIndex[20]は、後攻プレイヤー（裏）のスコアの合計
    //として使用する設計です。

    /// <summary>
    /// 今が表か裏かのbool型です
    /// </summary>
    private bool isOmote = true;  //trueが表、falseが裏

    /// <summary>
    /// さよなら試合かどうかのbool型です
    /// </summary>
    private bool isSayonara = false;  //trueがサヨナラ、falseが通常の試合

    [SerializeField]
    ResultScript resultScript;  //結果表示用スクリプト

    [SerializeField]
    PitcherSelect pitcherSelect;  //ピッチャー選択用スクリプト

    private void Awake()
    {
        BaseBallManager.GetInstance().SetBBR(this);  //BaseBallManagerにBBRをセット
    }

    void Start()
    {
        ScoreIndex = new int[21];  //スコア配列の初期化

        //スクリプトの取得
        if (resultScript == null)
        {
            resultScript = gameObject.GetComponent<ResultScript>();
        }
        if (pitcherSelect == null)
        {
            pitcherSelect = gameObject.GetComponent<PitcherSelect>();
        }
    }


    #region 投球結果の処理を外部から呼び出すためのメソッド
    //============================================  結果一覧  ====================================================
    //命名規則　Add　+　結果名


    /// <summary>
    /// ボールが投げられたときの処理
    /// </summary>
    public void AddBall()
    {
        Debug.Log("ボールが増えました");
        BallCount++;  //ボールを増やす
        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI用にボール数を送信！

        if (BallCount >= 4)  //ボールが４以上の時
        {
            Debug.Log("フォアボールになりました");
            // フォアボール処理
            if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2Pが打ったとき
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFourBall);  //赤が守備の時のフォアボールの映像を流す
            }

            else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1Pが打ったとき
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueFourBall);  //青が守備の時のフォアボールの映像を流す
            }
            BaseBallManager.GetInstance()._BaseBall.Run(1, BaseBallManager.GetInstance()._BaseBall.nowBatterID);  //バッターを一塁に進める
            BaseBallManager.GetInstance()._BaseBall.ResetSouryoku();  //走力をリセット
            BatterChange();  //バッター交代処理
        }
        else  //ボールが４未満の時
        {
            if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2Pが打ったとき
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedBall);  //赤が守備の時のボールの映像を流す
            }

            else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1Pが打ったとき
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueBall);  //青が守備の時のボールの映像を流す
            }
        }
    }

    /// <summary>
    /// ストライクが投げられたときの処理
    /// </summary>
    public void AddStrike()
    {
        Debug.Log("ストライクが増えました");
        StrikeCount++;  //ストライクを増やす
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI用にストライク数を送信！
        if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2Pが打ったとき
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedStrike);  //赤が守備の時のストライクの映像を流す
        }

        else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1Pが打ったとき
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueStrike);  //青が守備の時のストライクの映像を流す
        }

        if (StrikeCount >= 3)  //ストライクが３以上の時
        {
            AddOut();  //アウト処理
        }
    }

    /// <summary>
    /// バッターが予想を当てたときの処理
    /// </summary>
    public void AddFoul()
    {
        Debug.Log("ファウルになりました");
        if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2Pが打ったとき
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFoul);  //赤が守備の時のファウルの映像を流す
        }

        else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1Pが打ったとき
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueFoul);  //青が守備の時のファウルの映像を流す
        }
        if (StrikeCount < 2)  //ストライクが2未満の時
        {
            StrikeCount++;  //ストライクを増やす
            if (BaseBallManager.GetInstance()._ScoreBoard != null)
            {
                BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI用にストライク数を送信！
                Debug.Log("ストライクが増えました");
            }
            else
            {
                Debug.LogError("null");

            }
        }
        //ストライクが2の場合はカウントしない
    }

    /// <summary>
    /// アウトになったときの処理
    /// </summary>
    public void AddOut()
    {
        Debug.Log("アウトが一つ増えました");
        OutCount++;  //アウトを増やす
        BallCount = 0;
        StrikeCount = 0;
        BaseBallManager.GetInstance()._ScoreBoard.SetOutCount(OutCount);  //UI用にアウト数を送信！
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI用にストライク数を送信！
        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI用にボール数を送信
        BaseBallManager.GetInstance()._BaseBall.ResetSouryoku();  //走力をリセット
        BaseBallManager.GetInstance()._BaseBall.isZoneRefreash = false;

        // 攻守交代またはイニングの変更
        if (OutCount >= 3)  //アウトが３以上の時
        {
            if (isOmote)  //表の場合
            {
                Change();  //攻守交代
            }
            else  //裏の場合
            {
                NextInning();  //裏の場合、次のイニングへ
            }
        }
        else  //アウトが３未満の時
        {
            BatterChange();  //バッター交代
        }
    }

    /// <summary>
    /// スコアが増えた時の処理
    /// </summary>
    public void AddScore()
    {
        if (isOmote)  //表の時
        {
            ScoreIndex[InningCount * 2 - 1]++;  //今のイニングの表のスコアを１加算
            Debug.Log("ScoreBoard[" + (InningCount * 2 - 1) + "]にスコアを１加算しました");

            //先攻の合計スコアを再計算して、ScoreIndex[19]に代入
            ScoreIndex[19] = ScoreIndex[1] + ScoreIndex[3] + ScoreIndex[5] + ScoreIndex[7] + ScoreIndex[9] + ScoreIndex[11] + ScoreIndex[13] + ScoreIndex[15] + ScoreIndex[17];
            BaseBallManager.GetInstance()._ScoreBoard.SetATScore(ScoreIndex[19]);  //UI用に先攻チームの合計スコアを送信
        }
        else  //裏の時
        {
            ScoreIndex[InningCount * 2]++;  //今のイニングの裏のスコアを１加算
            Debug.Log("ScoreBoard[" + (InningCount * 2) + "]にスコアを１加算しました");

            //後攻の合計スコアを再計算して、ScoreIndex[20]に代入
            ScoreIndex[20] = ScoreIndex[2] + ScoreIndex[4] + ScoreIndex[6] + ScoreIndex[8] + ScoreIndex[10] + ScoreIndex[12] + ScoreIndex[14] + ScoreIndex[16] + ScoreIndex[18];
            BaseBallManager.GetInstance()._ScoreBoard.SetDFScore(ScoreIndex[20]);  //UI用に後攻チームの合計スコアを送信
        }
    }
    //============================================  結果一覧終了  ====================================================
    #endregion

    //セッタ―使用のため未使用のゲッター
    #region 外部から値を取得するためのメソッド
    //============================================  取得方法一覧  ====================================================
    // 命名規則　Get　+　変数名　+　Count

    public int GetBallCount() => BallCount;

    public int GetStrikeCount() => StrikeCount;

    public int GetOutCount() => OutCount;

    public int GetInning() => InningCount;

    /// <summary>
    /// スコアを取得します
    /// </summary>
    /// <param name="i">イニング数</param>
    /// <returns>指定したイニングのスコア</returns>
    public int Get_Score(int i) => ScoreIndex[i];
    //============================================  取得方法一覧終了  ====================================================

    #endregion
    //セッタ―使用のため未使用のゲッター

    /// <summary>
    /// 次のイニングへの移行
    /// </summary>
    private void NextInning()
    {
        InningCount++;  //イニング数を増やす
        if (InningCount <= 9)
        {
            Debug.Log("次のイニングに移りました");
            OutCount = 0;  //アウト数をリセット
            BallCount = 0;  //ボール数をリセット
            StrikeCount = 0;  //ストライク数をリセット

            BaseBallManager.GetInstance()._ScoreBoard.SetOutCount(OutCount);  //UI用にアウト数を送信！
            BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI用にボール数を送信！
            BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI用にストライク数を送信！
            BaseBallManager.GetInstance()._ScoreBoard.SetInning(InningCount);  //UI用にイニング数を送信！
            Debug.Log("今のイニング数は " + InningCount + " です。");

            // 表へ
            isOmote = true;
            BaseBallManager.GetInstance()._ScoreBoard.SetIsOmote(isOmote);  //UI用にisOmoteを送信！
            BaseBallManager.GetInstance()._BaseBall.ResetRun();  //ランナー情報をリセット
            BaseBallManager.GetInstance()._ScoreBoard.SetRunner(BaseBallManager.GetInstance()._BaseBall.runer);  //UI用にランナーのデータを送信！
            pitcherSelect.ResetPriNum();  //ピッチャーの選択をリセット

            BatterChange();  //バッター交代処理
        }
        else if (!isSayonara && InningCount == 10)  //さよならじゃなくてイニング数が10になった時
        {
            Result();  //普通に終わるとき
        }
    }


    /// <summary>
    /// 結果表示
    /// </summary>
    private void Result()
    {
        Debug.Log("ゲーム終了");
        isSayonara = false;  //サヨナラでない
        resultScript.SetResultScore(ScoreIndex, isSayonara, BaseBallManager.GetInstance()._BaseBall.is1Pfirst);  //結果をUIに送信！
        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Result);//結果画面に遷移
    }

    /// <summary>
    /// サヨナラ勝ちしたときの処理
    /// </summary>
    private void GoodbyResult()
    {
        Debug.Log("サヨナラゲーム終了");
        isSayonara = true;  //サヨナラフラグをたてる
        resultScript.SetResultScore(ScoreIndex, isSayonara, BaseBallManager.GetInstance()._BaseBall.is1Pfirst);  //結果をUIに送信！
        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Result);//結果画面に遷移
    }

    /// <summary>
    /// バッター交代時の処理
    /// </summary>
    public void BatterChange()
    {
        Debug.Log("バッター交代");
        BallCount = 0;  //ボール数をリセット
        StrikeCount = 0;  //ストライク数をリセット

        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI用にボール数を送信！
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI用にストライク数を送信！

        BaseBallManager.GetInstance()._BaseBall.NextDajyun();  //打順を次に進める
        BaseBallManager.GetInstance()._BaseBall.isZoneRefreash = false;  //ゾーン更新フラグをリセット
        ModeManeger.BatterMode();  //バッターモードへ
    }

    /// <summary>
    /// 攻守交代時の処理
    /// </summary>
    private void Change()
    {
        //裏へ
        isOmote = false;

        if (InningCount == 9 && !isOmote)  //９回裏のとき
        {
            if (IsSayonara())  //さよならの場合
            {
                GoodbyResult();  //サヨナラ処理を呼び出し
                return;
            }

        }

        Debug.Log("攻守交代");

        OutCount = 0;  //アウト数をリセット
        BallCount = 0;  //ボール数をリセット
        StrikeCount = 0;  //ストライク数リセット

        BaseBallManager.GetInstance()._ScoreBoard.SetOutCount(OutCount);  //UI用にアウト数を送信！
        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI用にボール数を送信！
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI用にストライク数を送信！

        BaseBallManager.GetInstance()._ScoreBoard.SetIsOmote(isOmote);  //UI用にisOmoteを送信！
        BaseBallManager.GetInstance()._BaseBall.ResetRun();  //ランナー情報をリセット
        BaseBallManager.GetInstance()._ScoreBoard.SetRunner(BaseBallManager.GetInstance()._BaseBall.runer);  //UI用にランナーのデータを送信！
        pitcherSelect.ResetPriNum();  //ピッチャーの選択をリセット

        if (InningCount > 1)  //イニング１の最初のバッターは交代しない
        {
            BatterChange();  //バッター交代処理
        }
        else  //１回裏の場合のみ
        {
            if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst)  //1Pが先攻、2Pが後攻の時
            {
                BaseBallManager.GetInstance()._BaseBall.nowBatterID = BaseBallManager.GetInstance()._BaseBall.Dajyun[9];  //2Pの先頭バッターのID取り出し
                Debug.Log("2Pの最初のバッターのIDは" + BaseBallManager.GetInstance()._BaseBall.nowBatterID);
                Debug.Log("2Pの最初のバッターのタイプは" + BaseBallManager.GetInstance()._BaseBall.characterManager.GetCharaType(BaseBallManager.GetInstance()._BaseBall.nowBatterID));
            }
            else  //1Pが後攻の時、2Pが先攻の時
            {
                BaseBallManager.GetInstance()._BaseBall.nowBatterID = BaseBallManager.GetInstance()._BaseBall.Dajyun[0];  //1P先頭のバッターのID取り出し
                Debug.Log("1Pの最初のバッターのIDは" + BaseBallManager.GetInstance()._BaseBall.nowBatterID);
            }
        }

    }

    /// <summary>
    /// 守備側がリードしているかを判定するメソッド
    /// </summary>
    private bool IsSayonara()  //ここをスコアの処理書き終わったら、変更
    {
        // スコアの取得方法に合わせて実装してください
        if (ScoreIndex[19] < ScoreIndex[20])   //先攻の合計得点より後攻の方が高いなら
        {
            return true;   //サヨナラ勝ち
        }
        else
        {
            return false;  //サヨナラでない
        }
    }

    /// <summary>
    /// 今どちらが表か取得するメソッド
    /// </summary>
    /// <returns>isOmote</returns>
    public bool GetIsOmote()
    {
        return isOmote;
    }

    /// <summary>
    /// 今のイニング数を取得するメソッド
    /// </summary>
    /// <returns>InningCount</returns>
    public int GetInningCount()
    {
        return InningCount;
    }
}
