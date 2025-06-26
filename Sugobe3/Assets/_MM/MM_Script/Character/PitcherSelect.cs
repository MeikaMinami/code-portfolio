using System.Collections.Generic;
using UnityEngine;
using static PadInput;

public class PitcherSelect : MonoBehaviour
{
    /// <summary>
    /// ���݂̑I���}�X�ԍ�
    /// </summary>
    private int Pitch_SelectNum = 0;

    /// <summary>
    /// 1P�̑I���ς݂̃}�X���Ǘ�
    /// </summary>
    private List<int> Selected1PTiles = new List<int>();

    /// <summary>
    /// 2P�̑I���ς݂̃}�X���Ǘ�
    /// </summary>
    private List<int> Selected2PTiles = new List<int>();


    /// <summary>
    /// ��x�ڂ̑I�����ǂ������Ǘ�
    /// </summary>
    private bool IsFirstSelection = true;

    /// <summary>
    /// �m�F��ʂ��\������Ă��邩�ǂ������Ǘ�
    /// </summary>
    private bool IsConfirmationPanelActive = false;

    /// <summary>
    /// PitchCursor�z����g���ăA�E�g���C����ݒ肵�܂��B
    /// </summary>
    public GameObject[] PitchCursor;

    /// <summary>
    /// PitchCursor�z����g���ăA�E�g���C����ݒ肵�܂��B
    /// </summary>
    public GameObject[] PitchSelectedUI;

    /// <summary>
    /// �ړ��͈͗p�̔z��
    /// </summary>
    private int x = 0;
    private int y = 0;
    private int[,] selectedIndex;

    private BaseBall baseBall; //Baseball�R���|�[�l���g�̎Q��

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        selectedIndex = new int[3, 3];

        // selectedIndex �z���������
        ConvertToSelectedIndex();

        if(baseBall == null)
        {
            baseBall = BaseBallManager.GetInstance()._BaseBall;
        }

        Debug.Log("����������");
    }

    /// <summary>
    /// ���t���[���̍X�V����
    /// </summary>
    void Update()
    {
        if (ModeManeger.ModeAccess.NowMode == ModeManeger.AllMode.Pitcher_Mode) //Pitcher_Mode�̎�
        {
            if (!IsConfirmationPanelActive)
            {
                InputCheck(); //���̓`�F�b�N����
                Swicher(); //�I���}�X���X�V
                if (Selected1PTiles.Count == 5)
                {
                    ResetPriNum();
                }
                if (Selected2PTiles.Count == 5)
                {
                    ResetPriNum();
                }

                //1P����U�ŕ\�̎���������1P����U�ŗ��̎�
                if ((!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
                {
                    // �G���^�[�L�[�Ŋm�F��ʂ�\��
                    if (LB_1P && RB_1P)
                    {
                        if (Selected1PTiles.Contains(Pitch_SelectNum))
                        {
                            Debug.Log("���̃}�X�͊��ɑI������Ă��܂��B");
                            AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSamePoint);
                            return;
                        }
                        ConfirmPanel(); //�m�F��ʕ\��
                    }
                }

                //1P����U�ŕ\�̎���������1P����U�ŗ��̎�
                if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
                {
                    // �G���^�[�L�[�Ŋm�F��ʂ�\��
                    if (LB_2P && RB_2P)
                    {
                        if (Selected2PTiles.Contains(Pitch_SelectNum))
                        {
                            Debug.Log("���̃}�X�͊��ɑI������Ă��܂��B");
                            AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSamePoint);
                            return;
                        }
                        ConfirmPanel(); //�m�F��ʕ\��
                    }
                }
            }
            else
            {
                ConfirmHandle(); //�m�F��ʂ̌���E�L�����Z������
            }
        }
        // UI�̍X�V
    }

    /// <summary>
    /// ���̓`�F�b�N����
    /// </summary>
    private void InputCheck()
    {
        //�����SE�̍Đ�����


        //1P����U�ŕ\�̎���������1P����U�ŗ��̎�
        if ((!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && y <= -1 || CrossUp_1P && y <= -1)
            {
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
                y++;//��Ɉړ�
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && y >= -1 || CrossDown_1P && y >= -1)
            {
                y--;//���Ɉړ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && x <= 1 || CrossRight_1P && x <= 1)
            {
                x++;//�E�Ɉړ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && x >= 1 || CrossLeft_1P && x >= 1)
            {
                x--;//���Ɉړ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
        }

        //1P����U�ŕ\�̎���������1P����U�ŗ��̎�
        if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && y <= -1 || CrossUp_2P && y <= -1)
            {
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
                y++;//��Ɉړ�
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && y >= -1 || CrossDown_2P && y >= -1)
            {
                y--;//���Ɉړ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && x <= 1 || CrossRight_2P && x <= 1)
            {
                x++;//�E�Ɉړ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && x >= 1 || CrossLeft_2P && x >= 1)
            {
                x--;//���Ɉړ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_PitcherSelect);
            }
        }
    }

    /// <summary>
    /// �m�F��ʂ̑I������
    /// </summary>
    private void ConfirmHandle()
    {
        //1P����U�ŗ��̎���������1P����U�ŕ\�̎�
        if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.Y) || A_1P)
            {
                if (!Selected1PTiles.Contains(Pitch_SelectNum))//���ɑI�������}�X�Ɠ����}�X��I�����Ă��Ȃ��Ƃ�
                {
                    Selected1PTiles.Add(Pitch_SelectNum);   //�I���ς݃}�X�Ɍ��݂̃}�X��ǉ�
                    // 2P�̑I���ς݃}�X�ɑΉ�����UI��\��
                    foreach (var index in Selected1PTiles)
                    {
                        if (index >= 0 && index < PitchSelectedUI.Length)//�z��͈͓̔��̂ݗL����
                        {
                            PitchSelectedUI[index].SetActive(true);
                        }
                    }
                }
                //�m�F�p�l����\��
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);

                baseBall.SetPitcherSekect();//�I���m�菈��
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_OkA);//���艹�Đ�
                Debug.Log("�I���m��: " + Pitch_SelectNum);
            }
            else if (Input.GetKeyDown(KeyCode.N) || B_1P)
            {
                //�L�����Z������
                //�m�F�p�l����\��
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);
                IsFirstSelection = true;//��x�ڂ̑I����Ԃɖ߂�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CancelB);//�L�����Z�����Đ�
                Debug.Log("�I���L�����Z��");
            }
        }

        //1P����U�ŕ\�̎���������1P����U�ŗ��̎�
        if ((BaseBallManager.GetInstance()._BaseBall.is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())
                    || (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))
        {
            if (Input.GetKeyDown(KeyCode.Y) || A_2P)
            {
                if (!Selected2PTiles.Contains(Pitch_SelectNum))
                {
                    Selected2PTiles.Add(Pitch_SelectNum);  //�I���ς݃}�X�Ɍ��݂̃}�X��ǉ�
                    // 2P�̑I���ς݃}�X�ɑΉ�����UI��\��
                    foreach (var index in Selected2PTiles)
                    {
                        if (index >= 0 && index < PitchSelectedUI.Length)//�z��͈͓̔��̂ݗL����
                        {
                            PitchSelectedUI[index].SetActive(true);
                        }
                    }
                }
                //�m�F�p�l����\��
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);

                baseBall.SetPitcherSekect();//�I���m�菈��
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_OkA);//���艹�Đ�

                Debug.Log("�I���m��: " + Pitch_SelectNum);
            }
            else if (Input.GetKeyDown(KeyCode.N) || B_2P)
            {
                // �L�����Z������
                //�m�F�p�l����\��
                IsConfirmationPanelActive = false;
                ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(false);
                IsFirstSelection = true;//��x�ڂ̏�Ԃɖ߂�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_CancelB);//�L�����Z�����Đ�
                Debug.Log("�I���L�����Z��");
            }
        }
    }


    /// <summary>
    /// �m�F��ʂ�\��
    /// </summary>
    private void ConfirmPanel()
    {
        IsConfirmationPanelActive = true;//�Ǘ���ʃA�N�e�B�u�t���O
        ScreenManager.GetInstance()._MainManager.Pitcher_Decid.SetActive(true);//�m�F���UI�\��
        AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_LBRB);//�m�F��ʉ��iLB�{RB�j

        Debug.Log("�m�F��ʕ\��: " + Pitch_SelectNum);
    }

    /// <summary>
    /// ���݂̑I���}�X���X�V
    /// </summary>
    private void Swicher()
    {
        switch (x, y)
        {
            case (0, 0):
                SetNowNum(0);
                break;
            case (1, 0):
                SetNowNum(1);
                break;
            case (2, 0):
                SetNowNum(2);
                break;
            case (0, -1):
                SetNowNum(3);
                break;
            case (1, -1):
                SetNowNum(4);
                break;
            case (2, -1):
                SetNowNum(5);
                break;
            case (0, -2):
                SetNowNum(6);
                break;
            case (1, -2):
                SetNowNum(7);
                break;
            case (2, -2):
                SetNowNum(8);
                break;
            default:
                SetNowNum(0);
                SetNowNum(2);
                SetNowNum(6);
                SetNowNum(8);
                break;
        }
    }
    /// <summary>
    /// selectedIndex �z����������i�񎟌��C���f�b�N�X����ꎟ���C���f�b�N�X�ւ̕ϊ��j
    /// </summary>
    private void ConvertToSelectedIndex()
    {
        int value = 0;//0�`8�̃C���f�b�N�X
        for (int y = 0; y < 3; y++)//�s���[�v
        {
            for (int x = 0; x < 3; x++)//�񃋁[�v
            {
                selectedIndex[x, 2 - y] = value;
                value++;
            }
        }
    }

    /// <summary>
    /// �w�肳�ꂽ�C���f�b�N�X�̗v�f�݂̂� true �ɂ��A���̗v�f�� false �ɂ���֐�
    /// </summary>
    /// <param name="activeIndex">�A�N�e�B�u�ɂ���C���f�b�N�X</param>
    private void SetNowNum(int activeIndex)
    {
        for (int i = 0; i < PitchCursor.Length; i++)
        {
            PitchCursor[i].SetActive(i == activeIndex);
        }
        if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst)//1P����U�̏ꍇ
        {
            if (IsFirstSelection)//�ŏ��̑I���̎��̂ݍX�V
            {
                Pitch_SelectNum = activeIndex; //���݂̑I�����X�V
                                               //���̂܂܂̔ԍ����g�p
            }
        }
        else if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst)//2P����U�̏ꍇ
        {
            if (IsFirstSelection)//�ŏ��̑I���̎��̂ݍX�V
            {
                Pitch_SelectNum = activeIndex + 9; //���݂̑I�����X�V
                                                   //2P�p��9�����Z����9�`17���g�p
            }
        }
    }


    #region �������������� ���������������@�@Public Methods�@�@�������������� ����������������������������

    /// <summary>
    /// �I�������}�X�̔ԍ���ݒ肷��֐�
    /// </summary>
    /// <param name="selected">�ݒ肷��}�X�̔ԍ�</param>
    public int GetSelectedTile()
    {
        return Pitch_SelectNum;//�I�𒆂̃}�X�ԍ���Ԃ�
    }


    /// <summary>
    /// 2�l�̑I���}�X�����Z�b�g����֐��iReset�͂��̊֐��̂ݎg�p�j
    /// </summary>
    public void ResetPriNum()
    {
        Selected1PTiles.Clear();//1P���I�������}�X�̃��X�g��������
        Selected2PTiles.Clear();//2P���I�������}�X�̃��X�g��������
        Pitch_SelectNum = -1;//�I��ԍ���-1�i���I����ԁj�ɖ߂�
        IsFirstSelection = true; //���Z�b�g���Ɉ�x�ڂ̑I����Ԃɖ߂�
        Debug.Log("�I���}�X�����Z�b�g: " + Pitch_SelectNum.ToString());
        //���ׂĂ�UI���\���ɂ���
        foreach (var uiElement in PitchSelectedUI)
        {
            uiElement.SetActive(false);//UI���A�N�e�B�u��
        }
    }

    #endregion�������������� ���������������@�@Public Methods�@�@�������������� ����������������������������

}
