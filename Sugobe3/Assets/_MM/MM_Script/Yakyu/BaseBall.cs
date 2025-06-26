using UnityEngine;
using static CharacterData;
using static ModeManeger;

public class BaseBall : MonoBehaviour
{
    public bool is1Pfirst;  //1P����U��
    bool isPitcherSelect = false;  //�s�b�`���[���}�X�����߂���

    bool isHit = false;  //�o�b�^�[�����Ă���
    int[] Souryoku = { 0, 0, 0 };  //�R�̑��͂�����Ƃ���

    int[,] zone = new int[3,3];  //�X�g���C�N�]�[���ɏo�Ă���3*3������Ƃ���

    int batterPos;  //�o�b�^�[���ǂ̎���I�񂾂��A�����ォ��0~2�A�c��������3~5

    int pitchNum;  //�s�b�`���[���������Ƃ��A0~8

    public bool isZoneRefreash = false;  //�]�[�����X�V���ꂽ��

    public int?[] runer = new int?[3];  //�����i�[���Ǘ�����null���e�^�z��, 0~2

    int[] ZoneInfo = new int[9];  //UI�ɃX�g���C�N�]�[�����𑗂�悤�̈ꎟ���z��

    public int nowBatterID;  //���̃o�b�^�[��ID
    int now1PBatterNum = 0;  //����1P�̃o�b�^�[�̑ŏ��A�O�`�W
    int now2PBatterNum = 9;  //����2P�̃o�b�^�[�̑ŏ��A�O�`�W
    int HitSouryoku = -1;  //���������Ƃ��̐���
    int SouryokuAmari;//���̗͂]����C�x���g�ȂǂɊ��p�H

    bool isDajyunSet = false;  //�ŏ����Z�b�g������

    //�f�o�b�N�p�ŏ�
    //int[] DebugDajyun = new int[18];

    public int[] Dajyun = new int[18];  //�S���̑ŏ�������A�O�`�P�V

    private Ability ability;//�\�͊Ǘ�

    public CharacterManager characterManager;//�L�����N�^�[�Ǘ�

    [SerializeField]
    private PitcherSelect pitcherSelect;//�s�b�`���[�̓��͊Ǘ�
    [SerializeField]
    private ATDFScript atdfScript;//��U��U�Ǘ�
    [SerializeField]
    private BatterScript batterScript;//�o�b�^�[���͊Ǘ�
    [SerializeField]
    private character1P chara1P;//1P
    [SerializeField]
    private character2P chara2P;//2P

    private void Awake()
    {
        BaseBallManager.GetInstance().SetBaseBall(this);  //���̃X�N���v�g��BaseBallManager��
        Debug.Log("BaseBall�X�N���v�g��BaseBallManager�ɃZ�b�g���܂���");
    }

    void Start()
    {
        //�e�X�N���v�g�̏������i�A�^�b�`����ĂȂ���Βǉ��j
        if (ability == null)
        {
            ability = gameObject.AddComponent<Ability>();
        }
        if (characterManager == null)
        {
            characterManager = gameObject.AddComponent<CharacterManager>();
        }
        if (atdfScript == null)
        {
            atdfScript = gameObject.GetComponent<ATDFScript>();
        }
        if (batterScript == null)
        {
            batterScript = gameObject.GetComponent<BatterScript>();
        }
        if (pitcherSelect == null)
        {
            pitcherSelect = gameObject.GetComponent<PitcherSelect>();
        }
        if (chara1P == null)
        {
            chara1P = gameObject.GetComponent<character1P>();
        }
        if (chara2P == null)
        {
            chara2P = gameObject.GetComponent<character2P>();
        }
        WhoFirst();  //�ŏ��̓^�C�g�����[�h
        Debug.Log("�܂���U��U�I�����[�h�ɂ��܂����B");
        Debug.Log("��U��U�����߂�ɂ�1P��LB + RB�������Ă�������");

 
 
 
 
 
 //Debug
        /*for(int i = 0; i < 18; i++)  //�f�o�b�N�p�ŏ���ݒ�
        {
            DebugDajyun[i] = i;
            //Debug.Log("�f�o�b�N�p�ŏ�" + DebugDajyun[i] + "��" + i + "�ɐݒ肵�܂���");
        }*/
    }
    
    void Update()
    {
        if (ModeAccess.NowMode == AllMode.Batter_Mode)//�o�b�^�[���[�h�̂Ƃ�
        {
            if(!isDajyunSet)  //�ŏ��̃Z�b�g���I����Ă��Ȃ�������
            {
                //�ŏ����擾
                for (int i = 0; i < 9; i++)  //1P�̑ŏ����O�`�W�ɓ����
                {
                    Dajyun[i] = chara1P.batterList1P[i];
                }
                for (int i = 9; i < 18; i++)  //2P�̑ŏ����X�`�P�V�ɓ����
                {
                    Dajyun[i] = chara2P.batterList2P[i-9] + 9;
                }
                isDajyunSet = true;

                if(is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P����U�̏ꍇ
                {
                    //now�o�b�^�[ID���ŏ��̐ݒ�
                    nowBatterID = Dajyun[0];  //���̃o�b�^�[��ID���擾
                    Debug.Log("���̃o�b�^�[��ID��" + nowBatterID + "�ł�");
                }
                else if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P����U�̏ꍇ
                {
                    nowBatterID = Dajyun[9];  //���̃o�b�^�[��ID���擾
                    Debug.Log("���̃o�b�^�[��ID��" + nowBatterID + "�ł�");
                }
            }

            if (!isZoneRefreash)  //�X�g���C�N�]�[���̍X�V���I����Ă��Ȃ�������
            {
                Debug.Log(nowBatterID);
                zone = ability.TypeZone(characterManager.GetCharaType(nowBatterID));//�X�g���C�N�]�[���̐������X�V

                //UI�p�ɒ��g�𑗐M
                batterScript.SetPoint(To1jiHairetu());

                isZoneRefreash = true;
            }
        }


        if(ModeAccess.NowMode == AllMode.Pitcher_Mode)
        {
            if(isPitcherSelect)   //�s�b�`���[���I�яI����Ă�����
            {
                HitJudge();  //�q�b�g���ǂ����̔���
            }
        }
    }
    //Debug�p
    /*    void ZoneRandom()  //�X�g���C�N�]�[���̐������X�V
        {
            string debugMessage = "�X�g���C�N�]�[��:\n"; // �f�o�b�O�p���b�Z�[�W
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    zone[i, j] = Random.Range(1, 22); // 1~21, �����_�����u��
                    debugMessage += zone[i, j].ToString("00") + " "; // 2���ŕ\��
                }
                debugMessage += "\n"; // ���s��ǉ�
            }
            Debug.Log(debugMessage);
            Debug.Log("�X�g���C�N�]�[���̍X�V���s���܂���");
            isZoneRefreash = true;
            isPitcherSelect = false;
            Debug.Log("�o�b�^�[�͗��I�����Ă�������");
        }*/
    //Debug�p

    private int[] To1jiHairetu()  //UI�p�ɁA�X�g���C�N�]�[���̏����ꎟ���z��ɕϊ����郁�\�b�h
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                 ZoneInfo[j + (i * 3)] = zone[i, j];
            }
        }
        return ZoneInfo;
    }


    void HitJudge()  //�q�b�g���ǂ����̔���
    {
        if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//�\�̎�
        {
            batterPos = batterScript.GetAimingPos();  //UI����o�b�^�[���I�񂾎����󂯂Ƃ�A�O�`�T
        }
        else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//���̎�
        {
            batterPos = batterScript.GetAimingPos() - 6;  //UI����o�b�^�[���I�񂾎����󂯂Ƃ�A6�`11
        }
        Debug.Log("�o�b�^�[���I�񂾎���" + batterPos + "�ł�");
        pitchNum = pitcherSelect.GetSelectedTile();  //�s�b�`���[���������ꏊ���󂯎��A�O�`17
        if (pitchNum >= 9)//�������
        {
            pitchNum -= 9;
        }
        if (batterPos < 3)  //�������I�΂ꂽ�ꍇ
        {
            int pitchRow;  //�s�b�`���[���������i
            int pitchCol;  //�s�b�`���[����������

            //�s�b�`���[���������ꏊ�����
            if (pitchNum ==�@0)  //�O�͊���Ȃ��̂�
            {
                pitchRow = 0;
                pitchCol = 0;
            }
            else
            {
                pitchRow = pitchNum / 3;  //�i�����߂�
                pitchCol = pitchNum % 3;  //������߂�
            }

            int HitSouryoku = -1;//������

            for (int line = 0; line < 3; line++)
            {
                if (pitchRow == batterPos)  //����������
                {
                    isHit = true;//�q�b�g����
                }
                else
                {
                    isHit = false;//��q�b�g����
                }
            }
            if (isHit)
            {
                Debug.Log("����������I�т܂����I������I");
                HitSouryoku = zone[pitchRow, pitchCol];//�����������̃}�X�̖ڂ𑖗͂Ƃ��ċL�^
                Debug.Log("�l���������́F{" + HitSouryoku + "}");
                SouryokuCount(HitSouryoku);//���̓J�E���g������
            }
            else if (!isHit)
            {
                Debug.Log("�͂���I");
                BaseBallManager.GetInstance()._BBR.AddStrike();  //�X�g���C�N�����Z
                BatterMode();  //�o�b�^�[���[�h��

                if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//�\�̎�
                {
                    //���f��Screen�؂�ւ�
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                }
                else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//���̎�
                {
                    //���f��Screen�؂�ւ�
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                }
            }
        }

        else if(2 < batterPos)  //�c�����I�΂ꂽ�ꍇ
        {
            isHit = false;
            int pitchRow;  //�s�b�`���[���������i
            int pitchCol;  //�s�b�`���[����������

            //�s�b�`���[���������ꏊ�����
            if (pitchNum == 0)  //�O�͊���Ȃ��̂�
            {
                pitchRow = 0;
                pitchCol = 0;
            }
            else
            {
                pitchRow = pitchNum / 3;  //�i�����߂�
                pitchCol = pitchNum % 3;  //������߂�
            }

            for (int line = 0; line < 3; line++)
            {
                if(pitchCol == batterPos - 3)  //�c��������
                {
                    isHit = true;//�q�b�g����
                }
                else
                {
                    isHit = false;//��q�b�g����
                }
            }
            if (isHit)
            {
                Debug.Log("�����c����I�т܂����I������I");
                HitSouryoku = zone[pitchRow, pitchCol];//���������}�X�̖ڂ𑖗͂Ƃ��ċL�^
                Debug.Log("�l���������́F{" + HitSouryoku + "}");
                SouryokuCount(HitSouryoku);//���̓J�E���g����
            }
            else if (!isHit)
            {
                Debug.Log("�͂���I");
                BaseBallManager.GetInstance()._BBR.AddStrike();  //�X�g���C�N�����Z
                BatterMode();  //�o�b�^�[���[�h��

                if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//�\�̎�
                {
                    //���f��Screen�؂�ւ�
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                }
                else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//���̎�
                {
                    //���f��Screen�؂�ւ�
                    ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                }
            }
        }
        isPitcherSelect = false;  //�t���O��������
    }

    void HitLevel(int souryoku)  //�ǂꂮ�炢�̃q�b�g�Ȃ̂�
    {
        if (characterManager.GetCharaType(nowBatterID) == CharaType.Speed)  //���̃o�b�^�[���X�s�[�h�^�C�v�̏ꍇ
        {
            souryoku += 2;  //���͂ɂQ�����Z
            Debug.Log("�X�s�[�h�^�C�v�̃o�b�^�[�ł��I���͂ɂQ�����Z���܂����I");
        }
        if (souryoku < 5)  //�l���������͂��T�ȉ��̏ꍇ
        {
            Debug.Log("�t���C�A�E�g�I");
            BaseBallManager.GetInstance()._BBR.AddOut();

            if(BaseBallManager.GetInstance()._BBR.GetOutCount() < 2)  //�A�E�g���O���P�̏ꍇ
            {
                if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P���ł����Ƃ�
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFly);  //�Ԃ�����̎��̃t���C�̉f���𗬂�
                }

                else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P���ł����Ƃ�
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFly);  //������̎��̃t���C�̉f���𗬂�
                }
            }
            else  //�A�E�g�����łɂQ����ꍇ
            {
                if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P���ł����Ƃ�
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedChange);  //�Ԃ�����̎��̃t���C�`�F���W�̉f���𗬂�
                }

                else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P���ł����Ƃ�
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueChange);  //������̎��̃t���C�`�F���W�̉f���𗬂�
                }
            }
        }
        else  //�l���������͂��T�ȏ�̏ꍇ
        {
            if(souryoku >= 10)  //�P�O�ȏ�̏ꍇ
            {
                if(souryoku >= 15)  //�P�T�ȏ�̏ꍇ
                {
                    if(souryoku >= 20)  //20�ȏ�̏ꍇ
                    {
                        Debug.Log("�z�[�������I");
                        Run(4, nowBatterID);
                        SouryokuAmari = 0;
                        if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P���ł����Ƃ�
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedHomeRun);  //�Ԃ�����̎��̃����x�[�X�q�b�g�̉f���𗬂�
                        }

                        else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P���ł����Ƃ�
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueHomeRun);  //������̎��̃����x�[�X�q�b�g�̉f���𗬂�
                        }
                    }
                    else  //15�`19�̏ꍇ
                    {
                        Debug.Log("�X���[�x�[�X�q�b�g");
                        Run(3, nowBatterID);
                        if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P���ł����Ƃ�
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Red3Hit);  //�Ԃ�����̎��̂R�x�[�X�q�b�g�̉f���𗬂�
                        }

                        else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P���ł����Ƃ�
                        {
                            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Blue3Hit);  //������̎��̂R�x�[�X�q�b�g�̉f���𗬂�
                        }
                    }
                }
                else  //10�`14�̏ꍇ
                {
                    Debug.Log("�c�[�x�[�X�q�b�g");
                    Run(2, nowBatterID);
                    SouryokuAmari = souryoku - 10;
                    if (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P���ł����Ƃ�
                    {
                        AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Red2Hit);  //�Ԃ�����̎��̂Q�x�[�X�q�b�g�̉f���𗬂�
                    }

                    else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P���ł����Ƃ�
                    {
                        AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_Blue2Hit);  //������̎��̂Q�x�[�X�q�b�g�̉f���𗬂�
                    }
                }
            }
            else  //5�`10�̏ꍇ
            {
                Debug.Log("�q�b�g");
                Run(1, nowBatterID);
                SouryokuAmari = souryoku - 5;
                if(!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //2P���ł����Ƃ�
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedHit);  //�Ԃ�����̎��̃����x�[�X�q�b�g�̉f���𗬂�
                }

                else if (is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote() || !is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote())  //1P���ł����Ƃ�
                {
                    AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueHit);  //������̎��̃����x�[�X�q�b�g�̉f���𗬂�
                }
            }
        }
    }


    void SouryokuCount(int GetSouryoku)  //���͎O�ɂȂ�܂Ő�������
    {
        for(int i = 0; i < 3; i++)  //�O�������Ă�Ƃ���������܂ŒT��
        {
            if(Souryoku[i] == 0)  //�O��������
            {
                if(GetSouryoku != 21)  //�{�[������Ȃ�������
                {
                    Souryoku[i] = GetSouryoku;  //���͂���
                    Debug.Log(i + 1 + "�ڂ̑��͂�" + GetSouryoku + "�����Z");
                }
                else if(GetSouryoku == 21)  //�{�[����������
                {
                    Debug.Log("�{�[�����I�΂�܂���");
                    BaseBallManager.GetInstance()._BBR.AddBall();  //�{�[������
                    BatterMode();  //�o�b�^�[���[�h��
                if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//�\�̎�
                {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                }
                else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//���̎�
                {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                        ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                         ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                }
                    break;
                }

                if (i == 2)//�������łɓ���܂��Ă���ꍇ
                {
                    Debug.Log("���͂��R���܂�����I");
                    HitLevel(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //�O�̍��v�����Z���ăq�b�g���x����
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //���v��UI�p�ɑ��M�I
                    isZoneRefreash = false;  //�o�b�^�[����シ��̂ŁA�]�[�����X�V����
                    BatterMode();  //�o�b�^�[���[�h��  //��Ń��[�r�[���[�h�ɕύX
                    ResetSouryoku();  //���͂����Z�b�g
                    BaseBallManager.GetInstance()._BBR.BatterChange();  //�o�b�^�[�`�F���W
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//�\�̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//���̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

                else if((Souryoku[0] + Souryoku[1] + Souryoku[2]) % 5 == 0)  //�O�̍��v���T�Ŋ��������̗]�肪�O�̏ꍇ(�T�̔{���̏ꍇ)
                {
                    Debug.Log("���͂̍��v���T�̔{���ɂȂ�����I");
                    HitLevel(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //�O�̍��v�����Z���ăq�b�g���x����
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //���v��UI�p�ɑ��M�I
                    isZoneRefreash = false;  //�o�b�^�[����シ��̂ŁA�]�[�����X�V����
                    BatterMode();  //�o�b�^�[���[�h��  //��Ń��[�r�[���[�h�ɕύX
                    ResetSouryoku();  //���͂����Z�b�g
                    BaseBallManager.GetInstance()._BBR.BatterChange();  //�o�b�^�[�`�F���W
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())  //�\�̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())  //���̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

                else if((Souryoku[0] + Souryoku[1] + Souryoku[2]) >= 20)  //���v��20�ȏ�ɂȂ����u�ԃz�[��������
                {
                    Debug.Log("���͂̍��v���Q�O�ȏ�ɂȂ�����I");
                    HitLevel(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //�O�̍��v�����Z���ăq�b�g���x����
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //���v��UI�p�ɑ��M�I
                    isZoneRefreash = false;  //�o�b�^�[����シ��̂ŁA�]�[�����X�V����
                    BatterMode();  //�o�b�^�[���[�h��  //��Ń��[�r�[���[�h�ɕύX
                    ResetSouryoku();  //���͂����Z�b�g
                    BaseBallManager.GetInstance()._BBR.BatterChange();  //�o�b�^�[�`�F���W
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())  //�\�̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())  //���̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

                else if (i <= 1)
                {
                    Debug.Log("���͂��܂����܂��Ă��Ȃ���");
                    isZoneRefreash = true;  //�����o�b�^�[�̂܂܂Ȃ̂ŁA�]�[���͍X�V���Ȃ�
                    BaseBallManager.GetInstance()._BBR.AddFoul();  //�P��ځA�Q��ڂ̓t�@�E������
                    BatterMode();  //�o�b�^�[���[�h��
                    BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(Souryoku[0] + Souryoku[1] + Souryoku[2]);  //���v��UI�p�ɑ��M
                    if (BaseBallManager.GetInstance()._BBR.GetIsOmote())//�\�̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
                    }
                    else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())//���̎�
                    {
                        //���f��Screen�؂�ւ�
                        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                            ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                             ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
                    }
                    break;
                }

            }
            else if(i == 3)  //�O�Ƃ����܂��Ă�����
            {
                Debug.LogError("�O�Ƃ����͂����܂��Ă���̂ɁA���͂����Z���悤�Ƃ��Ă��܂��I�I�I");
            }
        }
        BaseBallManager.GetInstance()._ScoreBoard.SetPointer(Souryoku);  //���v��UI�p�ɑ��M�I
    }

    public void NextDajyun()  //�o�b�^�[�����̐l�ɉ񂷂�A�U���֏������s���Ă�����s���悤
    {
        if((is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote()) || (!is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()))  //1P����U�ō��\�̎��A��������2P����U�ō����̎�(1P�U����)
        {
            now1PBatterNum++;  //1P�̑ŏ�����i�߂�
            if(now1PBatterNum >= 9)  //�ŏ���9�𒴂����烋�[�v������
            {
                now1PBatterNum = 0;
            }
            nowBatterID = Dajyun[now1PBatterNum];  //���̃o�b�^�[��ID��ۑ�
            pitcherSelect.ResetPriNum();  //�s�b�`���[�̑I�������Z�b�g
            Debug.Log("1P�̃o�b�^�[ID : " + nowBatterID + "�ɂȂ�܂���");
            Debug.Log("1P�̑ŏ��� " + now1PBatterNum + " �ɂȂ�܂���");
        }
        if((is1Pfirst && !BaseBallManager.GetInstance()._BBR.GetIsOmote()) || (!is1Pfirst && BaseBallManager.GetInstance()._BBR.GetIsOmote()))  //1P����U�ō����̎��A��������2P����U�ō��\�̎�(2P�U����)
        {
            now2PBatterNum++;  //�ŏ������ɉ�
            if (now2PBatterNum >= 18)  //�ŏ���18�𒴂����烋�[�v������
            {
                now2PBatterNum = 9;
            }
            nowBatterID = Dajyun[now2PBatterNum];  //���̃o�b�^�[��ID��ۑ�
            pitcherSelect.ResetPriNum();  //�s�b�`���[�̑I�������Z�b�g   
            Debug.Log("2P�̃o�b�^�[ID : " + nowBatterID + "�ɂȂ�܂���");
            Debug.Log("2P�̑ŏ��� " + now2PBatterNum+ " �ɂȂ�܂���");
        }
    }

    public void Run(int BaseNum, int baterID)  //���x�[�X�i�ނ��A���̎��ł����o�b�^�[��ID������
    {
        for(int i = 0; i < BaseNum; i++)  //�i�ރx�[�X���J��Ԃ�
        {
            //�O�ۃo�b�^�[������ꍇ
            if (runer[2].HasValue)  //�l�������Ă���ꍇ(null����Ȃ��ꍇ)
            {
                BaseBallManager.GetInstance()._BBR.AddScore();
                Debug.Log("�o�b�^�[ID" + runer[2] + "���z�[���x�[�X�ɓ��B�����I");
            }
            runer[2] = runer[1];  //������炷
            runer[1] = runer[0];  //������炷
            if(i == 0)
            {
                runer[0] = baterID;  //�P�ۂɑł����o�b�^�[��ID������
            }
            else
            {
                runer[0] = null;
            }
        }
        BaseBallManager.GetInstance()._ScoreBoard.SetRunner(runer);  //UI�p�Ƀ����i�[�̃f�[�^�𑗐M�I
    }

    public void ResetSouryoku()  //���͏������Z�b�g���܂�
    {
        Debug.Log("���͂����Z�b�g���܂���");
        for (int i = 0; i < 3; i++)
        {
            Souryoku[i] = 0;
            Debug.Log("����" + i + "��" + Souryoku[i] + "�Ƀ��Z�b�g���܂���");
        }
        BaseBallManager.GetInstance()._ScoreBoard.SetPointer(Souryoku);  //���v��UI�p�ɑ��M�I
        BaseBallManager.GetInstance()._ScoreBoard.SetRushPower(0);  //���v��UI�p�ɑ��M�I
    }

    public void ResetRun()  //�����i�[�������Z�b�g���܂�
    {
        BaseBallManager.GetInstance()._ScoreBoard.SetRunner(runer);  //UI�p�Ƀ����i�[�̃f�[�^�𑗐M�I
        //�P�`�R�ۃ����i�[�����Z�b�g
        runer[0] = null;
        runer[1] = null;
        runer[2] = null;
    }

    public void SetPitcherSekect()  //�s�b�`���[���}�X��I�񂾎��ɌĂяo��
    {
        isPitcherSelect = true;
    }
}

