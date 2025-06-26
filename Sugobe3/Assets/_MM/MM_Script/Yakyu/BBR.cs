using UnityEngine;

public class BBR : MonoBehaviour
{
    /// <summary>
    /// ���݂̃{�[������ۑ����܂�
    /// </summary>
    private int BallCount = 0;  //0�`3

    /// <summary>
    /// ���݂̃X�g���C�N��ۑ����܂� �t�@�[�����܂�
    /// </summary>    
    private int StrikeCount = 0;  //0�`2

    /// <summary>
    /// ���݂̃A�E�g��ۑ����܂�
    /// </summary>
    private int OutCount = 0; //0�`2

    /// <summary>
    /// ���݂̃C�j���O����ۑ����܂�
    /// </summary>
    private int InningCount = 1;  //1�`

    /// <summary>
    /// ���݂̃X�R�A�{�[�h��ۑ����܂�
    /// </summary>
    private int[] ScoreIndex;  //�e�C�j���O�̃X�R�A�A21�̗v�f������܂�

    //ScoreIndex��
    //[�P�C�j���O�ڂ̕\][�P�C�j���O�߂̗�][�Q�C�j���O�ڂ̕\][�Q�C�j���O�ڂ̗�]�c
    //�Ƃ����z��ɂȂ��Ă��܂��B

    //ScoreIndex[19]�́A��U�v���C���[�i�\�j�̃X�R�A�̍��v
    //ScoreIndex[20]�́A��U�v���C���[�i���j�̃X�R�A�̍��v
    //�Ƃ��Ďg�p����݌v�ł��B

    /// <summary>
    /// �����\��������bool�^�ł�
    /// </summary>
    private bool isOmote = true;  //true���\�Afalse����

    /// <summary>
    /// ����Ȃ玎�����ǂ�����bool�^�ł�
    /// </summary>
    private bool isSayonara = false;  //true���T���i���Afalse���ʏ�̎���

    [SerializeField]
    ResultScript resultScript;  //���ʕ\���p�X�N���v�g

    [SerializeField]
    PitcherSelect pitcherSelect;  //�s�b�`���[�I��p�X�N���v�g

    private void Awake()
    {
        BaseBallManager.GetInstance().SetBBR(this);  //BaseBallManager��BBR���Z�b�g
    }

    void Start()
    {
        ScoreIndex = new int[21];  //�X�R�A�z��̏�����

        //�X�N���v�g�̎擾
        if (resultScript == null)
        {
            resultScript = gameObject.GetComponent<ResultScript>();
        }
        if (pitcherSelect == null)
        {
            pitcherSelect = gameObject.GetComponent<PitcherSelect>();
        }
    }


    #region �������ʂ̏������O������Ăяo�����߂̃��\�b�h
    //============================================  ���ʈꗗ  ====================================================
    //�����K���@Add�@+�@���ʖ�


    /// <summary>
    /// �{�[����������ꂽ�Ƃ��̏���
    /// </summary>
    public void AddBall()
    {
        Debug.Log("�{�[���������܂���");
        BallCount++;  //�{�[���𑝂₷
        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI�p�Ƀ{�[�����𑗐M�I

        if (BallCount >= 4)  //�{�[�����S�ȏ�̎�
        {
            Debug.Log("�t�H�A�{�[���ɂȂ�܂���");
            // �t�H�A�{�[������
            if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2P���ł����Ƃ�
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFourBall);  //�Ԃ�����̎��̃t�H�A�{�[���̉f���𗬂�
            }

            else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1P���ł����Ƃ�
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueFourBall);  //������̎��̃t�H�A�{�[���̉f���𗬂�
            }
            BaseBallManager.GetInstance()._BaseBall.Run(1, BaseBallManager.GetInstance()._BaseBall.nowBatterID);  //�o�b�^�[����ۂɐi�߂�
            BaseBallManager.GetInstance()._BaseBall.ResetSouryoku();  //���͂����Z�b�g
            BatterChange();  //�o�b�^�[��㏈��
        }
        else  //�{�[�����S�����̎�
        {
            if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2P���ł����Ƃ�
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedBall);  //�Ԃ�����̎��̃{�[���̉f���𗬂�
            }

            else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1P���ł����Ƃ�
            {
                AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueBall);  //������̎��̃{�[���̉f���𗬂�
            }
        }
    }

    /// <summary>
    /// �X�g���C�N��������ꂽ�Ƃ��̏���
    /// </summary>
    public void AddStrike()
    {
        Debug.Log("�X�g���C�N�������܂���");
        StrikeCount++;  //�X�g���C�N�𑝂₷
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI�p�ɃX�g���C�N���𑗐M�I
        if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2P���ł����Ƃ�
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedStrike);  //�Ԃ�����̎��̃X�g���C�N�̉f���𗬂�
        }

        else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1P���ł����Ƃ�
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueStrike);  //������̎��̃X�g���C�N�̉f���𗬂�
        }

        if (StrikeCount >= 3)  //�X�g���C�N���R�ȏ�̎�
        {
            AddOut();  //�A�E�g����
        }
    }

    /// <summary>
    /// �o�b�^�[���\�z�𓖂Ă��Ƃ��̏���
    /// </summary>
    public void AddFoul()
    {
        Debug.Log("�t�@�E���ɂȂ�܂���");
        if (!BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //2P���ł����Ƃ�
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_RedFoul);  //�Ԃ�����̎��̃t�@�E���̉f���𗬂�
        }

        else if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst && isOmote || !BaseBallManager.GetInstance()._BaseBall.is1Pfirst && !isOmote)  //1P���ł����Ƃ�
        {
            AssetsManager.GetInstance()._VideoLoader.Play_Mv(AssetsManager.GetInstance()._VideoLoader.Mv_BlueFoul);  //������̎��̃t�@�E���̉f���𗬂�
        }
        if (StrikeCount < 2)  //�X�g���C�N��2�����̎�
        {
            StrikeCount++;  //�X�g���C�N�𑝂₷
            if (BaseBallManager.GetInstance()._ScoreBoard != null)
            {
                BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI�p�ɃX�g���C�N���𑗐M�I
                Debug.Log("�X�g���C�N�������܂���");
            }
            else
            {
                Debug.LogError("null");

            }
        }
        //�X�g���C�N��2�̏ꍇ�̓J�E���g���Ȃ�
    }

    /// <summary>
    /// �A�E�g�ɂȂ����Ƃ��̏���
    /// </summary>
    public void AddOut()
    {
        Debug.Log("�A�E�g��������܂���");
        OutCount++;  //�A�E�g�𑝂₷
        BallCount = 0;
        StrikeCount = 0;
        BaseBallManager.GetInstance()._ScoreBoard.SetOutCount(OutCount);  //UI�p�ɃA�E�g���𑗐M�I
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI�p�ɃX�g���C�N���𑗐M�I
        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI�p�Ƀ{�[�����𑗐M
        BaseBallManager.GetInstance()._BaseBall.ResetSouryoku();  //���͂����Z�b�g
        BaseBallManager.GetInstance()._BaseBall.isZoneRefreash = false;

        // �U����܂��̓C�j���O�̕ύX
        if (OutCount >= 3)  //�A�E�g���R�ȏ�̎�
        {
            if (isOmote)  //�\�̏ꍇ
            {
                Change();  //�U����
            }
            else  //���̏ꍇ
            {
                NextInning();  //���̏ꍇ�A���̃C�j���O��
            }
        }
        else  //�A�E�g���R�����̎�
        {
            BatterChange();  //�o�b�^�[���
        }
    }

    /// <summary>
    /// �X�R�A�����������̏���
    /// </summary>
    public void AddScore()
    {
        if (isOmote)  //�\�̎�
        {
            ScoreIndex[InningCount * 2 - 1]++;  //���̃C�j���O�̕\�̃X�R�A���P���Z
            Debug.Log("ScoreBoard[" + (InningCount * 2 - 1) + "]�ɃX�R�A���P���Z���܂���");

            //��U�̍��v�X�R�A���Čv�Z���āAScoreIndex[19]�ɑ��
            ScoreIndex[19] = ScoreIndex[1] + ScoreIndex[3] + ScoreIndex[5] + ScoreIndex[7] + ScoreIndex[9] + ScoreIndex[11] + ScoreIndex[13] + ScoreIndex[15] + ScoreIndex[17];
            BaseBallManager.GetInstance()._ScoreBoard.SetATScore(ScoreIndex[19]);  //UI�p�ɐ�U�`�[���̍��v�X�R�A�𑗐M
        }
        else  //���̎�
        {
            ScoreIndex[InningCount * 2]++;  //���̃C�j���O�̗��̃X�R�A���P���Z
            Debug.Log("ScoreBoard[" + (InningCount * 2) + "]�ɃX�R�A���P���Z���܂���");

            //��U�̍��v�X�R�A���Čv�Z���āAScoreIndex[20]�ɑ��
            ScoreIndex[20] = ScoreIndex[2] + ScoreIndex[4] + ScoreIndex[6] + ScoreIndex[8] + ScoreIndex[10] + ScoreIndex[12] + ScoreIndex[14] + ScoreIndex[16] + ScoreIndex[18];
            BaseBallManager.GetInstance()._ScoreBoard.SetDFScore(ScoreIndex[20]);  //UI�p�Ɍ�U�`�[���̍��v�X�R�A�𑗐M
        }
    }
    //============================================  ���ʈꗗ�I��  ====================================================
    #endregion

    //�Z�b�^�\�g�p�̂��ߖ��g�p�̃Q�b�^�[
    #region �O������l���擾���邽�߂̃��\�b�h
    //============================================  �擾���@�ꗗ  ====================================================
    // �����K���@Get�@+�@�ϐ����@+�@Count

    public int GetBallCount() => BallCount;

    public int GetStrikeCount() => StrikeCount;

    public int GetOutCount() => OutCount;

    public int GetInning() => InningCount;

    /// <summary>
    /// �X�R�A���擾���܂�
    /// </summary>
    /// <param name="i">�C�j���O��</param>
    /// <returns>�w�肵���C�j���O�̃X�R�A</returns>
    public int Get_Score(int i) => ScoreIndex[i];
    //============================================  �擾���@�ꗗ�I��  ====================================================

    #endregion
    //�Z�b�^�\�g�p�̂��ߖ��g�p�̃Q�b�^�[

    /// <summary>
    /// ���̃C�j���O�ւ̈ڍs
    /// </summary>
    private void NextInning()
    {
        InningCount++;  //�C�j���O���𑝂₷
        if (InningCount <= 9)
        {
            Debug.Log("���̃C�j���O�Ɉڂ�܂���");
            OutCount = 0;  //�A�E�g�������Z�b�g
            BallCount = 0;  //�{�[���������Z�b�g
            StrikeCount = 0;  //�X�g���C�N�������Z�b�g

            BaseBallManager.GetInstance()._ScoreBoard.SetOutCount(OutCount);  //UI�p�ɃA�E�g���𑗐M�I
            BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI�p�Ƀ{�[�����𑗐M�I
            BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI�p�ɃX�g���C�N���𑗐M�I
            BaseBallManager.GetInstance()._ScoreBoard.SetInning(InningCount);  //UI�p�ɃC�j���O���𑗐M�I
            Debug.Log("���̃C�j���O���� " + InningCount + " �ł��B");

            // �\��
            isOmote = true;
            BaseBallManager.GetInstance()._ScoreBoard.SetIsOmote(isOmote);  //UI�p��isOmote�𑗐M�I
            BaseBallManager.GetInstance()._BaseBall.ResetRun();  //�����i�[�������Z�b�g
            BaseBallManager.GetInstance()._ScoreBoard.SetRunner(BaseBallManager.GetInstance()._BaseBall.runer);  //UI�p�Ƀ����i�[�̃f�[�^�𑗐M�I
            pitcherSelect.ResetPriNum();  //�s�b�`���[�̑I�������Z�b�g

            BatterChange();  //�o�b�^�[��㏈��
        }
        else if (!isSayonara && InningCount == 10)  //����Ȃ炶��Ȃ��ăC�j���O����10�ɂȂ�����
        {
            Result();  //���ʂɏI���Ƃ�
        }
    }


    /// <summary>
    /// ���ʕ\��
    /// </summary>
    private void Result()
    {
        Debug.Log("�Q�[���I��");
        isSayonara = false;  //�T���i���łȂ�
        resultScript.SetResultScore(ScoreIndex, isSayonara, BaseBallManager.GetInstance()._BaseBall.is1Pfirst);  //���ʂ�UI�ɑ��M�I
        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Result);//���ʉ�ʂɑJ��
    }

    /// <summary>
    /// �T���i�����������Ƃ��̏���
    /// </summary>
    private void GoodbyResult()
    {
        Debug.Log("�T���i���Q�[���I��");
        isSayonara = true;  //�T���i���t���O�����Ă�
        resultScript.SetResultScore(ScoreIndex, isSayonara, BaseBallManager.GetInstance()._BaseBall.is1Pfirst);  //���ʂ�UI�ɑ��M�I
        ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Result);//���ʉ�ʂɑJ��
    }

    /// <summary>
    /// �o�b�^�[��㎞�̏���
    /// </summary>
    public void BatterChange()
    {
        Debug.Log("�o�b�^�[���");
        BallCount = 0;  //�{�[���������Z�b�g
        StrikeCount = 0;  //�X�g���C�N�������Z�b�g

        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI�p�Ƀ{�[�����𑗐M�I
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI�p�ɃX�g���C�N���𑗐M�I

        BaseBallManager.GetInstance()._BaseBall.NextDajyun();  //�ŏ������ɐi�߂�
        BaseBallManager.GetInstance()._BaseBall.isZoneRefreash = false;  //�]�[���X�V�t���O�����Z�b�g
        ModeManeger.BatterMode();  //�o�b�^�[���[�h��
    }

    /// <summary>
    /// �U���㎞�̏���
    /// </summary>
    private void Change()
    {
        //����
        isOmote = false;

        if (InningCount == 9 && !isOmote)  //�X�񗠂̂Ƃ�
        {
            if (IsSayonara())  //����Ȃ�̏ꍇ
            {
                GoodbyResult();  //�T���i���������Ăяo��
                return;
            }

        }

        Debug.Log("�U����");

        OutCount = 0;  //�A�E�g�������Z�b�g
        BallCount = 0;  //�{�[���������Z�b�g
        StrikeCount = 0;  //�X�g���C�N�����Z�b�g

        BaseBallManager.GetInstance()._ScoreBoard.SetOutCount(OutCount);  //UI�p�ɃA�E�g���𑗐M�I
        BaseBallManager.GetInstance()._ScoreBoard.SetBallCount(BallCount);  //UI�p�Ƀ{�[�����𑗐M�I
        BaseBallManager.GetInstance()._ScoreBoard.SetStrikeCount(StrikeCount);  //UI�p�ɃX�g���C�N���𑗐M�I

        BaseBallManager.GetInstance()._ScoreBoard.SetIsOmote(isOmote);  //UI�p��isOmote�𑗐M�I
        BaseBallManager.GetInstance()._BaseBall.ResetRun();  //�����i�[�������Z�b�g
        BaseBallManager.GetInstance()._ScoreBoard.SetRunner(BaseBallManager.GetInstance()._BaseBall.runer);  //UI�p�Ƀ����i�[�̃f�[�^�𑗐M�I
        pitcherSelect.ResetPriNum();  //�s�b�`���[�̑I�������Z�b�g

        if (InningCount > 1)  //�C�j���O�P�̍ŏ��̃o�b�^�[�͌�サ�Ȃ�
        {
            BatterChange();  //�o�b�^�[��㏈��
        }
        else  //�P�񗠂̏ꍇ�̂�
        {
            if (BaseBallManager.GetInstance()._BaseBall.is1Pfirst)  //1P����U�A2P����U�̎�
            {
                BaseBallManager.GetInstance()._BaseBall.nowBatterID = BaseBallManager.GetInstance()._BaseBall.Dajyun[9];  //2P�̐擪�o�b�^�[��ID���o��
                Debug.Log("2P�̍ŏ��̃o�b�^�[��ID��" + BaseBallManager.GetInstance()._BaseBall.nowBatterID);
                Debug.Log("2P�̍ŏ��̃o�b�^�[�̃^�C�v��" + BaseBallManager.GetInstance()._BaseBall.characterManager.GetCharaType(BaseBallManager.GetInstance()._BaseBall.nowBatterID));
            }
            else  //1P����U�̎��A2P����U�̎�
            {
                BaseBallManager.GetInstance()._BaseBall.nowBatterID = BaseBallManager.GetInstance()._BaseBall.Dajyun[0];  //1P�擪�̃o�b�^�[��ID���o��
                Debug.Log("1P�̍ŏ��̃o�b�^�[��ID��" + BaseBallManager.GetInstance()._BaseBall.nowBatterID);
            }
        }

    }

    /// <summary>
    /// ����������[�h���Ă��邩�𔻒肷�郁�\�b�h
    /// </summary>
    private bool IsSayonara()  //�������X�R�A�̏��������I�������A�ύX
    {
        // �X�R�A�̎擾���@�ɍ��킹�Ď������Ă�������
        if (ScoreIndex[19] < ScoreIndex[20])   //��U�̍��v���_����U�̕��������Ȃ�
        {
            return true;   //�T���i������
        }
        else
        {
            return false;  //�T���i���łȂ�
        }
    }

    /// <summary>
    /// ���ǂ��炪�\���擾���郁�\�b�h
    /// </summary>
    /// <returns>isOmote</returns>
    public bool GetIsOmote()
    {
        return isOmote;
    }

    /// <summary>
    /// ���̃C�j���O�����擾���郁�\�b�h
    /// </summary>
    /// <returns>InningCount</returns>
    public int GetInningCount()
    {
        return InningCount;
    }
}
