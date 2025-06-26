using UnityEngine;
using System;

public class GameManager
{
    private static GameManager GMInstance;//�V���O���g���C���X�^���X

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    private GameManager()
    {
        //�t���[�����[�g�ꗥ�U�O��
        QualitySettings.vSyncCount = 0;//��������������
        Application.targetFrameRate = 60;//�t���[�����[�g60�ɌŒ�
        Screen.SetResolution(1920, 1080, true);//�𑜓x�A�t���X�N���[���ݒ�
        Console.WriteLine("GameManager�Ă΂�܂�");
        Console.WriteLine(Application.targetFrameRate + "�̃t���[�����[�g��ݒ�");
    }

    public static GameManager GetInstance()//�C���X�^���X���擾
    {
        if (GMInstance == null)
            GMInstance = new GameManager();

        return GMInstance;
    }
}