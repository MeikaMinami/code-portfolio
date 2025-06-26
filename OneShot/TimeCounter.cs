using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    public float countdownMinutes = 5.0f;  //�T���ԃ^�C�}�[

    private float countdownSeconds;   //�b��

    [SerializeField]
    private TextMeshProUGUI TimertextMeshPro;  //�^�C�}�[�̎c�莞�Ԃ�\������e�L�X�g���b�V���v��

    [SerializeField]
    private Image TimerCircle;  //�^�C�}�[�̂��񂾂񌸂��Ă��~

    private SceneLording sceneLording;  //�V�[�����[�f�B���O�X�N���v�g

    private bool onceOnly = true;

    private void Start()
    {
        countdownSeconds = countdownMinutes * 60;  //������b���ɕύX
        sceneLording = GameObject.FindWithTag("SceneManager").GetComponent<SceneLording>();  //�^�O�ŃV�[�����[�f�B���O�X�N���v�g���擾
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;  //���Ԃ��o�ɂ�āA���Ԃ�����
        countdownSeconds = Mathf.Clamp(countdownSeconds, 0, countdownMinutes * 60);  //0�����ɂȂ�Ȃ��悤�ɐ���
        
        var span = new TimeSpan(0, 0, (int)countdownSeconds);  //TimeSpan���g���āA�b����hh:mm:ss�ϊ�
                                                               //������������(h)�A����������(min)�A��O�������b(sec)
                                                               //��O�����͋����I��int�^�ɂ����
        TimertextMeshPro.SetText(span.ToString(@"mm\:ss"));  //���ƕb��\��


        TimerCircle.fillAmount = countdownSeconds / (countdownMinutes * 60);  //�c�莞�Ԃ̔䗦��fillAmount���v�Z

        if (countdownSeconds <= 0 && onceOnly)
        {
            onceOnly = false;
            // 0�b�ɂȂ����Ƃ��̏���
            Debug.Log("�T���^�C�}�[���I������");

            //�B�e�V�[���ɑJ��
            sceneLording.StartLoad();
        }
    }
}
