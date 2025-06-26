using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineMixingCamera mixingCamera;
    public int TansakuCameraIndex = 0;  // �T���p�J������Index�i�ŏ��̃J�����j
    public int GojyasuCameraIndex = 1;  // �S�[�W���X��b�p�J������Index�i2�ڂ̃J�����j
    public int HachiCameraIndex = 2;    // �n�`��b�p�J������Index�i2�ڂ̃J�����j
    public int InuCameraIndex = 3;      // �C�k��b�p�J������Index�i2�ڂ̃J�����j
    public int BanbiCameraIndex = 4;    // �o���r��b�p�J������Index�i2�ڂ̃J�����j
    public int TomasCameraIndex = 5;    // �g�[�}�X��b�p�J������Index�i2�ڂ̃J�����j
    public int UshiCameraIndex = 6;     // �E�V��b�p�J������Index�i2�ڂ̃J�����j

    /// <summary>
    /// 1:�S�[�W���X�A�Q�F�͂��A�F�R�F���ʁA�S�F�΂�сA�T�F�g�[�}�X�A�U�F����
    /// </summary>

    private void Start()
    {
        // �ŏ��͒T���p�J����100%�A��b�p�J����0%
        mixingCamera.SetWeight(TansakuCameraIndex, 1);  //�T���p�J����
        mixingCamera.SetWeight(GojyasuCameraIndex, 0);  //�S�[�W���X�p�J����
        mixingCamera.SetWeight(HachiCameraIndex, 0);    //�n�`�p�J����
        mixingCamera.SetWeight(InuCameraIndex, 0);      //���p�J����
        mixingCamera.SetWeight(BanbiCameraIndex, 0);�@�@//�o���r�p�J����
        mixingCamera.SetWeight(TomasCameraIndex, 0);    //�g�[�}�X�p�J����
        mixingCamera.SetWeight(UshiCameraIndex, 0);     //�E�V�p�J����
    }

    public void StartConversation(int cameraID)
    {
        // �w�肳�ꂽ��b�J�����փX���[�Y�ɐ؂�ւ�
        StartCoroutine(SmoothTransition(cameraID, 1));
    }

    public void EndConversation()
    {
        // �T���J�����փX���[�Y�ɖ߂�
        StartCoroutine(SmoothTransition(0, 1));
    }

    private IEnumerator SmoothTransition(int targetIndex, float duration)  //�R���[�`���ŃE�F�C�g�𑝂₵�Ċ��炩�ɐ؂�ւ���
    {
        float elapsedTime = 0f;

        Dictionary<int, float> startWeights = new Dictionary<int, float>();

        // ���ׂẴJ�����̏����E�F�C�g���擾
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            startWeights[i] = mixingCamera.GetWeight(i);
        }


        //�w�肳�ꂽ����(duration)�ŃJ�����̃E�F�C�g�����X�ɐ؂�ւ���
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float blend = elapsedTime / duration;

            // ���ׂẴJ�����̃E�F�C�g����n���(Lerp)�ōX�V
            for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
            {
                float targetWeight = (i == targetIndex) ? 1 : 0;  //�ΏۃJ�����������P�C����ȊO�͂O��
                mixingCamera.SetWeight(i, Mathf.Lerp(startWeights[i], targetWeight, blend));
            }

            yield return null;
        }
    }
}
