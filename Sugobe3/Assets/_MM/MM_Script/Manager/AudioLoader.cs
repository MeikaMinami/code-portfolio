using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// アドレッサブルアセットシステムを使用して音声クリップを読み込み、再生および停止するクラス。
/// </summary>
public class AudioLoader : MonoBehaviour
{
    /// <summary>
    /// 接続時音声
    /// </summary>
    public AssetReference Aud_Cnect;

    /// <summary>
    /// 1P接続時音声
    /// </summary>
    public AssetReference Aud_Cnect1P;

    /// <summary>
    /// 2P接続時音声
    /// </summary>
    public AssetReference Aud_Cnect2P;

    /// <summary>
    /// あなたが1Pです
    /// </summary>
    public AssetReference Aud_You1P;

    /// <summary>
    /// あなたが2Pです
    /// </summary>
    public AssetReference Aud_You2P;

    /// <summary>
    /// キャラクター選択音
    /// </summary>
    public AssetReference Aud_CharacterSelect;

    /// <summary>
    /// ピッチャーの選択音
    /// </summary>
    public AssetReference Aud_PitcherSelect;

    /// <summary>
    /// ピッチャーが同じところを選択した音
    /// </summary>
    public AssetReference Aud_PitcherSamePoint;

    /// <summary>
    /// バッターの選択音
    /// </summary>
    public AssetReference Aud_BatterSelect;

    /// <summary>
    /// LBRBの音声
    /// </summary>
    public AssetReference Aud_LBRB;

    /// <summary>
    /// キャンセルB音声
    /// </summary>
    public AssetReference Aud_CancelB;

    /// <summary>
    /// 確定A音声
    /// </summary>
    public AssetReference Aud_OkA;

    /// <summary>
    /// ゲーム開始音声
    /// </summary>
    public AssetReference Aud_Opening;

    [SerializeField]
    private AudioMixer _audMixer; //全体の音量を管理するミキサー

    private AudioSource audioSource;  //ゲームの音声を再生するソース
    private AsyncOperationHandle<AudioClip> audioHandle; //Addressables 経由でロードした AudioClip の状態を保持。
    private bool isAudioLoaded = false; //ロードされているかどうかを判定するフラグ
    private bool isAudioPlaying = false; // 再生中かどうかを判定するフラグ

    void Awake()
    {
        AssetsManager.GetInstance().SetAudioLoader(this);  //AssetManagerからこのクラスにアクセスできるようにする
    }

    void Start()
    {
        //初期化
        InitializeAudioSource();
    }


    /// <summary>
    /// 初期化（VoiceLoaderクラス内で初期化してる）<para>※Start関数内の為Awake注意</para>
    /// </summary>
    private void InitializeAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _audMixer.FindMatchingGroups("CV")[0]; //ミキサーのCVグループに接続
        audioSource.playOnAwake = false; //自動再生防止
    }

    /// <summary>
    /// 音声再生の関数
    /// </summary>
    public void PlayAudio(AssetReference asset)
    {
        if (isAudioPlaying && !audioSource.isPlaying)//再生中の音声があり再生終了した場合
        {
            ClearAudio();
        }

        if (isAudioPlaying) //再生中の場合は処理をスキップ
        {
            audioSource.Stop();
            LoadAndPlayAudio(asset);

        }


        if (!isAudioLoaded) //ロードできていなかったらロード
        {
            LoadAndPlayAudio(asset);
        }
    }

    /// <summary>
    /// 再生中の音声やリソースを解放する
    /// </summary>
    private void ClearAudio()
    {
        audioSource.Stop();//再生停止
        audioSource.clip = null;//AudioClipを外す

        if (isAudioLoaded)
        {
            Addressables.Release(audioHandle); //Addresablesのリソースを解放
            isAudioLoaded = false;
        }

        isAudioPlaying = false; //再生中フラグをリセット
    }


    /// <summary>
    /// アドレッサブルから音声を読み込んで再生を開始する
    /// </summary>
    private void LoadAndPlayAudio(AssetReference asset)
    {
        isAudioPlaying = true; //再生中フラグを設定
        audioHandle = Addressables.LoadAssetAsync<AudioClip>(asset); //指定されたアセットを非同期で読み込む
        audioHandle.Completed += OnAudioLoaded; //読み込み完了時に呼ばれるコールバック関数を登録
    }

    /// <summary>
    /// AudioClipの読み込み完了後に呼ばれるコールバック関数
    /// </summary>
    private void OnAudioLoaded(AsyncOperationHandle<AudioClip> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)//読み込みが完了した場合
        {
            audioSource.clip = handle.Result; //AudioSourceに読み込んだAudioClipを設定
            audioSource.Play(); //再生
            isAudioLoaded = true; //読み込み済みを記録
            audioSource.loop = false; //ループ再生しない

        }
        else //読み込み失敗
        {
            Debug.LogError("オーディオの再生に失敗");
        }
    }
}
