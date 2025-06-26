using System;

public class ScreenManager
{
    private TitleManager InstTSM;//TitleManager�̃C���X�^���X
    private MainScreenManager InstMSM;//MainManager�̃C���X�^���X

    private static ScreenManager SMInstace;//ScreenManager�̃V���O���g���C���X�^���X

    //�R���X�g���N�^
    private ScreenManager()
    {
        Console.WriteLine("ScreenManager����������܂���");
        GameManager.GetInstance();
        //����������
    }

    //TitleManager�̃C���X�^���X�ݒ�
    public void SetTitleManager(TitleManager instTitle)
    {
        InstTSM = instTitle;
    }

    //MainManager�̃C���X�^���X�ݒ�
    public void SetMainManager(MainScreenManager instMain)
    {
        InstMSM = instMain;
    }

    //TitleManager�̃C���X�^���X���擾
    public TitleManager _TitleManager
    {
        get
        {
            return InstTSM;
        }
    }

    //MainManager�̃C���X�^���X���擾
    public MainScreenManager _MainManager
    {
        get
        {
            return InstMSM;
        }
    }

    //ScreenManager�̃V���O���g���C���X�^���X���擾
    public static ScreenManager GetInstance()
    {
        if (SMInstace == null)//�Ȃ�������쐬
            SMInstace = new ScreenManager();

        return SMInstace;
    }

}
