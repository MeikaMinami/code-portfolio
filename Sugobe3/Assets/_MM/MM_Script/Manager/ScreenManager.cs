using System;

public class ScreenManager
{
    private TitleManager InstTSM;//TitleManagerのインスタンス
    private MainScreenManager InstMSM;//MainManagerのインスタンス

    private static ScreenManager SMInstace;//ScreenManagerのシングルトンインスタンス

    //コンストラクタ
    private ScreenManager()
    {
        Console.WriteLine("ScreenManagerが生成されました");
        GameManager.GetInstance();
        //初期化処理
    }

    //TitleManagerのインスタンス設定
    public void SetTitleManager(TitleManager instTitle)
    {
        InstTSM = instTitle;
    }

    //MainManagerのインスタンス設定
    public void SetMainManager(MainScreenManager instMain)
    {
        InstMSM = instMain;
    }

    //TitleManagerのインスタンスを取得
    public TitleManager _TitleManager
    {
        get
        {
            return InstTSM;
        }
    }

    //MainManagerのインスタンスを取得
    public MainScreenManager _MainManager
    {
        get
        {
            return InstMSM;
        }
    }

    //ScreenManagerのシングルトンインスタンスを取得
    public static ScreenManager GetInstance()
    {
        if (SMInstace == null)//なかったら作成
            SMInstace = new ScreenManager();

        return SMInstace;
    }

}
