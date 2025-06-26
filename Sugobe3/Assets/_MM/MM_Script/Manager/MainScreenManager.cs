using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    public GameObject CharaSelect;//キャラクター選択画面

    public GameObject Score;//スコア表示画面

    public GameObject BatterGame;//バッター操作画面

    public GameObject Batter_Decid;//バッター決定画面

    public GameObject PitcherGame;//ピッチャー操作画面

    public GameObject Pitcher_Decid;//ピッチャー決定画面

    public GameObject TargetNum;//ストライクゾーンの番号

    public GameObject Result;//結果画面

    /// <summary>
    /// 1Pの攻撃位置
    /// </summary>
    public GameObject Pos_Attack_1P;

    /// <summary>
    /// 2Pの攻撃位置
    /// </summary>
    public GameObject Pos_Attack_2P;

    /// <summary>
    /// 1Pの防衛位置
    /// </summary>
    public GameObject Pos_Defense_1P;

    /// <summary>
    /// 2Pの防衛位置
    /// </summary>
    public GameObject Pos_Defense_2P;


    [SerializeField]
    private GameObject[] NowScreens;//ScreenATDFをアタッチ

    [SerializeField]
    private GameObject flash;//フラッシュのUI

    [SerializeField]
    public GameObject Batter_Cam;//バッター側のカメラ

    [SerializeField]
    public GameObject Pitcher_Cam;//ピッチャー側のカメラ

    private void Awake()
    {
        ScreenManager.GetInstance().SetMainManager(this);//ScreenManagerにMainManagerのインスタンスを渡す
    }


    public void SetMainScreen(bool Onflash = true, params GameObject[] Screens)//モックの為paramsを使用、本来はオーバーロードして引数を変える
    {
        flash.SetActive(Onflash);//フラッシュオン

        if (NowScreens != null && NowScreens.Length > 0)//表示中の画面を全て非表示に
        {
            foreach (var screen in NowScreens)
            {
                if (screen != null)
                {
                    screen.SetActive(false);
                }
            }
        }

        if (Screens != null && Screens.Length > 0)//全ての画面を表示に
        {
            foreach (var screen in Screens)
            {
                if (screen != null)
                {
                    screen.SetActive(true);
                }
            }
        }

        NowScreens = Screens;//現在の表示画面リストを更新
    }

}
