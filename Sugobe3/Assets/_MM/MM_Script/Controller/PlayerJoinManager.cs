using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerJoinManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    private int maxPlayers = 2; //最大プレイヤー数
    public Dictionary<InputDevice, PlayerInput> activePlayers = new Dictionary<InputDevice, PlayerInput>(); //デバイスとプレイヤーを対応付ける
    public static int players;  //現在参加中のプレイヤー数

    void Start()
    {
        DontDestroyOnLoad(gameObject); //このオブジェクトをシーン間で破棄しないようにする

        playerInputManager = GetComponent<PlayerInputManager>(); //コンポーネント追加

        if (playerInputManager == null)//無ければ取得
        {
            playerInputManager = gameObject.AddComponent<PlayerInputManager>();
        }

        if (playerInputManager == null)//無ければ取得
        {
            Debug.LogError("PlayerInputManager が見つかりません。シーン内に配置してください。");
            return;
        }

        //デバイスの接続・切断を監視
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void Update()
    {
        if (activePlayers.Count >= maxPlayers)
            return; //最大人数を超えたら新しいプレイヤーは追加されない

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !activePlayers.ContainsKey(gamepad))
            {
                Debug.Log("Aボタンが押された: " + gamepad.deviceId);

                //プレイヤーを参加させる
                PlayerInput newPlayer = playerInputManager.JoinPlayer(activePlayers.Count, -1, null, gamepad);

                //使用済みのゲームパッドとプレイヤーを記録
                activePlayers[gamepad] = newPlayer;

                //生成されたプレイヤーオブジェクトをシーン間で破棄しないようにする
                DontDestroyOnLoad(newPlayer.gameObject);

                //接続音再生
                AssetsManager.GetInstance()._AudioLoader.PlayAudio(AssetsManager.GetInstance()._AudioLoader.Aud_Cnect);
            }
        }
        players = activePlayers.Count;
    }


    //デバイスの接続・切断時に呼ばれる
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected://デバイスが切断されたとき
                Debug.Log($"デバイスが切断されました: {device}");

                if (activePlayers.ContainsKey(device))
                {
                    PlayerInput playerToRemove = activePlayers[device];//対応するプレイヤーを取得

                    if (playerToRemove != null)
                    {
                        Destroy(playerToRemove.gameObject);//プレイヤーオブジェクトを削除
                    }
                    else
                    {
                        Debug.LogWarning("playerToRemove が null です。すでに削除されている可能性があります。");
                    }

                    //リストから削除
                    activePlayers.Remove(device);
                }
                else
                {
                    Debug.LogWarning($"activePlayers にデバイス {device} が見つかりませんでした。");
                }
                break;

            case InputDeviceChange.Reconnected://デバイスが再接続されたとき
                Debug.Log($"デバイスが再接続されました: {device}");
                break;
        }
    }
}
