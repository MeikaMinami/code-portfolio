using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
 
public class GamepadVibration : MonoBehaviour
{
    private IEnumerator Start()
    {
        var gamepad = Gamepad.current;

        if (gamepad == null)

        {

            //Debug.Log("ゲームパッド未接続");

            yield break;

        }

        Debug.Log("ZRボタンを押すと振動が開始します。");

        while (true)

        {

            if (gamepad.leftTrigger.isPressed) // ZRボタンを確認

            {

                Debug.Log("ZLボタン押下中: 振動開始");

                float vibrationStrength = 0.0f;

                float duration = 3.0f; // 最大1秒で振動強度を1.0fに変化

                float elapsedTime = 0.0f;

                yield return new WaitForSeconds(0.1f);

                while (gamepad.leftTrigger.isPressed)

                {

                    vibrationStrength = Mathf.Lerp(0.00f, 1.0f, elapsedTime / duration);

                    gamepad.SetMotorSpeeds(vibrationStrength, vibrationStrength);

                    elapsedTime += Time.deltaTime;

                    yield return null; // 次のフレームまで待機

                }


                // ボタンを離したら、振動を停止

                if (!gamepad.rightTrigger.isPressed)

                {

                    Debug.Log("ZLボタンを離しました。振動停止");

                    gamepad.SetMotorSpeeds(0.00f, 0.0f);

                }

            }

            // ゲームパッドのZRボタン（右トリガー）の値を取得

            if (gamepad != null)

            {

                float triggerValue = gamepad.rightTrigger.ReadValue(); // 押し込み具合（0.0f～1.0f）

                gamepad.SetMotorSpeeds(triggerValue, triggerValue); // 左右のモーターに同じ値を設定

                // Debugログで押し込み具合を表示

                //Debug.Log($"ZR押し込み具合: {triggerValue:F2}");

            }

            yield return null; // 次のフレームまで待機

        }


    }

}















