using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerTest : MonoBehaviour
{
    //�v���C���[�������������Ɏ󂯂Ƃ�ʒm
    public void OnPlayerJoied(PlayerInput playerInput)
    {
        Debug.Log("���������v���C���[��user.index : " + playerInput.user.index);
    }


    //�v���C���[���ގ��������Ɏ󂯂Ƃ�ʒm
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("�ގ������v���C���[��user.index : " + playerInput.user.index);
    }
}

