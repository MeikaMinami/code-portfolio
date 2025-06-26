using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerTest : MonoBehaviour
{
    //プレイヤーが入室した時に受けとる通知
    public void OnPlayerJoied(PlayerInput playerInput)
    {
        Debug.Log("入室したプレイヤーのuser.index : " + playerInput.user.index);
    }


    //プレイヤーが退室した時に受けとる通知
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("退室したプレイヤーのuser.index : " + playerInput.user.index);
    }
}

