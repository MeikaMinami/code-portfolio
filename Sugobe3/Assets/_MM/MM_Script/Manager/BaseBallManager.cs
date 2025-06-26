using System;

public class BaseBallManager
{
    private ScoreBoard InstScore;//スコアボードのインスタンス参照
    private BaseBall InstBase;//ベースボールのインスタンス参照
    private BBR InstBBR;//ベースボールルールのインスタンス参照

    private static BaseBallManager BBMInstance;//シングルトン


    /// <summary>
    /// コンストラクタ
    /// </summary>
    private BaseBallManager()
    {
        Console.WriteLine("BaseBallManagerが生成されました");
        //初期化処理
    }
    #region SeterGeter一覧
    //==============================================　　Seter一覧　　======================================================

    /// <summary>
    /// スコアボードの登録
    /// </summary>
    public void SetScoreBoard(ScoreBoard instScore)
    {
        InstScore = instScore;
    }

    /// <summary>
    /// BBRの登録
    /// </summary>
    public void SetBBR(BBR Bbr)
    {
        InstBBR = Bbr;
    }

    /// <summary>
    /// ベースボール本体の登録
    /// </summary>
    public void SetBaseBall(BaseBall Bb)
    {
        InstBase = Bb;
    }
    //==============================================　　Seter一覧　　======================================================

    //==============================================　　Geter一覧　　======================================================
    /// <summary>
    /// ScoreBoardの取得
    /// </summary>
    public ScoreBoard _ScoreBoard
    {
        get
        {
            return InstScore;
        }
    }

    /// <summary>
    /// BaseBallの取得
    /// </summary>
    public BaseBall _BaseBall
    {
        get
        {
            return InstBase;
        }
    }

    /// <summary>
    /// BBRの取得
    /// </summary>
    public BBR _BBR
    {
        get
        {
            return InstBBR;
        }
    }

    //==============================================　　Geter一覧　　======================================================
    #endregion

    /// <summary>
    /// シングルトンとしてBaseBallManagerインスタンスを取得
    /// </summary>
    public static BaseBallManager GetInstance()
    {
        if (BBMInstance == null)//無ければ作成
            BBMInstance = new BaseBallManager();

        return BBMInstance;//インスタンスを渡す
    }
}