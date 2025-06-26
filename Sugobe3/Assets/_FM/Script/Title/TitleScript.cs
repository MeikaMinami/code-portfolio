using UnityEngine;
using UnityEngine.UI;
using static PadInput;
using static ModeManeger;

public class TitleScript : MonoBehaviour
{
    [SerializeField] Image SButton; //スタートボタン
    [SerializeField] Image OButton; //オプションボタン
    [SerializeField] Image EButton; //終了ボタン
    [SerializeField] GameObject Exit;
    [SerializeField] GameObject Option;

    private int selectedIndex = 0;

    /*private float UDpushed = 0.0f;
    private float RLpushed = 0.0f;*/

    private void Update()
    {
        /*var CrossUpDown = Input.GetAxis("CrossUpDown");
        var CrossRightLeft = Input.GetAxis("CrossRightLeft");*/

        switch (selectedIndex)
        { 
            case 0:
                SButton.color = Color.red;
                OButton.color = Color.green;
                EButton.color = Color.green; 
                break;

            case 1:
                SButton.color = Color.green;
                OButton.color = Color.red;
                EButton.color = Color.green;
                break;

            case 2:
                SButton.color = Color.green;
                OButton.color = Color.green;
                EButton.color = Color.red;
                break;

            default:
                break;
        }
        
        if (!Exit.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || CrossDown_1P)
            {
                CrossDown_1P = false;
                selectedIndex++;
                if (selectedIndex >= 3)
                {
                    selectedIndex = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || CrossUp_1P)
            {
                CrossUp_1P = false;
                selectedIndex--;
                if (selectedIndex <= -1)
                {
                    selectedIndex = 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || A_1P)
            {
                if (selectedIndex == 2)
                {
                    Exit.SetActive(true);
                }
                else if (selectedIndex == 1)
                {
                    Option.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                else if (selectedIndex == 0)
                {
                    if (ModeAccess.NowMode == AllMode.Title_Mode)
                    {
                        ScreenManager SMInst = ScreenManager.GetInstance();
                        SMInst._TitleManager.SetTitleScreen(SMInst._TitleManager.ScreenATDF, true,sceneName:"GameScene");
                        WhoFirst();  //先攻後攻を決めるモード
                    }
                }
                A_1P = false;
            }
        }
    }
}
