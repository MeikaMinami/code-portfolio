using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class PadInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private PlayerInput playerInput;  //PlayerInputを取得
    private InputAction LBAction;  //LBボタンAction取得用変数

    private int playerIndex;  //プレイヤーの番号（0 = 1P、1 = 2P）

    // 大量の静的な入力フラグについて======================
    //
    // 各プレイヤー（1Pと2P）のコントローラー入力を明確に分けて簡単にif文の中で扱いたいという要望を受けて、
    // 全ての入力を静的なbool変数として管理しています。
    // これにより、どのスクリプトからでも入力状態を簡単に参照できるようにしました。
    //
    //===============================================

    //入力のフラグ==============================

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

    //入力のフラグ End===================


    private void Awake()
    {
        inputActions = new InputSystem_Actions();//入力アクションを初期化
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.user.index;  //ユーザインデックスを取得（0 = 1P,1 = 2P）
        Debug.Log(playerIndex + "が割り当てられた");
    }

    //各入力アクションの処理（プレイヤーごとにフラグを管理）
    public void OnBat_up()  //Yボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            Y_1P = true;
            Debug.Log("1PがYを入力しました");
        }
        else if (playerIndex == 1)
        {
            Y_2P = true;
            Debug.Log("2PがYを入力しました");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }

    public void OnBat_Right()  //Bボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            B_1P = true;
            Debug.Log("1PがBを入力しました");
        }
        else if (playerIndex == 1)
        {
            B_2P = true;
            Debug.Log("2PがBを入力しました");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    public void OnBat_Left()  //Xボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            X_1P = true;
            Debug.Log("1PがXを入力しました");
        }
        else if (playerIndex == 1)
        {
            X_2P = true;
            Debug.Log("2PがXを入力しました");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }
    

public void OnBat_Down()  //Aボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            A_1P = true;
            Debug.Log("1PがAを入力しました");
        }
        else if (playerIndex == 1)
        {
            A_2P = true;
            Debug.Log("2PがAを入力しました");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    public void OnCross_Left()　 //十字キー左
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            CrossLeft_1P = true;
            Debug.Log("1Pが十字キー左を入力しました");
        }
        else if (playerIndex == 1)
        {
            CrossLeft_2P = true;
            Debug.Log("2Pが十字キー左を入力しました");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    public void OnCross_Up()  //十字キー上
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            CrossUp_1P = true;
            Debug.Log("1Pが十字キー左を入力しました");
        }
        else if (playerIndex == 1)
        {
            CrossUp_2P = true;
            Debug.Log("2Pが十字キー左を入力しました");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    public void OnCross_Right()  //十字キー右
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            CrossRight_1P = true;
            Debug.Log("1Pが十字キー左を入力しました");
        }
        else if (playerIndex == 1)
        {
            CrossRight_2P = true;
            Debug.Log("2Pが十字キー左を入力しました");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    public void OnCross_Down()  //十字キー下
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            CrossDown_1P = true;
            Debug.Log("1Pが十字キー左を入力しました");
        }
        else if (playerIndex == 1)
        {
            CrossDown_2P = true;
            Debug.Log("2Pが十字キー左を入力しました");

        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    public void OnLStick(InputValue value)  //左スティック
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        Vector2 LStick_Input = value.Get<Vector2>();

        if (LStick_Input.y > 0.5f)  //左スティック上
        {
            if (playerIndex == 0)
            {
                LStickUp_1P = true;
                Debug.Log("1Pが左スティック上入力しました");
            }
            else if (playerIndex == 1)
            {
                LStickUp_2P = true;
                Debug.Log("2Pが左スティック上入力しました");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("コントローラーが二つ以上ある！！");
            }
        }
        else if (LStick_Input.y < -0.5f)  //左スティック下
        {
            if (playerIndex == 0)
            {
                LStickDown_1P = true;
                Debug.Log("1Pが左スティック下入力しました");
            }
            else if (playerIndex == 1)
            {
                LStickDown_2P = true;
                Debug.Log("2Pが左スティック下入力しました");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("コントローラーが二つ以上ある！！");
            }
        }
        else if (LStick_Input.x > 0.5f)  //左スティック右
        {
            if (playerIndex == 0)
            {
                LStickRight_1P = true;
                Debug.Log("1Pが左スティック右入力しました");
            }
            else if (playerIndex == 1)
            {
                LStickRight_2P = true;
                Debug.Log("2Pが左スティック右入力しました");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("コントローラーが二つ以上ある！！");
            }
        }
        else if (LStick_Input.x < -0.5f)  //左スティック左
        {
            if (playerIndex == 0)
            {
                LStickLeft_1P = true;
                Debug.Log("1Pが左スティック左入力しました");
            }
            else if (playerIndex == 1)
            {
                LStickLeft_2P = true;
                Debug.Log("2Pが左スティック左入力しました");
            }
            else if (playerIndex >= 2)
            {
                Debug.Log("コントローラーが二つ以上ある！！");
            }
        }
    }


    public void OnLB()  //LBボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            LB_1P = true;
            Debug.Log("1PがLBを入力しました");
        }
        else if (playerIndex == 1)
        {
            LB_2P = true;
            Debug.Log("2PがLBを入力しました");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }

    public void OnRB()  //RBボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            RB_1P = true;
            Debug.Log("1PがRBを入力しました");
        }
        else if (playerIndex == 1)
        {
            RB_2P = true;
            Debug.Log("2PがRBを入力しました");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }

    public void OnMenu()  //Menuボタン
    {
        if (AssetsManager.GetInstance()._VideoLoader.IsVideoPlaying) return; // 再生中の場合は処理をスキップ
        if (playerIndex == 0)
        {
            Menu_1P = true;
            Debug.Log("1PがMenuを入力しました");
        }
        else if (playerIndex == 1)
        {
            Menu_2P = true;
            Debug.Log("2PがMenuを入力しました");
        }
        else if (playerIndex >= 2)
        {
            Debug.Log("コントローラーが二つ以上ある！！");
        }
    }


    private void LateUpdate()
    {
        //全ての入力フラグをフレームごとにリセット

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
