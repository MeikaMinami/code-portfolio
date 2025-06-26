using UnityEngine;

public class TansakuModeManager  //�T���V�[���̃��[�h���Ǘ�
{
    private static TansakuModeManager _instance;
    public static TansakuModeManager ModeAccess => _instance ??= new TansakuModeManager();
    private AllMode _nowMode;

    public AllMode NowMode
    {
        get => _nowMode;
        private set
        {
            if (_nowMode != value)
            {
                _nowMode = value;
                Debug.Log($"{_nowMode}���[�h");
                UpdateModeAction();
            }
        }
    }

    //���[�h�̎��
    public enum AllMode
    {
        Dialog_Mode,   //��b�����[�h
        Tansaku_Mode,  //�T�������[�h
        ItemGet_Mode,  //�A�C�e�����艉�o���[�h
        Option_Mode,   //�I�v�V������ʕ\�����[�h
        Inventry_Mode  //�C���x���g����ʕ\�����[�h
    }

    private TansakuModeManager()
    {
        //�f�t�H���g�̃��[�h��ݒ�
        NowMode = AllMode.Tansaku_Mode;
        //�����ł�Debug.Log��UpdateModeAction�̌Ăяo���͕s�v
    }

    private static void SetMode(AllMode mode)
    {
        var instance = ModeAccess; //�C���X�^���X���擾

        if (instance.NowMode == mode)
        {
            //�������[�h�̏ꍇ�͏������X�L�b�v
            return;
        }

        instance.NowMode = mode;
        //�����ł�Debug.Log��UpdateModeAction�̌Ăяo���͕s�v
    }

    private void UpdateModeAction()
    {
        //���[�h���Ƃɕς��鏈��
        switch (NowMode)
        {
            case AllMode.Dialog_Mode:    //��b�����[�h
                break;
            case AllMode.Tansaku_Mode:   //�T�������[�h
                break;
            case AllMode.ItemGet_Mode:   //�A�C�e�����艉�o���[�h
                break;
            case AllMode.Option_Mode:    //�I�v�V������ʕ\�����[�h
                break;
            case AllMode.Inventry_Mode:  //�C���x���g����ʕ\�����[�h
                break;
        }
    }

    //���[�h��؂�ւ��邽�߂̐ÓI���\�b�h
    public static void Dialog_Mode() => SetMode(AllMode.Dialog_Mode);
    public static void Tansaku_Mode() => SetMode(AllMode.Tansaku_Mode);
    public static void ItemGet_Mode() => SetMode(AllMode.ItemGet_Mode);
    public static void Option_Mode() => SetMode(AllMode.Option_Mode);
    public static void Inventry_Mode() => SetMode(AllMode.Inventry_Mode);
}
