using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static PadInput;

public class OptionScript : MonoBehaviour
{
    [SerializeField] Image VolSetC; //音量設定
    [SerializeField] Image ExplaC; //操作説明
    [SerializeField] Image HTPC; //遊び方
    [SerializeField] Image ExpImage;
    [SerializeField] Image HTPImage;
    [SerializeField] GameObject VolSet; //音量設定
    [SerializeField] GameObject Expla; //操作説明
    [SerializeField] GameObject HTP; //遊び方
    [SerializeField] RectTransform[] Barplace; //バーの位置
    [SerializeField] Image[] Vols;
    [SerializeField] GameObject[] Keys;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Sprite[] ExpSprite;
    [SerializeField] Sprite[] HTPSprite;
    [SerializeField] TextMeshProUGUI ExpName;   
    public int selectedMenuIndex = 0; //上の項目
    public int selectedBarIndex = 0; //音量設定のバーを選ぶ
    public float Mastervelocity; //マスターボリュームの値
    public float BGMvelocity; //BGMの値
    public float SEvelocity; //SEの値
    public float CVvelocity; //キャラクターボイスの値
    public float MVvelocity; //環境音の値
    public int[] volsVelocity;
    private int expIndex;
    private int HTPIndex;
    [SerializeField] GameObject Menu;


    private void Start()
    {
        audioMixer.GetFloat("Master", out float Master);
        audioMixer.GetFloat("BGM", out float BGM);
        audioMixer.GetFloat("SE", out float SE);
        audioMixer.GetFloat("CV", out float CV);
        audioMixer.GetFloat("MV", out float MV);
        Mastervelocity = Master;
        BGMvelocity = BGM;
        SEvelocity = SE;
        CVvelocity = CV;
        MVvelocity = MV;
    }

    private void Update()
    {
        audioMixer.SetFloat("Master", Mastervelocity);
        audioMixer.SetFloat("BGM", BGMvelocity);
        audioMixer.SetFloat("SE", SEvelocity);
        audioMixer.SetFloat("CV", CVvelocity);
        audioMixer.SetFloat("MV", MVvelocity);

        Vector2 Barpos = Barplace[selectedBarIndex].position;

        Vols[selectedBarIndex].color = Color.green;
        Keys[selectedBarIndex].SetActive(true);

        switch (selectedMenuIndex)
        {
            case 0:
                VolSetC.color = Color.green;
                ExplaC.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                HTPC.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                VolSet.SetActive(true);
                Expla.SetActive(false);
                HTP.SetActive(false);
                break;
            case 1:
                VolSetC.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                ExplaC.color = Color.green;
                HTPC.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                VolSet.SetActive(false);
                Expla.SetActive(true);
                HTP.SetActive(false);
                break;
            case 2:
                VolSetC.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                ExplaC.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                HTPC.color = Color.green;
                VolSet.SetActive(false);
                Expla.SetActive(false);
                HTP.SetActive(true);
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.E) || RB_1P && VolSet.activeSelf  || RB_1P && Expla.activeSelf || RB_1P && HTP.activeSelf)
        {
            RB_1P = false;
            selectedMenuIndex++;
            if (selectedMenuIndex >= 3)
            {
                selectedMenuIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) || LB_1P && VolSet.activeSelf || LB_1P && Expla.activeSelf || LB_1P && HTP.activeSelf)
        {
            LB_1P = false;
            selectedMenuIndex--;
            if (selectedMenuIndex <= -1)
            {
                selectedMenuIndex = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || CrossDown_1P)
        {
            CrossDown_1P = false;
            Vols[selectedBarIndex].color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
            Keys[selectedBarIndex].SetActive(false);
            selectedBarIndex++;
            if (selectedBarIndex >= 5)
            {
                selectedBarIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || CrossUp_1P)
        {
            CrossUp_1P = false;
            Vols[selectedBarIndex].color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
            Keys[selectedBarIndex].SetActive(false);
            selectedBarIndex--;
            if (selectedBarIndex <= -1)
            {
                selectedBarIndex = 4;
            }

        }


        if (VolSet.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossLeft_1P)
            {
                CrossLeft_1P = false;
                Barpos.x -= 100f;
                Barplace[selectedBarIndex].position = Barpos;
                volsVelocity[selectedBarIndex]--;
                switch (selectedBarIndex)
                {
                    default:
                        break;
                    case 0:
                        Mastervelocity -= 4.0f;
                        if (Mastervelocity < -40f)
                        {
                            Mastervelocity = -40f;
                        }
                        break;
                    case 1:
                        BGMvelocity -= 4.0f;
                        if (BGMvelocity < -40f)
                        {
                            BGMvelocity = -40f;
                        }
                        break;
                    case 2:
                        SEvelocity -= 4.0f;
                        if (SEvelocity < -40f)
                        {
                            SEvelocity = -40f;
                        }
                        break;
                    case 3:
                        CVvelocity -= 4.0f;
                        if (CVvelocity < -40f)
                        {
                            CVvelocity = -40f;
                        }
                        break;
                    case 4:
                        MVvelocity -= 4.0f;
                        if (MVvelocity < -40f)
                        {
                            MVvelocity = -40f;
                        }
                        break;
                }
                if (volsVelocity[selectedBarIndex] <= -10)
                {
                    Barpos.x += 100f;
                    Barplace[selectedBarIndex].position = Barpos;
                    volsVelocity[selectedBarIndex] = -9;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRight_1P)
            {
                CrossRight_1P = false;
                Barpos.x += 100f;
                Barplace[selectedBarIndex].position = Barpos;
                volsVelocity[selectedBarIndex]++;
                switch (selectedBarIndex)
                {
                    default:
                        break;
                    case 0:
                        Mastervelocity += 4.0f;
                        if (Mastervelocity > 0f)
                        {
                            Mastervelocity = 0f;
                        }
                        break;
                    case 1:
                        BGMvelocity += 4.0f;
                        if (BGMvelocity > 0f)
                        {
                            BGMvelocity = 0f;
                        }
                        break;
                    case 2:
                        SEvelocity += 4.0f;
                        if (SEvelocity > 0f)
                        {
                            SEvelocity = 0f;
                        }
                        break;
                    case 3:
                        CVvelocity += 4.0f;
                        if (CVvelocity > 0f)
                        {
                            CVvelocity = 0f;
                        }
                        break;
                    case 4:
                        MVvelocity += 4.0f;
                        if (MVvelocity > 0f)
                        {
                            MVvelocity = 0f;
                        }
                        break;
                }
                if (volsVelocity[selectedBarIndex] >= 1)
                {
                    Barpos.x -= 100f;
                    Barplace[selectedBarIndex].position = Barpos;
                    volsVelocity[selectedBarIndex] = 0;
                }
            }
        }

        else if (Expla.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRight_1P)
            {
                CrossRight_1P = false;
                expIndex++;
                if (expIndex == 3)
                {
                    expIndex = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossLeft_1P)
            {
                CrossLeft_1P = false;
                expIndex--;
                if (expIndex == -1)
                {
                    expIndex = 2;
                }
            }
            switch(expIndex)
            {
                default:
                    break;
                case 0:
                    ExpImage.sprite = ExpSprite[0];
                    ExpName.text = "UI操作説明";
                    break;
                case 1:
                    ExpImage.sprite = ExpSprite[1];
                    ExpName.text = "バッター操作説明";
                    break;
                case 2:
                    ExpImage.sprite = ExpSprite[2];
                    ExpName.text = "ピッチャー操作説明";
                    break;
            }
        }

        else if (HTP.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || CrossRight_1P)
            {
                CrossRight_1P = false;
                HTPIndex++;
                if (HTPIndex == 2)
                {
                    HTPIndex = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || CrossLeft_1P)
            {
                CrossLeft_1P = false;
                HTPIndex--;
                if (HTPIndex == -1)
                {
                    HTPIndex = 1;
                }
            }
            switch (HTPIndex)
            {
                default:
                    break;
                case 0:
                    HTPImage.sprite = HTPSprite[0];
                    break;
                case 1:
                    HTPImage.sprite = HTPSprite[1];
                    break;
            }
        }




        if (Input.GetKeyDown(KeyCode.Escape) || B_1P)
        {
            B_1P = false;
            Menu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
