using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour  //�ŏ��Ƀ}�E�X�ŒT���V�[���̃f�o�b�O���s�����߂̃X�N���v�g
{
    //[SerializeField] GameObject Player;
    [SerializeField] Transform PlayerTransform;

    Vector3 currentCameraPos;  //���݂̃J�����̈ʒu
    Vector3 pastCameraPos;     //�ߋ��̃J�����̈ʒu

    Vector3 diff;  //�ړ�����

    //ToDo �v���C���[���ݒ肵���}�E�X���x�𔽉f��������
    public float sensitivity = 1.0f;  //���x

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̃v���C���[�̈ʒu�̎擾
        pastCameraPos = PlayerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�����̈ړ�====================

        //Debug.Log("PlayerPositoin = " + PlayerTransform.position);
        //�v���C���[�̌��݈ʒu�̎擾
        currentCameraPos = PlayerTransform.position;

        diff = currentCameraPos - pastCameraPos;  //���݈ʒu�Ɖߋ��̈ʒu�̍��ňړ����������߂�

        //�J�����̈ʒu���v���C���[�̈ړ���������������
        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);

        pastCameraPos = currentCameraPos;  //���݈ʒu��ۑ�

        //�J�����̈ړ��@End===================



        //�J�����̉�]===============

        //�}�E�X�̈ړ��ʂ��擾
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //X�����Ɉ��ȏ�}�E�X���ړ����Ă���Ή���]
        if (Mathf.Abs(mx) > 0.01f)
        {
            mx = mx * sensitivity;  //�����Ń}�E�X���x��ݒ�

            //��]���̓��[���h���W��Y��
            transform.RotateAround(PlayerTransform.position, Vector3.up, mx);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(my) > 0.01f)
        {
            my = my * sensitivity;  //�����Ń}�E�X���x��ݒ�

            // ��]���̓J�������g��X��
            transform.RotateAround(PlayerTransform.position, transform.right, -my);
        }
        //�J�����̉�]�@End===================
    }
}
