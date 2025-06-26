using System;

public class ModeManeger
{
    private static ModeManeger _instance;//�V���O���g���C���X�^���X
    public static ModeManeger ModeAccess => _instance ??= new ModeManeger();//�C���X�^���X�̃A�N�Z�X���ɃC���X�^���X�쐬
    private AllMode _nowMode;//���݂̃��[�h
    
    //���[�h�̃v���p�e�B
    public AllMode NowMode
    {
        get => _nowMode;
        private set
        {
            if (_nowMode != value)//���[�h���ύX���ꂽ�Ƃ��̂�
            {
                _nowMode = value;
                Console.WriteLine($"{_nowMode}���[�h");
                UpdateModeAction();//���[�h�ɉ����ď���
            }
        }
    }

    // ���[�h�̎��
    public enum AllMode
    {
        UI_Mode,   // UI
        WhoFirst_Mode,  // ��U��U�����߂郂�[�h
        CharaSelect_Mode,  //�L�����N�^�[�̑ŏ������߂郂�[�h
        Pitcher_Mode,     // �s�b�`���[
        Batter_Mode,   // �o�b�^�[
        Title_Mode,  //�^�C�g��
        MovieMode,  //���[�r�[
    }

    private ModeManeger()
    {
        //�f�t�H���g�̃��[�h��ݒ�
        NowMode = AllMode.UI_Mode;
    }

    public static void SetMode(AllMode mode)
    {

        var instance = ModeAccess; //�C���X�^���X���擾

        if (instance.NowMode == mode)
        {
            //�������[�h�̏ꍇ�͏������X�L�b�v
            return;
        }

        instance.NowMode = mode;
    }

    private void UpdateModeAction()
    {
        //���[�h���Ƃɕς��鏈��
        switch (NowMode)
        {
            case AllMode.UI_Mode:
                break;
            case AllMode.WhoFirst_Mode:
                break;
            case AllMode.Pitcher_Mode:
                break;
            case AllMode.Batter_Mode:
                break;
            case AllMode.Title_Mode:
                break;
        }
    }

    public static void UIMode() => SetMode(AllMode.UI_Mode);
    public static void WhoFirst() => SetMode(AllMode.WhoFirst_Mode);
    public static void CharaSelect() => SetMode(AllMode.CharaSelect_Mode);
    public static void PitcherMode() => SetMode(AllMode.Pitcher_Mode);
    public static void BatterMode() => SetMode(AllMode.Batter_Mode);
    public static void TitleMode() => SetMode(AllMode.Title_Mode);

}
