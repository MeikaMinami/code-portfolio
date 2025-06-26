using UnityEngine;
using System;

public class GameManager
{
    private static GameManager GMInstance;//シングルトンインスタンス

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private GameManager()
    {
        //フレームレート一律６０に
        QualitySettings.vSyncCount = 0;//垂直同期無効化
        Application.targetFrameRate = 60;//フレームレート60に固定
        Screen.SetResolution(1920, 1080, true);//解像度、フルスクリーン設定
        Console.WriteLine("GameManager呼ばれます");
        Console.WriteLine(Application.targetFrameRate + "のフレームレートを設定");
    }

    public static GameManager GetInstance()//インスタンスを取得
    {
        if (GMInstance == null)
            GMInstance = new GameManager();

        return GMInstance;
    }
}