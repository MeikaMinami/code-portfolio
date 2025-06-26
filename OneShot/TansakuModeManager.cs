using UnityEngine;

public class TansakuModeManager  //探索シーンのモードを管理
{
    private static TansakuModeManager _instance;
    public static TansakuModeManager ModeAccess => _instance ??= new TansakuModeManager();
    private AllMode _nowMode;

    public AllMode NowMode
    {
        get => _nowMode;
        private set
        {
            if (_nowMode != value)
            {
                _nowMode = value;
                Debug.Log($"{_nowMode}モード");
                UpdateModeAction();
            }
        }
    }

    //モードの種類
    public enum AllMode
    {
        Dialog_Mode,   //会話中モード
        Tansaku_Mode,  //探索中モード
        ItemGet_Mode,  //アイテム入手演出モード
        Option_Mode,   //オプション画面表示モード
        Inventry_Mode  //インベントリ画面表示モード
    }

    private TansakuModeManager()
    {
        //デフォルトのモードを設定
        NowMode = AllMode.Tansaku_Mode;
        //ここでのDebug.LogとUpdateModeActionの呼び出しは不要
    }

    private static void SetMode(AllMode mode)
    {
        var instance = ModeAccess; //インスタンスを取得

        if (instance.NowMode == mode)
        {
            //同じモードの場合は処理をスキップ
            return;
        }

        instance.NowMode = mode;
        //ここでのDebug.LogとUpdateModeActionの呼び出しは不要
    }

    private void UpdateModeAction()
    {
        //モードごとに変える処理
        switch (NowMode)
        {
            case AllMode.Dialog_Mode:    //会話中モード
                break;
            case AllMode.Tansaku_Mode:   //探索中モード
                break;
            case AllMode.ItemGet_Mode:   //アイテム入手演出モード
                break;
            case AllMode.Option_Mode:    //オプション画面表示モード
                break;
            case AllMode.Inventry_Mode:  //インベントリ画面表示モード
                break;
        }
    }

    //モードを切り替えるための静的メソッド
    public static void Dialog_Mode() => SetMode(AllMode.Dialog_Mode);
    public static void Tansaku_Mode() => SetMode(AllMode.Tansaku_Mode);
    public static void ItemGet_Mode() => SetMode(AllMode.ItemGet_Mode);
    public static void Option_Mode() => SetMode(AllMode.Option_Mode);
    public static void Inventry_Mode() => SetMode(AllMode.Inventry_Mode);
}
