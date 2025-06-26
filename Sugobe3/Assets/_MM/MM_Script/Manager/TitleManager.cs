using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject ScreenInput;//���͂̉��

    public GameObject ScreenTitle;//�^�C�g���̉��

    public GameObject ScreenATDF;//��U��U���͉��


    [SerializeField]
    private GameObject NowScreen; //��̃I�u�W�F�N�g

    [SerializeField]
    private  GameObject flash;//�t���b�V���pUI

    [SerializeField]
    private Camera Cam;//�J����


    private void Awake()
    {
        ScreenManager.GetInstance().SetTitleManager(this);//ScreenManager�̃V���O���g���ɓo�^
    }

    private void Start()
    {
        Cam.enabled = false;//������
        SetTitleScreen(NowScreen);//���݂̉�ʂ��\���ɂ��Ă���
        SetTitleScreen(ScreenInput);//���͉�ʂ�\��
        Cam.enabled = true;//�L����
    }

    /// <summary>
    /// �w�肵����ʂɐ؂�ւ���֐�
    /// </summary>
    /// <param name="Screen">�؂�ւ�������ʂ�GameObject</param>
    /// <param name="Onflash">�؂�ւ����Ƀt���b�V����\�����邩�ǂ����i�f�t�H���g��false�j</param>
    /// <param name="sceneName">�؂�ւ���̃V�[�����i�ȗ��\�j</param>
    public void SetTitleScreen(GameObject Screen, bool Onflash = false, string sceneName = null)
    {
        flash.SetActive(Onflash);//�t���b�V�����펞
        NowScreen.SetActive(false);//���̉�ʂ��\��
        Screen.SetActive(true);//�V������ʂ�\��
        NowScreen = Screen;//���݂̉�ʂ�o�^
        if (!string.IsNullOrEmpty(sceneName))//�V�[����������΃��[�h
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
