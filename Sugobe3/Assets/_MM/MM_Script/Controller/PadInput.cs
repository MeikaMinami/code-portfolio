using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class PadInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private PlayerInput playerInput;  //PlayerInput���擾
    private InputAction LBAction;  //LB�{�^��Action�擾�p�ϐ�

    private int playerIndex;  //�v���C���[�̔ԍ��i0 = 1P�A1 = 2P�j

    // ��ʂ̐ÓI�ȓ��̓t���O�ɂ���======================
    //
    // �e�v���C���[�i1P��2P�j�̃R���g���[���[���͂𖾊m�ɕ����ĊȒP��if���̒��ň��������Ƃ����v�]���󂯂āA
    // �S�Ă̓��͂�ÓI��bool�ϐ��Ƃ��ĊǗ����Ă��܂��B
    // ����ɂ��A�ǂ̃X�N���v�g����ł����͏�Ԃ��ȒP�ɎQ�Ƃł���悤�ɂ��܂����B
    //
    //===============================================

    //���͂̃t���O==============================

    public static bool Y_1P = false;
    public static bool Y_2P = false;

    public static bool B_1P = false;
    public static bool B_2P = false;

    public static bool X_1P = false;
    public static bool X_2P = false;

    public static bool A_1P = false;
    public static bool A_2P = false;

    public static bool CrossLeft_1P = false;
    public static bool CrossLeft_2P = false;

    public static bool CrossUp_1P = false;
    public static bool CrossUp_2P = false;

    public static bool CrossRight_1P = false;
    public static bool CrossRight_2P = false;

    public static bool CrossDown_1P = false;
    public static bool CrossDown_2P = false;

    public static bool LStickUp_1P = false;
    public static bool LStickUp_2P = false;

    public static bool LStickRight_1P = false;
    public static bool LStickRight_2P = false;

    public static bool LStickLeft_1P = false;
    public static bool LStickLeft_2P = false;

    public static bool LStickDown_1P = false;
    public static bool LStickDown_2P = false;

    public static bool LB_1P = false;
    public static bool LB_2P = false;

    public static bool RB_1P = false;
    public static bool RB_2P = false;

    public static bool Menu_1P = false;
    public static bool Menu_2P = false;

    //���͂̃t���O End===================


    private void Awake()
    {
        inputActions = new InputSystem_Actions();//���̓A�N�V������������
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.user.index;  //���[�U�C���f�b�N�X���擾�i0 = 1P,1 = 2P�j
        Debug.Log(playerIndex + "�����蓖�Ă�ꂽ");
    }

    //�e���̓A�N�V�����̏����i�v���C���[���ƂɃt���O���Ǘ��j
    public void OnBat_up()  //Y�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            Y_1P = true;
            Debug.Log("1P��Y����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            Y_2P = true;
            Debug.Log("2P��Y����͂��܂���");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }

    public void OnBat_Right()  //B�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            B_1P = true;
            Debug.Log("1P��B����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            B_2P = true;
            Debug.Log("2P��B����͂��܂���");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    public void OnBat_Left()  //X�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            X_1P = true;
            Debug.Log("1P��X����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            X_2P = true;
            Debug.Log("2P��X����͂��܂���");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }
    

public void OnBat_Down()  //A�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            A_1P = true;
            Debug.Log("1P��A����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            A_2P = true;
            Debug.Log("2P��A����͂��܂���");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    public void OnCross_Left()�@ //�\���L�[��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            CrossLeft_1P = true;
            Debug.Log("1P���\���L�[������͂��܂���");
        }
        else if (playerIndex == 1)
        {
            CrossLeft_2P = true;
            Debug.Log("2P���\���L�[������͂��܂���");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    public void OnCross_Up()  //�\���L�[��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            CrossUp_1P = true;
            Debug.Log("1P���\���L�[������͂��܂���");
        }
        else if (playerIndex == 1)
        {
            CrossUp_2P = true;
            Debug.Log("2P���\���L�[������͂��܂���");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    public void OnCross_Right()  //�\���L�[�E
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            CrossRight_1P = true;
            Debug.Log("1P���\���L�[������͂��܂���");
        }
        else if (playerIndex == 1)
        {
            CrossRight_2P = true;
            Debug.Log("2P���\���L�[������͂��܂���");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    public void OnCross_Down()  //�\���L�[��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            CrossDown_1P = true;
            Debug.Log("1P���\���L�[������͂��܂���");
        }
        else if (playerIndex == 1)
        {
            CrossDown_2P = true;
            Debug.Log("2P���\���L�[������͂��܂���");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    public void OnLStick(InputValue value)  //���X�e�B�b�N
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        Vector2 LStick_Input = value.Get<Vector2>();

        if (LStick_Input.y > 0.5f)  //���X�e�B�b�N��
        {
            if (playerIndex == 0)
            {
                LStickUp_1P = true;
                Debug.Log("1P�����X�e�B�b�N����͂��܂���");
            }
            else if (playerIndex == 1)
            {
                LStickUp_2P = true;
                Debug.Log("2P�����X�e�B�b�N����͂��܂���");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("�R���g���[���[����ȏ゠��I�I");
            }
        }
        else if (LStick_Input.y < -0.5f)  //���X�e�B�b�N��
        {
            if (playerIndex == 0)
            {
                LStickDown_1P = true;
                Debug.Log("1P�����X�e�B�b�N�����͂��܂���");
            }
            else if (playerIndex == 1)
            {
                LStickDown_2P = true;
                Debug.Log("2P�����X�e�B�b�N�����͂��܂���");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("�R���g���[���[����ȏ゠��I�I");
            }
        }
        else if (LStick_Input.x > 0.5f)  //���X�e�B�b�N�E
        {
            if (playerIndex == 0)
            {
                LStickRight_1P = true;
                Debug.Log("1P�����X�e�B�b�N�E���͂��܂���");
            }
            else if (playerIndex == 1)
            {
                LStickRight_2P = true;
                Debug.Log("2P�����X�e�B�b�N�E���͂��܂���");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("�R���g���[���[����ȏ゠��I�I");
            }
        }
        else if (LStick_Input.x < -0.5f)  //���X�e�B�b�N��
        {
            if (playerIndex == 0)
            {
                LStickLeft_1P = true;
                Debug.Log("1P�����X�e�B�b�N�����͂��܂���");
            }
            else if (playerIndex == 1)
            {
                LStickLeft_2P = true;
                Debug.Log("2P�����X�e�B�b�N�����͂��܂���");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("�R���g���[���[����ȏ゠��I�I");
            }
        }
    }


    public void OnLB()  //LB�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            LB_1P = true;
            Debug.Log("1P��LB����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            LB_2P = true;
            Debug.Log("2P��LB����͂��܂���");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }

    public void OnRB()  //RB�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            RB_1P = true;
            Debug.Log("1P��RB����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            RB_2P = true;
            Debug.Log("2P��RB����͂��܂���");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }

    public void OnMenu()  //Menu�{�^��
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v
        if (playerIndex == 0)
        {
            Menu_1P = true;
            Debug.Log("1P��Menu����͂��܂���");
        }
        else if (playerIndex == 1)
        {
            Menu_2P = true;
            Debug.Log("2P��Menu����͂��܂���");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("�R���g���[���[����ȏ゠��I�I");
        }
    }


    private void LateUpdate()
    {
        //�S�Ă̓��̓t���O���t���[�����ƂɃ��Z�b�g

        Y_1P = false;
        Y_2P = false;

        B_1P = false;
        B_2P = false;

        X_1P = false;
        X_2P = false;

        A_1P = false;
        A_2P = false;

        CrossLeft_1P = false;
        CrossLeft_2P = false;
        CrossUp_1P = false;
        CrossUp_2P = false;
        CrossRight_1P = false;
        CrossRight_2P = false;
        CrossDown_1P = false;
        CrossDown_2P = false;

        LStickUp_1P = false;
        LStickUp_2P = false;
        LStickRight_1P = false;
        LStickRight_2P = false;
        LStickLeft_1P = false;
        LStickLeft_2P = false;
        LStickDown_1P = false;
        LStickDown_2P = false;

        LB_1P = false;
        LB_2P = false;

        RB_1P = false;
        RB_2P = false;

        Menu_1P = false;
        Menu_2P = false;
    }

}
