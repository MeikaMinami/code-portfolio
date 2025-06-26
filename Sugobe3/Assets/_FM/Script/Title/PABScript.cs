using UnityEngine;
using UnityEngine.UI;
using static PadInput;
using static ModeManeger;

public class PABScript : MonoBehaviour
{
    [SerializeField] Image[] controllerUI;
    [SerializeField] Outline[] outlines;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip[] SE;
    [SerializeField] Image[] ButtonImage;
    [SerializeField] Sprite[] Buttons;

    public int playerCount = 0;

    private float P1second = 0.0f;
    private float P2second = 0.0f;

    private bool P1pressed = false;
    private bool P2pressed = false;

    /*private bool wasRightTriggerPressed = false;

    private Gamepad targetGamepad;*/

    private void Start()
    {
        TitleMode();
    }
    private void Update()
    {
        playerCount = PlayerJoinManager.players;
        
        if (playerCount == 0)
        {
            controllerUI[0].color = new Color(0.4f, 0.4f, 0.4f, 1);
            controllerUI[1].color = new Color(0.4f, 0.4f, 0.4f, 1);
            ButtonImage[0].sprite = Buttons[0];
            ButtonImage[1].sprite = Buttons[0];
        }

        else if (playerCount == 1)
        {
            controllerUI[0].color = new Color(1, 1, 1, 1);
            controllerUI[1].color = new Color(0.4f, 0.4f, 0.4f, 1);
            ButtonImage[0].sprite = Buttons[1];
            ButtonImage[1].sprite = Buttons[0];
        }
        else if (playerCount == 2)
        {
            controllerUI[1].color = new Color(1, 1, 1, 1);
            ButtonImage[0].sprite = Buttons[1];
            ButtonImage[1].sprite = Buttons[1];
        }

        if (RB_1P || P1pressed)
        {
            outlines[0].enabled = true;
            P1second += Time.deltaTime;
            P1pressed = true;
            Debug.Log(P1second);
            if (P1second > 1.5f)
            {
                outlines[0].enabled = false;
                P1pressed = false;
                P1second = 0.0f;
            }
            if (RB_1P)
            {
                AS.PlayOneShot(SE[1]);
            }
            RB_1P = false;
        }

        if (RB_2P || P2pressed)
        {
            outlines[1].enabled = true;
            P2second += Time.deltaTime;
            P2pressed = true;
            if (P2second > 1.5f)
            {
                outlines[1].enabled = false;
                P2pressed = false;
                P2second = 0.0f;
            }
            if (RB_2P)
            {
                AS.PlayOneShot(SE[2]);
            }
            RB_2P = false;
        }

        if (playerCount == 2 && Menu_1P)
        {
            AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_Opening);
            ScreenManager SMInstance = ScreenManager.GetInstance();
            SMInstance._TitleManager.SetTitleScreen(SMInstance._TitleManager.ScreenTitle,true);
            Menu_1P = false;
            //ƒV[ƒ“Ø‚è‘Ö‚¦
        }
    }
    

}
