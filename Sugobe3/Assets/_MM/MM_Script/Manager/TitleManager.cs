using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject ScreenInput;//入力の画面

    public GameObject ScreenTitle;//タイトルの画面

    public GameObject ScreenATDF;//先攻後攻入力画面


    [SerializeField]
    private GameObject NowScreen; //空のオブジェクト

    [SerializeField]
    private  GameObject flash;//フラッシュ用UI

    [SerializeField]
    private Camera Cam;//カメラ


    private void Awake()
    {
        ScreenManager.GetInstance().SetTitleManager(this);//ScreenManagerのシングルトンに登録
    }

    private void Start()
    {
        Cam.enabled = false;//無効化
        SetTitleScreen(NowScreen);//現在の画面を非表示にしてから
        SetTitleScreen(ScreenInput);//入力画面を表示
        Cam.enabled = true;//有効化
    }

    /// <summary>
    /// 指定した画面に切り替える関数
    /// </summary>
    /// <param name="Screen">切り替えたい画面のGameObject</param>
    /// <param name="Onflash">切り替え時にフラッシュを表示するかどうか（デフォルトはfalse）</param>
    /// <param name="sceneName">切り替え先のシーン名（省略可能）</param>
    public void SetTitleScreen(GameObject Screen, bool Onflash = false, string sceneName = null)
    {
        flash.SetActive(Onflash);//フラッシュを非常時
        NowScreen.SetActive(false);//今の画面を非表示
        Screen.SetActive(true);//新しい画面を表示
        NowScreen = Screen;//現在の画面を登録
        if (!string.IsNullOrEmpty(sceneName))//シーン名があればロード
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
