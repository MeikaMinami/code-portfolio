using UnityEngine;
using UnityEngine.UI;
using static PadInput;

public class ExitScript : MonoBehaviour
{
    [SerializeField] Image Yes;
    [SerializeField] Image No;
    private int index = 1;
    /*private float UDpushed = 0.0f;
    private float RLpushed = 0.0f;*/

    private void Update()
    {

        /*var CrossUpDown = Input.GetAxis("CrossUpDown");
        var CrossRightLeft = Input.GetAxis("CrossRightLeft");*/

        switch (index)
        {
            case 0:
                Yes.color = Color.red;
                No.color = Color.green;
                break; 
            case 1:
                Yes.color = Color.green;
                No.color = Color.red;
                break;
        }
        /*if (RLpushed == 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRightLeft > 0)
            {
                RLpushed++;
                index++;
                if (index >= 2)
                {
                    index = 0;
                }

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossRightLeft < 0)
            {
                RLpushed++;
                index--;
                if (index <= -1)
                {
                    index = 1;
                }
            }
        }
        else if (CrossRightLeft == 0.0f)
        {
            RLpushed = 0.0f;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown("joystick button 0")))
        {
            if (index == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                index = 0;
                this.gameObject.SetActive(false);
            }
        }*/
        if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRight_1P)
        {
            CrossRight_1P = false;
            index++;
            if (index >= 2)
            {
                index = 0;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossLeft_1P)
        {
            CrossLeft_1P = false;
            index--;
            if (index <= -1)
            {
                index = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || A_1P)
        {
            A_1P = false;
            if (index == 0)
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                #else
                    Application.Quit();//ゲームプレイ終了
                #endif
            }
            else
            {
                index = 1;
                this.gameObject.SetActive(false);
            }
        }
    }
}
