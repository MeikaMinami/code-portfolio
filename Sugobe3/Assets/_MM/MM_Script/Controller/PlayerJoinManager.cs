using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerJoinManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    private int maxPlayers = 2; //�ő�v���C���[��
    public Dictionary<InputDevice, PlayerInput> activePlayers = new Dictionary<InputDevice, PlayerInput>(); //�f�o�C�X�ƃv���C���[��Ή��t����
    public static int players;  //���ݎQ�����̃v���C���[��

    void Start()
    {
        DontDestroyOnLoad(gameObject); //���̃I�u�W�F�N�g���V�[���ԂŔj�����Ȃ��悤�ɂ���

        playerInputManager = GetComponent<PlayerInputManager>(); //�R���|�[�l���g�ǉ�

        if (playerInputManager == null)//������Ύ擾
        {
            playerInputManager = gameObject.AddComponent<PlayerInputManager>();
        }

        if (playerInputManager == null)//������Ύ擾
        {
            Debug.LogError("PlayerInputManager ��������܂���B�V�[�����ɔz�u���Ă��������B");
            return;
        }

        //�f�o�C�X�̐ڑ��E�ؒf���Ď�
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void Update()
    {
        if (activePlayers.Count >= maxPlayers)
            return; //�ő�l���𒴂�����V�����v���C���[�͒ǉ�����Ȃ�

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !activePlayers.ContainsKey(gamepad))
            {
                Debug.Log("A�{�^���������ꂽ: " + gamepad.deviceId);

                //�v���C���[���Q��������
                PlayerInput newPlayer = playerInputManager.JoinPlayer(activePlayers.Count, -1, null, gamepad);

                //�g�p�ς݂̃Q�[���p�b�h�ƃv���C���[���L�^
                activePlayers[gamepad] = newPlayer;

                //�������ꂽ�v���C���[�I�u�W�F�N�g���V�[���ԂŔj�����Ȃ��悤�ɂ���
                DontDestroyOnLoad(newPlayer.gameObject);

                //�ڑ����Đ�
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_Cnect);
            }
        }
        players = activePlayers.Count;
    }


    //�f�o�C�X�̐ڑ��E�ؒf���ɌĂ΂��
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected://�f�o�C�X���ؒf���ꂽ�Ƃ�
                Debug.Log($"�f�o�C�X���ؒf����܂���: {device}");

                if (activePlayers.ContainsKey(device))
                {
                    PlayerInput playerToRemove = activePlayers[device];//�Ή�����v���C���[���擾

                    if (playerToRemove != null)
                    {
                        Destroy(playerToRemove.gameObject);//�v���C���[�I�u�W�F�N�g���폜
                    }
                    else
                    {
                        Debug.LogWarning("playerToRemove �� null �ł��B���łɍ폜����Ă���\��������܂��B");
                    }

                    //���X�g����폜
                    activePlayers.Remove(device);
                }
                else
                {
                    Debug.LogWarning($"activePlayers �Ƀf�o�C�X {device} ��������܂���ł����B");
                }
                break;

            case InputDeviceChange.Reconnected://�f�o�C�X���Đڑ����ꂽ�Ƃ�
                Debug.Log($"�f�o�C�X���Đڑ�����܂���: {device}");
                break;
        }
    }
}
