using System;

public class ModeManeger
{
    private static ModeManeger _instance;//シングルトンインスタンス
    public static ModeManeger ModeAccess => _instance ??= new ModeManeger();//インスタンスのアクセス時にインスタンス作成
    private AllMode _nowMode;//現在のモード
    
    //モードのプロパティ
    public AllMode NowMode
    {
        get => _nowMode;
        private set
        {
            if (_nowMode != value)//モードが変更されたときのみ
            {
                _nowMode = value;
                Console.WriteLine($"{_nowMode}モード");
                UpdateModeAction();//モードに応じて処理
            }
        }
    }

    // モードの種類
    public enum AllMode
    {
        UI_Mode,   // UI
        WhoFirst_Mode,  // 先攻後攻を決めるモード
        CharaSelect_Mode,  //キャラクターの打順を決めるモード
        Pitcher_Mode,     // ピッチャー
        Batter_Mode,   // バッター
        Title_Mode,  //タイトル
        MovieMode,  //ムービー
    }

    private ModeManeger()
    {
        //デフォルトのモードを設定
        NowMode = AllMode.UI_Mode;
    }

    public static void SetMode(AllMode mode)
    {

        var instance = ModeAccess; //インスタンスを取得

        if (instance.NowMode == mode)
        {
            //同じモードの場合は処理をスキップ
            return;
        }

        instance.NowMode = mode;
    }

    private void UpdateModeAction()
    {
        //モードごとに変える処理
        switch (NowMode)
        {
            case AllMode.UI_Mode:
                break;
            case AllMode.WhoFirst_Mode:
                break;
            case AllMode.Pitcher_Mode:
                break;
            case AllMode.Batter_Mode:
                break;
            case AllMode.Title_Mode:
                break;
        }
    }

    public static void UIMode() => SetMode(AllMode.UI_Mode);
    public static void WhoFirst() => SetMode(AllMode.WhoFirst_Mode);
    public static void CharaSelect() => SetMode(AllMode.CharaSelect_Mode);
    public static void PitcherMode() => SetMode(AllMode.Pitcher_Mode);
    public static void BatterMode() => SetMode(AllMode.Batter_Mode);
    public static void TitleMode() => SetMode(AllMode.Title_Mode);

}
