using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    public GameObject CharaSelect;//�L�����N�^�[�I�����

    public GameObject Score;//�X�R�A�\�����

    public GameObject BatterGame;//�o�b�^�[������

    public GameObject Batter_Decid;//�o�b�^�[������

    public GameObject PitcherGame;//�s�b�`���[������

    public GameObject Pitcher_Decid;//�s�b�`���[������

    public GameObject TargetNum;//�X�g���C�N�]�[���̔ԍ�

    public GameObject Result;//���ʉ��

    /// <summary>
    /// 1P�̍U���ʒu
    /// </summary>
    public GameObject Pos_Attack_1P;

    /// <summary>
    /// 2P�̍U���ʒu
    /// </summary>
    public GameObject Pos_Attack_2P;

    /// <summary>
    /// 1P�̖h�q�ʒu
    /// </summary>
    public GameObject Pos_Defense_1P;

    /// <summary>
    /// 2P�̖h�q�ʒu
    /// </summary>
    public GameObject Pos_Defense_2P;


    [SerializeField]
    private GameObject[] NowScreens;//ScreenATDF���A�^�b�`

    [SerializeField]
    private GameObject flash;//�t���b�V����UI

    [SerializeField]
    public GameObject Batter_Cam;//�o�b�^�[���̃J����

    [SerializeField]
    public GameObject Pitcher_Cam;//�s�b�`���[���̃J����

    private void Awake()
    {
        ScreenManager.GetInstance().SetMainManager(this);//ScreenManager��MainManager�̃C���X�^���X��n��
    }


    public void SetMainScreen(bool Onflash = true, params GameObject[] Screens)//���b�N�̈�params���g�p�A�{���̓I�[�o�[���[�h���Ĉ�����ς���
    {
        flash.SetActive(Onflash);//�t���b�V���I��

        if (NowScreens != null && NowScreens.Length > 0)//�\�����̉�ʂ�S�Ĕ�\����
        {
            foreach (var screen in NowScreens)
            {
                if (screen != null)
                {
                    screen.SetActive(false);
                }
            }
        }

        if (Screens != null && Screens.Length > 0)//�S�Ẳ�ʂ�\����
        {
            foreach (var screen in Screens)
            {
                if (screen != null)
                {
                    screen.SetActive(true);
                }
            }
        }

        NowScreens = Screens;//���݂̕\����ʃ��X�g���X�V
    }

}
