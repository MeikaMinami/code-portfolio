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

            //Debug.Log("�Q�[���p�b�h���ڑ�");

            yield break;

        }

        Debug.Log("ZR�{�^���������ƐU�����J�n���܂��B");

        while (true)

        {

            if (gamepad.leftTrigger.isPressed) // ZR�{�^�����m�F

            {

                Debug.Log("ZL�{�^��������: �U���J�n");

                float vibrationStrength = 0.0f;

                float duration = 3.0f; // �ő�1�b�ŐU�����x��1.0f�ɕω�

                float elapsedTime = 0.0f;

                yield return new WaitForSeconds(0.1f);

                while (gamepad.leftTrigger.isPressed)

                {

                    vibrationStrength = Mathf.Lerp(0.00f, 1.0f, elapsedTime / duration);

                    gamepad.SetMotorSpeeds(vibrationStrength, vibrationStrength);

                    elapsedTime += Time.deltaTime;

                    yield return null; // ���̃t���[���܂őҋ@

                }


                // �{�^���𗣂�����A�U�����~

                if (!gamepad.rightTrigger.isPressed)

                {

                    Debug.Log("ZL�{�^���𗣂��܂����B�U����~");

                    gamepad.SetMotorSpeeds(0.00f, 0.0f);

                }

            }

            // �Q�[���p�b�h��ZR�{�^���i�E�g���K�[�j�̒l���擾

            if (gamepad != null)

            {

                float triggerValue = gamepad.rightTrigger.ReadValue(); // �������݋�i0.0f�`1.0f�j

                gamepad.SetMotorSpeeds(triggerValue, triggerValue); // ���E�̃��[�^�[�ɓ����l��ݒ�

                // Debug���O�ŉ������݋��\��

                //Debug.Log($"ZR�������݋: {triggerValue:F2}");

            }

            yield return null; // ���̃t���[���܂őҋ@

        }


    }

}















