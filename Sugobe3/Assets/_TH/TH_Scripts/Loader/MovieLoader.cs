using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Audio;


/// <summary>
/// �A�h���b�T�u���A�Z�b�g�V�X�e�����g�p���ăr�f�I�N���b�v��ǂݍ��݁A�Đ�����ђ�~����N���X�B
/// </summary>
public class MovieLoader : MonoBehaviour
{
    /// <summary>
    /// �Ԃ�����̎��̂P�x�[�X�q�b�g�̉f��
    /// </summary>
    public AssetReference Mv_RedHit;

    /// <summary>
    /// �Ԃ�����̎��̂Q�x�[�X�q�b�g�̉f��
    /// </summary>
    public AssetReference Mv_Red2Hit;

    /// <summary>
    /// �Ԃ�����̎��̂R�x�[�X�q�b�g�̉f��
    /// </summary>
    public AssetReference Mv_Red3Hit;

    /// <summary>
    /// ������̎��̂P�x�[�X�q�b�g�̉f��
    /// </summary>
    public AssetReference Mv_BlueHit;

    /// <summary>
    /// ������̎��̂Q�x�[�X�q�b�g�̉f��
    /// </summary>
    public AssetReference Mv_Blue2Hit;

    /// <summary>
    /// ������̎��̂R�x�[�X�q�b�g�̉f��
    /// </summary>
    public AssetReference Mv_Blue3Hit;

    /// <summary>
    /// �Ԃ�����̎��̃t�@�E���̉f��
    /// </summary>
    public AssetReference Mv_RedFoul;

    /// <summary>
    /// ������̎��̃t�@�E���̉f��
    /// </summary>
    public AssetReference Mv_BlueFoul;

    /// <summary>
    /// �Ԃ�����̎��̃{�[���̉f��
    /// </summary>
    public AssetReference Mv_RedBall;

    /// <summary>
    /// ������̎��̃{�[���̉f��
    /// </summary>
    public AssetReference Mv_BlueBall;

    /// <summary>
    /// �Ԃ�����̎��̃X�g���C�N�̉f��
    /// </summary>
    public AssetReference Mv_RedStrike;

    /// <summary>
    /// ������̎��̃X�g���C�N�̉f��
    /// </summary>
    public AssetReference Mv_BlueStrike;

    /// <summary>
    /// �Ԃ�����̎��̃t�H�A�{�[���̉f��
    /// </summary>
    public AssetReference Mv_RedFourBall;

    /// <summary>
    /// ������̎��̃t�H�A�{�[���̉f��
    /// </summary>
    public AssetReference Mv_BlueFourBall;

    /// <summary>
    /// �Ԃ�����̎��̃t���C�̉f��
    /// </summary>
    public AssetReference Mv_RedFly;

    /// <summary>
    /// ������̎��̃t���C�̉f��
    /// </summary>
    public AssetReference Mv_BlueFly;

    /// <summary>
    /// �Ԃ�����̎��̃`�F���W�̉f��
    /// </summary>
    public AssetReference Mv_RedChange;

    /// <summary>
    /// ������̎��̃`�F���W�̉f��
    /// </summary>
    public AssetReference Mv_BlueChange;

    /// <summary>
    /// �Ԃ�����̎��̃z�[�������̉f��
    /// </summary>
    public AssetReference Mv_RedHomeRun;

    /// <summary>
    /// ������̎��̃z�[�������̉f��
    /// </summary>
    public AssetReference Mv_BlueHomeRun;


    AudioSource audioSource;
    private VideoPlayer Video_Play;
    private AsyncOperationHandle<VideoClip> Mv_handle;
    private bool IsVideoLoaded = false;
    public bool IsVideoPlaying = false; // �Đ������ǂ����𔻒肷��t���O

    [SerializeField]
    private RawImage Mv_Display;

    void Awake()
    {
        AssetsManager.GetInstance().SetVideoLoader(this);
    }

    void Start()
    {
        //������
        InitializeVideoPlayer();
    }
    void Update()
    {
        //������Audiosource���擾�E�ݒ�
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            Video_Play.audioOutputMode = VideoAudioOutputMode.AudioSource;
            Video_Play.SetTargetAudioSource(0, audioSource);
        }
    }



    /// <summary>
    /// �������iVideoLoader�N���X���ŏ��������Ă�j<para>��Start�֐����̈�Awake����</para>
    /// </summary>
    private void InitializeVideoPlayer()
    {
        Video_Play = gameObject.AddComponent<VideoPlayer>();

        Video_Play.renderMode = VideoRenderMode.RenderTexture;
        Video_Play.targetTexture = new RenderTexture(1920, 1080, 0);
        Mv_Display.texture = Video_Play.targetTexture;
        Video_Play.loopPointReached += OnVideoEnd; // �Đ��I�����̃C�x���g��o�^
    }

    private void ClearVideo()
    {
        Video_Play.Stop();
        Mv_Display.texture = null;
        Mv_Display.gameObject.SetActive(false);
        Video_Play.clip = null;

        if (IsVideoLoaded)
        {
            Addressables.Release(Mv_handle);
            IsVideoLoaded = false;
        }

        IsVideoPlaying = false; // �Đ����t���O�����Z�b�g
        ResetVideo();
    }

    /// <summary>
    /// ����Đ��̊֐�
    /// <para>��Start�֐����̈�Awake����</para>
    /// </summary>
    /// <returns>Mv_Display.[�ϐ���]</returns>
    public void Play_Mv(AssetReference asset)
    {
        if (IsVideoPlaying) return; // �Đ����̏ꍇ�͏������X�L�b�v

        if (!IsVideoLoaded)
        {
            Mv_Display.gameObject.SetActive(true);
            LoadAndPlayMv(asset);
        }
    }

    private void ResetVideo()
    {
        if (Video_Play.targetTexture != null)
        {
            Video_Play.targetTexture.Release();
        }
        Video_Play.targetTexture = new RenderTexture(1920, 1080, 0);
        Mv_Display.texture = Video_Play.targetTexture;
    }



    private void LoadAndPlayMv(AssetReference asset)
    {
        Mv_handle = Addressables.LoadAssetAsync<VideoClip>(asset);
        Mv_handle.Completed += OnVideoLoaded;
    }

    private void OnVideoLoaded(AsyncOperationHandle<VideoClip> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Video_Play.clip = handle.Result;
            Video_Play.time = 0;
            Video_Play.Play();
            IsVideoLoaded = true;
            IsVideoPlaying = true; // �Đ����t���O��ݒ�

        }
        else
        {
            Debug.LogError("Failed to load video.");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        ClearVideo(); // �Đ��I�����Ƀr�f�I���N���A
    }
}
