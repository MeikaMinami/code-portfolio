using System;

public class BaseBallManager
{
    private ScoreBoard InstScore;//�X�R�A�{�[�h�̃C���X�^���X�Q��
    private BaseBall InstBase;//�x�[�X�{�[���̃C���X�^���X�Q��
    private BBR InstBBR;//�x�[�X�{�[�����[���̃C���X�^���X�Q��

    private static BaseBallManager BBMInstance;//�V���O���g��


    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    private BaseBallManager()
    {
        Console.WriteLine("BaseBallManager����������܂���");
        //����������
    }
    #region SeterGeter�ꗗ
    //==============================================�@�@Seter�ꗗ�@�@======================================================

    /// <summary>
    /// �X�R�A�{�[�h�̓o�^
    /// </summary>
    public void SetScoreBoard(ScoreBoard instScore)
    {
        InstScore = instScore;
    }

    /// <summary>
    /// BBR�̓o�^
    /// </summary>
    public void SetBBR(BBR Bbr)
    {
        InstBBR = Bbr;
    }

    /// <summary>
    /// �x�[�X�{�[���{�̂̓o�^
    /// </summary>
    public void SetBaseBall(BaseBall Bb)
    {
        InstBase = Bb;
    }
    //==============================================�@�@Seter�ꗗ�@�@======================================================

    //==============================================�@�@Geter�ꗗ�@�@======================================================
    /// <summary>
    /// ScoreBoard�̎擾
    /// </summary>
    public ScoreBoard _ScoreBoard
    {
        get
        {
            return InstScore;
        }
    }

    /// <summary>
    /// BaseBall�̎擾
    /// </summary>
    public BaseBall _BaseBall
    {
        get
        {
            return InstBase;
        }
    }

    /// <summary>
    /// BBR�̎擾
    /// </summary>
    public BBR _BBR
    {
        get
        {
            return InstBBR;
        }
    }

    //==============================================�@�@Geter�ꗗ�@�@======================================================
    #endregion

    /// <summary>
    /// �V���O���g���Ƃ���BaseBallManager�C���X�^���X���擾
    /// </summary>
    public static BaseBallManager GetInstance()
    {
        if (BBMInstance == null)//������΍쐬
            BBMInstance = new BaseBallManager();

        return BBMInstance;//�C���X�^���X��n��
    }
}