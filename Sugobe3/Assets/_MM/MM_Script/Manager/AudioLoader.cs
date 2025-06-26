using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// �A�h���b�T�u���A�Z�b�g�V�X�e�����g�p���ĉ����N���b�v��ǂݍ��݁A�Đ�����ђ�~����N���X�B
/// </summary>
public class AudioLoader : MonoBehaviour
{
    /// <summary>
    /// �ڑ�������
    /// </summary>
    public AssetReference Aud_Cnect;

    /// <summary>
    /// 1P�ڑ�������
    /// </summary>
    public AssetReference Aud_Cnect1P;

    /// <summary>
    /// 2P�ڑ�������
    /// </summary>
    public AssetReference Aud_Cnect2P;

    /// <summary>
    /// ���Ȃ���1P�ł�
    /// </summary>
    public AssetReference Aud_You1P;

    /// <summary>
    /// ���Ȃ���2P�ł�
    /// </summary>
    public AssetReference Aud_You2P;

    /// <summary>
    /// �L�����N�^�[�I����
    /// </summary>
    public AssetReference Aud_CharacterSelect;

    /// <summary>
    /// �s�b�`���[�̑I����
    /// </summary>
    public AssetReference Aud_PitcherSelect;

    /// <summary>
    /// �s�b�`���[�������Ƃ����I��������
    /// </summary>
    public AssetReference Aud_PitcherSamePoint;

    /// <summary>
    /// �o�b�^�[�̑I����
    /// </summary>
    public AssetReference Aud_BatterSelect;

    /// <summary>
    /// LBRB�̉���
    /// </summary>
    public AssetReference Aud_LBRB;

    /// <summary>
    /// �L�����Z��B����
    /// </summary>
    public AssetReference Aud_CancelB;

    /// <summary>
    /// �m��A����
    /// </summary>
    public AssetReference Aud_OkA;

    /// <summary>
    /// �Q�[���J�n����
    /// </summary>
    public AssetReference Aud_Opening;

    [SerializeField]
    private AudioMixer _audMixer; //�S�̂̉��ʂ��Ǘ�����~�L�T�[

    private AudioSource audioSource;  //�Q�[���̉������Đ�����\�[�X
    private AsyncOperationHandle<AudioClip> audioHandle; //Addressables �o�R�Ń��[�h���� AudioClip �̏�Ԃ�ێ��B
    private bool isAudioLoaded = false; //���[�h����Ă��邩�ǂ����𔻒肷��t���O
    private bool isAudioPlaying = false; // �Đ������ǂ����𔻒肷��t���O

    void Awake()
    {
        AssetsManager.GetInstance().SetAudioLoader(this);  //AssetManager���炱�̃N���X�ɃA�N�Z�X�ł���悤�ɂ���
    }

    void Start()
    {
        //������
        InitializeAudioSource();
    }


    /// <summary>
    /// �������iVoiceLoader�N���X���ŏ��������Ă�j<para>��Start�֐����̈�Awake����</para>
    /// </summary>
    private void InitializeAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _audMixer.FindMatchingGroups("CV")[0]; //�~�L�T�[��CV�O���[�v�ɐڑ�
        audioSource.playOnAwake = false; //�����Đ��h�~
    }

    /// <summary>
    /// �����Đ��̊֐�
    /// </summary>
    public void PlayAudio(AssetReference asset)
    {
        if (isAudioPlaying && !audioSource.isPlaying)//�Đ����̉���������Đ��I�������ꍇ
        {
            ClearAudio();
        }

        if (isAudioPlaying) //�Đ����̏ꍇ�͏������X�L�b�v
        {
            audioSource.Stop();
            LoadAndPlayAudio(asset);

        }


        if (!isAudioLoaded) //���[�h�ł��Ă��Ȃ������烍�[�h
        {
            LoadAndPlayAudio(asset);
        }
    }

    /// <summary>
    /// �Đ����̉����⃊�\�[�X���������
    /// </summary>
    private void ClearAudio()
    {
        audioSource.Stop();//�Đ���~
        audioSource.clip = null;//AudioClip���O��

        if (isAudioLoaded)
        {
            Addressables.Release(audioHandle); //Addresables�̃��\�[�X�����
            isAudioLoaded = false;
        }

        isAudioPlaying = false; //�Đ����t���O�����Z�b�g
    }


    /// <summary>
    /// �A�h���b�T�u�����特����ǂݍ���ōĐ����J�n����
    /// </summary>
    private void LoadAndPlayAudio(AssetReference asset)
    {
        isAudioPlaying = true; //�Đ����t���O��ݒ�
        audioHandle = Addressables.LoadAssetAsync<AudioClip>(asset); //�w�肳�ꂽ�A�Z�b�g��񓯊��œǂݍ���
        audioHandle.Completed += OnAudioLoaded; //�ǂݍ��݊������ɌĂ΂��R�[���o�b�N�֐���o�^
    }

    /// <summary>
    /// AudioClip�̓ǂݍ��݊�����ɌĂ΂��R�[���o�b�N�֐�
    /// </summary>
    private void OnAudioLoaded(AsyncOperationHandle<AudioClip> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)//�ǂݍ��݂����������ꍇ
        {
            audioSource.clip = handle.Result; //AudioSource�ɓǂݍ���AudioClip��ݒ�
            audioSource.Play(); //�Đ�
            isAudioLoaded = true; //�ǂݍ��ݍς݂��L�^
            audioSource.loop = false; //���[�v�Đ����Ȃ�

        }
        else //�ǂݍ��ݎ��s
        {
            Debug.LogError("�I�[�f�B�I�̍Đ��Ɏ��s");
        }
    }
}
